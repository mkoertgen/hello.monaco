using contracts.Posts;

namespace contracts.Weather;

public class JsonWeather : JsonProvider<WeatherForecast>, IProvideWeather
{
    public JsonWeather(HttpClient http, string uri): base(http, uri)
    { }

    public async Task<IEnumerable<WeatherForecast>> GetForecast(DateTime startDate)
    {
        return await GetAll();
    }
}
