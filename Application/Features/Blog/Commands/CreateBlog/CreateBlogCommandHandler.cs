using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogResponse>
    {
        private readonly IRepository<Domain.Entity.Blog> _blogRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateBlogCommandHandler(IRepository<Domain.Entity.Blog> blogRepository, IMapper mapper,
            ILogger<CreateBlogCommandHandler> logger)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CreateBlogResponse> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var createBlogResponse = new CreateBlogResponse();
            var validator = new CreateBlogValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    createBlogResponse.Success = false;
                    createBlogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createBlogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (createBlogResponse.Success)
                {
                    var blogEntity = _mapper.Map<Domain.Entity.Blog>(request.CreateBlog);
                    var result = await _blogRepository.AddAsync(blogEntity);
                    createBlogResponse.BlogId = result.Id;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                createBlogResponse.Success = false;
                createBlogResponse.Message = ex.Message;

            }

            return createBlogResponse;

        }
    }
}
