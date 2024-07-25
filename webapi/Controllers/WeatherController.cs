using System.Collections;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ServiceController : ControllerBase
{
    private readonly ISmartHomeRepository _smartHomeRepository;
    private readonly ILogger<ServiceController> logger;
    public ServiceController(ISmartHomeRepository smartHomeRepository, ILogger<ServiceController> logger)
    {
        _smartHomeRepository = smartHomeRepository;
        this.logger = logger;
    }
    [HttpGet("/api")]
    public async Task<ActionResult<IEnumerable<SmartHomeInformation>>> GetAllAsync()
    {
        try
        {
            var companies = await _smartHomeRepository.GetAllAsync();
            if (companies.Count() == 0) return NotFound($"No companies found.");

            return Ok(companies);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

}

