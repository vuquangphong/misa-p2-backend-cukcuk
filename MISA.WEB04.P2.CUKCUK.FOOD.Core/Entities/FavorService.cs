﻿using MISA.WEB04.P2.CUKCUK.FOOD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities
{
    public class FavorService
    {
        // Primary Key
        [PrimaryKey]
        public int? FavorServiceID { get; set; }

        // Foreign key mapping to Food
        [NotEmpty]
        public int? FoodID { get; set; }

        // The content of serving preferences
        [NotEmpty]
        public string? Content { get; set; }

        // The extra charge for the services
        [NotEmpty]
        public double? Surcharge { get; set; }

        // The Date of record creation
        public DateTime? CreatedDate { get; set; }
    }
}