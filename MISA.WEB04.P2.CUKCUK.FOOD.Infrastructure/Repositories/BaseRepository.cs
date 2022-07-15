using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Some properties

        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
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

        #endregion

        #region Main Methods

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

        public T GetById(string entityId)
        {
            using (SqlConnection = ConnectDatabase())
            {
                // Create dynamic parameters
                DynamicParams = new DynamicParameters();
                DynamicParams.Add($"@${_entityName}Id", entityId);

                // Query data in database
                var sqlQuery = $"Proc_Get{_entityName}ById";

                var entity = SqlConnection.QueryFirstOrDefault<T>(
                    sqlQuery,
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return entity;
            }
        }

        public object GetPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, string? priceFilter)
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

            using (SqlConnection = ConnectDatabase())
            {
                var entitiesPaging = SqlConnection.Query<T>(
                    $"Proc_Get{_entityName}Paging",
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                int totalRecords = DynamicParams.Get<int>("@$TotalRecords");
                int totalPages = DynamicParams.Get<int>("@$TotalPages");

                return new
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Data = entitiesPaging,
                };
            }
        }

        public int Insert(T entity)
        {
            // Create dynamic parameters
            DynamicParams = new DynamicParameters();

            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;

                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    DynamicParams.Add($"@${propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    DynamicParams.Add($"@${propertyName}", propertyValue);
                }
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

        public int UpdateById(T entity, Guid entityId)
        {
            // Create dynamic parameters
            DynamicParams = new DynamicParameters();
            DynamicParams.Add($"@${_entityName}Id", entityId, DbType.String);

            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                if (propertyName == $"{_entityName}Id")
                {
                    property.SetValue(entity, entityId);
                }
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;

                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    DynamicParams.Add($"@${propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    DynamicParams.Add($"@${propertyName}", propertyValue);
                }
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

        public bool IsDuplicateCode(string entityCode, string entityId, bool isPut)
        {
            using (SqlConnection = ConnectDatabase())
            {
                DynamicParams = new DynamicParameters();
                DynamicParams.Add($"@{_entityName}Code", entityCode);

                if (isPut)
                {
                    DynamicParams.Add($"@{_entityName}Id", entityId);

                    var sqlQuery = $"SELECT {_entityName}Code FROM {_entityName} WHERE {_entityName}Id = @{_entityName}Id";
                    var currentEntity = SqlConnection.QueryFirstOrDefault<T>(sqlQuery, param: DynamicParams);
                    var propsCurEntity = currentEntity.GetType().GetProperties();

                    foreach (var prop in propsCurEntity)
                    {
                        if (prop.GetValue(currentEntity) != null)
                        {
                            var propValue = prop.GetValue(currentEntity).ToString();
                            if (propValue == entityCode)
                            {
                                return false;
                            }
                        }
                    }
                }

                var sqlCheck = $"SELECT {_entityName}Code FROM {_entityName} WHERE {_entityName}Code = @{_entityName}Code";
                var isExist = SqlConnection.QueryFirstOrDefault(sqlCheck, param: DynamicParams);

                if (isExist == null)
                {
                    return false;
                }
                return true;

            }
        }

        public int DeleteById(string entityId)
        {
            using (SqlConnection = ConnectDatabase())
            {
                // Create dynamic parameters
                DynamicParams = new DynamicParameters();
                DynamicParams.Add($"@${_entityName}Id", entityId);

                // Query data in Database
                var sqlQuery = $"Proc_Delete{_entityName}ById";
                var rowsEffect = SqlConnection.Execute(
                    sqlQuery,
                    param: DynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return rowsEffect;
            }
        }

        #endregion
    }
}
