using Microsoft.AspNetCore.Mvc;
[ApiController]
public class PricingController : ControllerBase
{
    private readonly IPricingRepository _pricingRepository;
    private readonly ILogger<PricingController> _logger;
    private readonly IConfiguration _configuration;

    private static readonly HashSet<string> RouteMappings = new()
    {
         "webapp" , "webpage" ,"mobile" ,"software"
    };

    public PricingController(IPricingRepository pricingRepository, ILogger<PricingController> logger, IConfiguration configuration)
    {
        _pricingRepository = pricingRepository;
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet("/api/{type}")]
    public async Task<ActionResult<IEnumerable<PricingModel>>> GetAllPricingAsync([FromRoute] string type)
    {
        if (!RouteMappings.Contains(type))
        {
            return StatusCode(400,"Invalid route type provided.");
        }
        IEnumerable<PricingModel> services;

        try
        {
            services = await _pricingRepository.GetAllAsync(type);
            return StatusCode(200,services);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving pricing models for route type '{RouteType}'.", type);
            return StatusCode(500, "Internal server error.");
        }

    }
}