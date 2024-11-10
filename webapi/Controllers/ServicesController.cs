using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
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
            var apiKey = _configuration.GetValue<string>("EmailApi:SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("kdeja.webdev@gmail.com", emailModel.EmailSender);
            var subject = emailModel.EmailTopic;
            var to = new EmailAddress("kdeja.webdev@gmail.com");
            var plainTextContent = emailModel.EmailContent;
            var htmlContent = emailModel.EmailContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return Ok(response);
        }
        catch (Exception ex)
        {

            _logger.LogError($"{ex.Message}", "unable to send message please try again.");
            return StatusCode(500, "Internal server error.");
        }

    }
}