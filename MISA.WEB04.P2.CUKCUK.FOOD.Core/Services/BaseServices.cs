using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Services
{
    public class BaseServices<T> : IBaseServices<T>
    {
        #region Dependency Injection

        private readonly IBaseRepository<T> _baseRepository;

        public BaseServices(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #endregion

        #region Main Functions

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service of GetAll
        /// </summary>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData GetAllData()
        {
            var data = _baseRepository.GetAll();

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(data == null || data?.Count() == 0 ? Core.Enum.CustomizeStatusCode.NoContent : Core.Enum.CustomizeStatusCode.GetOkay),
                responseData = data
            };

            return res;
        }

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The service of Get by Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData GetDataById(int entityId)
        {
            var data = _baseRepository.GetById(entityId);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(data == null ? Core.Enum.CustomizeStatusCode.NoContent : Core.Enum.CustomizeStatusCode.GetOkay),
                responseData = data
            };

            return res;
        }

        /// <summary>
        /// @author: VQPhong (13/02/2022)
        /// @desc: Service of Get Paging
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="codeFilter"></param>
        /// <param name="nameFilter"></param>
        /// <param name="groupFilter"></param>
        /// <param name="unitFilter"></param>
        /// <param name="priceFilter"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData GetDataPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, double? priceFilter)
        {
            var data = _baseRepository.GetPaging(pageIndex, pageSize, codeFilter, nameFilter, groupFilter, unitFilter, priceFilter);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(Core.Enum.CustomizeStatusCode.GetOkay),
                responseData = data
            };

            return res;
        }

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service for Adding a new Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public virtual ControllerResponseData InsertData(T entity)
        {
            // Validate data from request
            var emptyValidation = this.EmptyValidation(entity);

            if (emptyValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = emptyValidation,
                        userMsg = emptyValidation,
                    }
                };
            }

            var duplicatedValidation = _baseRepository.CheckDuplicatedProp(entity, null);

            if (duplicatedValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = duplicatedValidation,
                        userMsg = duplicatedValidation,
                    }
                };
            }

            // Everything is Okay
            int rowsEffect = _baseRepository.Insert(entity);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(rowsEffect > 0 ? Core.Enum.CustomizeStatusCode.Created : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = rowsEffect,
            };

            return res;
        }

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service for Updating an Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData UpdateData(T entity, int entityId)
        {
            // Validate data from request
            var emptyValidation = this.EmptyValidation(entity);

            if (emptyValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = emptyValidation,
                        userMsg = emptyValidation,
                    }
                };
            }

            var duplicatedValidation = _baseRepository.CheckDuplicatedProp(entity, entityId);

            if (duplicatedValidation != null)
            {
                return new ControllerResponseData
                {
                    customStatusCode = (int?)Core.Enum.CustomizeStatusCode.BadRequest,
                    responseData = new
                    {
                        devMsg = duplicatedValidation,
                        userMsg = duplicatedValidation,
                    }
                };
            }

            // Everything is Okay
            int rowsEffect = _baseRepository.UpdateById(entity, entityId);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(rowsEffect > 0 ? Core.Enum.CustomizeStatusCode.Updated : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = rowsEffect,
            };

            return res;
        }

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service of Removing an Entity by Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData DeleteData(int entityId)
        {
            int rowsEffect = _baseRepository.DeleteById(entityId);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(rowsEffect > 0 ? Core.Enum.CustomizeStatusCode.Deleted : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = rowsEffect,
            };

            return res;
        }

        /// <summary>
        /// @author: VQPhong (08/08/2022)
        /// @desc: Control response data from Repo for Checking duplicated code
        /// </summary>
        /// <param name="code">The code needs to be checked</param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData CheckDuplicatedCodeData(string code)
        {
            bool isDuplicated = _baseRepository.CheckDuplicatedCode(code);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(Core.Enum.CustomizeStatusCode.GetOkay),
                responseData = isDuplicated,
            };

            return res;
        }

        #endregion

        #region Support Methods

        // General Validation
        /// <summary>
        /// @author: Vũ Quang Phong (14/07/2022)
        /// @desc: Check if NotEmpty Props
        /// </summary>
        /// <param name="entity"></param>
        protected virtual string? EmptyValidation<T>(T entity)
        {
            // Getting properties marked not allowed Empty
            var notEmptyProps = entity?.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(NotEmpty))
            );

            if (notEmptyProps != null)
            {
                foreach (var prop in notEmptyProps)
                {
                    // Getting the value of the Property
                    var propValue = prop.GetValue(entity);

                    // Check if propValue null or not null
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                    {
                        // Getting PropsName of the Property
                        var propsName = prop.GetCustomAttributes(typeof(PropsName), true);
                        var propNameDisplay = string.Empty;
                        if (propsName.Length > 0)
                        {
                            propNameDisplay = ((PropsName)propsName[0]).Name;
                        }

                        return String.Format(Core.Resourses.VI_Resource.PropNotEmpty, propNameDisplay);
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
