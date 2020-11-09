using System.Threading.Tasks;
using Covid.Business.Dto;

namespace Covid.Business.Services
{
    public interface ICovidImportService
    {
        Task<ImportResponse> Import();
    }


}
