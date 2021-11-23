using Web.Model.Common;
namespace Web.Services
{
    public interface IJwtAuthService
    {
        string Authentication(UserCredential login);

        string Register(RegisterCredential register);
    }
}
