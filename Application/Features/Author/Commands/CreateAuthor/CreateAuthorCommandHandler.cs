using Application.Interface;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAuthorCommandHandler> _logger;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper,
            ILogger<CreateAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CreateAuthorResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var createAuthorResponse = new CreateAuthorResponse();
            var validator = new CreateAuthorValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    createAuthorResponse.Success = false;
                    createAuthorResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createAuthorResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (createAuthorResponse.Success)
                {
                    var authorEntity = _mapper.Map<Domain.Entity.Author>(request.CreateAuthor);
                    var result = await _authorRepository.AddAsync(authorEntity);
                    createAuthorResponse.AuthorId = result.Id;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                createAuthorResponse.Success = false;
                createAuthorResponse.Message = ex.Message;

            }

            return createAuthorResponse;
        }
    }
}
