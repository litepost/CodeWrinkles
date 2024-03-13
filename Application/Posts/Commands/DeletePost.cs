using MediatR;

namespace Application;

public class DeletePost : IRequest
{
    public int PostId { get; set; }
}
