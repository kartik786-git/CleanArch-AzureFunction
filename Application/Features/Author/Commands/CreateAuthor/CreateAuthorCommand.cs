using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<CreateAuthorResponse>
    {
        public AuthorDto CreateAuthor { get; set; }
    }
}
