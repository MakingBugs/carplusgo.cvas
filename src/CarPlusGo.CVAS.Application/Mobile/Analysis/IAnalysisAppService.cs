using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.Analysis.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Mobile.Analysis
{
    public interface IAnalysisAppService
        : IApplicationService
    {
        Task<HomeDataDto> HomeData(HomeDataResultRequestDto input);
        Task<PerformanceTrendDto> PerformanceTrend(PerformanceTrendResultRequestDto input);
        Task<PerformanceOverviewDto> PerformanceOverview(PerformanceOverviewResultRequestDto input);
        Task<OrderOverviewDto> OrderOverview(OrderOverviewResultRequestDto input);
        Task<OrderTrendDto> OrderTrend(OrderTrendResultRequestDto input);
        Task<IEnumerable<DaypartingOrderTrendDto>> DaypartingOrderTrend(DaypartingOrderTrendResultRequestDto input);
        Task<OrderTrendDto> CancelOrderTrend(CancelOrderTrendResultRequestDto input);
        Task<IEnumerable<DaypartingOrderTrendDto>> DaypartingCancelOrderTrend(DaypartingCancelOrderTrendResultRequestDto input);
        Task<CancelOrderReasonDto> CancelOrderReason(CancelOrderReasonResultRequestDto input);
        Task<OrderInfoDto> OrderInfo(OrderInfoResultRequestDto input);
        Task<OrderInfoTrendDto> OrderInfoTrend(OrderInfoTrendResultRequestDto input);
    }
}
