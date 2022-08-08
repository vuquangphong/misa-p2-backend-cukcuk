using MISA.WEB04.P2.CUKCUK.FOOD.Core.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services
{
    public interface IBaseServices<T>
    {
        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service of GetAll
        /// </summary>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData GetAllData();

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The service of Get by Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData GetDataById(int entityId);

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
        public ControllerResponseData GetDataPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, double? priceFilter);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service for Adding a new Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData InsertData(T entity);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service for Updating an Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData UpdateData(T entity, int entityId);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: The Service of Removing an Entity by Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData DeleteData(int entityId);

        /// <summary>
        /// @author: VQPhong (08/08/2022)
        /// @desc: Control response data from Repo for Checking duplicated code
        /// </summary>
        /// <param name="code">The code needs to be checked</param>
        /// <returns>
        /// A model of ControllerResponseData
        /// </returns>
        public ControllerResponseData CheckDuplicatedCodeData(string code);
    }
}
