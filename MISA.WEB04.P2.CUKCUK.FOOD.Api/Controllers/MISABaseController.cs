using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MISABaseController<T> : ControllerBase
    {
        #region Dependency Injection

        private readonly IBaseRepository<T> _baseRepository;
        private readonly IBaseServices<T> _baseServices;

        public MISABaseController(IBaseRepository<T> baseRepository, IBaseServices<T> baseServices)
        {
            _baseRepository = baseRepository;
            _baseServices = baseServices;
        }

        #endregion

        #region Support Method

        /// <summary>
        /// @desc: Catching Exceptions from Server Errors
        /// @author: VQPhong (15/07/2022)
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>
        /// Status Code 500
        /// </returns>
        protected IActionResult CatchException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = Core.Resourses.VI_Resource.UserMsgServerError,
            };
            return StatusCode(500, res);
        }

        #endregion

        #region Main Controllers

        /// <summary>
        /// @method: GET /Entities
        /// @desc: Get the Info of all Entities
        /// @author: VQPhong (15/07/2022)
        /// </summary>
        /// <returns>
        /// An array of Entities
        /// </returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var res = _baseServices.GetAllData();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
            
        }

        /// <summary>
        /// @method: GET /Entities/{entityId}
        /// @desc: Get the Info of an Entity by Id
        /// @author: VQPhong (15/07/2022)
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// The Entity corresponding
        /// </returns>
        [HttpGet("{entityId}")]
        public IActionResult Get(string entityId)
        {
            try
            {
                var res = _baseServices.GetDataById(entityId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }

        /// <summary>
        /// @method: GET /Entities/filter?...Filter=...
        /// @desc: Search for Entities by some filter and pageIndex, pageSize
        /// Get Paging
        /// @author: VQPhong (15/07/2022)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="codeFilter"></param>
        /// <param name="nameFilter"></param>
        /// <param name="groupFilter"></param>
        /// <param name="unitFilter"></param>
        /// <param name="priceFilter"></param>
        [HttpGet("filter")]
        public IActionResult GetPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, string? priceFilter)
        {
            try
            {
                var res = _baseServices.GetDataPaging(pageIndex, pageSize, codeFilter, nameFilter, groupFilter, unitFilter, priceFilter);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }

        /// <summary>
        /// @method: POST /Entities
        /// @desc: Insert a new Entity into Database
        /// @author: VQPhong (15/07/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// A Message (Success or Fail)
        /// </returns>
        [HttpPost]
        public IActionResult Post(T entity)
        {
            try
            {
                var res = _baseServices.InsertData(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }

        /// <summary>
        /// @method: PUT /Entities/{entityId}
        /// @desc: Update some Info of an Entity
        /// @author: VQPhong (15/07/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// A Message (Success or Fail)
        /// </returns>
        [HttpPut("{entityId}")]
        public IActionResult Put(T entity, Guid entityId)
        {
            try
            {
                var res = _baseServices.UpdateData(entity, entityId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }

        /// <summary>
        /// @method: DELETE /Entities/{entityId}
        /// @desc: Remove an Entity by Id
        /// @author: VQPhong (15/07/2022)
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// A Message (Success or Fail)
        /// </returns>
        [HttpDelete("{entityId}")]
        public IActionResult Delete(string entityId)
        {
            try
            {
                var res = _baseServices.DeleteData(entityId);
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
