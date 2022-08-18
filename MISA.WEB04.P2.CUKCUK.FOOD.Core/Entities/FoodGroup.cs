using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    /// <summary>
    /// The entity illustrate the Group of Food
    /// </summary>
    public class FoodGroup
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [PrimaryKey]
        public int? FoodGroupID { get; set; }

        /// <summary>
        /// Code of group food
        /// </summary>
        [NotEmpty, NotDuplicated, PropsName("Mã nhóm thức ăn")]
        public string? FoodGroupCode { get; set; }

        /// <summary>
        /// Name of the group food
        /// </summary>
        [NotEmpty, PropsName("Tên nhóm thức ăn")]
        public string? FoodGroupName { get; set; }

        /// <summary>
        /// The description of group food
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The Date of record creation
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
