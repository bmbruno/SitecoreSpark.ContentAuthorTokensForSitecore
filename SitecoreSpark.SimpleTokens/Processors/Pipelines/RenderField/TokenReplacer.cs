using Sitecore.Diagnostics;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SitecoreSpark.CATS.Infrastructure;

/* 
 * A Note on Performance:
 * 
 * String.Replace(string, string) is used for token replacement. While StringBuilder.Replace(string, string) is considerably more memory-efficient
 * and quicker for a larger number of replacements, String.Replace() works just fine for a small number of strings (<20 per page). If you need to
 * tokenize a large amount of content across many pages, you may want to consider changing this to use StringBuilder.Replace().
 * 
 * A Note on Casing:
 * 
 * Tokens are case-sensitive because String.Replace(string, string) is case-sensitive. Other possible replacements, such as RegEx, would allow for
 * case-insensitive strings, but as stated above, String.Replace() is used for raw performance reasons.
 * 
 */

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
            // HashSet.Contains() is a native, highly-optimzed method; preferred over the array Contains() extension method
            HashSet<string> validTokenFields = new HashSet<string>(Constants.CATS_ValidTokenFieldTypes);

            if (validTokenFields.Contains(args.FieldTypeKey))
            {
                try
                {
                    // Set up metadata and keys
                    string startTag = Caching.CATSTokenCacheManager.GetCache(Constants.CATS_Token_Start_Tag);
                    string endTag = Caching.CATSTokenCacheManager.GetCache(Constants.CATS_Token_End_Tag);
                    string[] allKeys = Caching.CATSTokenCacheManager.GetKeys();

                    // Replace token (if token-like pattern is found)
                    if (args.FieldValue.Contains(startTag) && args.FieldValue.Contains(endTag))
                    {
                        string result = args.Result.FirstPart;
                        string pattern = string.Empty;

                        // Iterate over cached tokens, check for replacements
                        foreach (string key in allKeys)
                        {
                            pattern = string.Concat(startTag, key, endTag);

                            string newValue = Caching.CATSTokenCacheManager.GetCache(key);
                            result = result.Replace(pattern, newValue);
                        }

                        args.Result.FirstPart = result.ToString();
                    }
                }
                catch (Exception exc)
                {
                    Logger.Error($"Exception while rendering a content author token: {exc.Message.ToString()}", this);
                }
            }
        }
    }
}