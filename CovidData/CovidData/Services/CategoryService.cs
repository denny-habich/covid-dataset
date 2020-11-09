using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Covid.Business.Dto;
using Covid.Data.Entities;
using Covid.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Covid.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ILogger<CategoryService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryResponse> GetTop(int limit)
        {
            var categories = _unitOfWork.GetCollection<QuestionaireStatistics>()
                .Get()
                .Where(x=>x.Type == (int)StatisticType.Category)
                .OrderByDescending(x => x.Count)
                .Take(limit);

            return categories.Select(question => _mapper.Map<CategoryResponse>(question));
        }
    }
}