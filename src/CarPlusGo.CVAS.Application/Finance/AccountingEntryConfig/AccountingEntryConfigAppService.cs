using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using CarPlusGo.CVAS.Finance.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Finance
{
    public class AccountingEntryConfigAppService
        : AsyncCrudAppService<AccountingEntryConfig, AccountingEntryConfigDto, long, PagedAccountingEntryConfigResultRequestDto, CreateOrUpdateAccountingEntryConfigDto, CreateOrUpdateAccountingEntryConfigDto>, IAccountingEntryConfigAppService
    {
        private readonly IRepository<AccountingTitle, long> _accountingTitleRepository;

        public AccountingEntryConfigAppService(IRepository<AccountingEntryConfig, long> repository, IRepository<AccountingTitle, long> accountingTitleRepository)
            : base(repository)
        {
            _accountingTitleRepository = accountingTitleRepository;
        }

        protected override IQueryable<AccountingEntryConfig> CreateFilteredQuery(PagedAccountingEntryConfigResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Children, x => x.AccountingTitle)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword))
                .WhereIf(input.IsMaster.HasValue, x => x.IsMaster == input.IsMaster)
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .WhereIf(input.From.HasValue && input.To.HasValue, x => x.CreationTime >= input.From && x.CreationTime <= input.To);
        }

        public override async Task<PagedResultDto<AccountingEntryConfigDto>> GetAll(PagedAccountingEntryConfigResultRequestDto input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            var accountingTitles = await _accountingTitleRepository.GetAllListAsync(x => x.IsActive && !x.IsDeleted);

            SetChildrenAccountingTitle(entities, accountingTitles);

            return new PagedResultDto<AccountingEntryConfigDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }

        public override async Task<AccountingEntryConfigDto> Create(CreateOrUpdateAccountingEntryConfigDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);

            UpdateAccountingEntryConfig(entity);

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<AccountingEntryConfigDto> Update(CreateOrUpdateAccountingEntryConfigDto input)
        {
            CheckUpdatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            MapToEntity(input, entity);

            UpdateAccountingEntryConfig(entity);

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        private void SetChildrenAccountingTitle(List<AccountingEntryConfig> accountingEntryConfigs, List<AccountingTitle> accountingTitles)
        {
            foreach (var accountingEntryConfig in accountingEntryConfigs)
            {
                if (!accountingEntryConfig.IsMaster)
                {
                    accountingEntryConfig.AccountingTitle = accountingTitles.FirstOrDefault(x => x.Id == accountingEntryConfig.AccountingTitleId);
                }
                    
                if (accountingEntryConfig.Children != null && accountingEntryConfig.Children.Count > 0)
                {
                    SetChildrenAccountingTitle(accountingEntryConfig.Children.ToList(), accountingTitles);
                }  
            }
        }


        private void UpdateAccountingEntryConfig(AccountingEntryConfig accountingEntryConfig)
        {
            if (accountingEntryConfig.IsMaster)
            {
                accountingEntryConfig.AccountingTitle = null;
                accountingEntryConfig.AccountingTitleId = null;
                accountingEntryConfig.ElementChangeDirection = null;
                accountingEntryConfig.ParentId = null;
                accountingEntryConfig.Sort = null;
            }
            else if(accountingEntryConfig.ParentId == accountingEntryConfig.Id)
            {
                throw new UserFriendlyException("上级分录不可设置为分录本身");
            }
        }


    }
}
