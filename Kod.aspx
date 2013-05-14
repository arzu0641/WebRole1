<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Kod.aspx.cs" Inherits="WebRole1.Kod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
    
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblMail" runat="server" Text="Mail"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="lblKullaniciAdi" runat="server" Text="KullaniciAdi"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtKullaniciAdi" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSifre" runat="server" Text="Sifre"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSifre" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
</table>
          <table>
                <tr>
                    <td>
                        
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="213px">
                            <asp:ListItem Selected="True">MD5</asp:ListItem>
                            <asp:ListItem>SHA1</asp:ListItem>
                            <asp:ListItem>SHA256</asp:ListItem>
                            <asp:ListItem>SHA384</asp:ListItem>
                            <asp:ListItem>SHA512</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>

                </tr>

          </table>

        <table>      
                <tr>
                    <td>
                        <asp:Button ID="btnKod" runat="server" Text="KodOlustur" OnClick="btnKod_Click" />
                    </td>

                </tr>
                <tr>
                    <td>
                    <asp:TextBox ID="txtKod" runat="server" Width="602px" Height="31px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </table>
        </div>
    
    </div>

</asp:Content>
