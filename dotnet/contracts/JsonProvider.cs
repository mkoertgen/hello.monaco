using System.Net.Http.Json;

namespace contracts;

public class JsonProvider<T> : IProvide<T>
{
    private readonly HttpClient _http;
    private readonly string _uri;

    protected JsonProvider(HttpClient http, string uri)
    {
        _http = http ?? throw new ArgumentNullException(nameof(http));
        _uri = uri ?? throw new ArgumentNullException(nameof(uri));
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _http.GetFromJsonAsync<IEnumerable<T>>(_uri) ?? Enumerable.Empty<T>();
    }
}
