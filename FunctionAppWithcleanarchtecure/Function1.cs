using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Features.Blog.Commands.CreateBlog;
using Application.Features;
using Application.Features.Blog.Queries.GetBlogById;
using Application.Features.Blog.Queries.GetBlogs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Application.Features.Blog.Commands.UpdateBlog;
using Application.Features.Blog.Commands.DeleteBlog;

namespace FunctionAppWithcleanarchtecure
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IMediator _mediator;

        public Function1(ILogger<Function1> log, IMediator mediator)
        {
            _logger = log;
            _mediator = mediator;
        }

        [FunctionName("GetBlogs")]
        [OpenApiOperation(operationId: "GetBlogs", tags: new[] { "blogs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetBlogs(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = await _mediator.Send(new GetBlogsQuery());
            return new OkObjectResult(response);
        }

        [FunctionName("GetBlogById")]
        [OpenApiOperation(operationId: "GetBlogById", tags: new[] { "blogs" })]
        [OpenApiParameter(name: "Id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetBlogById/{Id}")] HttpRequest req, int Id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = await _mediator.Send(new GetBlogByIdQuery() { Id = Id });

            return new OkObjectResult(response);
        }

        [FunctionName("PostBlog")]
        [OpenApiOperation(operationId: "PostBlog", tags: new[] { "blogs" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(BlogDto), Description = "Parameters", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> PostBlog(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)

        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string reuestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var blogModel = System.Text.Json.JsonSerializer.Deserialize<BlogDto>(reuestBody, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var response = await _mediator.Send(new CreateBlogCommand() { CreateBlog = blogModel });

            return response.Success ? new OkObjectResult(response) : new BadRequestObjectResult(response);
        }

        [FunctionName("PutBlog")]
        [OpenApiOperation(operationId: "PutBlog", tags: new[] { "blogs" })]
        [OpenApiParameter(name: "Id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Id** parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(BlogDto), Description = "Parameters", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> PutBlog(
           [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "PutBlogs/{Id}")] HttpRequest req, int Id)

        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string reuestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var blogModel = System.Text.Json.JsonSerializer.Deserialize<BlogDto>(reuestBody,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var response = await _mediator.Send(new UpdateBlogCommand() { Id = Id, BlogUpdate = blogModel });

            return response.Success ? new OkObjectResult(response) : new BadRequestObjectResult(response);
        }

        [FunctionName("DeleteBlog")]
        [OpenApiOperation(operationId: "DeleteBlog", tags: new[] { "blogs" })]
        [OpenApiParameter(name: "Id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> DeleteBlog(
          [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteBlogs/{Id}")] HttpRequest req, int Id)

        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string reuestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var response = await _mediator.Send(new DeleteBlogCommand() { Id = Id });

            return new OkObjectResult(response);
        }
    }
}

