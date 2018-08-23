using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Infrastructure.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// Retreives an array of the item IDs assigned to a Treelist field.
        /// </summary>
        /// <param name="item">Item to extend.</param>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Array of item IDs.</returns>
        public static string[] GetTreelistValuesRaw(this Item item, string fieldName)
        {
            MultilistField treelist = item.Fields[fieldName];
            return treelist.Items;
        }
    }
}