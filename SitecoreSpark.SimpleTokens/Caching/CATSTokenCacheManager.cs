using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using SitecoreSpark.CATS.Infrastructure;
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

            // TODO: implement this

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

            // Get token library items from Sitecore
            IEnumerable<Item> libraries = TokenManager.GetAllTokenLibraries();

            // Get tokens from all libraries
            IEnumerable<CATS.Models.Token> tokens = TokenManager.GetTokensFromLibraries(libraries);

            foreach (CATS.Models.Token token in tokens)
            {
                _tokenCache.SetString(token.Pattern, token.Value);
            }

            // TODO: (maybe?) check token cache size; if larger than allowed max, throw an exception

            Sitecore.Diagnostics.Log.Info($"[CATS] Content author tokens cache rebuilt. '{tokens.Count()}' tokens cached.", tokens);
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