namespace Demo.Contracts.Configs
{
    public interface IRabbitMqConfig
    {
        string Host { get; set; }
        string VirtualHost { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}