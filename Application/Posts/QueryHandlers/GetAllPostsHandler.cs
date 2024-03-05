using Domain;
using MediatR;

namespace Application;

public class GetAllPostsHandler(IPostRepository postRepo) : IRequestHandler<GetAllPosts, ICollection<Post>>
{
    private readonly IPostRepository _postRepo = postRepo;

    public async Task<ICollection<Post>> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        return await _postRepo.GetAllPosts();
    }
}
