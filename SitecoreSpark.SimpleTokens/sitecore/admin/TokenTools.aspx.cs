using Sitecore.sitecore.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.sitecore.admin
{
    public partial class TokenTools : AdminPage
    {
        protected override void OnInit(EventArgs e)
        {
            CheckSecurity(true);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRebuildCache_Click(object sender, EventArgs e)
        {
            try
            {
                Caching.CATSTokenCacheManager.BuildCache();
                lblStatus.Text = "Cache rebuilt!";
            }
            catch (Exception exc)
            {
                lblStatus.Text = $"Exception rebuilding cache: {exc.ToString()}";
            }
        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            try
            {
                Caching.CATSTokenCacheManager.ClearCache();
                lblStatus.Text = "Cache cleared!";
            }
            catch (Exception exc)
            {
                lblStatus.Text = $"Exception clearing cache: {exc.ToString()}";
            }
        }

    }
}