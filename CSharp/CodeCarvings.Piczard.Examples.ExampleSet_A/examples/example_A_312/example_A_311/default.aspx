<%@ Page Title="Piczard Examples | Web - PictureTrimmer | InlinePictureTrimmer | Overview" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_311_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.311 - InlinePictureTrimmer - Overview" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function InlinePictureTrimmer1_Freeze()
            {
                // Get the PictureTrimmer instance
                var oPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =this.InlinePictureTrimmer1.ClientID %>");
                // Get the previous frozen status
                var frozen = oPictureTrimmer.ui.get_frozen();
                // Set the new frozen status
                oPictureTrimmer.ui.set_frozen(!frozen);
            }
            
            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <asp:Button runat="server" ID="btnLoadImage" Text="Load/Unload image" OnClick="btnLoadImage_Click" CausesValidation="false" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnFreeze" Text="Freeze/Unfreeze" CausesValidation="false" OnClientClick="InlinePictureTrimmer1_Freeze(); return false;" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnEnable" Text="Enable/Disable" OnClick="btnEnable_Click" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnShow" Text="Show/Hide" OnClick="btnShow_Click" CausesValidation="false" />
                </div>
            
                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="440px" 
                  Culture="en" AutoFreezeOnFormSubmit="true" />
                         
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="InlinePanel2" style="margin-top:20px;">
            <strong>
                PictureTrimmer Frozen Status
            </strong><br />
            The PictureTrimmer "frozen" status is a feature that allows to hide the Flash User Interface (without losing any parameter).<br />
            It can be activated only by a client side code.<br />
            The "frozen" status cannot be activated by a server side code.<br />
            Also, the "frozen" status is reset after every page postback.<br />
            It is useful to hide the Flash UI (when using FlashWMode="Window"), in order to prevent Z-INDEX issues.
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


