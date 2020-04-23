using Abp.EntityFrameworkCore;
using CarPlusGo.CVAS.Accessories;
using CarPlusGo.CVAS.BPM;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.CarFixFile;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.CXLPFile;
using CarPlusGo.CVAS.Finance;
using CarPlusGo.CVAS.Insure;
using CarPlusGo.CVAS.LocationFile;
using CarPlusGo.CVAS.OrdersFeeTypeFile;
using CarPlusGo.CVAS.OrdersFile;
using CarPlusGo.CVAS.RepositoryOutCar;
using CarPlusGo.CVAS.TakeCarFile;
using CarPlusGo.CVAS.UseCarApplyFile;
using CarPlusGo.CVAS.UseCarFiles;
using Microsoft.EntityFrameworkCore;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    public class RentalDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<AccountingEntry> AccountingEntry { set; get; }
        public DbSet<AccountingEntryConfig> AccountingEntryConfig { set; get; }
        public DbSet<AccountingTitle> AccountingTitle { set; get; }
        public DbSet<InsuranceType> InsuranceType { set; get; }
        public DbSet<InsuranceDetail> InsuranceDetail { set; get; }
        public DbSet<InsurancePolicy> InsurancePolicy { set; get; }
        public DbSet<InsurancePreset> InsurancePreset { set; get; }
        public DbSet<InsuranceLog> InsuranceLog { set; get; }
        public DbSet<InsuranceApproval> InsuranceApproval { set; get; }
        public DbSet<InsuranceApprovalDetail> InsuranceApprovalDetail { set; get; }
        public DbSet<Supplier> Supplier { set; get; }
        public DbSet<TradeItem> TradeItem { set; get; }
        public DbSet<CarBase> CarBase { set; get; }
        public DbSet<Brand> Brand { set; get; }
        public DbSet<FactoryBrand> FactoryBrand { set; get; }
        public DbSet<Clasen> Clasen { set; get; }
        public DbSet<ItemCode> ItemCode { get; set; }
        public DbSet<AccessoriesMainType> AccessoriesMainType { get; set; }
        public DbSet<AccessoriesTs> AccessoriesTs { get; set; }
        public DbSet<AccessoriesType> AccessoriesType { get; set; }
        public DbSet<Inc> Inc { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersFeeType> OrdersFeeType { get; set; }
        public DbSet<CreditProvince> CreditProvince { get; set; }
        public DbSet<CreditCity> CreditCity { get; set; }
        public DbSet<CreditArea> CreditArea { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Contect> Contect { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<IndustryCode> IndustryCode { get; set; }
        public DbSet<VEmp> VEmp { get; set; }
        public DbSet<AccBank> AccBank { get; set; }
        public DbSet<BankType> BankType { get; set; }
        public DbSet<BankDetail> BankDetail { get; set; }
        public DbSet<SupplierContect> SupplierContect { get; set; }
        public DbSet<CarRepair> CarRepair { get; set; }
        public DbSet<AdditionalItem> AdditionalItem { get; set; }
        public DbSet<CarFix> CarFix { get; set; }
        public DbSet<PRInvLink> PRInvLink { get; set; }
        public DbSet<PRInv> PRInv { get; set; }
        public DbSet<CarFixItem> CarFixItem { get; set; }
        public DbSet<LKRTotal> LKRTotal { get; set; }
        public DbSet<CarFixBatch> CarFixBatch { get; set; }
        public DbSet<CarFixBatchT> CarFixBatchT { get; set; }
        public DbSet<FileUpload> FileUpload { get; set; }
        public DbSet<FormFlow> FormFlow { get; set; }
        public DbSet<ReadyBPM> ReadyBPM { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<CXLP> CXLP { get; set; }
        public DbSet<Insure2> Insure2 { get; set; }
        public DbSet<RRLKR> RRLKR { get; set; }
        public DbSet<CXLPDZF> CXLPDZF { get; set; }
        public DbSet<CXLPRecord> CXLPRecord { get; set; }
        public DbSet<CXLPFee> CXLPFee { get; set; }
        public DbSet<CXLPPFDetail> CXLPPFDetail { get; set; }
        public DbSet<CXLPSupplement> CXLPSupplement { get; set; }
        public DbSet<CXLPMaterial> CXLPMaterial { get; set; }
        public DbSet<CarMemo> CarMemo { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<LocationManager> LocationManager { get; set; }
        public DbSet<Repository> Repository { get; set; }
        public DbSet<UseCarApply> UseCarApply { get; set; }
        public DbSet<RepositoryManager> RepositoryManager { get; set; }
        public DbSet<CarAccessory> CarAccessory { get; set; }
        public DbSet<CarAccessoryRight> CarAccessoryRight { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<CarPart> CarPart { get; set; }
        public DbSet<CarPartRight> CarPartRight { get; set; }
        public DbSet<CarCertificate> CarCertificate { get; set; }
        public DbSet<CarCertificateRight> CarCertificateRight { get; set; }
        public DbSet<RepositoryOutCarPart> RepositoryOutCarParts { get; set; }
        public DbSet<RepositoryOutAccessory> RepositoryOutAccessory { get; set; }
        public DbSet<RepositoryOutCertificate> RepositoryOutCertificate { get; set; }
        public DbSet<RepositoryOut> RepositoryOut { get; set; }
        public DbSet<RepositoryOutFile> RepositoryOutFile { get; set; }
        public DbSet<TakeCar> TakeCar { get; set; }
        public DbSet<TakeCarApply> TakeCarApply { get; set; }
        public DbSet<UseCarPart> UseCarPart { get; set; }
        public DbSet<UseCarAccessory> UseCarAccessory { get; set; }
        public DbSet<UseCarCertificate> UseCarCertificate { get; set; }
        public DbSet<UseCarFile> UseCarFile { get; set; }

        public RentalDbContext(DbContextOptions<RentalDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemCode>()
            .HasKey(c => new { c.ItemType, c.Num });

            modelBuilder.Entity<CreditProvince>()
            .HasKey(c => new { c.Code});
            modelBuilder.Entity<CreditCity>()
            .HasKey(c => new { c.Code });
            modelBuilder.Entity<CreditArea>()
            .HasKey(c => new { c.Code });
        }
    }
}
