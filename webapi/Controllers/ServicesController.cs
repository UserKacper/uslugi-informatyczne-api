using System.Collections;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ServiceController : ControllerBase
{
    private readonly ISmartHomeRepository _smartHomeRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IDynamicWPRepository _dynamicWPRepository;
    private readonly IStaticWPRepository _staticWPRepository;
    private readonly ILogger<ServiceController> logger;
    public ServiceController(ISoftwareRepository softwareRepository, ISmartHomeRepository smartHomeRepository, ILogger<ServiceController> logger, IDynamicWPRepository dynamicWPRepository, IStaticWPRepository staticWPRepository)
    {
        _softwareRepository = softwareRepository;
        _smartHomeRepository = smartHomeRepository;
        _dynamicWPRepository = dynamicWPRepository;
        _staticWPRepository = staticWPRepository;
        this.logger = logger;
    }
    [HttpGet("/api/smarthome")]
    public async Task<ActionResult<IEnumerable<SmartHomeInformatioModel>>> GetAllSmarthomeInformationAsync()
    {
        try
        {
            var services = await _smartHomeRepository.GetAllAsync();
            return Ok(services);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("/api/software")]
    public async Task<ActionResult<IEnumerable<SmartHomeInformatioModel>>> GetAllSoftwareInformationAsync()
    {
        try
        {
            var services = await _softwareRepository.GetAllAsync();
            return Ok(services);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("/api/dynamic")]
    public async Task<ActionResult<IEnumerable<SmartHomeInformatioModel>>> GetAllDynamicWPInformationAsync()
    {
        try
        {
            var services = await _dynamicWPRepository.GetAllAsync();
            return Ok(services);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("/api/static")]
    public async Task<ActionResult<IEnumerable<SmartHomeInformatioModel>>> GetAllStaticWPInformationAsync()
    {
        try
        {
            var services = await _staticWPRepository.GetAllAsync();
            return Ok(services);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

}

