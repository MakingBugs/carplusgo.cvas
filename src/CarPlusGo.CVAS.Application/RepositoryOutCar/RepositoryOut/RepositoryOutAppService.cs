using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.Extensions;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public class RepositoryOutAppService:AsyncCrudAppService<RepositoryOut, RepositoryOutDto,long, RepositoryOutResultRequestDto, CreateRepositoryOutDto, CreateRepositoryOutDto>,IRepositoryOutAppService
    {
        private readonly IRepository<RepositoryOutCarPart, long> _RepositoryOutCarPart;
        private readonly IRepository<RepositoryOutAccessory, long> _RepositoryOutAccessory;
        private readonly IRepository<RepositoryOutCertificate, long> _RepositoryOutCertificate;
        private readonly IRepository<RepositoryOutFile, long> _RepositoryOutFile;
        public RepositoryOutAppService(IRepository<RepositoryOut,long> repository, IRepository<RepositoryOutCarPart, long> RepositoryOutCarPart, IRepository<RepositoryOutAccessory, long> RepositoryOutAccessory, IRepository<RepositoryOutCertificate, long> RepositoryOutCertificate, IRepository<RepositoryOutFile, long> RepositoryOutFile) : base(repository)
        {
            _RepositoryOutCarPart = RepositoryOutCarPart;
            _RepositoryOutAccessory = RepositoryOutAccessory;
            _RepositoryOutCertificate = RepositoryOutCertificate;
            _RepositoryOutFile = RepositoryOutFile;
        }
        protected override IQueryable<RepositoryOut> CreateFilteredQuery(RepositoryOutResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.CarBase, x => x.CarBase.Clasen, x => x.TakeCarApply, x => x.TakeCarApply.ClasenData, x => x.TakeCar, x => x.OutRepositoryData, x => x.OutRepositoryData.Location, x => x.InRepositoryData, x => x.InRepositoryData.Location, x => x.ItemCodeStatus, x => x.ItemCodeOutReason, x => x.ItemCodeInReason)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.MakNo != null && input.MakNo != "", x => x.MakNo.Contains(input.MakNo))
                .WhereIf(input.OutRepositoryID.HasValue, x => x.OutRepositoryID == input.OutRepositoryID)
                .WhereIf(input.InRepositoryID.HasValue, x => x.InRepositoryID == input.InRepositoryID)
                .WhereIf(input.OutDateForm != null && input.OutDateTo != null, x => input.OutDateForm.Value.ToLocalTime().Date <= x.OutDate && x.OutDate <= input.OutDateTo.Value.ToLocalTime().ToDayEnd())
                .WhereIf(input.InDateForm != null && input.InDateTo != null, x => input.InDateForm.Value.ToLocalTime().Date <= x.InDate && x.InDate <= input.InDateTo.Value.ToLocalTime().ToDayEnd())
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status);
        }
        public override async Task<RepositoryOutDto> Create(CreateRepositoryOutDto input)
        {
            CheckCreatePermission();
            var entity = MapToEntity(input);
            entity.Id = Repository.InsertAndGetId(entity);
            //出入库车辆部位记录
            foreach (var item in input.RepositoryOutCarPartList)
            {
                var outCarPart = ObjectMapper.Map<RepositoryOutCarPart>(item);
                outCarPart.RepositoryOutID = entity.Id;
                await _RepositoryOutCarPart.InsertAsync(outCarPart);
            }
            //出入库车辆配件记录
            foreach (var item in input.RepositoryOutAccessorieList)
            {
                var outAccessory = ObjectMapper.Map<RepositoryOutAccessory>(item);
                outAccessory.RepositoryOutID = entity.Id;
                await _RepositoryOutAccessory.InsertAsync(outAccessory);
            }
            //出入库车辆证件记录
            foreach (var item in input.RepositoryOutCertificateList)
            {
                var outCertificate = ObjectMapper.Map<RepositoryOutCertificate>(item);
                outCertificate.RepositoryOutID = entity.Id;
                await _RepositoryOutCertificate.InsertAsync(outCertificate);
            }
            //出入库附件记录
            foreach (var item in input.RepositoryOutFileList)
            {
                var outFile = ObjectMapper.Map<RepositoryOutFile>(item);
                outFile.RepositoryOutID = entity.Id;
                await _RepositoryOutFile.InsertAsync(outFile);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(entity);
        }
    }
}
