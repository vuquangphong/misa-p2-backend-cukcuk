using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
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
    /// @desc: Implementation of Food Service interface
    /// </summary>
    public class FoodServices : BaseServices<Food>, IFoodServices
    {
        #region Dependency Injection
        private readonly IFoodRepository _foodRepository;
        private readonly IFavorServiceRepository _favorServiceRepository;
        private readonly IFavorServiceServices _favorServiceServices;

        public FoodServices(IFoodRepository foodRepository, IFavorServiceRepository favorServiceRepository, IFavorServiceServices favorServiceServices) : base(foodRepository)
        {
            _foodRepository = foodRepository;
            _favorServiceRepository = favorServiceRepository;
            _favorServiceServices = favorServiceServices;
        }
        #endregion

        #region Main methods

        /// <summary>
        /// @author: VQPhong (03/08/2022)
        /// @desc: The service of validation for Full transaction for inserting a Food and its favorite services
        /// </summary>
        /// <param name="food">The Food needs to be inserted</param>
        /// <param name="favorServices">List of Favorite services need to be inserted</param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData InsertFullFoodData(Food food, List<FavorService>? favorServices)
        {
            // Validate data from request
            // 1. Validate Food
            var foodValidation = this.ValidateFood(food, null);
            if (foodValidation != null) return foodValidation;

            // 2.Validate FavorService
            List<FavorService> finalFavorService = new();

            if (favorServices != null && favorServices.Any())
            {
                // Ignore FSs that are empty
                finalFavorService = FavorServiceServices.FilterEmptyFavorService(favorServices);

                // 1. Check if list of FS contains items that has no Content but Surcharge
                // 2. Check if any FS is duplicated in the list
                var fsValidation = this.ValidateFavorService(finalFavorService);
                if (fsValidation != null) return fsValidation;

                // Assign ID (ID as a state of favorite service)
                finalFavorService = _favorServiceServices.AssignOrRemoveIdForFS(finalFavorService);
            }

            // Everything is Okay
            ControllerResponseData res = _foodRepository.InsertFullFood(food, finalFavorService);

            return res;
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: The service of validation for Full transaction for updating a Food and its favorite services
        /// </summary>
        /// <param name="food">Food needs to be updated</param>
        /// <param name="foodId">The ID of the food</param>
        /// <param name="favorServices">List of favorite services need to be inserted or not</param>
        /// <param name="delFavorServiceIds">List of IDs of favorite services need to be removed in the FoodFavorService</param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData UpdateFullFoodData(Food food, int foodId, List<FavorService>? favorServices)
        {
            // Validate data from request
            // 1. Validate Food
            var foodValidation = this.ValidateFood(food, foodId);
            if (foodValidation != null) return foodValidation;

            // 2.Validate FavorService
            List<FavorService> finalFavorService = new();

            if (favorServices != null && favorServices.Any())
            {
                // Ignore FSs that are empty
                finalFavorService = FavorServiceServices.FilterEmptyFavorService(favorServices);

                // 1. Check if list of FS contains items that has no Content but Surcharge
                // 2. Check if any FS is duplicated in the list
                var fsValidation = this.ValidateFavorService(finalFavorService);
                if (fsValidation != null) return fsValidation;
            }

            // Create List of FS_IDs that need to be remove from Intermediate table
            List<int> delFavorServiceIds = _favorServiceServices.CreateListDelFavorServiceIds(finalFavorService, foodId);

            if (finalFavorService.Any())
            {
                // Remove FSs that are duplicated with old FSs
                finalFavorService = _favorServiceServices.IgnoreOldFSForUpdate(finalFavorService, foodId);

                // Assign ID (ID as a state of favorite service)
                finalFavorService = _favorServiceServices.AssignOrRemoveIdForFS(finalFavorService);
            }

            // Everything is Okay
            ControllerResponseData res = _foodRepository.UpdateFullFoodById(food, foodId, finalFavorService, delFavorServiceIds);
            
            return res;
        }

        //public override bool Equals(object? obj)
        //{
        //    validate

        //    tạo 1 connection
        //    tạo transacrtion


        //    insert food(food, transaction)

        //    insert so thich phuc vu(sothichpv, tracsaction)

        //    / bang trung gian

        //    / tran.commit

        //    / rollback
        //    truyền transaction vào

        //}

        #endregion

        #region Support method

        /// <summary>
        /// @author: VQPhong (18/08/2022)
        /// @desc: Method for validation of Food
        /// </summary>
        /// <param name="food">Food model need to be validated</param>
        /// <param name="foodId">ID of Food model</param>
        /// <returns>
        /// A object of ControllerResponseData <--> Validate failed
        /// Null <--> Validate success
        /// </returns>
        private ControllerResponseData? ValidateFood(Food food, int? foodId)
        {
            // 1. Check if Food contains empty fields that are not allow to be empty
            var emptyFoodValidation = this.EmptyValidation(food);

            if (emptyFoodValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = emptyFoodValidation,
                        userMsg = emptyFoodValidation,
                    }
                };
            }

            // 2. Check if Food contains unique fields that coincides with Database
            var duplicatedFoodValidation = _foodRepository.CheckDuplicatedProp(food, foodId);

            if (duplicatedFoodValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = duplicatedFoodValidation,
                        userMsg = duplicatedFoodValidation,
                    }
                };
            }

            return null;
        }

        /// <summary>
        /// @author: VQPhong (18/08/2022)
        /// @desc: Method for validation of List of FavorService
        /// </summary>
        /// <param name="favorServices">List of FavorService needs to be validated</param>
        /// <returns>
        /// A object of ControllerResponseData <--> Validate failed
        /// Null <--> Validate success
        /// </returns>
        private ControllerResponseData? ValidateFavorService(List<FavorService> favorServices)
        {
            // 1. Check if list of FS contains items that has no Content but Surcharge
            if (FavorServiceServices.IsNoContentFavorService(favorServices))
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = Core.Resourses.VI_Resource.NoContentFS,
                        userMsg = Core.Resourses.VI_Resource.NoContentFS
                    }
                };
            }

            // 2. Check if any FS is duplicated in the list
            var listDuplicated = FavorServiceServices.CheckDuplicatedInList(favorServices);
            if (listDuplicated.Any())
            {
                StringBuilder contentAlert = new();
                string delimiter = "";

                foreach (var item in listDuplicated)
                {
                    contentAlert.Append($"{delimiter}{item}");
                    delimiter = ", ";
                }

                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = String.Format(Core.Resourses.VI_Resource.NotDuplicatedCombo, Core.Resourses.VI_Resource.DisplayNameFS, contentAlert),
                        userMsg = String.Format(Core.Resourses.VI_Resource.NotDuplicatedCombo, Core.Resourses.VI_Resource.DisplayNameFS, contentAlert)
                    }
                };
            }

            return null;
        }

        #endregion
    }
}
