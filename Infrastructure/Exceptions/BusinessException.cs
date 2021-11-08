using System;

namespace Infrastructure.Exceptions
{
  public class BusinessException : Exception
  {
    public string Code { get; set; }

    public BusinessException(string message, string code = null) : base(message)
    {
      Code = code;
    }

    public BusinessException(string message, Exception exception) : base(message, exception)
    {
    }

    public override string ToString()
    {
      return $"Handled Business Exception: {Message}";
    }

  }
}
