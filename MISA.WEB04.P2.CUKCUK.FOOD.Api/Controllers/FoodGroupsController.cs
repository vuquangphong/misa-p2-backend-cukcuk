using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodGroupsController : MISABaseController<FoodGroup>
    {
        #region Dependency Injection

        private readonly IFoodGroupRepository _foodGroupRepository;
        private readonly IFoodGroupServices _foodGroupServices;

        public FoodGroupsController(IFoodGroupRepository foodGroupRepository, IFoodGroupServices foodGroupServices) : base(foodGroupRepository, foodGroupServices)
        {
            _foodGroupRepository = foodGroupRepository;
            _foodGroupServices = foodGroupServices;
        }

        #endregion
    }
}
