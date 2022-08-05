using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Infrastructure.Repositories
{
    public class FavorServiceRepository : BaseRepository<FavorService>, IFavorServiceRepository
    {
        public FavorServiceRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Get all Favorite Services of a Food by FoodID
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns>
        /// A list of Favorite Services
        /// </returns>
        public IEnumerable<FavorService> GetByFoodId(int foodId)
        {
            using (SqlConnection = ConnectDatabase())
            {
                // Create dynamic parameters
                DynamicParams = new DynamicParameters();
                DynamicParams.Add("@$FoodID", foodId);

                // Query data in database
                var sqlQuery = "Proc_GetFavorServiceByFoodID";

                var favorServices = SqlConnection.Query<FavorService>(
                    sqlQuery,
                    param: DynamicParams,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return favorServices;
            }
        }
    }
}
