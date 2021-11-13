﻿using Application.Models;
using FluentValidation;

namespace Application.Validators
{
  public class MailMessageTemplateRequestValidator : AbstractValidator<MailMessageTemplateRequest>
  {
    public MailMessageTemplateRequestValidator()
    {
      RuleFor(x => x.Subject)
        .MaximumLength(998)
        .NotEmpty()
        .NotNull();

      RuleFor(x => x.Body)
        .MaximumLength(384000)
        .NotEmpty()
        .NotNull();
    }
  }
}