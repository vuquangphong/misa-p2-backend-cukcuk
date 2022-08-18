using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    /// <summary>
    /// The intermediate entity between Food and FavorService
    /// </summary>
    public class FoodFavorService
    {
        /// <summary>
        /// The primary key mapping to Food entity
        /// </summary>
        [PrimaryKey, NotEmpty, PropsName("FoodID")]
        public int? FoodID { get; set; }

        /// <summary>
        /// The primary key mapping to FavorService entity
        /// </summary>
        [PrimaryKey, NotEmpty, PropsName("FavorServiceID")]
        public int? FavorServiceID { get; set; }

        /// <summary>
        /// The Date of record creation
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
