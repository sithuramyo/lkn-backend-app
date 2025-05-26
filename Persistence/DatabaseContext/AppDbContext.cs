using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.DataModels.Auth;
using Persistence.DataModels.ECommerce;
using Persistence.DataModels.Master;
using Persistence.DataModels.Setup;
using Persistence.DataModels.Voucher;

namespace Persistence.DatabaseContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    #region Auth
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Consumer> Consumers { get; set; }
    #endregion

    #region Admintration Management System
    public virtual DbSet<MasterCode> MasterCodes { get; set; }
    public virtual DbSet<MasterCodeItem> MasterCodeItems { get; set; }
    public virtual DbSet<Merchant> Merchants { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductColor> ProductColors { get; set; }
    public virtual DbSet<Delivery> Deliveries { get; set; }
    #endregion

    #region E-commerce Management System
    public virtual DbSet<Stock> Stocks { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<BundleCategory> BundleCategories { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public virtual DbSet<PurchaseStock> PurchaseStocks { get; set; }
    public virtual DbSet<PurchaseBundle> PurchaseBundles { get; set; }
    public virtual DbSet<DeveloperCommission> DeveloperCommissions { get; set; }
    public virtual DbSet<DeveloperCommissionClaim> DeveloperCommissionClaims { get; set; }
    #endregion

    #region Voucher Management System
    public virtual DbSet<MerchantVoucher> MerchantVouchers { get; set; }
    public virtual DbSet<MerchantVoucherProduct> MerchantVoucherProducts { get; set; }
    public virtual DbSet<MerchantVoucherPaidHistory> MerchantVoucherPaidHistories { get; set; }
    #endregion

}
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(
            "Host=122.248.249.116;Port=5432;Database=lawkanat_db;Username=admin;Password=@Rootroot1;Pooling=true;Trust Server Certificate=true;");

        return new AppDbContext(optionsBuilder.Options);
    }
}