using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    /// <summary>
    /// The entity illustrate the unit of foods
    /// </summary>
    public class FoodUnit
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [PrimaryKey]
        public int? FoodUnitID { get; set; }

        /// <summary>
        /// The name of food unit
        /// </summary>
        [NotEmpty, NotDuplicated, PropsName("Tên đơn vị tính")]
        public string? FoodUnitName { get; set; }

        /// <summary>
        /// The description of food unit
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The Date of record creation
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
