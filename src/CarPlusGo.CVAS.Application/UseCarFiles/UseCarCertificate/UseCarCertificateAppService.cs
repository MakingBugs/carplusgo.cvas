using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.UseCarFiles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles
{
    public class UseCarCertificateAppService
         : AsyncCrudAppService<UseCarCertificate, UseCarCertificateDto, long, UseCarCertificateResultRequestDto, UseCarCertificateDto, UseCarCertificateDto>,IUseCarCertificateAppService
    {
        public UseCarCertificateAppService(IRepository<UseCarCertificate,long> repository) : base(repository)
        {

        }
        protected override IQueryable<UseCarCertificate> CreateFilteredQuery(UseCarCertificateResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false);
        }
    }
}
