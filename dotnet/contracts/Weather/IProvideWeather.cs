namespace contracts.Weather;

public interface IProvideWeather
{
  Task<IEnumerable<WeatherForecast>> GetForecast(DateTime startDate);
}
