using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Opendata.Web.Client.Model;
using Opendata.Web.Client.Repository;

namespace Opendata.Web.Client.Controllers
{
    [ApiController]
    [Route("api/regions")]
    public class RegionsController : ControllerBase
    {
        private readonly ILogger<RegionsController> _logger;
        private readonly IDataRepository _dataRepository;

        public RegionsController(ILogger<RegionsController> logger, IDataRepository dataRepository)
        {
            _logger = logger;
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IEnumerable<Region> Get()
        {
            return _dataRepository.GetRegions();
        }
    }
}