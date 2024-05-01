using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.UpdateBlog
{
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogCommand>
    {
        private readonly IRepository<Domain.Entity.Blog> _repository;

        public UpdateBlogValidator(IRepository<Domain.Entity.Blog> repository)
        {
            _repository = repository;
            RuleFor(p => p.BlogUpdate.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("{PropertyName} should have value");

            RuleFor(p => p.BlogUpdate.Description)
              .NotEmpty()
                .NotNull()
              .WithMessage("{PropertyName} should have value");
            
            RuleFor(x => x.Id).MustAsync(isExistBlog)
                .WithMessage("{PropertyName} does not exits.");
        }

        private async Task<bool> isExistBlog(int blogId, CancellationToken cancellationToken)
        {
           var blog = await _repository.GetByIdAsync(blogId).ConfigureAwait(false);
            return blog?.Id > 0;
        }
    }
}
