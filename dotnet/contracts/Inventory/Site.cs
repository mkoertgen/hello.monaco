using System.Text.Json.Serialization;

namespace contracts.Inventory;

public record Site( [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("config")] string Config)
{
    public const string DefaultConfig = @"
all:
  vars:
    stage: dev
  children:
    gateway:
      hosts:
        my-gateway:
          ansible_host: 192.168.1.101
      vars:
        iotHub:
          enabled: true
          name: my-iot-hub
          device:
            id: MyDevice01
            key: MyDevice01Key
        elastic:
          enabled: true
          hosts: https://elastic.mycloud.com:443
          username: my-gateway
          password: SuperSecret
";
}
