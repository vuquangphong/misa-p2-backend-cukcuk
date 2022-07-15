using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Core.Services
{
    public class FoodServices : BaseServices<Food>, IFoodServices
    {
        private readonly IFoodRepository _foodRepository;

        public FoodServices(IFoodRepository foodRepository) : base(foodRepository)
        {
            _foodRepository = foodRepository;
        }
    }
}
