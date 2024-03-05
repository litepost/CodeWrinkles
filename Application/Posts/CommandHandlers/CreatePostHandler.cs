using Domain;
using MediatR;

namespace Application;

public class CreatePostHandler(IPostRepository postRepo) : IRequestHandler<CreatePost, Post>
{
    private readonly IPostRepository _postRepo = postRepo;

    public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        // no need to set createdAt and lastModified because it is done in PostRepository
        var newPost = new Post { Content = request.PostContent };

        return await _postRepo.CreatePost(newPost);
    }
}
