using Domain;
using MediatR;

namespace Application;

public class GetPostById : IRequest<Post>
{
    public int PostId { get; set; }
}
