using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
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
            int tokensCached = 0;

            if (_tokenCache.InnerCache.Count > 0)
                ClearCache();

            // TODO: get token library items from Sitecore
            Item defaultLibrary = Sitecore.Data.Database.GetDatabase("master").GetItem(Constants.CATS_Default_Library_ID);

            if (defaultLibrary == null)
                throw new Exception($"No library item found with ID: {Constants.CATS_Default_Library_ID}");

            List<Item> tokens = defaultLibrary.Children.Where(u => u.TemplateID == new ID(Constants.CATS_Token_Template_ID)).ToList();
            
            // TODO: iterate over all tokens in each library; create cache entry for each token
            foreach (Item token in tokens)
            {
                string pattern = token["Pattern"];
                string value = token["Value"];

                bool validToken = true;
                if (String.IsNullOrEmpty(pattern))
                {
                    Sitecore.Diagnostics.Log.Warn($"[CATS] Missing pattern for token item {token.ID}; will not be cached or utilized.", token);
                    validToken = false;
                }

                if (String.IsNullOrEmpty(value))
                {
                    Sitecore.Diagnostics.Log.Warn($"[CATS] Missing value for token item {token.ID}; will not be cached or utilized.", token);
                    validToken = false;
                }

                if (validToken)
                {
                    SetCache(pattern, value);
                    tokensCached += 1;
                }
            }

            // TODO: (maybe?) check token cache size; if larger than allowed max, throw an exception

            Sitecore.Diagnostics.Log.Info($"[CATS] Content author tokens cache rebuilt. '{tokensCached}' tokens cached.", tokensCached);
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