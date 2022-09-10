using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace contracts.Posts;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public record BlogPost(
  [property: JsonPropertyName("id")] int Id,
  [property: JsonPropertyName("title")] string Title,
  [property: JsonPropertyName("body")] string Body);
