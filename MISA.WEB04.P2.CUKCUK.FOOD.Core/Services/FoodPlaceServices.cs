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
    public class FoodPlaceServices : BaseServices<FoodPlace>, IFoodPlaceServices
    {
        private readonly IFoodPlaceRepository _foodPlaceRepository;

        public FoodPlaceServices(IFoodPlaceRepository foodPlaceRepository) : base(foodPlaceRepository)
        {
            _foodPlaceRepository = foodPlaceRepository;
        }
    }
}
