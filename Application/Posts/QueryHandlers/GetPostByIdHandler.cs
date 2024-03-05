using Domain;
using MediatR;

namespace Application;

public class GetPostByIdHandler(IPostRepository postRepo) : IRequestHandler<GetPostById, Post>
{
    private readonly IPostRepository _postRepo = postRepo;

    public async Task<Post> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        return await _postRepo.GetPostById(request.PostId);
    }
}
