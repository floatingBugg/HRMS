using Web.Model.Common;
namespace Web_API
{
    public interface IJwtAuthService
    {
        string Authentication(UserCredential login);

        string Register(RegisterCredential register);
    }
}
