using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace FeatureFlagAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly string RequireChannelAndUserInfo = "RequireChannelAndUserInfo";

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IFeatureManager _featureManager;
    private readonly ISummaryValidator _summaryValidator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISummaryValidator summaryValidator)
    {
        _summaryValidator = summaryValidator;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost(Name = "AddWeatherForecast")]
    public async Task<ActionResult<WeatherForecast>> Get(WeatherForecastDTO dto)
    {
        if (!await _summaryValidator.Validate(dto, ModelState))
        {
            return BadRequest(ModelState);
        }

        return Ok(new WeatherForecast()
            {
                Date = dto.Date,
                TemperatureC = dto.TemperatureC,
                Summary = dto.Summary
            });
    }
}

public interface ISummary
{
    string Summary { get; set; }
}
