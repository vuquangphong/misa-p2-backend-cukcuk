using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Infrastructure.Repositories
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Implementation of Food FavorService (intermediate model) Repo interface
    /// </summary>
    public class FoodFavorServiceRepository : BaseRepository<FoodFavorService>, IFoodFavorServiceRepository
    {
        public FoodFavorServiceRepository(IConfiguration configuration) : base(configuration)
        {
        }

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
        public int InsertMultiFFSs(int foodId, List<int> favorServiceIds, MySqlConnection sqlConnection, MySqlTransaction transaction)
        {
            // Create Dynamic Parameter & insert query
            DynamicParams = new DynamicParameters();

            DynamicParams.Add("@FoodID", foodId);

            StringBuilder valueInsertQuery = new();
            string delimiter = "";

            foreach (var (id, index) in favorServiceIds.Select((id, index) => (id, index)))
            {
                DynamicParams.Add($"@FavorServiceID{index}", id);

                valueInsertQuery.Append($"{delimiter}(@FoodID, @FavorServiceID{index})");
                delimiter = ", ";
            }

            var sqlQuery = $"INSERT INTO FoodFavorService(FoodID, FavorServiceID) VALUES {valueInsertQuery};";

            // Execute query

            var rowsEffect = sqlConnection.Execute(sqlQuery, param: DynamicParams, transaction: transaction);
            return rowsEffect;
        }
    }
}
