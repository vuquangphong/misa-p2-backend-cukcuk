using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    public class FoodGroup
    {
        // Primary Key
        [PrimaryKey]
        public int? FoodGroupID { get; set; }

        // Code of group food
        [NotEmpty, NotDuplicated]
        public string? FoodGroupCode { get; set; }

        // Name of the group food
        [NotEmpty]
        public string? FoodGroupName { get; set; }

        // The description of group food
        public string? Description { get; set; }

        // The Date of record creation
        public DateTime? CreatedDate { get; set; }
    }
}
