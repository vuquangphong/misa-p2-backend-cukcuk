using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

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
    }
}
