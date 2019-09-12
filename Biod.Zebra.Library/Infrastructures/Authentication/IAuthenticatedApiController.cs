namespace Biod.Zebra.Library.Infrastructures.Authentication
{
    public interface IAuthenticatedApiController
    {
        void SetCurrentUserId(string userId);

        void SetCurrentUserName(string userName);
    }
}
