using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(SupplierContect))]
    public class SupplierContectDto : FullAuditedEntityDto<long>
    {
        public long SupplierAuto { get; set; }
        public string Title { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string MTel { get; set; }
        public string Fax { get; set; }
        public long? Province { get; set; }
        public long? City { get; set; }
        public long? Area { get; set; }
        public CreditProvince CreditProvince { get; set; }
        public CreditCity CreditCity { get; set; }
        public CreditArea CreditArea { get; set; }
        public string Address { get; set; }
    }
}
