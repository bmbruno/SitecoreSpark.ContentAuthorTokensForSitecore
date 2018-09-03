using Sitecore.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Caching
{
    /// <summary>
    /// Implementation of a basic Sitecore cache for CATS; based on CustomCache class.
    /// </summary>
    public class CATSTokenCache : CustomCache
    {
        public CATSTokenCache(string name, long maxSize) : base(name, maxSize) { }

        public new void SetString(string key, string value)
        {
            base.SetString(key, value);
        }

        public new string GetString(string key)
        {
            return base.GetString(key);
        }
    }
}