using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public class RepositoryOutCertificateAppService
        : AsyncCrudAppService<RepositoryOutCertificate, RepositoryOutCertificateDto,long, RepositoryOutCertificateResultRequestDto, RepositoryOutCertificateDto, RepositoryOutCertificateDto>,IRepositoryOutCertificateAppService
    {
        public RepositoryOutCertificateAppService(IRepository<RepositoryOutCertificate,long> repository) 
            : base(repository)
        {

        }
        protected override IQueryable<RepositoryOutCertificate> CreateFilteredQuery(RepositoryOutCertificateResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.RepositoryOut, x => x.CarCertificate)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.Ids.Count() > 0, x => input.Ids.Any(s => x.RepositoryOutID == s));
        }
    }
}
