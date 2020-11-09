using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Covid.Business.Configuration;
using Covid.Business.Dto;
using Covid.Data.Entities;
using Covid.Data.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Covid.Business.Services
{
    public class ImportStatisticsService : IImportStatisticsService
    {
        private readonly ILogger<ImportStatisticsService> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ImportStatisticsService(ILogger<ImportStatisticsService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ImportStatisticsResponse> GetStatistics(int limit)
        {
            var statistics = unitOfWork.GetCollection<ImportStatistics>()
                .Get()
                .OrderByDescending(x => x.StartDateTime)
                .Take(limit);

            return statistics.Select(question => mapper.Map<ImportStatisticsResponse>(question));
        }
    }
}
