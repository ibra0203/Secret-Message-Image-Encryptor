<%@ Page Language="C#"  MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="~/Controllers/Main.aspx.cs" Inherits="MessageDecoder.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="panels" align="center" BorderStyle="Double" Height="382px" HorizontalAlign="Center">
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Welcome! You can use this web app to encrypt any message you want into an image using a key, or decrypt a message that was encrypted here from an image."></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="EncMenuBtn" runat="server" OnClick="EncMenuBtn_Click1" Text="Encrypt a message into an Image" />
        <br />
        <br />
        <br />
        <asp:Button ID="DecMenuBtn" runat="server" OnClick="DecMenuBtn_Click1" Text="Decrypt a message from an image" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
    </asp:Panel>
</asp:Content>
