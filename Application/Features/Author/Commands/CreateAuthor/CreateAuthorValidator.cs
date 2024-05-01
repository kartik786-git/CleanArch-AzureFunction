using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.CreateAuthor
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator()
        {
            RuleFor(p => p.CreateAuthor.Name)
              .NotEmpty()
              .NotNull()
              .WithMessage("{PropertyName} should have value");
            RuleFor(p => p.CreateAuthor.Email)
              .NotEmpty()
                .NotNull()
              .WithMessage("{PropertyName} should have value");
        }
    }
}
