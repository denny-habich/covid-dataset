using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Covid.Business.Dto;
using Covid.Data.Entities;
using Covid.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Covid.Api.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QuestionService> _logger;
        private readonly IMapper _mapper;

        public QuestionService(ILogger<QuestionService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<QuestionResponse> GetTop(int limit)
        {
            var questions = _unitOfWork.GetCollection<QuestionaireStatistics>()
                .Get()
                .Where(x=>x.Type == (int)StatisticType.Question)
                .OrderByDescending(x => x.Count)
                .Take(limit);

            return questions.Select(question => _mapper.Map<QuestionResponse>(question));
        }
    }
}
