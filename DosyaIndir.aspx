<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DosyaIndir.aspx.cs" Inherits="WebRole1.DosyaIndir" %>
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
            <td><asp:Label ID="Label3" runat="server" Text="Sifre"></asp:Label></td>
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
            <asp:Button ID="btnSifreCoz" runat="server" Text="Indir" OnClick="btnSifreCoz_Click" /></td>
            
            <td>&nbsp;</td>
          </tr>
         </table>
    </div>


</asp:Content>
