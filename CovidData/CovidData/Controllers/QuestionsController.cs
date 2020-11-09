using System.Collections.Generic;
using Covid.Api.Services;
using Covid.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        
        /// <summary>
        /// Informatie over een opgegeven gebruiker 
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<QuestionResponse>> GetTop()
        {
            var result = _questionService.GetTop(10);

            return Ok(result);
        }

        /// <summary>
        /// Informatie over een opgegeven gebruiker 
        /// </summary>
        [HttpGet("{limit}")]
        public ActionResult<IEnumerable<QuestionResponse>> GetTop(int limit)
        {
            var result = _questionService.GetTop(limit);

            return Ok(result);
        }
    }
}
