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
            var sitecoreArgs = args as Sitecore.Data.Events.PublishEndRemoteEventArgs;
            if (sitecoreArgs == null)
                return;

            Item rootItem = Factory.GetDatabase("web").GetItem(new ID(sitecoreArgs.RootItemId));

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