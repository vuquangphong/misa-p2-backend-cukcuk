using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Infrastructure.Repositories
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Implementation of Food Repo interface
    /// </summary>
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        // Some properties
        private DynamicParameters? _foodParams;
        private DynamicParameters? _favorServiceParams;
        private DynamicParameters? _foodFavorServiceParams;

        // Constructor
        public FoodRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Full transaction for inserting a Food and its favorite services
        /// </summary>
        /// <param name="food">The Food needs to be inserted</param>
        /// <param name="favorServices">List of Favorite services need to be inserted</param>
        /// <returns>
        /// Number of rows affected
        /// </returns>
        public ControllerResponseData InsertFullFood(Food food, List<FavorService> favorServices)
        {
            using (SqlConnection = ConnectDatabase())
            {
                // Init transaction
                SqlConnection.Open();
                MySqlTransaction transaction = SqlConnection.BeginTransaction();

                // Handling the transaction
                try
                {
                    // Create Food
                    _foodParams = new DynamicParameters();
                    _foodParams.Add("@$NewID", direction: ParameterDirection.Output, dbType: DbType.Int32);

                    var foodProps = food.GetType().GetProperties();
                    AddEntityToDynamicParams(food, foodProps, _foodParams);

                    SqlConnection.Execute(
                        "Proc_CreateFood",
                        param: _foodParams,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    int newFoodID = _foodParams.Get<int>("@$NewID");

                    // Create Favorite Services and the intermediate records
                    _favorServiceParams = new DynamicParameters();
                    _foodFavorServiceParams = new DynamicParameters();

                    PropertyInfo[] favorProps;
                    int newFavorServiceID;

                    if (favorServices.Any())
                    {
                        foreach (FavorService favorService in favorServices)
                        {
                            // Create new favorite service --> Create intermediate record
                            if (favorService.FavorServiceID <= 0 || favorService.FavorServiceID == null)
                            {
                                // Create new service
                                _favorServiceParams.Add("@$NewID", direction: ParameterDirection.Output, dbType: DbType.Int32);

                                favorProps = favorService.GetType().GetProperties();
                                AddEntityToDynamicParams(favorService, favorProps, _favorServiceParams);

                                SqlConnection.Execute(
                                    "Proc_CreateFavorService",
                                    param: _favorServiceParams,
                                    transaction: transaction,
                                    commandType: CommandType.StoredProcedure
                                );

                                newFavorServiceID = _favorServiceParams.Get<int>("@$NewID");

                                // Create intermediate record
                                _foodFavorServiceParams.Add("@$FoodID", newFoodID);
                                _foodFavorServiceParams.Add("@$FavorServiceID", newFavorServiceID);

                                SqlConnection.Execute(
                                    "Proc_CreateFoodFavorService",
                                    param: _foodFavorServiceParams,
                                    transaction: transaction,
                                    commandType: CommandType.StoredProcedure
                                );
                            }
                            else // Favor service already exist --> Create intermediate record
                            {
                                _foodFavorServiceParams.Add("@$FoodID", newFoodID);
                                _foodFavorServiceParams.Add("@$FavorServiceID", favorService.FavorServiceID);

                                SqlConnection.Execute(
                                    "Proc_CreateFoodFavorService",
                                    param: _foodFavorServiceParams,
                                    transaction: transaction,
                                    commandType: CommandType.StoredProcedure
                                );
                            }
                        }
                    }

                    // Everything is Okay
                    // Commit transaction and return success
                    transaction.Commit();
                    return new ControllerResponseData
                    {
                        customStatusCode = (int?)Core.Enum.CustomizeStatusCode.Created,
                        responseData = Core.Enum.InsertUpdateResult.Success,
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    // Actually I do not want to throw Exception like below
                    // because I know that it waste resourse of server...
                    // But... anyway, I have not yet found other solutions
                    // Thus, I choose this way! (03/08/2022)
                    //throw new Exception(ex.Message);

                    // (08/08/2022)
                    // Okay I think that I will use this way
                    return new ControllerResponseData
                    {
                        customStatusCode = (int?)Core.Enum.CustomizeStatusCode.TransactionException,
                        responseData = new
                        {
                            devMsg = ex.Message,
                            userMsg = Core.Resourses.VI_Resource.UserMsgServerError,
                        }
                    };
                }
                finally
                {
                    SqlConnection.Close();
                }
            }
        }

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
        public ControllerResponseData UpdateFullFoodById(Food food, int foodId, List<FavorService> favorServices, List<int> delFavorServiceIds)
        {
            using (SqlConnection = ConnectDatabase())
            {
                // Init transaction
                SqlConnection.Open();
                MySqlTransaction transaction = SqlConnection.BeginTransaction();

                // Handling transaction
                try
                {
                    // Update Food
                    _foodParams = new DynamicParameters();

                    var foodProps = food.GetType().GetProperties();
                    AddEntityToDynamicParams(food, foodProps, _foodParams);

                    _foodParams.Add("@$FoodID", foodId);

                    SqlConnection.Execute(
                        "Proc_UpdateFood",
                        param: _foodParams,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    // Handling favorite service and intermediate records
                    // 1. Create some new FavorService (if necessary) and create (intermediate) FoodFavorService records
                    if (favorServices.Any())
                    {
                        _favorServiceParams = new DynamicParameters();
                        _foodFavorServiceParams = new DynamicParameters();

                        PropertyInfo[] favorProps;
                        int newFavorServiceID;

                        // The list of FavorServiceIDs for adding new (intermediate) FoodFavorService record
                        List<int> addFavorServiceIDs = new();

                        // Create new favorite services and push FavorServiceID into addFavorServiceIDs
                        foreach (var favorService in favorServices)
                        {
                            if (favorService.FavorServiceID <= 0 || favorService.FavorServiceID == null)
                            {
                                // Create new service
                                _favorServiceParams.Add("@$NewID", direction: ParameterDirection.Output, dbType: DbType.Int32);

                                favorProps = favorService.GetType().GetProperties();
                                AddEntityToDynamicParams(favorService, favorProps, _favorServiceParams);

                                SqlConnection.Execute(
                                    "Proc_CreateFavorService",
                                    param: _favorServiceParams,
                                    transaction: transaction,
                                    commandType: CommandType.StoredProcedure
                                );

                                newFavorServiceID = _favorServiceParams.Get<int>("@$NewID");

                                addFavorServiceIDs.Add(newFavorServiceID);
                            }
                            else // Favor service already exist
                            {
                                addFavorServiceIDs.Add((int)favorService.FavorServiceID);
                            }
                        }

                        // Create (intermediate) FoodFavorService record
                        _foodFavorServiceParams.Add("@FoodID", foodId);

                        StringBuilder valueInsertQuery = new();
                        string delimiter = "";

                        foreach (var (id, index) in addFavorServiceIDs.Select((id, index) => (id, index)))
                        {
                            _foodFavorServiceParams.Add($"@FavorServiceID{index}", id);
                            valueInsertQuery.Append($"{delimiter}(@FoodID, @FavorServiceID{index})");

                            delimiter = ", ";
                        }

                        var sqlQuery = $"INSERT INTO FoodFavorService(FoodID, FavorServiceID) VALUES {valueInsertQuery};";
                        SqlConnection.Execute(sqlQuery, param: _foodFavorServiceParams, transaction: transaction);
                    }

                    // 2. Remove some (intermediate) FoodFavorService records
                    if (delFavorServiceIds.Any())
                    {
                        _foodFavorServiceParams = new DynamicParameters();

                        StringBuilder idStringQuery = new();
                        string delimiter = "";

                        foreach (var (id, index) in delFavorServiceIds.Select((id, index) => (id, index)))
                        {
                            _foodFavorServiceParams.Add($"@ID{index}", id);
                            idStringQuery.Append($"{delimiter}@ID{index}");

                            delimiter = ", ";
                        }

                        var sqlQuery = $"DELETE FROM FoodFavorService WHERE FavorServiceID IN ({idStringQuery});";
                        SqlConnection.Execute(sqlQuery, param: _foodFavorServiceParams, transaction: transaction);
                    }

                    // Everything is Okay
                    // Commit transaction and return success
                    transaction.Commit();
                    return new ControllerResponseData
                    {
                        customStatusCode = (int?)Core.Enum.CustomizeStatusCode.Created,
                        responseData = Core.Enum.InsertUpdateResult.Success,
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return new ControllerResponseData
                    {
                        customStatusCode = (int?)Core.Enum.CustomizeStatusCode.TransactionException,
                        responseData = new
                        {
                            devMsg = ex.Message,
                            userMsg = Core.Resourses.VI_Resource.UserMsgServerError,
                        }
                    };
                }
                finally
                {
                    SqlConnection.Close();
                }
            }
        }
    }
}
