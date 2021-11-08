﻿namespace Application.Models
{
  public class MailRequest
  {
    public string Host { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public int Port { get; set; }

    public string Password { get; set; }
  }
}
