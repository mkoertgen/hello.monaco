using contracts.Weather;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor.Pages.Weather;

public class IndexModel : PageModel
{
    private readonly IProvideWeather _provider;
    private readonly List<WeatherForecast> _forecasts = Enumerable.Empty<WeatherForecast>().ToList();

    public IndexModel(IProvideWeather provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public IEnumerable<WeatherForecast> Forecasts => _forecasts;

    public async Task OnGet()
    {
        _forecasts.Clear();
        _forecasts.AddRange(await _provider.GetForecast());
    }
}
