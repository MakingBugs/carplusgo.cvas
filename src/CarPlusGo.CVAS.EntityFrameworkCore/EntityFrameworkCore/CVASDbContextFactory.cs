namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    /* 在此类中添加所有DbContextFactory */
    public class CVASDbContextFactory : SqlServerDbContextFactory<CVASDbContext>
    {
        public override string ConnectionStringName => CVASConsts.ConnectionStringName;
    }
    public class TShareBankDbContextFactory : SqlServerDbContextFactory<TShareBankDbContext>
    {
        public override string ConnectionStringName => CVASConsts.TShareBankConnectionStringName;
    }

    public class RentalDbContextFactory : SqlServerDbContextFactory<RentalDbContext>
    {
        public override string ConnectionStringName => CVASConsts.RentalConnectionStringName;
    }

    public class BiStatDbContextFactory : MySqlDbContextFactory<RentalDbContext>
    {
        public override string ConnectionStringName => CVASConsts.BiStatConnectionStringName;
    }
}
