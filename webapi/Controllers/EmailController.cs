using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using SendGrid;
using SendGrid.Helpers.Mail;
using webapi.Database.Models;

[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IAppInitization _appInitization;
    private readonly IEmailValidation _emailValidation;

    public EmailController(ILogger<EmailController> logger, IConfiguration configuration, IAppInitization appInitization, IEmailValidation emailValidation)
    {
        _logger = logger;
        _configuration = configuration;
        _appInitization = appInitization;
        _emailValidation = emailValidation;
    }

    [HttpPost("/api/mail")]
    public async Task<ActionResult> EmailSender([FromBody] EmailModel emailModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage)
        .ToList();
            return Problem(statusCode:StatusCodes.Status400BadRequest, detail: "Invalid input: " + string.Join("; ", errors));
        }

        var ipAddress = HttpContext.Items["IpAddress"]?.ToString();
        if (ipAddress == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail: "Ip Address not found");
        }

        if (!await _emailValidation.IsValidEmailAsync(emailModel.EmailSender))
        {
            return Problem(statusCode:StatusCodes.Status403Forbidden,detail: "Please use a valid Email Adress");
        }
        if (!_emailValidation.IsRateLimitReached(ipAddress))
        {
            return Problem(statusCode:StatusCodes.Status429TooManyRequests, detail: "You have exceeded the limit of 2 emails per hour.");
        }

        try
            {
            var apiKey = await _appInitization.AppInit("sgkey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("kdeja.webdev@gmail.com", emailModel.EmailSender);
            var subject = emailModel.EmailTopic;
            var to = new EmailAddress("kdeja.webdev@gmail.com");
            var plainTextContent = emailModel.EmailContent;
            var htmlContent = emailModel.EmailContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return StatusCode(200,response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to send message, please try again.");
            return StatusCode(500, "Internal server error.");
        }
    }
}