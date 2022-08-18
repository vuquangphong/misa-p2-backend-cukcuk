using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// @author: VQPhong (10/08/2022)
    /// @desc: Base interface of all models
    /// </summary>
    /// <typeparam name="T">T as a type of model</typeparam>
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
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// An Entity
        /// </returns>
        public T GetById(int entityId);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Get a list of Entities by PageIndex and PageSize, and/or searchText
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
        public object GetPaging(int? pageIndex, int? pageSize, string? codeFilter, string? nameFilter, string? groupFilter, string? unitFilter, double? priceFilter);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Inserting a new record into Entity Database
        /// </summary>
        /// <param name="entity">Entity needs to be inserted</param>
        /// <returns>
        /// A number of rows which is affected
        /// </returns>
        public int Insert(T entity);

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Updating an Entity by Id
        /// </summary>
        /// <param name="entity">Entity needs to be updated</param>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// A number of rows which is affected
        /// </returns>
        public int UpdateById(T entity, int entityId);

        /// <summary>
        /// @author: VQPhong (14/07/2022)
        /// @desc: Removing an Entity from Database
        /// </summary>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// A number of rows which is affected
        /// </returns>
        public int DeleteById(int entityId);

        /// <summary>
        /// @author: VQPhong (01/08/2022)
        /// @desc: Check if some props are duplicated or not
        /// </summary>
        /// <param name="entity">The entity needs to be checked</param>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>
        /// Duplicated --> An alert string corresponding
        /// All not duplicate --> null string
        /// </returns>
        public string? CheckDuplicatedProp(T entity, int? entityId);

        /// <summary>
        /// @author: VQPhong (03/08/2022)
        /// @desc: Check if combo props already exist
        /// </summary>
        /// <param name="entity">The entity record needs to be checked combo props</param>
        /// <returns>
        /// An alert message if exist
        /// Null if not exist
        /// </returns>
        public string? CheckDuplicatedCombo(T entity);

        /// <summary>
        /// @author: VQPhong (08/08/2022)
        /// Check if the current code is duplicated
        /// </summary>
        /// <param name="code">The code needs to be checked duplicated</param>
        /// <returns>
        /// True --> Duplicated
        /// False --> Not Duplicated
        /// </returns>
        public bool CheckDuplicatedCode(string code);
    }
}
