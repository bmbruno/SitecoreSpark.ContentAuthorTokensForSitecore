using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using SitecoreSpark.CATS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Processors.Pipelines.Initialize
{
    public class InitCATS
    {
        public void Process(PipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            try
            {
                Caching.CATSTokenCacheManager.BuildCache();
            }
            catch (Exception exc)
            {
                Logger.Error($"Exception during initialization caching of content author tokens: {exc.Message.ToString()}", this);
            }
        }
    }
}