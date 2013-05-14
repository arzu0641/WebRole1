<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="KimlikSorgu.aspx.cs" Inherits="WebRole1.KimlikSorgu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
        <table>
            <tr>
                <td>
                <asp:Label ID="lblKimlik" runat="server" Text="Kimlik No"></asp:Label></td>
                <td>
                     <asp:TextBox ID="txtTcKimlik" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDogum" runat="server" Text="Dogum Yili"></asp:Label></td>
                <td>
                      <asp:TextBox ID="txtDogumYili" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAd" runat="server" Text="Ad"></asp:Label></td>
                <td>
                      <asp:TextBox ID="txtAd" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSoyad" runat="server" Text="Soyad"></asp:Label></td>
                <td>
                      <asp:TextBox ID="txtSoyad" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr><td></td>
                <td>  <asp:Button ID="btnDogrula" runat="server" Text="Dogrula" OnClick="btnDogrula_Click"  /></td>
            </tr>
        </table>

    </div>
   
  
</asp:Content>
