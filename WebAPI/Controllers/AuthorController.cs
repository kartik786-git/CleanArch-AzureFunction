using Application.Features.Author.Commands.CreateAuthor;
using Application.Features;
using Application.Features.Author.Queries.GetAuthorsByName;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ApiControllerBase
    {
        [HttpGet("{name}")]
        public async Task<ActionResult<GetAuthorsByNameResponse>> GetById(string name)
        {
            var result = await Mediator.Send(new GetAuthorsByNameQuery() { Name = name });
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAuthorResponse>> Create(AuthorDto command)
        {
            var result = await Mediator.Send(new CreateAuthorCommand() { CreateAuthor = command });
            return result;
        }

    }
}
