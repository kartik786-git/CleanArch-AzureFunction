using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Queries.GetBlogById
{
    public class GetBlogByIdValidator : AbstractValidator<GetBlogByIdQuery>
    {
        public GetBlogByIdValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} greater then 0");
        }
    }
}
