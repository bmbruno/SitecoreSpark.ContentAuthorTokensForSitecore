using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Infrastructure
{
    public static class Constants
    {
        public static readonly string[] CATS_ValidTokenFieldTypes = { "single-line text", "text", "rich text", "image", "general link" };

        public static readonly string CATS_Token_Start_Tag = "_CATS_TOKEN_START_TAG";

        public static readonly string CATS_Token_End_Tag = "_CATS_TOKEN_END_TAG";

        public static readonly string CATS_Configuration_Item_ID = "{B5AD5377-D589-486F-8712-30D3AAF2248B}";

        public static readonly string CATS_Token_Template_ID = "{91883B15-4F51-4219-9E71-47F90DFC1179}";

        public static readonly string CATS_TokenLibrary_Template_ID = "{BEEE1586-D37F-4637-9798-0DFFA496E0FB}";
    }
}