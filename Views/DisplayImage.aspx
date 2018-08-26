<%@ Page Title="" Language="C#" MasterPageFile="./Master.Master" AutoEventWireup="true" CodeBehind="~/Controllers/DisplayImage.aspx.cs" Inherits="MessageDecoder.DisplayImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-cover"></div>
    <asp:Panel ID="Panel1" runat="server" CssClass="panels" align="center" BorderStyle="Double" HorizontalAlign="Center">
        <p class="centered-text">Your encrypted image is ready!</p>
               <a> <div class="img-view" >
                    <asp:Image ID="Image1" CssClass="img-thumbnail" runat="server" ImageAlign="Middle" />
                </div></a>
        <p class="centered-text" style="font-size: 15px">(click the image for preview)</p>
        <div style="height: 124px; margin-top: 24px">
            <asp:Button ID="DownloadImgBtn" runat="server" OnClick="DownloadImgBtn_Click" Text="Download Image" />
        </div>
        </asp:Panel>
    
</asp:Content>
