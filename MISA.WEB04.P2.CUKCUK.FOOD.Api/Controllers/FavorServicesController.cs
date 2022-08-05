using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FavorServicesController : MISABaseController<FavorService>
    {
        #region Dependency Injection

        private readonly IFavorServiceRepository _favorServiceRepository;
        private readonly IFavorServiceServices _favorServiceServices;

        public FavorServicesController(IFavorServiceRepository favorServiceRepository, IFavorServiceServices favorServiceServices) : base(favorServiceRepository, favorServiceServices)
        {
            _favorServiceRepository = favorServiceRepository;
            _favorServiceServices = favorServiceServices;
        }

        #endregion

        /// <summary>
        /// @method: GET /FavorServices/food/{foodId}
        /// @desc: Get the Info of an array of favorite services by foodId
        /// @author: VQPhong (25/07/2022)
        /// </summary>
        /// <param name="foodId"></param>
        [HttpGet("Foods/{foodId}")]
        public IActionResult GetFavor(int foodId)
        {
            try
            {
                var res = _favorServiceServices.GetDataByFoodId(foodId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }

        [HttpPost]
        public override IActionResult Post(FavorService favorService)
        {
            try
            {
                var res = _favorServiceServices.InsertData(favorService);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }
    }
}
