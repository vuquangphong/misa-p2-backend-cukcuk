using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Interface of FavorService Service
    /// </summary>
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

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Ignoring all FS that belongs to old list of FS
        /// </summary>
        /// <param name="newFavorServices">List of new FS</param>
        /// <param name="foodId">The ID of food for getting list of old FS</param>
        /// <returns>
        /// List of new FS that is totally new
        /// </returns>
        public List<FavorService> IgnoreOldFSForUpdate(List<FavorService> newFavorServices, int foodId);

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Assign ID for FS that already exists in Database
        /// Remove ID (Assign ID = 0) for FS that does not exist yet
        /// </summary>
        /// <param name="favorServices">List of FS</param>
        /// <returns>
        /// A list of FS that has correct value of IDs
        /// </returns>
        public List<FavorService> AssignOrRemoveIdForFS(List<FavorService> favorServices);

        /// <summary>
        /// @author: VQPhong (15/08/2022)
        /// @desc: Create list of FavorServiceIDs that need to be remove from intermediate table
        /// </summary>
        /// <param name="newFavorServices">List of new FS</param>
        /// <param name="foodId">The ID of food for getting list of old FS</param>
        /// <returns></returns>
        public List<int> CreateListDelFavorServiceIds(List<FavorService> newFavorServices, int foodId);
    }
}
