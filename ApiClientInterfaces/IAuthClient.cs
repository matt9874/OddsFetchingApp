using OddsFetchingEntities;

namespace ApiClientInterfaces
{
    public interface IAuthClient
    {
        LoginResponse DoLogin();
    }
}
