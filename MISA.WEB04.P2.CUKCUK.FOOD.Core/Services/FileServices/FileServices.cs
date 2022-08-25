using Microsoft.AspNetCore.Http;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services.FileServices;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Services.FileServices
{
    public class FileServices : IFileServices
    {
        /// <summary>
        /// @author: VQPhong (25/08/2022)
        /// @desc: Xử lý service của việc upload ảnh lên server
        /// </summary>
        /// <param name="image">Ảnh cần upload</param>
        /// <returns>
        /// ControllerResponseData
        /// </returns>
        public async Task<ControllerResponseData> UploadImage(IFormFile image)
        {
            // Kiểm tra dung lượng của ảnh
            if (image.Length > 5 * 1024 * 1024)
            {
                // Trả về câu cảnh báo
                // Dung lượng ảnh phải nhỏ hơn 5MB
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = Core.Resourses.VI_Resource.ImageOverSize,
                        userMsg = Core.Resourses.VI_Resource.ImageOverSize,
                    }
                };
            }

            // Đặt tên lại cho ảnh
            var imgExt = ".jpg";
            string imgName = $"{Guid.NewGuid()}{imgExt}";

            // Tạo đường dẫn chung để lưu file, media (nếu chưa có)
            var buildPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Food");

            if (!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }

            // Tạo đường dẫn cho ảnh
            var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Food", imgName);

            // TODO: Giảm dung lượng ảnh xuống

            // Copy ảnh theo đường dẫn 
            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Tạo link ảnh trả về cho client
            var imgLink = "/Upload/Food/" + imgName;

            // Trả về response
            return new ControllerResponseData
            {
                customStatusCode = (int?)Core.Enum.CustomizeStatusCode.Created,
                responseData = imgLink,
            };
        }
    }
}
