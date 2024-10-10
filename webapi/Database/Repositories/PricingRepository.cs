using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

class PricingRepository : IPricingRepository
{
    private readonly DataBaseApiContext _ctx;
    private readonly ILogger<PricingRepository> _logger;
    public PricingRepository(DataBaseApiContext ctx, ILogger<PricingRepository> logger)
    {
        _logger = logger;
        _ctx = ctx;
    }

    public async Task<IEnumerable<PricingModel>> GetAllAsync(string type)
    {
        try
        {
            return await _ctx.pricingModels
                 .Where(e => e.Type == type)
                 .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<PricingModel>();
        }
    }
}