using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;

class StaticWPRepository : IStaticWPRepository
{
    private readonly DataBaseApiContext _ctx;
    private readonly ILogger<DynamicWPRepository> _logger;
    public StaticWPRepository(DataBaseApiContext ctx, ILogger<DynamicWPRepository> logger)
    {
        _logger = logger;
        _ctx = ctx;
    }

    public async Task<IEnumerable<StaticWPInformationModel>> GetAllAsync()
    {
        try
        {
            return await _ctx.staticWPInformationModels
                            .OrderBy(e => e.PricingMin)
                            .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<StaticWPInformationModel>();
        }
    }
}

