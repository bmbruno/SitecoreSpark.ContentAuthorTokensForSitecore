<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TokenTools.aspx.cs" Inherits="SitecoreSpark.CATS.sitecore.admin.TokenTools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Database Cleanup</title>
        <link rel="Stylesheet" type="text/css" href="/sitecore/shell/themes/standard/default/WebFramework.css" />
        <link rel="Stylesheet" type="text/css" href="./default.css" />
    </head>
    <body>
        <form id="formCATS" runat="server" class="wf-container">
            <div class="wf-content">
                <h1>
                    <a href="/sitecore/admin/">Administration Tools</a> - Content Author Token Tools
                </h1>

                <br/>

                <div class="root">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>

                    <h3>Tokens (Database vs Cache)</h3>
                    <table>
                        <tr>
                            <td class="top">
                                <div class="chunk">
                                    <h3>Database</h3>
                                    <asp:CheckBoxList runat="server" ID="databaseList" />
                                </div>
                            </td>
                            <td class="top">
                                <div class="chunk">
                                    <h3>Cache</h3>
                                    <asp:CheckBoxList runat="server" ID="taskList" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    
                    <div style="clear:both;"></div>
                    
                    <div>
                        <h3>Actions</h3>
                        <asp:Button runat="server" ID="btnRebuildCache" OnClick="btnRebuildCache_Click" Text="Rebuild Token Cache" />
                        <asp:Button runat="server" ID="btnClearCache" OnClick="btnClearCache_Click" Text="Clear Token Cache" />
                        
                        <h3>Status</h3>
                        <asp:Label runat="server" ID="lblStatus"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" ID="log" TextMode="MultiLine" Rows="40" Style="width:100%;" ReadOnly="true"></asp:TextBox>

                    </div>


                </div>
            </div>
        </form>
    </body>
</html>
