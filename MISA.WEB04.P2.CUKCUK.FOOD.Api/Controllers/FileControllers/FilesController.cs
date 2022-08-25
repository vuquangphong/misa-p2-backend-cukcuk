using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services.FileServices;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers.FileControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        #region Dependency Injection

        private readonly IFileServices _fileServices;

        public FilesController(IFileServices fileServices)
        {
            _fileServices = fileServices;
        }

        #endregion

        /// <summary>
        /// @desc: Catching Exceptions from Server Errors
        /// @author: VQPhong (15/08/2022)
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

        /// <summary>
        /// @author: VQPhong (22/08/2022)
        /// @method: POST /Files/Images
        /// @desc: Upload image from client
        /// </summary>
        /// <param name="image">Image file</param>
        /// <returns>
        /// A message
        /// </returns>
        [HttpPost("Images")]
        public async Task<IActionResult> PostImage(IFormFile image)
        {
            try
            {
                var res = await _fileServices.UploadImage(image);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return CatchException(ex);
            }
        }
    }
}
