using Application.Models;
using FluentValidation;

namespace Application.Validators
{
  public class RecipientRequestValidator : AbstractValidator<RecipientRequest>
  {
    public RecipientRequestValidator()
    {
      RuleFor(x => x.Email)
        .MaximumLength(255)
        .NotEmpty();
    }
  }
}
