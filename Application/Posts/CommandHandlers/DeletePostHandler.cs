using Domain;
using MediatR;

namespace Application;

public class DeletePostHandler(IPostRepository postRepo) : IRequestHandler<DeletePost, Post>
{
    private readonly IPostRepository _postRepo = postRepo;

    public async Task<Unit> Handle(DeletePost request, CancellationToken cancellationToken)
    {
        await _postRepo.DeletePost(request.PostId);
        return Unit.Value;
    }

    Task<Post> IRequestHandler<DeletePost, Post>.Handle(DeletePost request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
