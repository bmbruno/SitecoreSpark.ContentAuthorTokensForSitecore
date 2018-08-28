<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TokenTools.aspx.cs" Inherits="SitecoreSpark.CATS.sitecore.admin.TokenTools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Database Cleanup</title>
        <link rel="Stylesheet" type="text/css" href="/sitecore/shell/themes/standard/default/WebFramework.css" />
        <link rel="Stylesheet" type="text/css" href="./default.css" />

        <style>
            .status { display: block; margin: 1em 0; padding: 1em; border: 1px solid #660; color: #660; background-color: #FFA; }
            .token-detail { width: 100%; height: 300px; font-family: monospace; }
        </style>

    </head>
    <body>
        <form id="formCATS" runat="server" class="wf-container">
            <div class="wf-content">
                <h1>
                    <a href="/sitecore/admin/">Administration Tools</a> - Content Author Token Tools
                </h1>

                <br />

                <div class="root">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>

                    <asp:Label runat="server" ID="lblStatus" CssClass="status" Visible="false"></asp:Label>

                    <h3>Tokens (Database vs Cache)</h3>
                    <p>This tool lets you view all tokens defined in the Sitecore content database and tokens defined in cache. Use this to help debug issues when tokens are not being replaced during page rendering.</p>
                    
                    <asp:Button runat="server" ID="btnGetTokenInfo" OnClick="btnGetTokenInfo_Click" Text="Get Token Info" />

                    <br />

                    <table style="width: 100%;">
                        <tr>
                            <td class="top" style="width: 50%; padding-right: 16px;">
                                
                                <h3>Database</h3>
                                <p>Tokens defined in the 'master' database.</p>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDatabaseTokens" ReadOnly="true" CssClass="token-detail"></asp:TextBox>
                                
                            </td>
                            <td class="top" style="width: 50%;">
                                
                                <h3>Cache</h3>
                                <p>Tokens in cache.</p>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtCacheTokens" ReadOnly="true" CssClass="token-detail"></asp:TextBox>
                                
                            </td>
                        </tr>
                    </table>
                    
                    <br /><br />

                    <div>
                        <h3>Actions</h3>
                        <p>Clear and rebuild the token cache.</p>
                        <asp:Button runat="server" ID="btnRebuildCache" OnClick="btnRebuildCache_Click" Text="Rebuild Token Cache" />

                        <p>Clear the token cache. This can also be done from <a href="/sitecore/admin/cache.aspx">/sitecore/admin/cache.aspx</a></p>
                        <asp:Button runat="server" ID="btnClearCache" OnClick="btnClearCache_Click" Text="Clear Token Cache" />
                        <br />

                    </div>

                </div>
            </div>
        </form>
    </body>
</html>
