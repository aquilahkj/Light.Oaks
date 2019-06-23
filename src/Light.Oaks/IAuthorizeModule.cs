namespace Light.Oaks
{
    /// <summary>
    /// Authorize repository.
    /// </summary>
    public interface IAuthorizeModule
    {
        VerifyInfo VerifyToken(string token);

        string CreateAuthorization(VerifyInfo erifyInfo);

        void RemoveAuthorize(string id);

        bool CheckAuthorize(string id);

        string ToeknName { get; }
    }
}