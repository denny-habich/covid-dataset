using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Business.Dto;
using Covid.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Covid.Data.Entities;

namespace Covid.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IImportStatisticsService _statusService;

        public StatisticsController(IImportStatisticsService statusService)
        {
            _statusService = statusService;
        }
        
        [HttpGet("{limit}")]
        public ActionResult<ImportStatisticsResponse> Get(int limit)
        {
            var result = _statusService.GetStatistics(limit);

            return Ok(result);
        }
    }
}
