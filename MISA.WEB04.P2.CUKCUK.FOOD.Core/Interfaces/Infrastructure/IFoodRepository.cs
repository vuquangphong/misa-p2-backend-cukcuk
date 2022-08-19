using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Interface of Food repository
    /// </summary>
    public interface IFoodRepository : IBaseRepository<Food>
    {
        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Full transaction for inserting a Food and its favorite services
        /// </summary>
        /// <param name="food">The Food needs to be inserted</param>
        /// <param name="favorServices">List of Favorite services need to be inserted</param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        //public ControllerResponseData InsertFullFood(Food food, List<FavorService> favorServices);

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Full transaction for updating a Food and its favorite services
        /// </summary>
        /// <param name="food">Food needs to be updated</param>
        /// <param name="foodId">The ID of the food</param>
        /// <param name="favorServices">List of favorite services need to be inserted or not</param>
        /// <param name="delFavorServiceIds">List of IDs of favorite services need to be removed in the FoodFavorService</param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        //public ControllerResponseData UpdateFullFoodById(Food food, int foodId, List<FavorService> favorServices, List<int> delFavorServiceIds);
    }
}
