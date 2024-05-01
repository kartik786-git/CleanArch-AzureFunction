using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogResponse : BaseResponse
    {
        public int BlogId { get; set; }
    }
}
