using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
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
            Caching.CATSTokenCacheManager.BuildCache();
        }
    }
}