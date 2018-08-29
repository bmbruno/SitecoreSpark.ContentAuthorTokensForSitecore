using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// https://sitecore.stackexchange.com/questions/5945/how-do-a-open-a-speak-dialog-or-dashboard-application-from-within-code

namespace SitecoreSpark.CATS.Commands
{
    public class TokenList : Command
    {
        public override void Execute(CommandContext context)
        {
            SheerResponse.ShowModalDialog(new ModalDialogOptions("/sitecore/admin/TokenTools.aspx")
            {
                Width = "600",
                Height = "700",
                Response = false,
                ForceDialogSize = true
            });
        }
    }
}