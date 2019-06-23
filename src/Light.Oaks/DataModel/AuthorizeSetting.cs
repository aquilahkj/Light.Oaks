using System;
namespace Light.Oaks
{
    /// <summary>
    /// Authorize settings.
    /// </summary>
    public class AuthorizeSetting
    {
        /// <summary>
        /// Gets or sets the type of the cache.
        /// </summary>
        /// <value>The type of the cache.</value>
        public string CacheType { get; set; }

        /// <summary>
        /// Gets or sets the redis config.
        /// </summary>
        /// <value>The redis config.</value>
        public string RedisConfig { get; set; }

        /// <summary>
        /// Gets or sets the token key.
        /// </summary>
        /// <value>The token key.</value>
        public string TokenKey { get; set; }

        /// <summary>
        /// Gets or sets the cache expiry.
        /// </summary>
        /// <value>The cache expiry.</value>
        public int? Expiry { get; set; }

        /// <summary>
        /// Gets or sets the test mode.
        /// </summary>
        /// <value>The test mode.</value>
        public bool? TestMode { get; set; }

    }
}
