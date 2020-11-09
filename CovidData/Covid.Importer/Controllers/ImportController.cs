using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Business.Dto;
using Covid.Business.Services;
using Covid.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Importer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ICovidImportService _covidImportService;
        private readonly IImportStatisticsService _serviceManagementStatusService;

        public ImportController(IImportStatisticsService statisticsService, ICovidImportService covidImportService)
        {
            _serviceManagementStatusService = statisticsService;
            _covidImportService = covidImportService;
        }

        /// <summary>
        /// Volledige import; vernieuwt alle reeds geimporteerde data
        /// </summary>
        [HttpPost("reimport")]
        public async Task<ActionResult<ImportStatisticsResponse>> ReimportAsync()
        {
            var result = await _covidImportService.Import();

            return Ok(result);
        }
    }
}
