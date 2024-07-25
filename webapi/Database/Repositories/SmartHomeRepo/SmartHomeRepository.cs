using Microsoft.EntityFrameworkCore;

public class SmartHomeRepository : ISmartHomeRepository{
    private readonly ILogger<SmartHomeRepository> logger;
    private readonly DataBaseApiContext ctx;
    public SmartHomeRepository(ILogger<SmartHomeRepository> logger, DataBaseApiContext ctx){
        this.logger = logger;
        this.ctx = ctx;
    }

    public async Task<IEnumerable<SmartHomeInformation>> GetAllAsync() {
        try
        {
            return await ctx.serviceInformation
                .OrderBy(e => e.PricingMin)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return new List<SmartHomeInformation>();
        }
    }
}