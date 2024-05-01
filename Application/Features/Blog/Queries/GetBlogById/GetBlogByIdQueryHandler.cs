using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Queries.GetBlogById
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, GetBlogByIdResponse>
    {
        private readonly IRepository<Domain.Entity.Blog> _blogRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetBlogByIdQueryHandler(IRepository<Domain.Entity.Blog> blogRepository, IMapper mapper,
            ILogger<GetBlogByIdQueryHandler> logger)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetBlogByIdResponse> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blogResponse = new GetBlogByIdResponse();
            var validator = new GetBlogByIdValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    blogResponse.Success = false;
                    blogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        blogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (blogResponse.Success)
                {
                    var result = await _blogRepository.GetByIdAsync(request.Id);
                    blogResponse.Blog = _mapper.Map<BlogDto>(result);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                blogResponse.Success = false;
                blogResponse.Message = ex.Message;

            }

            return blogResponse;
        }
    }
}
