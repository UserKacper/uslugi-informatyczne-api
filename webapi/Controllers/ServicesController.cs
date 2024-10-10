using Microsoft.AspNetCore.Mvc;
[ApiController]
public class PricingController : ControllerBase
{
    private readonly IPricingRepository _pricingRepository;
    private readonly ILogger<PricingController> _logger;

    private static readonly HashSet<string> RouteMappings = new()
    {
         "webapp" , "webpage" ,"mobile" ,"software"
    };

    public PricingController(IPricingRepository pricingRepository, ILogger<PricingController> logger)
    {
        _pricingRepository = pricingRepository;
        _logger = logger;
    }

    [HttpGet("/api/{type}")]
    public async Task<ActionResult<IEnumerable<PricingModel>>> GetAllPricingAsync([FromRoute] string type)
    {
        if (!RouteMappings.Contains(type))
        {
            return BadRequest("Invalid route type provided.");
        }

        IEnumerable<PricingModel> services;

        try
        {
            services = await _pricingRepository.GetAllAsync(type);
            return Ok(services);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving pricing models for route type '{RouteType}'.", type);
            return StatusCode(500, "Internal server error.");
        }
    }
    [HttpPost("/api/mail")]
    public async Task<ActionResult> EmailSender([FromBody] EmailModel emailModel)
    {
        try
        {
            TryValidateModel(emailModel);
            
            return Ok("email sent.");
        }
        catch (Exception ex)
        {

            _logger.LogError($"{ex.Message}", "unable to send message please try again.");
            return  StatusCode(500, "Internal server error.");
        }

    }
}