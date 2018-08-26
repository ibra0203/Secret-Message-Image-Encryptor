<%@ Page Language="C#"  MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="~/Controllers/Encode.aspx.cs" Inherits="MessageDecoder.Encode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="panels" align="center" BorderStyle="Double" HorizontalAlign="Center">
        <asp:Panel ID="Panel2" CssClass="submit-panel panels" runat="server" Height="310px" Width="404px">
            <br />
            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Key to encode the message with"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Width="228px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Message to encode"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" style="margin-bottom: 0px" Width="291px"></asp:TextBox>
            <br />
            <br />
            <br />
            <div class="g-recaptcha" data-sitekey="6LdtJlAUAAAAABETbH9cvv5pSr7VkiUsRhGXN44x">
            </div>
            <br />
            <asp:Button ID="SubmitEncBtn" runat="server" OnClick="SubmitEncBtn_Click" style="margin-top: 13px" Text="Submit" />
            <br />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
    #TextArea1 {
        height: 134px;
        width: 471px;
        margin-left: 0px;
    }
    #TextArea2 {
        height: 98px;
        width: 288px;
    }
</style>
</asp:Content>

