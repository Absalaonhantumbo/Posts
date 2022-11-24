using Aplication.Dtos;
using Doiman;

namespace Aplication.Interfaces;

public interface IPostRepository
{
    void Add(Post post);

    Task<int> Complete();
}