﻿using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.Publish;
using SitecoreSpark.CATS.Infrastructure;
using System;

namespace SitecoreSpark.CATS.Processors.Pipelines.Publish
{
    public class RebuildTokenCache
    {
        public void Process(PublishContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));

            try
            {
                Caching.CATSTokenCacheManager.ClearCache();
                Caching.CATSTokenCacheManager.BuildCache();
            }
            catch (Exception exc)
            {
                Logger.Error($"Exception during publish caching of content author tokens: {exc.Message.ToString()}", this);
            }

        }
    }
}