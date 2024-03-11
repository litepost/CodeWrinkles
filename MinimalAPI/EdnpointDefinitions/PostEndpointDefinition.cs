
using Application;
using Domain;
using MediatR;

namespace MinimalAPI;

public class PostEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/posts");

        posts.MapGet("/{id}", async (IMediator mediator, int id) => 
        {
            var getPost = new GetPostById { PostId = id };
            var post = await mediator.Send(getPost);
            return Results.Ok(post);
        })
        .WithName("GetPostById");
        
        posts.MapGet("/", async (IMediator mediator) => 
        {
            var getCommamd = new GetAllPosts();
            var posts = await mediator.Send(getCommamd);
            return Results.Ok(posts);
        });
        
        posts.MapPost("/", async (IMediator mediator, Post post) => 
        {
            var createPost = new CreatePost { PostContent = post.Content };
            var createdPost = await mediator.Send(createPost);
            // GetPostById references the route name above so they must match
            return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
        });
        
        posts.MapPut("/{id}", async (IMediator mediator, Post post, int id) => 
        {
            var updatePost = new UpdatePost { PostId = id, PostContent = post.Content };
            var updatedPost = await mediator.Send(updatePost);
            return Results.Ok(updatedPost);
        });
        
        posts.MapDelete("/{id}", async (IMediator mediator, int id) => 
        {
            var deletePost = new DeletePost { PostId = id };
            var deletedPost = await mediator.Send(deletePost);
            return Results.NoContent();
        });
    }
}
