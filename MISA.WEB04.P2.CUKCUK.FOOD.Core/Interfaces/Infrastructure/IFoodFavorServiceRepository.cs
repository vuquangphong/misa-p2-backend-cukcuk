using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
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
    /// @desc: Interface of Food Favorite Service (intermediate model) repository
    /// </summary>
    public interface IFoodFavorServiceRepository : IBaseRepository<FoodFavorService>
    {
        /// <summary>
        /// @author: VQPhong (18/08/2022)
        /// @desc: Insert multi intermediate models
        /// </summary>
        /// <param name="favorServiceIds">List of favorite service ids</param>
        /// <param name="foodId">ID of food</param>
        /// <param name="transaction"></param>
        /// <returns>
        /// Number of rows affected
        /// </returns>
        public int InsertMultiFFSs(int foodId, List<int> favorServiceIds, MySqlConnection sqlConnection, MySqlTransaction transaction);
    }
}
