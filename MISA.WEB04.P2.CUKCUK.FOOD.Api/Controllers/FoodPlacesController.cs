using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodPlacesController : MISABaseController<FoodPlace>
    {
        #region Dependency Injection

        private readonly IFoodPlaceRepository _foodPlaceRepository;
        private readonly IFoodPlaceServices _foodPlaceServices;

        public FoodPlacesController(IFoodPlaceRepository foodPlaceRepository, IFoodPlaceServices foodPlaceServices) : base(foodPlaceRepository, foodPlaceServices)
        {
            _foodPlaceRepository = foodPlaceRepository;
            _foodPlaceServices = foodPlaceServices;
        }

        #endregion
    }
}
