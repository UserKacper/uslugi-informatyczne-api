using Microsoft.EntityFrameworkCore;
public class DataBaseApiContext : DbContext
{
    public DbSet<PricingModel> pricingModels { get; set; }
    public DataBaseApiContext(DbContextOptions<DataBaseApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


}