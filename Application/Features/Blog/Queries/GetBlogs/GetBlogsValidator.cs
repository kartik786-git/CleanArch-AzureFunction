using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Queries.GetBlogs
{
    public class GetBlogsValidator : AbstractValidator<GetBlogsQuery>
    {
        public GetBlogsValidator()
        {
            //
            //RuleFor(p => 1)
            //    .GreaterThan(0)
            //    .WithMessage("{PropertyName} shuoul have greater thenvalue");
        }
    }
}
