<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SpkSorgu.aspx.cs" Inherits="WebRole1.SpkSorgu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblFonKod" runat="server" Text="FonKodu"></asp:Label> 
                </td>
                <td>
                    <asp:TextBox ID="txtFonKod" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSorgula" runat="server" Text="Sorgula" OnClick="btnSorgula_Click" />
                </td>

            </tr>
            <tr><td></td>
                <td>
                    <asp:TextBox ID="txtSonuc" runat="server" Height="317px" TextMode="MultiLine" Width="612px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
