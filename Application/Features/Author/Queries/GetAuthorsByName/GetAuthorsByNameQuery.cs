using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAuthorsByName
{
    public class GetAuthorsByNameQuery : IRequest<GetAuthorsByNameResponse>
    {
        public string Name { get; set; }
    }
}
