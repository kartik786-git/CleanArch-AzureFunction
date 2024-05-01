using Application.Features.Blog.Commands.CreateBlog;
using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.DeleteBlog
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, DeleteBlogResponse>
    {
        private readonly IRepository<Domain.Entity.Blog> _blogRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DeleteBlogCommandHandler(IRepository<Domain.Entity.Blog> blogRepository, IMapper mapper,
            ILogger<DeleteBlogCommandHandler> logger)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<DeleteBlogResponse> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var deleteBlogResponse = new DeleteBlogResponse();
            var validator = new DeleteBlogValidator(_blogRepository);
            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    deleteBlogResponse.Success = false;
                    deleteBlogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        deleteBlogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (deleteBlogResponse.Success)
                {
                    var blogEntity = await _blogRepository.GetByIdAsync(request.Id);
                    await _blogRepository.DeleteAsync(blogEntity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while due to error- {ex.Message} ");
                deleteBlogResponse.Success = false;
                deleteBlogResponse.Message = ex.Message;
            }

            return deleteBlogResponse;
        }
    }
}
