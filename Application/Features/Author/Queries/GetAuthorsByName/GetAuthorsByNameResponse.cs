using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAuthorsByName
{
    public class GetAuthorsByNameResponse : BaseResponse
    {
        public List<AuthorDto> Authors { get; set; }
    }
}
