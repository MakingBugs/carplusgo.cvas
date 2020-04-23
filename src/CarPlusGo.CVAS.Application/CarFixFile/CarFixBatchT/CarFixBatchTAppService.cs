using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.CarFixFile.Dto;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.CarFixFile
{
    public class CarFixBatchTAppService
        : AsyncCrudAppService<CarFixBatchT, CarFixBatchTDto, long, CarFixBatchTResultRequestDto, CarFixBatchTDto, CarFixBatchTDto>, ICarFixBatchTAppService
    {
        public CarFixBatchTAppService(IRepository<CarFixBatchT, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CarFixBatchT> CreateFilteredQuery(CarFixBatchTResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false);

        }

        public long? SelectMaxCarFixBatchTNO(CarFixBatchTResultRequestDto input)
        {
            return CreateFilteredQuery(input).Max(x => x.CarFixBatchTNO);
        }
    }
}
