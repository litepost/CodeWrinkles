using Domain;
using MediatR;

namespace Application;

public class DeletePost : IRequest<Post>
{
    public int PostId { get; set; }
}
