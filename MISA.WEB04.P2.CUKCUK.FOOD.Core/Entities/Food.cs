using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    /// <summary>
    /// The entity illustrate the Food in the menu
    /// </summary>
    public class Food
    {
        // Primary Key
        [PrimaryKey]
        public int? FoodID { get; set; }

        // Code of food
        [NotEmpty, NotDuplicated, PropsName("Mã món")]
        public string? FoodCode { get; set; }

        // Name of food
        [NotEmpty, PropsName("Tên món")]
        public string? FoodName { get; set; }

        // The foreign key
        // The group ID of food
        public int? FoodGroupID { get; set; }

        // The group name of food
        public string? FoodGroupName { get; set; }

        // The foreign key
        // The unit ID of food
        [NotEmpty, PropsName("Đơn vị tính")]
        public int? FoodUnitID { get; set; }

        // The unit name of food
        public string? FoodUnitName { get; set; }

        // The price of food
        [NotEmpty, PropsName("Giá bán")]
        public double? FoodPrice { get; set; }

        // The init cost of food
        public double? FoodInvest { get; set; }

        // The description of food
        public string? Description { get; set; }

        // The foreign key
        // The ID of place where the food is prepared
        public int? FoodPlaceID { get; set; }

        // The name of place where the food is prepared
        public string? FoodPlaceName { get; set; }

        // Food shows up on the menu or not
        // Null/0 --> yes
        // Otherwise --> no
        public AppearOnMenu? Appear { get; set; }

        // The link mapping to the illustration of food
        public string? Avatar { get; set; }

        // Date of record creation
        [NotUsageParams]
        public DateTime? CreatedDate { get; set; }

        // Date of record modification
        [NotUsageParams]
        public DateTime? ModifiedDate { get; set; }

        // List of FavorService of the Food
        [NotUsageParams]
        public IEnumerable<FavorService>? FavorServices { get; set; }

        // List of FavorServiceIDs which need to be removed
        [NotUsageParams]
        public IEnumerable<int>? DelFavorServiceIds { get; set; }
    }
}
