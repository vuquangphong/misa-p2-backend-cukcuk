using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    /// <summary>
    /// The entity illustrate the place in which the food is prepared
    /// </summary>
    public class FoodPlace
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [PrimaryKey]
        public int? FoodPlaceID { get; set; }

        /// <summary>
        /// The name of place where food is prepared
        /// </summary>
        [NotEmpty, NotDuplicated, PropsName("Tên nơi chế biến")]
        public string? FoodPlaceName { get; set; }

        /// <summary>
        /// The description of place where food is prepared
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The Date of record creation
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
