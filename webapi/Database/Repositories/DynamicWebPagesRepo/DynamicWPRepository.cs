using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

class DynamicWPRepository : IDynamicWPRepository
{
    private readonly DataBaseApiContext _ctx;
    private readonly ILogger<DynamicWPRepository> _logger;
    public DynamicWPRepository(DataBaseApiContext ctx, ILogger<DynamicWPRepository> logger)
    {
        _logger = logger;
        _ctx = ctx;
    }

    public async Task<IEnumerable<DynamicWPInformationModel>> GetAllAsync()
    {
        try
        {
            return await _ctx.dynamicWPInformationModels
                            .OrderBy(e => e.PricingMin)
                            .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<DynamicWPInformationModel>();
        }
    }
}