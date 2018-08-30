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

        private static long _cacheMaxSize
        {
            get
            {
                return Sitecore.StringUtil.ParseSizeString(Sitecore.Configuration.Settings.GetSetting("SitecoreSpark.CATS.CacheSize"));
            }
        }

        static CATSTokenCacheManager()
        {
            if (_tokenCache == null)
            {
                _tokenCache = new CATSTokenCache("CATS_TokenCache", _cacheMaxSize);
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

        /// <summary>
        /// Gets an array of all keys in the cache.
        /// </summary>
        /// <returns>String array of cache keys.</returns>
        public static string[] GetKeys()
        {
            return _tokenCache.InnerCache.GetCacheKeys();
        }

        /// <summary>
        /// Gets an array of all keys in the cache, with an option to exclude CATS-specific tokens.
        /// </summary>
        /// <param name="onlyUserTokens">Determines if only user tokens should be returned.</param>
        /// <returns>String array of cache keys, with or without CATS cached-items.</returns>
        public static string[] GetKeys(bool onlyUserTokens)
        {
            string[] allKeys = GetKeys();

            if (onlyUserTokens)
                return allKeys.Where(u => !u.StartsWith("CATS_")).ToArray();

            return allKeys;
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
            if (!IsCacheEmpty())
                ClearCache();

            // Cache token delimiters
            _tokenCache.SetString(SitecoreSpark.CATS.Infrastructure.Constants.CATS_Token_Start_Tag, TokenManager.GetTokenStartTag());
            _tokenCache.SetString(SitecoreSpark.CATS.Infrastructure.Constants.CATS_Token_End_Tag, TokenManager.GetTokenEndTag());

            // Get token library items from Sitecore
            IEnumerable<Item> libraries = TokenManager.GetAllTokenLibraries();

            // Get tokens from all libraries
            IEnumerable<CATS.Models.Token> tokens = TokenManager.GetTokensFromLibraries(libraries);

            // FUTURE: sanitize inbound tokens

            bool cacheOverflow = false;
            foreach (CATS.Models.Token token in tokens)
            {
                if (_tokenCache.GetString(token.Pattern) != null)
                    Logger.Info($"Duplicate token found! Token pattern: {token.Pattern}", typeof(CATSTokenCacheManager));

                _tokenCache.SetString(token.Pattern, token.Value);

                if (_tokenCache.InnerCache.Size >= _cacheMaxSize)
                    cacheOverflow = true; 
            }

            if (cacheOverflow)
                Logger.Info($"Max cache size exceeded. This may cause page rendering performance problems. If possible, increase the size of 'SitecoreSpark.CATS.CacheSize'. Size/Max: {_tokenCache.InnerCache.Size}/{_cacheMaxSize}", typeof(CATSTokenCacheManager));

            Logger.Info($"Content author tokens cache rebuilt. {tokens.Count()} tokens cached.", typeof(CATSTokenCacheManager));
        }

        /// <summary>
        /// Clears the CATS token cache.
        /// </summary>
        public static void ClearCache()
        {
            if (_tokenCache != null)
                _tokenCache.Clear();
        }

        /// <summary>
        /// Determines if the token cache is empty.
        /// </summary>
        /// <returns>True/false.</returns>
        public static bool IsCacheEmpty()
        {
            return (_tokenCache.InnerCache.Count == 0);
        }
    }
}