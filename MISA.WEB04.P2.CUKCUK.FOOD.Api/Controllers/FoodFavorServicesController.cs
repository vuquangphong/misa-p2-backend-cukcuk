using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodFavorServicesController : MISABaseController<FoodFavorService>
    {
        #region Dependency Injection

        private readonly IFoodFavorServiceRepository _foodFavorServiceRepository;
        private readonly IFoodFavorServiceServices _foodFavorServiceServices;

        public FoodFavorServicesController(IFoodFavorServiceRepository foodFavorServiceRepository, IFoodFavorServiceServices foodFavorServiceServices) : base(foodFavorServiceRepository, foodFavorServiceServices)
        {
            _foodFavorServiceRepository = foodFavorServiceRepository;
            _foodFavorServiceServices = foodFavorServiceServices;
        }

        #endregion
    }
}
