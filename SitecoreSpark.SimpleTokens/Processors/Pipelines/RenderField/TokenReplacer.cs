using Sitecore.Diagnostics;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Processors.Pipelines.RenderField
{

    // Sitecore.Configuration.Settings.GetSetting("SitecoreSpark.SPRK.LogFolder")

    public class TokenReplacer
    {
        public void Process(RenderFieldArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (Sitecore.Context.PageMode.IsExperienceEditor)
                return;

            // TODO: Replace token
            if (args.FieldValue.Contains("{{"))
            {
                args.Result.FirstPart = args.FieldValue.Replace("{{TOKEN}}", "NEW VALUE!");
            }
        }
    }
}