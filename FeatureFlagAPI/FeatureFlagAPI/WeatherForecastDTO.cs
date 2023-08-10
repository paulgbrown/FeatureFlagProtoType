using System.ComponentModel.DataAnnotations;
using FeatureFlagAPI.Controllers;

namespace FeatureFlagAPI;

public class WeatherForecastDTO : ISummary
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    //[Required(AllowEmptyStrings = true)]
    public string Summary { get; set; }
}
