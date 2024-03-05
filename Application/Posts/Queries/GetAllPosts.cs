using Domain;
using MediatR;

namespace Application;

public class GetAllPosts : IRequest<ICollection<Post>>
{

}
