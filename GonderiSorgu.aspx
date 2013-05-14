<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GonderiSorgu.aspx.cs" Inherits="WebRole1.GonderiSorgu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div>


        <table>

            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Barkod : "></asp:Label>
                    <asp:TextBox ID="txtBarkod" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>

                    <asp:Button ID="Button1" runat="server" Text="Sorgula" OnClick="Button1_Click" />
                </td>

            </tr>

            <tr>
                <td>

                    <asp:TextBox ID="txtSonuc" runat="server" Height="252px" TextMode="MultiLine" Width="692px"></asp:TextBox>
                </td>
            </tr>

        </table>


    </div>
</asp:Content>
