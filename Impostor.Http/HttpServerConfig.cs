namespace Impostor.Http;

/**
 * <summary>
 * Configuration class for Impostor.Http.
 * </summary>
 */
public class HttpServerConfig
{
    /**
     * <summary>
     * Gets the name of this config section.
     * </summary>
     */
    public const string Section = "HttpServer";

    /**
     * <summary>
     * Gets or sets the IP address the HTTP Matchmaking server will listen on.
     * </summary>
     * Use "127.0.0.1" if you are running behind a reverse proxy or just testing locally.
     * Use "0.0.0.0" if you are directly exposing this server to the internet (not recommended).
     */
    public string ListenIp { get; set; } = "127.0.0.1";

    /**
     * <summary>
     * Gets or sets the port the HTTP Matchmaking server will listen on.
     * </summary>
     * For port forwarding purposes, this is a TCP port.
     */
    public ushort ListenPort { get; set; } = 22000;

    /**
     * <summary>
     * Gets or sets a value indicating whether Https is enabled or not.
     * </summary>
     * If true, also set CertificatePath.
     */
    public bool UseHttps { get; set; } = false;

    /**
     * <summary>
     * Gets or sets the path to the SSL certificate.
     * </summary>
     * This field is ignored if SSL is not used.
     */
    public string CertificatePath { get; set; } = string.Empty;

    /**
     * <summary>
     * Gets or sets the IP address the HTTP Matchmaking server will listen on.
     * </summary>
     * This should be set to the same value as Impostor's PublicIp
     */
    public string PublicIp { get; set; } = "127.0.0.1";

    /**
     * <summary>
     * Gets or sets the port the game server listens on.
     * </summary>
     * This should be set to the same value as Impostor's PublicPort
     */
    public ushort PublicPort { get; set; } = 22023;
}
