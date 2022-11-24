using Doiman;

namespace Aplication.Interfaces;

public interface IUserNamesList
{
    public List<string> GetUserNames(List<User> users);
}