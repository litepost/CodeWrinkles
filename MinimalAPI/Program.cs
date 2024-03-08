using Application;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SocialDbContext>(options => options.UseSqlite("Data source=MinimalAPICourse.db"));
builder.Services.AddScoped<IPostRepository, PostRepository>();
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/posts/{id}", async (IMediator mediator, int id) => 
{
    var getPost = new GetPostById { PostId = id };
    var post = await mediator.Send(getPost);
    return Results.Ok(post);
})
.WithName("GetPostById");

app.MapGet("api/posts", async (IMediator mediator) => 
{
    var getCommamd = new GetAllPosts();
    var posts = await mediator.Send(getCommamd);
    return Results.Ok(posts);
});

app.MapPost("/api/posts", async (IMediator mediator, Post post) => 
{
    var createPost = new CreatePost { PostContent = post.Content };
    var createdPost = await mediator.Send(createPost);
    // GetPostById references the route name above so they must match
    return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
});

app.MapPut("api/posts/{id}", async (IMediator mediator, Post post, int id) => 
{
    var updatePost = new UpdatePost { PostId = id, PostContent = post.Content };
    var updatedPost = await mediator.Send(updatePost);
    return Results.Ok(updatedPost);
});

app.MapDelete("api/posts/{id}", async (IMediator mediator, int id) => 
{
    var deletePost = new DeletePost { PostId = id };
    var deletedPost = await mediator.Send(deletePost);
    return Results.NoContent();
});

app.Run();
