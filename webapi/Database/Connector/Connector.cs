using Microsoft.EntityFrameworkCore;
public class DataBaseApiContext : DbContext
{
    public DbSet<SmartHomeInformatioModel> smarthomeInformationModel { get; set; }
    public DbSet<SoftwareInformationModel> softwareInformationModels { get; set; }
    public DbSet<DynamicWPInformationModel> dynamicWPInformationModels { get; set; }
    public DbSet<StaticWPInformationModel> staticWPInformationModels { get; set; }
    public DataBaseApiContext(DbContextOptions<DataBaseApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


}