using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Car
{
    public class CarCertificateRightAppService:AsyncCrudAppService<CarCertificateRight,CarCertificateRightDto,long,CarCertificateRightResultRequestDto,CarCertificateRightDto,CarCertificateRightDto>,ICarCertificateRightAppService
    {
        public CarCertificateRightAppService(IRepository<CarCertificateRight,long> repository) : base(repository)
        {
        }
        protected override IQueryable<CarCertificateRight> CreateFilteredQuery(CarCertificateRightResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.CarCertificate, x => x.ItemCode)
            .Where(x => x.IsDeleted == false)
            .WhereIf(input.CarCertificateID.HasValue, x => x.CarCertificateID == input.CarCertificateID)
            .WhereIf(input.Selected.HasValue, x => x.Selected == input.Selected);
        }
    }
}
