<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DosyaYukle.aspx.cs" Inherits="WebRole1.DosyaYukle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div>
     <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Kaynak"></asp:Label></td>
            <td><asp:FileUpload ID="FileUpload1" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Sifre"></asp:Label></td>
            <td><asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
        </tr>
         <tr>
          <td>
                 <asp:RadioButtonList ID="rblAlgoritma" runat="server">
                     <asp:ListItem>Aes</asp:ListItem>
                     <asp:ListItem Selected="True">Des</asp:ListItem>
                 </asp:RadioButtonList>
           </td>  
         </tr>
         <tr>
             <td>
            <asp:Button ID="btnSifrele" runat="server" Text="Yukle" OnClick="btnSifrele_Click" /></td>
            
            <td>&nbsp;</td>
          </tr>
         </table>
</asp:Content>
