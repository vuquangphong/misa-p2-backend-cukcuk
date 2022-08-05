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
        // Primary Key
        [PrimaryKey]
        public int? FoodPlaceID { get; set; }

        // The name of place where food is prepared
        [NotEmpty, NotDuplicated, PropsName("Tên nơi chế biến")]
        public string? FoodPlaceName { get; set; }

        // The description of place where food is prepared
        public string? Description { get; set; }

        // The Date of record creation
        public DateTime? CreatedDate { get; set; }
    }
}
