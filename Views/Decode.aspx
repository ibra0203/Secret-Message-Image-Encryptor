<%@ Page Language="C#"  MasterPageFile="./Master.Master" AutoEventWireup="true" CodeBehind="~/Controllers/Decode.aspx.cs" Inherits="MessageDecoder.Decode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="panels" align="center" BorderStyle="Double" HorizontalAlign="Center">
             <asp:Panel ID="Panel2" CssClass="submit-panel panels" runat="server" Height="310px" Width="404px">
  
                 <textarea id="TextArea1" cols="20" hidden="hidden" name="S1" runat="server"></textarea><br />
  
                 <asp:Label ID="Label2" runat="server" Text="Select image to decrypt"></asp:Label>
                 <br />
                 <asp:FileUpload CssClass="upload1" ID="FileUpload1" runat="server" />
                 <br />
                 <br />
                 <asp:Label ID="Label3" runat="server" Text="Enter key to decrypt with"></asp:Label>
                 <br />
                 <asp:TextBox ID="KeyBox" runat="server" Width="227px" style="margin-right: 0px"></asp:TextBox>
                 <br />
                 <br />
        <br />
        <div class="g-recaptcha" data-sitekey="6LdtJlAUAAAAABETbH9cvv5pSr7VkiUsRhGXN44x">
        </div>
   
                 <br />
                 <asp:Button ID="UploadBtn" CssClass="upload-btn" runat="server" OnClick="UploadBtn_Click" style="margin-top: 0px" Text="Upload" UseSubmitBehavior="false"/>
                 <br />
   </asp:Panel>
                 </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    
    <style type="text/css">
        #TextArea1 {
            height: 7px;
            width: 263px;
        }
    </style>
</asp:Content>

