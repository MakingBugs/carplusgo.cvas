using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace CarPlusGo.CVAS.Authorization
{
    public class CVASAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //中台系统
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Finance_AccountingTitle, L("AccountingTitle"));
            context.CreatePermission(PermissionNames.Pages_Finance_AccountingEntryConfig, L("AccountingEntryConfig"));
            var mobileOrder = context.CreatePermission(PermissionNames.Pages_MobileOrder, L("MobileOrder"));
            mobileOrder.CreateChildPermission(PermissionNames.Pages_MobileOrder_Find, L("Find"));
            mobileOrder.CreateChildPermission(PermissionNames.Pages_MobileOrder_Export, L("Export"));
            context.CreatePermission(PermissionNames.Pages_OperationTarget, L("ModifyOperationTarget"));
            context.CreatePermission(PermissionNames.Pages_TargetConfig, L("TargetConfig"));
            context.CreatePermission(PermissionNames.Pages_Supplier, L("Supplier"));
            context.CreatePermission(PermissionNames.Pages_Carbase, L("Carbase"));
            context.CreatePermission(PermissionNames.Pages_InsurancePolicy, L("InsurancePolicy"));
            context.CreatePermission(PermissionNames.Pages_InsuranceType, L("InsuranceType"));
            context.CreatePermission(PermissionNames.Pages_InsurancePreset, L("InsurancePreset"));
            context.CreatePermission(PermissionNames.Pages_VehicleInsure, L("VehicleInsure"));
            context.CreatePermission(PermissionNames.Pages_CarRepair, L("CarRepair"));
            context.CreatePermission(PermissionNames.Pages_CarFix, L("CarFix"));
            context.CreatePermission(PermissionNames.Pages_CarFixSend, L("CarFixSend"));
            context.CreatePermission(PermissionNames.Pages_Cxlp, L("Cxlp"));
            context.CreatePermission(PermissionNames.Pages_CxlpQuery, L("CxlpQuery"));
            context.CreatePermission(PermissionNames.Pages_CxlpQ, L("CxlpQ"));
            context.CreatePermission(PermissionNames.Pages_BankDetail, L("BankDetail"));
            context.CreatePermission(PermissionNames.Pages_VehicleParts, L("VehicleParts"));
            context.CreatePermission(PermissionNames.Pages_VehicleAccessories, L("VehicleAccessories"));
            context.CreatePermission(PermissionNames.Pages_WarehouseArea, L("WarehouseArea"));
            context.CreatePermission(PermissionNames.Pages_Warehouse, L("Warehouse"));
            context.CreatePermission(PermissionNames.Pages_UseCarApply, L("UseCarApply"));
            context.CreatePermission(PermissionNames.Pages_VehicleCertificate, L("VehicleCertificate"));
            context.CreatePermission(PermissionNames.Pages_TakeCarApply, L("TakeCarApply"));
            context.CreatePermission(PermissionNames.Pages_VehicleLoading, L("VehicleLoading"));
            context.CreatePermission(PermissionNames.Pages_VehicleUnloading, L("VehicleUnloading"));

            //分析报表
            context.CreatePermission(PermissionNames.Mobile_Home, L("MobileHome"));
            context.CreatePermission(PermissionNames.Mobile_PerformanceOverview, L("PerformanceOverview"));
            context.CreatePermission(PermissionNames.Mobile_PerformanceTrend, L("PerformanceTrend"));
            context.CreatePermission(PermissionNames.Mobile_OrderOverview, L("OrderOverview"));
            context.CreatePermission(PermissionNames.Mobile_OrderTrend, L("OrderTrend"));
            context.CreatePermission(PermissionNames.Mobile_DaypartingOrderTrend, L("DaypartingOrderTrend"));
            context.CreatePermission(PermissionNames.Mobile_CancelOrderTrend, L("CancelOrderTrend"));
            context.CreatePermission(PermissionNames.Mobile_DaypartingCancelOrderTrend, L("DaypartingCancelOrderTrend"));
            context.CreatePermission(PermissionNames.Mobile_CancelOrderReason, L("CancelOrderReason"));
            context.CreatePermission(PermissionNames.Mobile_OrderInfo, L("OrderInfo"));
            context.CreatePermission(PermissionNames.Mobile_OrderInfoTrend, L("OrderInfoTrend"));
            context.CreatePermission(PermissionNames.Mobile_OrderReceivingTime, L("OrderReceivingTime"));
            context.CreatePermission(PermissionNames.Mobile_Immediateorder, L("Immediateorder"));
            context.CreatePermission(PermissionNames.Mobile_PassengerWaiting, L("PassengerWaiting"));
            context.CreatePermission(PermissionNames.Mobile_DriverWait, L("DriverWait"));
            context.CreatePermission(PermissionNames.Mobile_Attendance, L("Attendance"));
            context.CreatePermission(PermissionNames.Mobile_AttendanceDetails, L("AttendanceDetails"));
            context.CreatePermission(PermissionNames.Mobile_OrderMap, L("OrderMap"));
            context.CreatePermission(PermissionNames.Mobile_UserOverview, L("UserOverview"));
            context.CreatePermission(PermissionNames.Mobile_UserGrowthTrend, L("UserGrowthTrend"));
            context.CreatePermission(PermissionNames.Mobile_RechargeOverview, L("RechargeOverview"));
            context.CreatePermission(PermissionNames.Mobile_RechargeTrend, L("RechargeTrend"));
            context.CreatePermission(PermissionNames.Mobile_ExpenditureOverview, L("ExpenditureOverview"));
            context.CreatePermission(PermissionNames.Mobile_ExpenditureTrend, L("ExpenditureTrend"));
            context.CreatePermission(PermissionNames.Mobile_OperationTarget, L("OperationTarget"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CVASConsts.LocalizationSourceName);
        }
    }
}
