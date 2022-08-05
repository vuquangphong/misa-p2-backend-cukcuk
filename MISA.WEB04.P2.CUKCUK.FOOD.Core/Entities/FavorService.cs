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
        // Primary Key
        // PropsName which tagged with Primary Key is the name of entity
        [PrimaryKey, PropsName("Sở thích phục vụ")]
        public int? FavorServiceID { get; set; }

        // The content of serving preferences
        [NotEmpty, PropsName("Nội dung sở thích"), NotDuplicatedCombo]
        public string? Content { get; set; }

        // The extra charge for the services
        [NotEmpty, PropsName("Phí thu thêm"), NotDuplicatedCombo]
        public double? Surcharge { get; set; }

        // The Date of record creation
        public DateTime? CreatedDate { get; set; }
    }
}
