using Domain;
using MediatR;

namespace Application;

public class DeletePostHandler(IPostRepository postRepo) : IRequestHandler<DeletePost>
{
    private readonly IPostRepository _postRepo = postRepo;

    async Task IRequestHandler<DeletePost>.Handle(DeletePost request, CancellationToken cancellationToken)
    {
        await _postRepo.DeletePost(request.PostId);
    }

}
