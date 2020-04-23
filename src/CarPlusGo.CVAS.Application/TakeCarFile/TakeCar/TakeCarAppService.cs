using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.TakeCarFile.Dto;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.UI;

namespace CarPlusGo.CVAS.TakeCarFile
{
    public class TakeCarAppService
        :AsyncCrudAppService<TakeCar, TakeCarDto,long, TakeCarResultRequestDto, CreateTakeCarDto, CreateTakeCarDto>,ITakeCarAppService
    {
        public TakeCarAppService(IRepository<TakeCar,long> repository) : base(repository)
        {

        }
        protected override IQueryable<TakeCar> CreateFilteredQuery(TakeCarResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.ItemStatusCode, x => x.TakeCarApply, x => x.CarBase, x => x.CarBase.Clasen, x => x.CarBase.Repository)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.Ids.Count() > 0, x => input.Ids.Any(s => x.CarTakeApplyID == s));
        }

        public async Task<int> CreateAll(CreateTakeCarDto[] inputList)
        {
            CheckCreatePermission();
            int insertNum = 0;
            List<long> a = new List<long> { 0, 10, 15, 20, 30, 35, 40 };

            foreach (var input in inputList)
            {
                //车辆ID相同，并且状态不是确认入库的，则不能重复提领车辆
                var data = Repository.GetAll().Where(x => x.CarBaseID == input.CarBaseID && a.Contains(x.Status.Value)).FirstOrDefault();
                if (data == null)
                {
                    input.Status = 0;
                    input.ItemStatus = 1607;
                    var entity = MapToEntity(input);

                    await Repository.InsertAsync(entity);

                    insertNum++;
                }
                else
                {
                    CheckTakeCar();
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return insertNum;
        }
        private void CheckTakeCar()
        {
            throw new UserFriendlyException($"该车子已经被提领，不能重复提领");
        }
    }
}
