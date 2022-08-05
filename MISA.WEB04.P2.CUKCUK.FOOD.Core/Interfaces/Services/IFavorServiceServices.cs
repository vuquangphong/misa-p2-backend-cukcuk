using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services
{
    public interface IFavorServiceServices : IBaseServices<FavorService>
    {
        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The service of Get Favorite Services by Food Id
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        public ControllerResponseData GetDataByFoodId(int foodId);

        /// <summary>
        /// @author: VQPhong (03/08/2022)
        /// @desc: The Service for Adding a new FavorService
        /// </summary>
        /// <param name="favorService"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public new ControllerResponseData InsertData(FavorService favorService);
    }
}
