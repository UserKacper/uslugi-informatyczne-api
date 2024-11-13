using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IAppInitization _appInitization;


    public EmailController(ILogger<EmailController> logger, IConfiguration configuration, IAppInitization appInitization)
    {
        _logger = logger;
        _configuration = configuration;
        _appInitization = appInitization;
    }

    [HttpPost("/api/mail")]
    public async Task<IActionResult> EmailSender([FromBody] EmailModel emailModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var apiKey = await _appInitization.AppInit();
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
            _logger.LogError(ex, "Unable to send message, please try again.");
            return StatusCode(500, "Internal server error.");
        }
    }
}