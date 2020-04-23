using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.MemberAnalysis.Expenditure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.Expenditure
{
    public interface IExpenditureAppService 
        : IApplicationService
    {
        Task<ExpenditureOverviewDto> ExpenditureOverview(ExpenditureOverviewResultRequestDto input);
        Task<IEnumerable<ExpenditureDto>> ExpenditureTrend(ExpenditureTrendResultRequestDto input);
    }
}
