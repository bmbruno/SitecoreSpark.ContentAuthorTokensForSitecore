using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using SitecoreSpark.CATS.Infrastructure;
using SitecoreSpark.CATS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Caching
{
    /// <summary>
    /// Support class for managing content token cache.
    /// </summary>
    public static class CATSTokenCacheManager
    {
        private static CATSTokenCache _tokenCache;

        /// <summary>
        /// The maximum size (in bytes) of the CAT token cache. Loaded from configuration.
        /// </summary>
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
            // Get token from cache first
            if (_tokenCache.InnerCache.ContainsKey(key))
                return _tokenCache.GetString(key);

            // CACHE MISS: load token from Sitecore (this will degrade page rendering performance).
            // This will almost never happen since the token source list must be loaded from cache in the first place (the local 'key' variable).
            IEnumerable<Item> libraries = TokenService.GetAllTokenLibraries();
            IEnumerable<ContentToken> tokens = TokenService.GetTokensFromLibraries(libraries);
            ContentToken token = tokens.FirstOrDefault(u => u.Pattern.Equals(key, StringComparison.Ordinal));

            if (token != null)
                return token.Output;

            // No token found: holy crap, someone screwed up
            Logger.Warn($"Fatal content token error: no token found in cache or the database for '{token.Pattern}'", typeof(CATSTokenCacheManager));
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
                return allKeys.Where(u => !u.StartsWith("_CATS_")).ToArray();

            return allKeys;
        }

        /// <summary>
        /// Sets a value to cache. Will throw an exception if the cache size exceeds its configuration-defined limit.
        /// </summary>
        /// <param name="key">Key to store in cache.</param>
        /// <param name="value">Value to store.</param>
        public static void SetCache(string key, string value)
        {
            if (_tokenCache.InnerCache.RemainingSpace < System.Text.ASCIIEncoding.Unicode.GetByteCount(value))
                throw new Exception($"SetCache will overflow cache size; key: {key}; value: {value}; value size: {System.Text.ASCIIEncoding.Unicode.GetByteCount(value)}");

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
            _tokenCache.SetString(SitecoreSpark.CATS.Infrastructure.Constants.CATS_Token_Start_Tag, TokenService.GetTokenStartTag());
            _tokenCache.SetString(SitecoreSpark.CATS.Infrastructure.Constants.CATS_Token_End_Tag, TokenService.GetTokenEndTag());

            // Get token library items from Sitecore
            IEnumerable<Item> libraries = TokenService.GetAllTokenLibraries();

            // Get tokens from all libraries
            IEnumerable<ContentToken> tokens = TokenService.GetTokensFromLibraries(libraries);

            // FUTURE: sanitize inbound tokens

            bool cacheOverflow = false;
            foreach (ContentToken token in tokens)
            {
                if (_tokenCache.GetString(token.Pattern) != null)
                    Logger.Warn($"Duplicate token found! Token pattern: {token.Pattern}", typeof(CATSTokenCacheManager));

                if (String.IsNullOrEmpty(token.Pattern))
                {
                    Logger.Warn($"Missing Pattern for token item ID {token.ItemID}; will not be cached.", typeof(TokenService));
                    continue;
                }

                if (String.IsNullOrEmpty(token.Output))
                {
                    Logger.Warn($"Missing Output for token item ID {token.ItemID}; will not be cached.", typeof(TokenService));
                    continue;
                }

                _tokenCache.SetString(token.Pattern, token.Output);

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