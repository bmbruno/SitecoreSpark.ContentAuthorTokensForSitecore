using Sitecore.Data.Items;
using Sitecore.sitecore.admin;
using SitecoreSpark.CATS.Caching;
using SitecoreSpark.CATS.Infrastructure;
using SitecoreSpark.CATS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected void Page_Load(object sender, EventArgs e){ }

        protected void btnGetTokenInfo_Click(object sender, EventArgs e)
        {
            IEnumerable<Item> libraries = TokenManager.GetAllTokenLibraries();
            StringBuilder sb = new StringBuilder();

            // Tokens in database
            IEnumerable<ContentToken> dbTokens = TokenManager.GetTokensFromLibraries(libraries).OrderBy(u => u.Pattern);

            sb.Clear();
            sb.Append($"Token Count: {dbTokens.Count()}\n\n");
            foreach (var token in dbTokens)
            {
                sb.Append($"{token.Pattern}\n");
            }

            txtDatabaseTokens.Text = sb.ToString();

            // Tokens in cache
            string[] cacheTokens = CATSTokenCacheManager.GetKeys(true).OrderBy(u => u).ToArray();

            sb.Clear();
            sb.Append($"Cache Count: {cacheTokens.Length}\n\n");
            foreach (var cacheItem in cacheTokens)
            {
                sb.Append($"{cacheItem}\n");
            }

            txtCacheTokens.Text = sb.ToString();
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

            lblStatus.Visible = true;
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

            lblStatus.Visible = true;
        }

    }
}