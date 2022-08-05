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
    public class FoodServices : BaseServices<Food>, IFoodServices
    {
        #region Dependency Injection
        private readonly IFoodRepository _foodRepository;
        private readonly IFavorServiceRepository _favorServiceRepository;

        public FoodServices(IFoodRepository foodRepository, IFavorServiceRepository favorServiceRepository) : base(foodRepository)
        {
            _foodRepository = foodRepository;
            _favorServiceRepository = favorServiceRepository;
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
        public ControllerResponseData InsertFullFoodData(Food food, IEnumerable<FavorService>? favorServices)
        {
            // Validate data from request
            // 1. Validate Food
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

            var duplicatedFoodValidation = _foodRepository.CheckDuplicatedProp(food, null);

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

            // 2.Validate FavorService
            if (favorServices != null && favorServices.Any())
            {
                foreach (var favorService in favorServices)
                {
                    if (favorService.FavorServiceID <= 0 || favorService.FavorServiceID == null)
                    {
                        var emptyFavorServiceValidation = this.EmptyValidation<FavorService>(favorService);

                        if (emptyFavorServiceValidation != null)
                        {
                            return new ControllerResponseData
                            {
                                customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                                responseData = new
                                {
                                    devMsg = emptyFavorServiceValidation,
                                    userMsg = emptyFavorServiceValidation,
                                }
                            };
                        }

                        var duplicatedComboFavorServiceValidation = _favorServiceRepository.CheckDuplicatedCombo(favorService);

                        if (duplicatedComboFavorServiceValidation != null)
                        {
                            return new ControllerResponseData
                            {
                                customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                                responseData = new
                                {
                                    devMsg = duplicatedComboFavorServiceValidation,
                                    userMsg = duplicatedComboFavorServiceValidation,
                                }
                            };
                        }
                    }
                }
            }
            
            // Everything is Okay
            int resRepo = _foodRepository.InsertFullFood(food, favorServices);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(resRepo > 0 ? Core.Enum.CustomizeStatusCode.Created : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = resRepo,
            };

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
        public ControllerResponseData UpdateFullFoodData(Food food, int foodId, IEnumerable<FavorService>? favorServices, IEnumerable<int>? delFavorServiceIds)
        {
            // Validate data from request
            // 1. Validate Food
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

            // 2.Validate FavorService
            if (favorServices != null && favorServices.Any())
            {
                foreach (var favorService in favorServices)
                {
                    if (favorService.FavorServiceID <= 0 || favorService.FavorServiceID == null)
                    {
                        var emptyFavorServiceValidation = this.EmptyValidation<FavorService>(favorService);

                        if (emptyFavorServiceValidation != null)
                        {
                            return new ControllerResponseData
                            {
                                customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                                responseData = new
                                {
                                    devMsg = emptyFavorServiceValidation,
                                    userMsg = emptyFavorServiceValidation,
                                }
                            };
                        }

                        var duplicatedComboFavorServiceValidation = _favorServiceRepository.CheckDuplicatedCombo(favorService);

                        if (duplicatedComboFavorServiceValidation != null)
                        {
                            return new ControllerResponseData
                            {
                                customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                                responseData = new
                                {
                                    devMsg = duplicatedComboFavorServiceValidation,
                                    userMsg = duplicatedComboFavorServiceValidation,
                                }
                            };
                        }
                    }
                }
            }

            // Everything is Okay
            int resRepo = _foodRepository.UpdateFullFoodById(food, foodId, favorServices, delFavorServiceIds);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(resRepo > 0 ? Core.Enum.CustomizeStatusCode.Updated : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = resRepo,
            };

            return res;
        }

        #endregion
    }
}
