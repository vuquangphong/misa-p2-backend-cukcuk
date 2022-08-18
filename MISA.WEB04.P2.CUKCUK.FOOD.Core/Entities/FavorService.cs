using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    /// <summary>
    /// The entity illustrate the Favorite Services of foods
    /// </summary>
    public class FavorService
    {
        /// <summary>
        /// Primary Key
        /// PropsName which tagged with Primary Key is the name of entity
        /// </summary>
        [PrimaryKey, PropsName("Sở thích phục vụ")]
        public int? FavorServiceID { get; set; }

        /// <summary>
        /// The content of serving preferences
        /// </summary>
        [NotEmpty, PropsName("Nội dung sở thích"), NotDuplicatedCombo]
        public string? Content { get; set; }

        /// <summary>
        /// The extra charge for the services
        /// </summary>
        [NotEmpty, PropsName("Phí thu thêm"), NotDuplicatedCombo]
        public double? Surcharge { get; set; }

        /// <summary>
        /// The Date of record creation
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
