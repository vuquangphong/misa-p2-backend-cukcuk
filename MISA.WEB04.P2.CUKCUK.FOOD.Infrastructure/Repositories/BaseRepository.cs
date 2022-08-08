using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
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
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Some properties & Constructor

        private readonly string _connectionString;
        private readonly string _entityName = typeof(T).Name;

        protected MySqlConnection? SqlConnection;
        protected DynamicParameters? DynamicParams;

        /// <summary>
        /// @desc: Constructor for Passing (Injection) connection string from appsettings.json
        /// @author: VQPhong (14/07/2022)
        /// </summary>
        /// <param name="configuration"></param>
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("VQPHONG");
        }

        #endregion

        #region Support Methods

        /// <summary>
        /// @desc: Create MySQL Connection from Connection string
        /// @author: VQPhong (14/07/2022)
        /// </summary>
        /// <returns>
        /// The MySqlConnection
        /// </returns>
        protected MySqlConnection ConnectDatabase()
        {
            // Initital Connection
            var sqlConnection = new MySqlConnection(_connectionString);

            return sqlConnection;
        }

        /// <summary>
        /// @author: VQPhong (02/08/2022)
        /// @desc: Add dynamic parameters for an entity
        /// </summary>
        /// <param name="entity">The entity need to be added dynamic params</param>
        /// <param name="props">The list of props of the entity</param>
        /// <param name="parameters">The dynamic parameters object</param>
        protected void AddEntityToDynamicParams(Object? entity, PropertyInfo[] props, DynamicParameters parameters)
        {
            foreach (var prop in props)
            {
                if (prop.GetCustomAttributes(typeof(NotUsageParams), true).Length <= 0)
                {
                    var propName = prop.Name;
                    var propValue = prop.GetValue(entity);

                    parameters.Add($"@${propName}", propValue);
                }
            }
        }

        #endregion

        #region Main Methods

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Get all entities from a table in the database
        /// </summary>
        /// <returns>
        /// A list of entities
        /// </returns>
        public IEnumerable<T> GetAll()
        {
            using (SqlConnection = ConnectDatabase())
            {
                string sqlString = $"Proc_GetAll{_entityName}";

                var entities = SqlConnection.Query<T>(
                    sqlString,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return entities;
            }
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Get an entity by its ID
        /// </summary>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// An entity
        /// </returns>
        public T GetById(int entityId)
        {
            // Create dynamic parameters
            DynamicParams = new DynamicParameters();
            DynamicParams.Add($"@${_entityName}ID", entityId);

            // Query data in database
            var sqlQuery = $"Proc_Get{_entityName}ByID";

            using (SqlConnection = ConnectDatabase())
            {
                var entity = SqlConnection.QueryFirstOrDefault<T>(
                    sqlQuery,
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return entity;
            }
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Get data paging of entites
        /// </summary>
        /// <param name="pageIndex">The page number</param>
        /// <param name="pageSize">The size of the page</param>
        /// <param name="codeFilter">The filter for Code property</param>
        /// <param name="nameFilter">The filter for Name property</param>
        /// <param name="groupFilter">The filter for Group Name property</param>
        /// <param name="unitFilter">The filter for Unit Name property</param>
        /// <param name="priceFilter">The filter for Price property</param>
        /// <returns>
        /// An object contains: TotalRecords, TotalPages, TotalRecordsInPage, [main]Data 
        /// </returns>
        public object GetPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, double? priceFilter)
        {
            DynamicParams = new DynamicParameters();

            DynamicParams.Add("@$PageIndex", pageIndex);
            DynamicParams.Add("@$PageSize", pageSize);
            DynamicParams.Add("@$CodeFilter", codeFilter);
            DynamicParams.Add("@$NameFilter", nameFilter);
            DynamicParams.Add("@$GroupFilter", groupFilter);
            DynamicParams.Add("@$UnitFilter", unitFilter);
            DynamicParams.Add("@$PriceFilter", priceFilter);

            DynamicParams.Add("@$TotalRecords", direction: ParameterDirection.Output, dbType: DbType.Int32);
            DynamicParams.Add("@$TotalPages", direction: ParameterDirection.Output, dbType: DbType.Int32);
            DynamicParams.Add("@$TotalRecordsInPage", direction: ParameterDirection.Output, dbType: DbType.Int32);

            using (SqlConnection = ConnectDatabase())
            {
                var entitiesPaging = SqlConnection.Query<T>(
                    $"Proc_Get{_entityName}Paging",
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                int totalRecords = DynamicParams.Get<int>("@$TotalRecords");
                int totalPages = DynamicParams.Get<int>("@$TotalPages");
                int totalRecordsInPage = DynamicParams.Get<int>("@$TotalRecordsInPage");

                return new
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    TotalRecordsInPage = totalRecordsInPage,
                    Data = entitiesPaging,
                };
            }
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Insert an Entity record into database
        /// </summary>
        /// <param name="entity">The ID of the entity</param>
        /// <returns>
        /// Number or rows that are affected
        /// </returns>
        public int Insert(T entity)
        {
            // Create dynamic parameters
            DynamicParams = new DynamicParameters();
            DynamicParams.Add("@$NewID", direction: ParameterDirection.Output, dbType: DbType.Int32);

            var properties = entity?.GetType().GetProperties();

            if (properties != null)
            {
                AddEntityToDynamicParams(entity, properties, DynamicParams);
            }
            else
            {
                return 0;
            }

            var sqlQuery = $"Proc_Create{_entityName}";

            using (SqlConnection = ConnectDatabase())
            {
                var rowsEffect = SqlConnection.Execute(
                    sqlQuery,
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return rowsEffect;
            }
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Update an entity record of database by its ID
        /// </summary>
        /// <param name="entity">Entity needs to be updated</param>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// Number of rows that are affected
        /// </returns>
        public int UpdateById(T entity, int entityId)
        {
            // Create dynamic parameters
            DynamicParams = new DynamicParameters();
            DynamicParams.Add($"@${_entityName}ID", entityId);

            var properties = entity?.GetType().GetProperties();

            if (properties != null)
            {
                AddEntityToDynamicParams(entity, properties, DynamicParams);
            }
            else
            {
                return 0;
            }

            // Query data in database
            var sqlQuery = $"Proc_Update{_entityName}";

            using (SqlConnection = ConnectDatabase())
            {
                var rowsEffect = SqlConnection.Execute(
                    sqlQuery,
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return rowsEffect;
            }
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Delete an entity record by its ID
        /// </summary>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// Number of rows that are affected
        /// </returns>
        public int DeleteById(int entityId)
        {
            // Create dynamic parameters
            DynamicParams = new DynamicParameters();
            DynamicParams.Add($"@${_entityName}ID", entityId);

            using (SqlConnection = ConnectDatabase())
            {
                // Query data in Database
                var sqlQuery = $"Proc_Delete{_entityName}ByID";
                var rowsEffect = SqlConnection.Execute(
                    sqlQuery,
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return rowsEffect;
            }
        }

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Check if some props are duplicated or not
        /// </summary>
        /// <param name="entity">The entity needs to be</param>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// Duplicated --> An alert string corresponding
        /// All not duplicate --> null string
        /// </returns>
        public string? CheckDuplicatedProp(T entity, int? entityId)
        {
            // Init dynamic parameters
            DynamicParams = new DynamicParameters();

            if (entityId != null)
            {
                DynamicParams.Add($"@{_entityName}ID", entityId);
            }

            // Get list of properties that are NotDuplicated
            var notDuplicatedProps = entity?.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(NotDuplicated))
            );

            // Check if those props are duplicated or not?
            if (notDuplicatedProps != null)
            {
                foreach (var prop in notDuplicatedProps)
                {
                    var propValue = prop.GetValue(entity);
                    var propName = prop.Name;

                    DynamicParams.Add($"@{propName}", propValue);

                    string sqlQuery;

                    if (entityId != null)
                    {
                        // entityId != null => Updating
                        sqlQuery = $"SELECT {propName} FROM {_entityName} WHERE {propName} = @{propName} AND {_entityName}ID != @{_entityName}ID";
                    }
                    else
                    {
                        // entityId == null => Inserting
                        sqlQuery = $"SELECT {propName} FROM {_entityName} WHERE {propName} = @{propName}";
                    }

                    using (SqlConnection = ConnectDatabase())
                    {
                        var isExist = SqlConnection.QueryFirstOrDefault(sqlQuery, param: DynamicParams);

                        if (isExist != null)
                        {
                            var namesOfProp = prop.GetCustomAttributes(typeof(PropsName), true);
                            var propNameDisplay = string.Empty;
                            if (namesOfProp.Length > 0)
                            {
                                propNameDisplay = ((PropsName)namesOfProp[0]).Name;
                            }

                            return String.Format(Core.Resourses.VI_Resource.PropNotDuplicated, propNameDisplay);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// @author: VQPhong (03/08/2022)
        /// @desc: Check if combo props already exist
        /// </summary>
        /// <param name="entity">The entity record needs to be checked combo props</param>
        /// <returns>
        /// An alert message if exist
        /// Null if not exist
        /// </returns>
        public string? CheckDuplicatedCombo(T entity)
        {
            // Init dynamic parameters
            DynamicParams = new DynamicParameters();

            // Get list of properties that are NotDuplicated
            var notDuplicatedComboProps = entity?.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(NotDuplicatedCombo))
            );

            // Check if those props are duplicated or not?
            if (notDuplicatedComboProps != null)
            {
                // Get the name display of the entity 
                var primaryProps = entity?.GetType().GetProperties().Where(
                    prop => Attribute.IsDefined(prop, typeof(PrimaryKey))
                );
                var entityNameDisplay = string.Empty;
                var namesOfPrimaryKey = primaryProps?.First().GetCustomAttributes(typeof(PropsName), true);
                if (namesOfPrimaryKey?.Length > 0)
                {
                    entityNameDisplay = ((PropsName)namesOfPrimaryKey[0]).Name;
                }

                // Add dynamic parameters
                // Create prop name display for alert message if {combo props duplicated}
                var propNameDisplay = new StringBuilder();
                string delimiter = "";

                foreach (var prop in notDuplicatedComboProps)
                {
                    var propName = prop.Name;
                    var propValue = prop.GetValue(entity);

                    propNameDisplay.Append($"{delimiter}{propValue}");
                    delimiter = " - ";

                    DynamicParams.Add($"@${propName}", propValue);
                }

                // Query data in database
                var sqlQuery = $"Proc_CheckDuplicatedCombo{_entityName}";

                using (SqlConnection = ConnectDatabase())
                {
                    var isExist = SqlConnection.QueryFirstOrDefault(
                        sqlQuery,
                        param: DynamicParams,
                        commandType: CommandType.StoredProcedure
                    );

                    // If combo props already exist
                    if (isExist != null)
                    {
                        return String.Format(Core.Resourses.VI_Resource.NotDuplicatedCombo, entityNameDisplay, propNameDisplay.ToString());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// @author: VQPhong (08/08/2022)
        /// Check if the current code is duplicated
        /// </summary>
        /// <param name="code">The code needs to be checked duplicated</param>
        /// <returns>
        /// True --> Duplicated
        /// False --> Not Duplicated
        /// </returns>
        public bool CheckDuplicatedCode(string code)
        {
            // Create dynamic parameters
            DynamicParams = new DynamicParameters();
            DynamicParams.Add($"@{_entityName}Code", code);

            // SQL query
            var sqlQuery = $"SELECT {_entityName}Code FROM {_entityName} WHERE {_entityName}Code = @{_entityName}Code";

            // Connect Database
            using(SqlConnection = ConnectDatabase())
            {
                var isExist = SqlConnection.QueryFirstOrDefault(sqlQuery, param: DynamicParams);

                if (isExist != null) return true;

                return false;
            }
        }

        #endregion
    }
}
