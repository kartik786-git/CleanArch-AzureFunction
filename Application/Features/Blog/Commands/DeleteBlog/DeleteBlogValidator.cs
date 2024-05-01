using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.DeleteBlog
{
    public class DeleteBlogValidator : AbstractValidator<DeleteBlogCommand>
    {
        private readonly IRepository<Domain.Entity.Blog> _blogRepository;

        public DeleteBlogValidator(IRepository<Domain.Entity.Blog> blogRepository)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("{PropertyName} greater then 0");
            RuleFor(x => x.Id).MustAsync(isExistBlog).WithMessage("{PropertyName} does not exits.");
            _blogRepository = blogRepository;
        }
        private async Task<bool> isExistBlog(int blogId, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId);
            return blog?.Id > 0;
        }
    }
}
