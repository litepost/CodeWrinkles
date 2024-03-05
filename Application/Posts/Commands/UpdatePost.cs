using Domain;
using MediatR;

namespace Application;

public class UpdatePost : IRequest<Post>
{
    public int PostId { get; set; }
    public string? PostContent { get; set; }
}
