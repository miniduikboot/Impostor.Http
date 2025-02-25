# Impostor.Http

Adds HTTP matchmaking to [Impostor](https://github.com/Impostor/Impostor)

## Installation

1. [Install Impostor first](https://github.com/Impostor/Impostor/blob/master/docs/Running-the-server.md)
2. Install [ASP.NET Core Runtime 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) as well
3. Download [Impostor.Http.dll from the GitHub Releases](https://github.com/Impostor/Impostor.Http/releases) and put it in Impostor's `plugin` folder
4. In the base Impostor configuration file, add a PluginLoader section to let Impostor find the AspNetCore files:

```json
{
  "PluginLoader": {
    "LibraryPaths": ["/usr/share/dotnet/shared/Microsoft.AspNetCore.App/6.0.x"]
  }
}
```

Where `x` refers to the minor version of ASP.NET Core 6 you have installed.

5. Finally, create a configuration file for this plugin. You **must** set the PublicIp property, otherwise you will be unable to join your server. See the next section for this.

## Configuration

Configuration is read from the `config_http.json` file or from environment variables prefixed with `IMPOSTOR_HTTP_`. You can copy over [this file](https://github.com/Impostor/Impostor.Http/blob/main/config_http.json) for the default settings. These are the possible keys:

| Key                 | Default     | Description                                                                                                                                                              |
| ------------------- | ----------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **PublicIp**        | `127.0.0.1` | This is the public IP your server is available on. Should be the same as PublicIp in Impostor's `config.json`. You MUST set this key for Impostor.Http to work properly. |
| **PublicPort**      | `22023`     | Port your Among Us server is listening on, should be the same as PublicPort from your Impostor configuration file.                                                       |
| **ListenIp**        | `127.0.0.1` | IP address to listen on. Use 127.0.0.1 if using a reverse proxy like nginx (recommended), use 0.0.0.0 if exposed directly (not recommended)                              |
| **ListenPort**      | 22000       | Port the HTTP matchmaking server is running on.                                                                                                                          |
| **UseHttps**        | `false`     | Set to true if using encrypted communication to your reverse proxy or if you're exposing this server directly to the internet (not recommended)                          |
| **CertificatePath** | _not set_   | If UseHttps is enable, set this property to the path of your SSL certificate.                                                                                            |
