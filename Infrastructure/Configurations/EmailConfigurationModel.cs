namespace Infrastructure.Configurations
{
  public class EmailConfigurationModel
  {
    public string Host { get; set; }

    public string From { get; set; }

    public string Sender { get; set; }

    public string Password { get; set; }

    public int Port { get; set; }
  }
}
