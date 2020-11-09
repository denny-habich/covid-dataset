using System.Collections.Generic;
using Covid.Business.Dto;

namespace Covid.Api.Services
{
    public interface IQuestionService
    {
        IEnumerable<QuestionResponse> GetTop(int limit);
    }
}