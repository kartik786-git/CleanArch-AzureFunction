using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.CreateAuthor
{
    public class CreateAuthorResponse : BaseResponse
    {
        public int AuthorId { get; set; }
    }
}
