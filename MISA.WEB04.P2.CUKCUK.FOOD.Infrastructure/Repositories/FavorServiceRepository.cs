using Microsoft.Extensions.Configuration;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Infrastructure.Repositories
{
    public class FavorServiceRepository : BaseRepository<FavorService>, IFavorServiceRepository
    {
        public FavorServiceRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
