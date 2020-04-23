using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Mobile.TShareBank;
using System;

namespace CarPlusGo.CVAS.Mobile.Dto
{
    [AutoMap(typeof(OperationTarget))]
    public class OperationTargetDto : EntityDto<long>
    {
        public DateTime Date { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal CashPayAmount { get; set; }
        public long ActiveUserCount { get; set; }
        public long PlacedOrderUserCount { get; set; }
        public long CompletedOrderUserCount { get; set; }
        public long PlacedOrderCount { get; set; }
        public long AcceptedOrderCount { get; set; }
        public long CompletedOrderCount { get; set; }
        public decimal AvgCashPayAmount { get; set; }
        public double CompletedOrderAvgMileage { get; set; }
        public double CompletedOrderAvgDuration { get; set; }
        public long IosNewInstallUserCount { get; set; }
        public long AndroidNewInstallUserCount { get; set; }
        public long NewRegisterUserCount { get; set; }
        public long NewRechargeUserCount { get; set; }
        public long NewConsumeUserCount { get; set; }
        public long IosTotalInstallUserCount { get; set; }
        public long AndroidTotalInstallUserCount { get; set; }
        public long TotalRegisterUserCount { get; set; }
        public long TotalRechargeUserCount { get; set; }
        public long TotalConsumeUserCount { get; set; }
        public decimal RechargeAmount { get; set; }
        public long BalanceUserCount { get; set; }
        public decimal CashBalance { get; set; }
        public decimal GiftBalance { get; set; }
        public long OnDutyDriverCount { get; set; }
        public long RecruitDriverCount { get; set; }
        public long QuitDriverCount { get; set; }
        public long OnlineDriverCount { get; set; }
        public double AvgOnlineDuration { get; set; }
        public double AvgOvertimeDuration { get; set; }
        public long CarCount { get; set; }
        public long OperationCarCount { get; set; }
        public long AccidentCarCount { get; set; }
        public long MaintenanceCarCount { get; set; }
        public long OnlineCarCount { get; set; }
        public long ComplaintDriverUserCount { get; set; }
        public long ComplaintAppUserCount { get; set; }
        public long ComplaintCarUserCount { get; set; }
        public long PickupPlacedOrderCount { get; set; }
        public long PickupAcceptedOrderCount { get; set; }
        public long PickupCompletedOrderCount { get; set; }
        public decimal PickupOrderAmount { get; set; }
        public long DropoffPlacedOrderCount { get; set; }
        public long DropoffAcceptedOrderCount { get; set; }
        public long DropoffCompletedOrderCount { get; set; }
        public decimal DropoffOrderAmount { get; set; }
        public long PayCount { get; set; }
        public decimal PayAmount { get; set; }
        public long BalancePayCount { get; set; }
        public decimal BalancePayAmount { get; set; }
        public long AlipayPayCount { get; set; }
        public decimal AlipayPayAmount { get; set; }
        public long WeChatPayCount { get; set; }
        public decimal WeChatPayAmount { get; set; }
        public string MainWorkItems { get; set; }
    }
}
