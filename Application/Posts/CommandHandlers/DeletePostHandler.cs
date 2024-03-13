using Domain;
using MediatR;

namespace Application;

public class DeletePostHandler: IRequestHandler<DeletePost>
{
    private readonly IPostRepository _postRepo;

    public DeletePostHandler(IPostRepository postRepo) {
        _postRepo = postRepo;
    }

    public async Task<Unit> Handle(DeletePost request, CancellationToken cancellationToken)
    {
        await _postRepo.DeletePost(request.PostId);
        return Unit.Value;
    }

    Task IRequestHandler<DeletePost>.Handle(DeletePost request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // Task IRequestHandler<DeletePost>.Handle(DeletePost request, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }
}
