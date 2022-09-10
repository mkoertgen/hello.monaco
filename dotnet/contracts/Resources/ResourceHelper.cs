using System.Reflection;
using System.Text.Json;

namespace contracts.Resources;

public static class ResourceHelper
{
    public static T ReadJson<T>(this Assembly assembly, string path)
    {
        try
        {
            var normalized = path.Replace('/', '.').Replace('-', '_');
            var name = $"{assembly.GetName().Name}.{normalized}";
            using var stream = assembly.GetManifestResourceStream(name);
            using var reader = new StreamReader(stream!);
            var json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(json)!;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Could not deserialize '{path}' as '{typeof(T).Name}'", e);
        }
    }
}
