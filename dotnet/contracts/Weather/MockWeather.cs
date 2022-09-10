namespace contracts.Weather;

public class MockWeather : IProvideWeather
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IEnumerable<WeatherForecast>> GetForecast(DateTime startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast(
            startDate.AddDays(index),
            Random.Shared.Next(-20, 55),
            Summaries[Random.Shared.Next(Summaries.Length)])));
    }
}
