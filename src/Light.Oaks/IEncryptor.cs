namespace Light.Oaks
{
    /// <summary>
    /// Encryptor.
    /// </summary>
    public interface IEncryptor
    {
        /// <summary>
        /// Encrypt the specified content.
        /// </summary>
        /// <returns>The encrypt.</returns>
        /// <param name="content">Content.</param>
        string Encrypt(string content);
        /// <summary>
        /// Decrypt the specified content.
        /// </summary>
        /// <returns>The decrypt.</returns>
        /// <param name="content">Content.</param>
        string Decrypt(string content);
    }
}