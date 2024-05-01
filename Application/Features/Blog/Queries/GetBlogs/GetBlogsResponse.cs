using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Queries.GetBlogs
{
    public class GetBlogsResponse : BaseResponse
    {
        public List<BlogDto> Blogs { get; set; }
    }
}
