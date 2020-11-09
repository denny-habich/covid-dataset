using System.Collections.Generic;
using Covid.Business.Dto;

namespace Covid.Business.Services
{
    public interface IImportStatisticsService
    {
        public IEnumerable<ImportStatisticsResponse> GetStatistics(int limit);
    }


}