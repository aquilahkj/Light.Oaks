namespace Light.Oaks
{
    /// <summary>
    /// Authorize repository.
    /// </summary>
    public interface IAuthorizeModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        VerifyInfo VerifyToken(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="erifyInfo"></param>
        /// <returns></returns>
        string CreateAuthorization(VerifyInfo erifyInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void RemoveAuthorize(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckAuthorize(string id);

        /// <summary>
        /// 
        /// </summary>
        string ToeknName { get; }
    }
}