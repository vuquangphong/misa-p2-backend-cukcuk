using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodsController : MISABaseController<Food>
    {
        #region Dependency Injection

        private readonly IFoodRepository _foodRepository;
        private readonly IFoodServices _foodServices;

        public FoodsController(IFoodRepository foodRepository, IFoodServices foodServices) : base(foodRepository, foodServices)
        {
            _foodRepository = foodRepository;
            _foodServices = foodServices;
        }

        #endregion

        #region Main controllers

        /// <summary>
        /// @method: POST /Foods/FavorServices
        /// @desc: Insert Full Transaction of Food and FavorService
        /// @author: VQPhong (03/08/2022)
        /// </summary>
        /// <param name="food">The Food, which contains list of FavorSevices</param>
        /// <returns>
        /// A Message (Success or Fail)
        /// </returns>
        [HttpPost("FavorServices")]
        public IActionResult PostFullFood(Food food)
        {
            try
            {
                var res = _foodServices.InsertFullFoodData(food, food.FavorServices);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }

        /// <summary>
        /// @method: PUT /Foods/FavorServices/{foodId}
        /// @desc: Update Full Transaction of Food and FavorService by FoodID
        /// @author: VQPhong (03/08/2022)
        /// </summary>
        /// <param name="food">
        /// The food, which contains list of FavoriteService and 
        /// the list of FavorServiceIDs which need to be removed
        /// </param>
        /// <param name="foodId">The ID of the food</param>
        /// <returns>
        /// A Message (Success or Fail)
        /// </returns>
        [HttpPut("FavorServices/{foodId}")]
        public IActionResult PutFullFood(Food food, int foodId)
        {
            try
            {
                var res = _foodServices.UpdateFullFoodData(food, foodId, food.FavorServices);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }

        #endregion
    }
}
