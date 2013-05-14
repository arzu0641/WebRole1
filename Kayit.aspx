<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Kayit.aspx.cs" Inherits="WebRole1.Kayit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
      <div>
   
        <table>
            <tr>
                <td><asp:Label ID="lblUsername" runat="server" Text="KullaniciAdi"></asp:Label></td>
                <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
          </tr>

            <tr>
                <td><asp:Label ID="lblPassword" runat="server" Text="Sifre"></asp:Label></td>
                <td><asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td><asp:Label ID="lblConfirmPass" runat="server" Text="Sifre Tekrarı"></asp:Label></td>
                <td><asp:TextBox ID="txtPassConfirm" runat="server" TextMode="Password"></asp:TextBox></td>
             </tr>
             
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Isim"></asp:Label></td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td><asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
            </tr>

             <tr>
                <td><asp:Label ID="lblHint" runat="server" Text="Hatirlatma Sorusu"></asp:Label></td>
                <td><asp:TextBox ID="txtHatirlatmaSoru" runat="server"></asp:TextBox></td>
              </tr>
              <tr>
                 <td><asp:Label ID="lblHatirlatmaCevap" runat="server" Text="HatirlatmaCevap"></asp:Label></td>
                 <td><asp:TextBox ID="txtHatirlatmaCevap" runat="server"></asp:TextBox></td>
                </tr>
            <tr>
                
                <td>

                    <asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx"/>
                </td>
                <td>
                    
                    <asp:TextBox ID="txtimgcode" runat="server"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td><asp:Button ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" /></td>
            </tr>
        </table>
    </div>

</asp:Content>
