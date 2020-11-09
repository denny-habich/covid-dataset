using System.Collections.Generic;
using Covid.Api.Services;
using Covid.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        /// <summary>
        /// Informatie over een opgegeven gebruiker 
        /// </summary>
        [HttpGet()]
        public ActionResult<IEnumerable<QuestionResponse>> GetTop()
        {
            var result = _categoryService.GetTop(10);

            return Ok(result);
        }

        /// <summary>
        /// Informatie over een opgegeven gebruiker 
        /// </summary>
        [HttpGet("{limit}")]
        public ActionResult<IEnumerable<QuestionResponse>> GetTop(int limit)
        {
            var result = _categoryService.GetTop(limit);

            return Ok(result);
        }
    }
}
