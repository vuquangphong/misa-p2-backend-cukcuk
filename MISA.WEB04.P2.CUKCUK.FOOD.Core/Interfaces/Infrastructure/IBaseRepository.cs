using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Getting all the Entities <T> from Database
        /// </summary>
        /// <returns>
        /// An array of Entities <T>
        /// </returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Getting an Entity <T> from Database by Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// An Entity
        /// </returns>
        public T GetById(string entityId);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Get a list of Entities by PageIndex and PageSize, and/or searchText
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="codeFilter"></param>
        /// <param name="nameFilter"></param>
        /// <param name="groupFilter"></param>
        /// <param name="unitFilter"></param>
        /// <param name="priceFilter"></param>
        /// <returns>
        /// An object
        /// </returns>
        public object GetPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, string? priceFilter);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Inserting a new record into Entity Database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// A number of rows which is affected
        /// </returns>
        public int Insert(T entity);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Updating an Entity by Id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// A number of rows which is affected
        /// </returns>
        public int UpdateById(T entity, Guid entityId);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Check if the current EntityCode is duplicate
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns>
        /// True <--> EntityCode Coincidence
        /// False <--> No EntityCode Coincidence
        /// </returns>
        public bool IsDuplicateCode(string entityCode, string entityId, bool isPut);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Removing an Entity from Database
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// A number of rows which is affected
        /// </returns>
        public int DeleteById(string entityId);
    }
}
