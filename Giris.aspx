<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Giris.aspx.cs" Inherits="WebRole1.Giris" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div>
        
    <table>
        <tr>
            <td><asp:Label ID="lblKullaniciAdi" runat="server" Text="KullaniciAdi"></asp:Label></td>
            <td><asp:TextBox ID="txtKullaniciAdi" runat="server"></asp:TextBox></td>
        </tr>
       
        <tr>
            <td><asp:Label ID="lblSifre" runat="server" Text="Sifre"></asp:Label></td>
            <td><asp:TextBox ID="txtSifre" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblKullaniciKod" runat="server" Text="KullaniciKod"></asp:Label></td>
            <td><asp:TextBox ID="txtKullaniciKod" runat="server"></asp:TextBox></td>
            
        </tr>
        
         <tr><td></td><td><asp:Button ID="btnLogin" runat="server" Text="Giris" OnClick="btnLogin_Click" /></td></tr>
    </table>
    </div>

</asp:Content>
