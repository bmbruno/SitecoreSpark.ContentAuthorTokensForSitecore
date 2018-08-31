<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewTokens.aspx.cs" Inherits="SitecoreSpark.CATS.sitecore.admin.ViewTokens" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">

        <title>Token List</title>

        <style>
            body { background-color: #FFF; margin: 0; padding: 1em; font-family: Arial, sans-serif; }
            h1 { font-size: 2em; font-weight: bold; }
            .output { font-family: monospace; }

            table { border-collapse: collapse; }
            table td { border: 1px solid #000; padding: 0.5em; }
            table th { padding: 0.5em; }
        </style>

    </head>
    <body style>
        <form id="formCATS" runat="server">
          
            <div class="token-list">
                <h1>Token List</h1>
                <p>Available content tokens are listed below. To use a token, follow this format:</p>
                <p style="text-align: center;"><asp:Literal runat="server" ID="litTokenExample" /></p>
                <p>Tokens may be used in the following field types:<br />Single-Line Text, Text, Rich Text, Images, and General Link.</p>

                <asp:Repeater runat="server" ID="rptTokenList">
                    <HeaderTemplate>
                        <table>
                            <thead>
                                <tr>
                                    <th>Token</th>
                                    <th>Output</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Pattern")%></td>
                            <td class="output"><%#:Eval("Output")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

        </form>
    </body>
</html>
