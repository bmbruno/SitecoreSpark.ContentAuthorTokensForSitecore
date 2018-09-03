using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Commands
{
    public class TokenList : Command
    {
        public override void Execute(CommandContext context)
        {
            SheerResponse.ShowModalDialog(new ModalDialogOptions("/sitecore/shell/Applications/ContentAuthorTokens/ViewTokens.aspx")
            {
                Width = "600",
                Height = "700",
                Response = false,
                ForceDialogSize = true
            });
        }
    }
}