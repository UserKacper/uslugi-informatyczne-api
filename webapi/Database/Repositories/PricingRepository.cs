using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors;

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
            _logger.LogInformation($"Querying PricingModels with Type: {type}");

            if (string.IsNullOrEmpty(type))
            {
                _logger.LogWarning("Received empty or null 'type' value.");
                return new List<PricingModel>();
            }

            return await _ctx.PricingModels
                 .Where(e => e.Type == type)
                 .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching PricingModels.");
            return new List<PricingModel>();
        }
    }

}