using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Full transaction for inserting a Food and its favorite services
        /// </summary>
        /// <param name="food">The Food needs to be inserted</param>
        /// <param name="favorServices">List of Favorite services need to be inserted</param>
        /// <returns>
        /// Number of rows affected
        /// </returns>
        public int InsertFullFood(Food food, IEnumerable<FavorService>? favorServices);

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Full transaction for updating a Food and its favorite services
        /// </summary>
        /// <param name="food">Food needs to be updated</param>
        /// <param name="foodId">The ID of the food</param>
        /// <param name="favorServices">List of favorite services need to be inserted or not</param>
        /// <param name="delFavorServiceIds">List of IDs of favorite services need to be removed in the FoodFavorService</param>
        /// <returns>
        /// The number of rows affected
        /// </returns>
        public int UpdateFullFoodById(Food food, int foodId, IEnumerable<FavorService>? favorServices, IEnumerable<int>? delFavorServiceIds);
    }
}
