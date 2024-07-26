using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

class SoftwareRepository : ISoftwareRepository
{
    private readonly DataBaseApiContext _ctx;
    private readonly ILogger<DynamicWPRepository> _logger;
    public SoftwareRepository(DataBaseApiContext ctx, ILogger<DynamicWPRepository> logger)
    {
        _logger = logger;
        _ctx = ctx;
    }

    public async Task<IEnumerable<SoftwareInformationModel>> GetAllAsync()
    {
        try
        {
            return await _ctx.softwareInformationModels
                            .OrderBy(e => e.PricingMin)
                            .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<SoftwareInformationModel>();
        }
    }
}

