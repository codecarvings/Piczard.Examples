<%@ Page Title="Piczard Examples | Customize Piczard | Localize & customize texts" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_402_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="4" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="4" Title="A.402 - Localize & customize texts" />
        
        <script type="text/javascript">
            //<![CDATA[

            function openPopupPictureTrimmer1Window()
            {
                CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =Me.PopupPictureTrimmer1.ClientID %>');
            }     
            
            //]]>
        </script>

        <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
            Language:
            <asp:DropDownList runat="server" ID="ddlLanguage" CausesValidation="false" AutoPostBack="true">
                <asp:ListItem Value="" Text="Auto (CurrentThread.CurrentUICulture)" Selected="True"></asp:ListItem>
                <asp:ListItem Value="en" Text="en (built-in)"></asp:ListItem>
                <asp:ListItem Value="it" Text="it (built-in)"></asp:ListItem>
                <asp:ListItem Value="en-TT" Text="en-TT (Static Localization Plugin demo)"></asp:ListItem>
                <asp:ListItem Value="en-ZW" Text="en-ZW (Static Localization Plugin demo / Resource file)"></asp:ListItem>
                <asp:ListItem Value="en-JM" Text="en-JM (Dynamic Localization Plugin demo)"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" Text="Postback" />
        </div>
        
        <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="400px" AutoFreezeOnFormSubmit="true" /><br />
        
        <br />
        <br />                
        <ccPiczard:PopupPictureTrimmer ID="PopupPictureTrimmer1" runat="server" Tag="ExA402_PopupPictureTrimmer" />
        <asp:Button runat="server" ID="btnOpenPopupPictureTrimmer1" Text="Open PopupPictureTrimmer Window..." CausesValidation="false"
        OnClientClick="openPopupPictureTrimmer1Window(); return false;" />

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
</asp:Content>

