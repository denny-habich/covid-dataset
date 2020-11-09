using System.Collections.Generic;
using Covid.Business.Dto;

namespace Covid.Api.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryResponse> GetTop(int limit);
    }
}
