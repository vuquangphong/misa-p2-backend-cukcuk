using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    public class Food
    {
        // Primary Key
        [PrimaryKey]
        public int? FoodID { get; set; }

        // Code of food
        [NotEmpty, NotDuplicated]
        public string? FoodCode { get; set; }

        // Name of food
        [NotEmpty]
        public string? FoodName { get; set; }

        // The foreign key
        // The group ID of food
        public int? FoodGroupID { get; set; }

        // The foreign key
        // The unit ID of food
        [NotEmpty]
        public int? FoodUnitID { get; set; }

        // The price of food
        [NotEmpty]
        public double? FoodPrice { get; set; }

        // The init cost of food
        public double? FoodInvest { get; set; }

        // The description of food
        public string? Description { get; set; }

        // The foreign key
        // The ID of place where the food is prepared
        public int? FoodPlaceID { get; set; }

        // Food shows up on the menu or not
        // Null/0 --> yes
        // Otherwise --> no
        public AppearOnMenu? Appear { get; set; }

        // The link mapping to the illustration of food
        public string? Avatar { get; set; }

        // Date of record creation
        public DateTime? CreatedDate { get; set; }

        // Date of record modification
        public DateTime? ModifiedDate { get; set; }
    }
}
