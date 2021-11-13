using Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
  public class FileRequestValidator : AbstractValidator<FileRequest>
  {
    private const string KB = "KB";
    private const string MB = "MB";
    private const string GB = "GB";

    private readonly List<string> _extensions = new()
    {
      "image/jpeg",
      "image/png",
      "image/gif",
      "../pdf"
    };

    public FileRequestValidator(int maximumSize = 2, string postfix = MB)
    {
      var maxSize = postfix switch
      {
        KB => maximumSize * 1024,
        MB => maximumSize * 1024 * 1024,
        GB => maximumSize * 1024 * 1024 * 1024,
        _ => 1
      };

      RuleFor(x => x.File.Length)
        .LessThanOrEqualTo(maxSize)
        .WithMessage($"Maximum file size is {maxSize} {postfix}");

      RuleFor(x => x.File.ContentType)
        .Must(IsSafeFile)
        .WithMessage("Uploaded file is in incorrect format");
    }

    private bool IsSafeFile(string contentType)
    {
      return _extensions.Contains(contentType);
    }
  }
}
