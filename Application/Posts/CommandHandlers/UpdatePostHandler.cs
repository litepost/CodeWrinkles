using Domain;
using MediatR;

namespace Application;

public class UpdatePostHandler(IPostRepository postRepo) : IRequestHandler<UpdatePost, Post>
{
    private readonly IPostRepository _postRepo = postRepo;

    public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
    {
        return await _postRepo.UpdatePost(updatedContent: request.PostContent!, postId: request.PostId);
    }
}
