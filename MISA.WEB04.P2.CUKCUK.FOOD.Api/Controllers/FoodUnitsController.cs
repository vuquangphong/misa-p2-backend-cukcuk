using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodUnitsController : MISABaseController<FoodUnit>
    {
        #region Dependency Injection

        private readonly IFoodUnitRepository _foodUnitRepository;
        private readonly IFoodUnitServices _foodUnitServices;

        public FoodUnitsController(IFoodUnitRepository foodUnitRepository, IFoodUnitServices foodUnitServices) : base(foodUnitRepository, foodUnitServices)
        {
            _foodUnitRepository = foodUnitRepository;
            _foodUnitServices = foodUnitServices;
        }

        #endregion
    }
}
