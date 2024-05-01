using Application.Interface;
using de = Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Features.Blog.Queries.GetBlogs
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, GetBlogsResponse>
    {
        private readonly IRepository<de.Blog> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetBlogsQueryHandler(IRepository<de.Blog> repository , 
            IMapper mapper, ILogger<GetBlogsQueryHandler> logger )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetBlogsResponse> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var getBlogResponse = new GetBlogsResponse();
            var validator = new GetBlogsValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    getBlogResponse.Success = false;
                    getBlogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        getBlogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (getBlogResponse.Success)
                {
                    var result = await _repository.GetAllAsync();
                    getBlogResponse.Blogs = _mapper.Map<List<BlogDto>>(result);
                }


            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                getBlogResponse.Success = false;
                // conver to you own message to show user
                getBlogResponse.Message = ex.Message;
            }
            return getBlogResponse;
        }
    }
}
