using Microsoft.AspNetCore.Mvc;

namespace contracts.Weather;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IProvideWeather _service;

    public WeatherForecastController(IProvideWeather service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        return await _service.GetForecast(DateTime.Now);
    }
}
