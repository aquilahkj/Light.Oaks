using System;

namespace Light.Oaks
{
    /// <summary>
    /// Cache agent.
    /// </summary>
    public interface ICacheAgent
    {
        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <returns>The cache.</returns>
        /// <param name="key">Key.</param>
        string GetCache(string key);
        /// <summary>
        /// Sets the cache.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <param name="expiry">Expiry.</param>
        void SetCache(string key, string value, TimeSpan expiry);
        /// <summary>
        /// Removes the cache.
        /// </summary>
        /// <param name="key">Key.</param>
        void RemoveCache(string key);
    }
}