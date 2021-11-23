using Web.Model;
using Web.Model.Common;
namespace Web.Services
{
    public interface IJwtAuthService
    {
        BaseResponse Authentication(UserCredential login);

        string Register(RegisterCredential register);
    }
}
