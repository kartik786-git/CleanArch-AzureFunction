using Application.Features.Blog.Commands.CreateBlog;
using Application.Features;
using Application.Features.Blog.Queries.GetBlogById;
using Application.Features.Blog.Queries.GetBlogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Blog.Commands.UpdateBlog;
using Application.Features.Blog.Commands.DeleteBlog;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ApiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<GetBlogsResponse>> GetBlogs()
        {
            var reponse = await Mediator.Send(new GetBlogsQuery());
            return reponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBlogByIdResponse>> GetById(int id)
        {
            var result = await Mediator.Send(new GetBlogByIdQuery() { Id = id });
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<CreateBlogResponse>> Create(BlogDto command)
        {
            var result = await Mediator.Send(new CreateBlogCommand() { CreateBlog = command });
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateBlogResponse>> Update(int id, BlogDto updatedBlog)
        {
            var result = await Mediator.Send(new UpdateBlogCommand() { Id = id, BlogUpdate = updatedBlog });

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteBlogResponse>> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteBlogCommand { Id = id });

            return result;
        }
    }
}
