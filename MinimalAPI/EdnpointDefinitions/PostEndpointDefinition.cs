
using Application;
using Domain;
using MediatR;

namespace MinimalAPI;

public class PostEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/posts");

        posts.MapGet("/{id}", GetPostById).WithName("GetPostById");
        
        posts.MapGet("/", GetAllPosts);
        
        posts.MapPost("/", CreatePost);
        
        posts.MapPut("/{id}", UpdatePost);
        
        posts.MapDelete("/{id}", DeletePost);
    }

    private async Task<IResult> GetPostById(IMediator mediator, int id) {
        var getPost = new GetPostById { PostId = id };
        var post = await mediator.Send(getPost);
        return TypedResults.Ok(post);
    }

    private async Task<IResult> GetAllPosts(IMediator mediator) {
        var getCommamd = new GetAllPosts();
        var posts = await mediator.Send(getCommamd);
        return Results.Ok(posts);
    }

    private async Task<IResult> CreatePost(IMediator mediator, Post post) {
        var createPost = new CreatePost { PostContent = post.Content };
        var createdPost = await mediator.Send(createPost);
        // GetPostById references the route name above so they must match
        return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
    }

    private async Task<IResult> UpdatePost(IMediator mediator, Post post, int id) {
        var updatePost = new UpdatePost { PostId = id, PostContent = post.Content };
        var updatedPost = await mediator.Send(updatePost);
        return TypedResults.Ok(updatedPost);
    }

    private async Task<IResult> DeletePost(IMediator mediator, int id) {
        var deletePost = new DeletePost { PostId = id };
        var deletedPost = await mediator.Send(deletePost);
        return TypedResults.NoContent();
    } 
}
