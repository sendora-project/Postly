namespace Sendora.Postly.Domain.Models;

public class MailHostConfig
{
    public string Host { get; init; }
    public int Port { get; init; }
    public bool UseSsl { get; init; } = true;
    
    public MailHostConfig(string host, int port)
    {
        Host = host;
        Port = port;
    }
}