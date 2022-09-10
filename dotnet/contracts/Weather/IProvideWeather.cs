namespace contracts.Weather;

public interface IProvideWeather
{
  Task<IEnumerable<WeatherForecast>> GetForecast(DateTime startDate);
  public Task<IEnumerable<WeatherForecast>> GetForecast() => GetForecast(DateTime.UtcNow);
}
