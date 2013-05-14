<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Kontrol.aspx.cs" Inherits="WebRole1.Kontrol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div>
        <table>
            <tr>
                <td>
                <asp:Label ID="Label1" runat="server" Text="Path"></asp:Label></td>
                <td><asp:TextBox ID="txtPath" runat="server" Width="621px"></asp:TextBox></td>
            </tr>
            <tr><td></td>
                <td>
                    <asp:Button ID="btnHesapla" runat="server" Text="Hesapla" OnClick="btnHesapla_Click" />
                </td>
            </tr>
                <tr><td></td>
                <td><asp:TextBox ID="txtSonuc" runat="server" Height="254px" Width="622px" TextMode="MultiLine"></asp:TextBox></td></tr>
        </table>
    </div>
</asp:Content>
