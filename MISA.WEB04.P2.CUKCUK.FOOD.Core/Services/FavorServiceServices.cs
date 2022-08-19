using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Services
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Implementation of FavorService Service interface
    /// </summary>
    public class FavorServiceServices : BaseServices<FavorService>, IFavorServiceServices
    {
        #region Dependency Injection

        private readonly IFavorServiceRepository _favorServiceRepository;

        public FavorServiceServices(IFavorServiceRepository favorServiceRepository) : base(favorServiceRepository)
        {
            _favorServiceRepository = favorServiceRepository;
        }

        #endregion

        #region Main methods

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Handling data from infrastructure
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns>
        /// An object of ControllerResponseData 
        /// </returns>
        public ControllerResponseData GetDataByFoodId(int foodId)
        {
            var data = _favorServiceRepository.GetByFoodId(foodId);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(data == null || data?.Count() == 0 ? Core.Enum.CustomizeStatusCode.NoContent : Core.Enum.CustomizeStatusCode.GetOkay),
                responseData = data
            };

            return res;
        }

        /// <summary>
        /// @author: VQPhong (03/08/2022)
        /// @desc: The Service for Adding a new FavorService
        /// </summary>
        /// <param name="favorService"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public override ControllerResponseData InsertData(FavorService favorService)
        {
            // Validate data from request
            var emptyValidation = this.EmptyValidation(favorService);

            if (emptyValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = emptyValidation,
                        userMsg = emptyValidation,
                    }
                };
            }

            var duplicatedComboValidation = _favorServiceRepository.CheckDuplicatedCombo(favorService);

            if (duplicatedComboValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = duplicatedComboValidation,
                        userMsg = duplicatedComboValidation,
                    }
                };
            }

            // Everything is OKAY
            int newId = _favorServiceRepository.Insert(favorService, null);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(Core.Enum.CustomizeStatusCode.Created),
                responseData = newId,
            };

            return res;
        }

        #endregion

        #region Support methods

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Ignoring FS that is empty (Content is empty and Surcharge <= 0)
        /// </summary>
        /// <param name="favorServices">Lists of FavorService</param>
        /// <returns>
        /// New FS after filtering
        /// </returns>
        public static List<FavorService> FilterEmptyFavorService(List<FavorService> favorServices)
        {
            List<FavorService> res = new();

            foreach (var item in favorServices)
            {
                if (!((item.Content == null || item.Content.Trim() == "") && (item.Surcharge <= 0 || item.Surcharge == null)))
                {
                    res.Add(item);
                }
            }

            return res;
        }

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Check if List of FS contains item that has no content but surcharge
        /// </summary>
        /// <param name="favorServices"></param>
        /// <returns>
        /// True --> No Content and has Surcharge
        /// False --> otherwise
        /// </returns>
        public static bool IsNoContentFavorService(List<FavorService> favorServices)
        {
            foreach (var item in favorServices)
            {
                if ((item.Content == null || item.Content.Trim() == "") && item.Surcharge > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Check if any FS is duplicated in the list
        /// </summary>
        /// <param name="favorServices"></param>
        /// <returns>
        /// A list of string
        /// </returns>
        public static List<string> CheckDuplicatedInList(List<FavorService> favorServices)
        {
            var res = new List<string>();

            // [LINQ] Sorting list of FavorService
            List<FavorService> sortFS = (from fs in favorServices
                                                orderby fs.Content
                                                orderby fs.Surcharge
                                                select fs).ToList();

            var tempString = String.Empty;

            // Push value <Content - Surcharge> that is duplicated in the list
            for (int i = 1; i < sortFS.Count; i++)
            {
                if (sortFS.ElementAt(i).Content == sortFS.ElementAt(i - 1).Content && sortFS.ElementAt(i).Surcharge == sortFS.ElementAt(i - 1).Surcharge)
                {
                    tempString = $"{sortFS.ElementAt(i).Content} - {sortFS.ElementAt(i).Surcharge}";
                    if (!res.Contains(tempString))
                    {
                        res.Add(tempString);
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Ignoring all FS that belongs to old list of FS
        /// </summary>
        /// <param name="newFavorServices">List of new FS</param>
        /// <param name="foodId">The ID of food for getting list of old FS</param>
        /// <returns>
        /// List of new FS that is totally new
        /// </returns>
        public List<FavorService> IgnoreOldFSForUpdate(List<FavorService> newFavorServices, int foodId)
        {
            List<FavorService> res = new();

            var oldFavorService = _favorServiceRepository.GetByFoodId(foodId).ToList();

            foreach (var nfs in newFavorServices)
            {
                if (!oldFavorService.Where(p => p.Content == nfs.Content && p.Surcharge == nfs.Surcharge).ToList().Any())
                {
                    res.Add(nfs);
                }
            }

            return res;
        }

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Assign ID for FS that already exists in Database
        /// Remove ID (Assign ID = 0) for FS that does not exist yet
        /// </summary>
        /// <param name="favorServices">List of FS</param>
        /// <returns>
        /// A list of FS that has correct value of IDs
        /// </returns>
        public List<FavorService> AssignOrRemoveIdForFS(List<FavorService> favorServices)
        {
            List<FavorService> res = new();

            var allFavorServices = _favorServiceRepository.GetAll().ToList();

            foreach (var fs in favorServices)
            {
                var tempFS = allFavorServices.Where(p => p.Content == fs.Content && p.Surcharge == fs.Surcharge).ToList();

                if (tempFS.Any())
                {
                    fs.FavorServiceID = tempFS[0].FavorServiceID;
                }
                else
                {
                    fs.FavorServiceID = 0;
                }

                res.Add(fs);
            }

            return res;
        }

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Create list of FavorServiceIDs that need to be remove from intermediate table
        /// </summary>
        /// <param name="newFavorServices">List of new FS</param>
        /// <param name="foodId">The ID of food for getting list of old FS</param>
        /// <returns></returns>
        public List<int> CreateListDelFavorServiceIds(List<FavorService> newFavorServices, int foodId)
        {
            List<int> res = new();

            var oldFavorService = _favorServiceRepository.GetByFoodId(foodId).ToList();

            foreach (var old in oldFavorService)
            {
                if (!newFavorServices.Where(p => p.Content == old.Content && p.Surcharge == old.Surcharge).ToList().Any())
                {
                    if (old.FavorServiceID != null)
                    {
                        res.Add((int)old.FavorServiceID);
                    }
                }
            }

            return res;
        }

        #endregion
    }
}
