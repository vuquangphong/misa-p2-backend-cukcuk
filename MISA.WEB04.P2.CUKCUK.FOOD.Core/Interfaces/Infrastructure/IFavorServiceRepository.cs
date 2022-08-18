using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Interface of Favorite service repository
    /// </summary>
    public interface IFavorServiceRepository : IBaseRepository<FavorService>
    {
        /// <summary>
        /// @author: VQPhong (25/07/2022)
        /// @desc: Get a list of FavorServices by FoodID
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns>
        /// An array of Favorite Services
        /// </returns>
        public IEnumerable<FavorService> GetByFoodId(int foodId);
    }
}
