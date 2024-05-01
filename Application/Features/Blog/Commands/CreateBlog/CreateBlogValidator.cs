
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator()
        {
            RuleFor(p => p.CreateBlog.Name)
               .NotEmpty()
               .NotNull()
               .WithMessage("{PropertyName} should have value");
            RuleFor(p => p.CreateBlog.Description)
              .NotEmpty()
                .NotNull()
              .WithMessage("{PropertyName} should have value");


        }
    }
}
