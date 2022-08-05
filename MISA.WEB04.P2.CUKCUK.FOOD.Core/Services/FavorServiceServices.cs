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
            int rowsEffect = _favorServiceRepository.Insert(favorService);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(rowsEffect > 0 ? Core.Enum.CustomizeStatusCode.Created : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = rowsEffect,
            };

            return res;
        }

        #endregion
    }
}
