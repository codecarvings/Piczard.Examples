<%@ Page Title="Piczard Examples | Web - PictureTrimmer | Cropping Landscape and Portrait images" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_309_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.309 - Cropping Landscape and Portrait images" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function displayOutputImage(sender, args)
            {
                // Hide the PictureTrimmer UI since the flash wmode is set to window and there is z-index problem
                sender.ui.set_frozen(true);
            
		        $.fancybox({
			        // Add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser
			        "href" : "<% =Me.ResolveUrl(OutputImageFileName) %>" + "?timestamp=" + CodeCarvings.Wcs.Piczard.Shared.Helpers.TimeHelper.getTimeStamp(),
			        "padding" : 0,
			        "title" : "Output image",
			        "transitionIn" : "elastic",
			        "transitionOut" : "elastic",
			        "hideOnContentClick": true,
			        "onClosed" : function()
			        {
			            // Display the PictureTrimmer UI
			            sender.ui.set_frozen(false);
			        }
		        });
            }
            
            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
            
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <asp:Button runat="server" ID="btnLoadLandscape" Text="Load a Landscape image" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnLoadPortrait" Text="Load a Portrait image" CausesValidation="false" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnProcessImage" Text="Preview &raquo;" Enabled="false" CausesValidation="false" />
                </div>
            
                <div style="border: solid 1px #cccccc; background-color: #e6e6e6; color: #000000; width:632px">
                    <div style="padding:5px; text-align:right;">
                        Image orientation: 
                        <asp:DropDownList runat="server" ID="ddlCropOrientation" AutoPostBack="true" Enabled="false">
                            <asp:ListItem Value="Landscape" Text="Landscape"></asp:ListItem>
                            <asp:ListItem Value="Portrait" Text="Portrait"></asp:ListItem>
                        </asp:DropDownList>
                    </div>            
                    <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="400px" 
                    BorderStyle="None"
                    Culture="en" AutoFreezeOnFormSubmit="true" />    
                </div>
            
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


