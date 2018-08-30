using Sitecore;
using Sitecore.Data.Items;
using Sitecore.sitecore.admin;
using Sitecore.Web;
using SitecoreSpark.CATS.Caching;
using SitecoreSpark.CATS.Infrastructure;
using SitecoreSpark.CATS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace SitecoreSpark.CATS.sitecore.admin
{
    public partial class ViewTokens : Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (!Sitecore.Context.User.IsAuthenticated)
            {
                HttpContext.Current.Session["SC_LOGIN_REDIRECT"] = (object)WebUtil.GetRawUrl();
                WebUtil.RedirectToLoginPage();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IEnumerable<Item> libraries = TokenManager.GetAllTokenLibraries();
            StringBuilder sb = new StringBuilder();

            // Tag info
            string startTag = TokenManager.GetTokenStartTag();
            string endTag = TokenManager.GetTokenEndTag();

            litTokenExample.Text = $"{startTag}TOKEN{endTag}";

            // Tokens in database
            IEnumerable<ContentToken> dbTokens = TokenManager.GetTokensFromLibraries(libraries).OrderBy(u => u.Pattern);

            rptTokenList.DataSource = dbTokens;
            rptTokenList.DataBind();
        }
    }
}