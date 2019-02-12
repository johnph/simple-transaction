namespace Transaction.WebApi.Services
{
    using Transaction.WebApi.Models;

    public interface IIdentityService
    {
        IdentityModel GetIdentity();
    }
}
