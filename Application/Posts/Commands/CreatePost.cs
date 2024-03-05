using Domain;
using MediatR;

namespace Application;

public class CreatePost : IRequest<Post>
{
    public string? PostContent { get; set; }
}
