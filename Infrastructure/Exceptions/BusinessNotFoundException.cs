using System;

namespace Infrastructure.Exceptions
{
  public class BusinessNotFoundException :  Exception
  {
    public BusinessNotFoundException(string message) : base(message)
    {
    }

    public BusinessNotFoundException(string message, Exception ex) : base(message, ex)
    {
    }

    public override string ToString()
    {
      return $"Handled Business Not Found Exception: {Message}";
    }
  }
}
