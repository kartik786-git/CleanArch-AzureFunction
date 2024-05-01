using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, UpdateBlogResponse>
    {
        private readonly IRepository<Domain.Entity.Blog> _blogRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBlogCommandHandler> _logger;

        public UpdateBlogCommandHandler(IRepository<Domain.Entity.Blog> blogRepository, IMapper mapper,
            ILogger<UpdateBlogCommandHandler> logger)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<UpdateBlogResponse> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var updateBlogResponse = new UpdateBlogResponse();
            var validator = new UpdateBlogValidator(_blogRepository);
            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    updateBlogResponse.Success = false;
                    updateBlogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        updateBlogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (updateBlogResponse.Success)
                {
                    var blogEntity = await _blogRepository.GetByIdAsync(request.Id);
                    _mapper.Map(request.BlogUpdate, blogEntity);
                    await _blogRepository.UpdateAsync(blogEntity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while due to error- {ex.Message} ");
                updateBlogResponse.Success = false;
                updateBlogResponse.Message = ex.Message;
            }

            return updateBlogResponse;
        }
    }
}
