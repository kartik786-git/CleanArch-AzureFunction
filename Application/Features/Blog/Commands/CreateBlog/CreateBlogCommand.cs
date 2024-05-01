using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogCommand : IRequest<CreateBlogResponse>
    {
        public BlogDto CreateBlog { get; set; }
    }
}
