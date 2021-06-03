using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using SitecoreSpark.CATS.Infrastructure;

namespace SitecoreSpark.CATS.Handlers
{
    public class UpdateTokens
    {
        public void PublishEndRemote(object sender, EventArgs args)
        {
            try
            {
                Caching.CATSTokenCacheManager.ClearCache();
                Caching.CATSTokenCacheManager.BuildCache();
            }
            catch (Exception exc)
            {
                Logger.Error($"Exception during publish:end:remote caching of content author tokens: {exc.Message.ToString()}", this);
            }
        }
    }
}