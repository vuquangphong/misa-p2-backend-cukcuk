using Microsoft.AspNetCore.Http;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services.FileServices
{
    public interface IFileServices
    {
        /// <summary>
        /// @author: VQPhong (25/08/2022)
        /// @desc: Xử lý service của việc upload ảnh lên server
        /// </summary>
        /// <param name="image">Ảnh cần upload</param>
        /// <returns>
        /// ControllerResponseData
        /// </returns>
        public Task<ControllerResponseData> UploadImage(IFormFile image);
    }
}
