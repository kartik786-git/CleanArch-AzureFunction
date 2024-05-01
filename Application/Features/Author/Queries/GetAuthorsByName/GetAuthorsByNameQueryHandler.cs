using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAuthorsByName
{
    public class GetAuthorsByNameQueryHandler : IRequestHandler<GetAuthorsByNameQuery, GetAuthorsByNameResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAuthorsByNameQueryHandler> _logger;

        public GetAuthorsByNameQueryHandler(IAuthorRepository authorRepository, IMapper mapper,
            ILogger<GetAuthorsByNameQueryHandler> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetAuthorsByNameResponse> Handle(GetAuthorsByNameQuery request, CancellationToken cancellationToken)
        {
            var authorResponse = new GetAuthorsByNameResponse();
            var validator = new GetAuthorsByNameValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    authorResponse.Success = false;
                    authorResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        authorResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (authorResponse.Success)
                {
                    var result = await _authorRepository.GetByNameAsync(request.Name);
                    authorResponse.Authors = _mapper.Map<List<AuthorDto>>(result);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                authorResponse.Success = false;
                authorResponse.Message = ex.Message;

            }

            return authorResponse;
        }
    }
}
