using Sitecore.Diagnostics;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SitecoreSpark.CATS.Infrastructure;

namespace SitecoreSpark.CATS.Processors.Pipelines.RenderField
{
    public class TokenReplacer
    {
        public void Process(RenderFieldArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (Sitecore.Context.PageMode.IsExperienceEditor)
                return;

            // Only operate on certain field types
            // TODO: how performant is this?
            if (Constants.CATS_ValidTokenFieldTypes.Contains(args.FieldTypeKey))
            {
                // Set up metadata
                string startTag = Caching.CATSTokenCacheManager.GetCache("CATS_TOKEN_START_TAG");
                string endTag = Caching.CATSTokenCacheManager.GetCache("CATS_TOKEN_END_TAG");

                // TODO: design a better algorithm
                // NOTE: option 1) iterate over all cached keys and replace
                // NOTE: option 2) check if tokens exist in content, if so, grab cache by key
                string[] allKeys = Caching.CATSTokenCacheManager.GetKeys();

                // Replace token (if token-like pattern is found)c
                if (args.FieldValue.Contains(startTag) && args.FieldValue.Contains(endTag))
                {
                    StringBuilder result = new StringBuilder(args.Result.FirstPart);

                    string pattern = string.Empty;

                    // Iterate over cached tokens, check for replacements
                    foreach (string key in allKeys)
                    {
                        pattern = $"{startTag}{key}{endTag}";

                        string newValue = Caching.CATSTokenCacheManager.GetCache(key);
                        result.Replace(pattern, newValue);
                    }

                    args.Result.FirstPart = result.ToString();
                }
            }
        }
    }
}