using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExercicioAula2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IOptionsMonitor<WeatherOptions> _weatherOptions;

        public WeatherForecastController(IOptionsMonitor<WeatherOptions> weatherOptions)
        {
            _weatherOptions = weatherOptions;
        }
        [HttpGet]
        public IActionResult Get()
        {
            if (_weatherOptions.CurrentValue.FixedValue)
            {
                var fixedWeather = new WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = _weatherOptions.CurrentValue.TemperatureC,
                    Summary = _weatherOptions.CurrentValue.Summary
                };
                return Ok(fixedWeather);
            }
            else
            {
                var rng = new Random();
                var weatherData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToArray();

                return Ok(weatherData);
            }
        }
    }
}
