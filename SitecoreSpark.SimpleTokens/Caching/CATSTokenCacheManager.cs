using Sitecore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Caching
{
    public static class CATSTokenCacheManager
    {
        private static CATSTokenCache _tokenCache;

        static CATSTokenCacheManager()
        {
            if (_tokenCache == null)
            {
                var cacheSize = Sitecore.StringUtil.ParseSizeString(Sitecore.Configuration.Settings.GetSetting("SitecoreSpark.CATS.CacheSize"));
                _tokenCache = new CATSTokenCache("CATS_TokenCache", cacheSize);
            }
        }

        public static string GetCache(string key)
        {
            if (_tokenCache.InnerCache.ContainsKey(key))
                return _tokenCache.GetString(key);

            // Cache miss: load from Sitecore (this might degrade page rendering performance)

            // TODO

            return string.Empty;
        }

        public static void SetCache(string key, string value)
        {
            // TODO: handle cache overflow possibility?
            // if (_tokenCache.InnerCache.RemainingSpace < System.Text.ASCIIEncoding.Unicode.GetByteCount(value))
            _tokenCache.SetString(key, value);

        }

        /// <summary>
        /// Initializes a token cache from the token library in Sitecore. Will clear the cache if it has values.
        /// </summary>
        public static void BuildCache()
        {
            if (_tokenCache.InnerCache.Count > 0)
                ClearCache();

            // TODO: get token library items from Sitecore

            // TODO: iterate over all tokens in each library; create cache entry for each token'

            // TODO: check token cache size; if larger than allowed max, throw an exception
        }

        /// <summary>
        /// Clears the CATS token cache.
        /// </summary>
        public static void ClearCache()
        {
            if (_tokenCache != null)
                _tokenCache.Clear();
        }
    }
}