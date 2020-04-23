using Abp.Data;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CarPlusGo.CVAS.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class RentalRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<RentalDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RentalRepositoryBase(IDbContextProvider<RentalDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
        }

        // Add your common methods for all repositories

        #region 存储过程扩展
        private readonly IActiveTransactionProvider _transactionProvider;
        /// <summary>
        /// 创建Command对象
        /// </summary>
        protected DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        /// <summary>
        /// 确认连接状态开启
        /// </summary>
        protected void EnsureConnectionOpen()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// 获取事务
        /// </summary>
        /// <returns></returns>
        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                { "ContextType", typeof(RentalDbContext) },
                { "MultiTenancySide", MultiTenancySide }
            });
        }
        #endregion

    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="RentalRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class RentalRepositoryBase<TEntity> : RentalRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected RentalRepositoryBase(IDbContextProvider<RentalDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider, transactionProvider)
        {
        }

        // Do not add any method here, add to the class above (since this inherits it)!!!
    }
}
