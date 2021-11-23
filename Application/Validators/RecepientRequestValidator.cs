using Application.Models;
using FluentValidation;

namespace Application.Validators
{
  public class RecepientRequestValidator : AbstractValidator<RecepientRequest>
  {
    public RecepientRequestValidator()
    {
      RuleFor(x => x.Email)
        .MaximumLength(255)
        .NotEmpty();
    }
  }
}
