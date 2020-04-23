﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Insure.Enum;
using System;

namespace CarPlusGo.CVAS.Insure.Dto
{
    [AutoMap(typeof(InsuranceDetail))]
    public class ImportInsuranceDetailDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        public long CarBaseId { get; set; }
        /// <summary>
        /// 车架号
        /// </summary>
        public string CarNo { get; set; }
        /// <summary>
        /// 险种Id
        /// </summary>
        public long InsuranceTypeId { get; set; }
        /// <summary>
        /// 险种名称
        /// </summary>
        public string InsuranceTypeName { get; set; }
        /// <summary>
        /// 保险预设Id
        /// </summary>
        public long InsurancePresetId { get; set; }
        /// <summary>
        /// 保险预设名称
        /// </summary>
        public string InsurancePresetName { get; set; }
        /// <summary>
        /// 保险合同类型
        /// </summary>
        public InsuranceContractType InsuranceContractType { get; set; }
        /// <summary>
        /// 保单类别
        /// </summary>
        public InsurancePolicyType InsurancePolicyType { get; set; }
        /// <summary>
        /// 保险操作类别
        /// </summary>
        public InsuranceOperationType InsuranceOperationType { get; set; }
        /// <summary>
        /// 厂商Id
        /// </summary>
        public long SupplierId { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 保险单号
        /// </summary>
        public string InsuranceNum { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public decimal InsuredAmount { get; set; }
        /// <summary>
        /// 原价保费
        /// </summary>
        public decimal OriginalAmount { get; set; }
        /// <summary>
        /// 签单保费
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// 不计免赔原价保费
        /// </summary>
        public decimal NoDeductibleOriginalAmount { get; set; }
        /// <summary>
        /// 不计免赔签单保费
        /// </summary>
        public decimal NoDeductibleTransactionAmount { get; set; }
        /// <summary>
        /// 保险开始日期
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 保险结束日期
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 批单号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 保单Id
        /// </summary>
        public long InsurancePolicyId { get; set; }
    }
}
