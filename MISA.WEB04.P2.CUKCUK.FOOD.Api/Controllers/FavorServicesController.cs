﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Entities;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Infrastructure;
using MISA.WEB04.P2.CUKCUK.FOOD.Core.Interfaces.Services;

namespace MISA.WEB04.P2.CUKCUK.FOOD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FavorServicesController : MISABaseController<FavorService>
    {
        #region Dependency Injection

        private readonly IFavorServiceRepository _favorServiceRepository;
        private readonly IFavorServiceServices _favorServiceServices;

        public FavorServicesController(IFavorServiceRepository favorServiceRepository, IFavorServiceServices favorServiceServices) : base(favorServiceRepository, favorServiceServices)
        {
            _favorServiceRepository = favorServiceRepository;
            _favorServiceServices = favorServiceServices;
        }

        #endregion
    }
}