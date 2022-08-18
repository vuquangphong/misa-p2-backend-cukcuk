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
        /// <summary>
        /// Primary Key
        /// </summary>
        [PrimaryKey]
        public int? FoodID { get; set; }

        /// <summary>
        /// Code of food
        /// </summary>
        [NotEmpty, NotDuplicated, PropsName("Mã món")]
        public string? FoodCode { get; set; }

        /// <summary>
        /// Name of food
        /// </summary>
        [NotEmpty, PropsName("Tên món")]
        public string? FoodName { get; set; }

        /// <summary>
        /// The foreign key
        /// The group ID of food
        /// </summary>
        public int? FoodGroupID { get; set; }

        /// <summary>
        /// The group name of food
        /// </summary>
        public string? FoodGroupName { get; set; }

        /// <summary>
        /// The foreign key
        /// The unit ID of food
        /// </summary>
        [NotEmpty, PropsName("Đơn vị tính")]
        public int? FoodUnitID { get; set; }

        /// <summary>
        /// The unit name of food
        /// </summary>
        public string? FoodUnitName { get; set; }

        /// <summary>
        /// The price of food
        /// </summary>
        [NotEmpty, PropsName("Giá bán")]
        public double? FoodPrice { get; set; }

        /// <summary>
        /// The init cost of food
        /// </summary>
        public double? FoodInvest { get; set; }

        /// <summary>
        /// The description of food
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The foreign key
        /// The ID of place where the food is prepared
        /// </summary>
        public int? FoodPlaceID { get; set; }

        /// <summary>
        /// The name of place where the food is prepared
        /// </summary>
        public string? FoodPlaceName { get; set; }

        /// <summary>
        /// Food shows up on the menu or not
        /// Null/0 --> yes
        /// Otherwise --> no
        /// </summary>
        public AppearOnMenu? Appear { get; set; }

        /// <summary>
        /// The link mapping to the illustration of food
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Date of record creation
        /// </summary>
        [NotUsageParams]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Date of record modification
        /// </summary>
        [NotUsageParams]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// List of FavorService of the Food
        /// </summary>
        [NotUsageParams]
        public List<FavorService>? FavorServices { get; set; }
    }
}
