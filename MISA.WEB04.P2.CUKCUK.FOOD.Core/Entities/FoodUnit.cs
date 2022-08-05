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
        // Primary Key
        [PrimaryKey]
        public int? FoodUnitID { get; set; }

        // The name of food unit
        [NotEmpty, NotDuplicated, PropsName("Tên đơn vị tính")]
        public string? FoodUnitName { get; set; }

        // The description of food unit
        public string? Description { get; set; }

        // The Date of record creation
        public DateTime? CreatedDate { get; set; }
    }
}
