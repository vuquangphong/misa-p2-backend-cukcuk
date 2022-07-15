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

        public ControllerResponseData GetDataById(string entityId)
        {
            var data = _baseRepository.GetById(entityId);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(data == null ? Core.Enum.CustomizeStatusCode.NoContent : Core.Enum.CustomizeStatusCode.GetOkay),
                responseData = data
            };

            return res;
        }

        public ControllerResponseData GetDataPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, string? priceFilter)
        {
            var data = _baseRepository.GetPaging(pageIndex, pageSize, codeFilter, nameFilter, groupFilter, unitFilter, priceFilter);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(Core.Enum.CustomizeStatusCode.GetOkay),
                responseData = data
            };

            return res;
        }

        public ControllerResponseData InsertData(T entity)
        {
            // Validate data from request
            this.EmptyValidation(entity);
            this.DuplicatedValidation(entity, Guid.NewGuid(), false);

            // Everything is Okay
            int rowsEffect = _baseRepository.Insert(entity);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(rowsEffect > 0 ? Core.Enum.CustomizeStatusCode.Created : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = rowsEffect,
            };

            return res;
        }

        public ControllerResponseData UpdateData(T entity, Guid entityId)
        {
            // Validate data from request
            this.EmptyValidation(entity);
            this.DuplicatedValidation(entity, entityId, true);

            // Everything is Okay
            int rowsEffect = _baseRepository.UpdateById(entity, entityId);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(rowsEffect > 0 ? Core.Enum.CustomizeStatusCode.Updated : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = rowsEffect,
            };

            return res;
        }

        public ControllerResponseData DeleteData(string entityId)
        {
            int rowsEffect = _baseRepository.DeleteById(entityId);

            var res = new ControllerResponseData
            {
                customStatusCode = (int?)(rowsEffect > 0 ? Core.Enum.CustomizeStatusCode.Deleted : Core.Enum.CustomizeStatusCode.NoContent),
                responseData = rowsEffect,
            };

            return res;
        }

        #endregion

        #region Support Methods

        // General Validation
        // 1. Check if NotEmpty Props
        /// <summary>
        /// @author: Vũ Quang Phong (14/07/2022)
        /// @desc: Check if NotEmpty Props
        /// </summary>
        /// <param name="entity"></param>
        private void EmptyValidation(T entity)
        {
            // Getting properties marked not allowed Empty
            var notEmptyProps = entity.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(NotEmpty))
            );

            foreach (var prop in notEmptyProps)
            {
                // Getting the value of the Property
                var propValue = prop.GetValue(entity);

                // Getting PropsName of the Property
                var propsName = prop.GetCustomAttributes(typeof(PropsName), true);
                var propNameDisplay = string.Empty;
                if (propsName.Length > 0)
                {
                    propNameDisplay = ((PropsName)propsName[0]).Name;
                }

                if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    // Method 1: Responding messages respectively
                    //throw new MISAValidateException(String.Format(Core.Resources.ResourceVietnam.PropNotEmpty, propNameDisplay));

                    // Method 2: Responding all at once
                    //_listErrMsgs.Add(String.Format(Core.Resources.ResourceVietnam.PropNotEmpty, propNameDisplay));



                    // Method 3: Do not throw Exception
                    // TODO...
                }
            }
        }

        // 2. Check if NotDuplicated Props
        /// <summary>
        /// @author: Vũ Quang Phong (14/07/2022)
        /// @desc: Check if NotDuplicated Props
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <param name="isPut"></param>
        private void DuplicatedValidation(T entity, Guid entityId, bool isPut)
        {
            // Getting properties marked not allowed Duplicated
            var notDuplicatedProps = entity.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(NotDuplicated))
            );

            foreach (var prop in notDuplicatedProps)
            {
                // Getting the value of the Property
                var propValue = prop.GetValue(entity);

                // Getting PropsName of the Property
                var propsName = prop.GetCustomAttributes(typeof(PropsName), true);
                var propNameDisplay = string.Empty;
                if (propsName.Length > 0)
                {
                    propNameDisplay = ((PropsName)propsName[0]).Name;
                }

                if (_baseRepository.IsDuplicateCode(propValue.ToString(), entityId.ToString(), isPut))
                {
                    // Method 1: Responding messages respectively
                    //throw new MISAValidateException(String.Format(Core.Resources.ResourceVietnam.PropNotDuplicated, propNameDisplay));

                    // Method 2: Responding all at once
                    //_listErrMsgs.Add(String.Format(Core.Resources.ResourceVietnam.PropNotDuplicated, propNameDisplay));



                    // Method 3: Do not throw Exception
                    // TODO
                }
            }
        }

        #endregion
    }
}
