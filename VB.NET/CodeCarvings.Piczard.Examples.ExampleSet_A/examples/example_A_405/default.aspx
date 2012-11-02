<%@ Page Title="Piczard Examples | Customize Piczard | Enrich the PopupPictureTrimmer window" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_405_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="4" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="4" Title="A.405 - Enrich the PopupPictureTrimmer window" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <script type="text/javascript">
            //<![CDATA[

            function PopupPictureTrimmer1_OnBeforePopupOpen(sender, args)
            {
                // Set the window size
                args.windowWidth = 850;
                args.windowHeight = 590;
                
                // Reserve a portion of the window height for the additional UI elements
                args.reservedWindowHeight = 120; // Top:40+5+5  -  Bottom:50+10+10 - Total:120

                // Add the "popupExt1" element to the top of the popup
                args.additionalTopElements[args.additionalTopElements.length] = document.getElementById("popupExt1");
                
                // Add the "popupExt2" element to the bottome of the popup
                args.additionalBottomElements[args.additionalBottomElements.length] = "popupExt2";
            }
            
            function PopupPictureTrimmer1_UserStateChanged(sender, args)
            {
                // Checks if the popup is open
                if (!sender.ui.get_popup_isOpen())
                {
                    // The popup is not open...
                    // Ignore this event
                    return;
                }
            
                // Get the current user state
                var userState = sender.get_popup_userState();
                
                var selectedRectangleSizeText;
                if ((userState.value.imageSelection.crop.rectangle.width < 400)
                ||
                (userState.value.imageSelection.crop.rectangle.height < 300))
                {
                    // Selected rectangle size is too small
                    selectedRectangleSizeText = "&nbsp;&nbsp;The selected size is too small";
                }
                else
                {
                    selectedRectangleSizeText = "";
                }
                
                // Refresh the UI
                var oSelectedRectangleSize = document.getElementById("selectedRectangleSize");
                if (oSelectedRectangleSize.innerHTML != selectedRectangleSizeText)
                {
                    oSelectedRectangleSize.innerHTML = selectedRectangleSizeText;
                }
            }
            
            function clientSideSelectWholeImage()
            {
                // Get the PictureTrimmer instance
                var oPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =Me.PopupPictureTrimmer1.ClientID %>");
            
                // Get the current user state
                var userState = oPictureTrimmer.get_popup_userState();
                
                // Select the whole image
                userState.value.imageSelection.crop.rectangle.x = 0;
                userState.value.imageSelection.crop.rectangle.y = 0;
                userState.value.imageSelection.crop.rectangle.width = Math.round(<% =Me.PopupPictureTrimmer1.SourceImageSize.Width.ToString() %> / 100 * userState.value.imageSelection.transformation.resizeFactor);
                userState.value.imageSelection.crop.rectangle.height = Math.round(<% =Me.PopupPictureTrimmer1.SourceImageSize.Height.ToString() %> / 100 * userState.value.imageSelection.transformation.resizeFactor);
                
                // Update the user state
                oPictureTrimmer.set_popup_userState(userState);
            }

            function serverSideSelectWholeImage()
            {
                // Close the popup (and save the current user state)
                CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_close(true);
            
                // Since the btnSelectWholeImage has been moved to the popup window
                // (outside the ASP.NET FORM) - it is necessary to "manyally" raise the page postback                
                <% =Me.ClientScript.GetPostBackEventReference(Me.btnSelectWholeImage, "", false) %>     
                
                // Cancel the original postback
                return false;
            }
           
            //]]>
        </script>       
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>        
            
                <ccPiczard:PopupPictureTrimmer ID="PopupPictureTrimmer1" runat="server" Culture="en"
                    OnClientBeforePopupOpenFunction="PopupPictureTrimmer1_OnBeforePopupOpen"
                    OnClientUserStateChangedFunction="PopupPictureTrimmer1_UserStateChanged"
                    ShowImageAdjustmentsPanel="true"
                    ShowZoomPanel="false"
                    AllowResize="false"
                 />

                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <input type="button" value="Open popup" onclick="CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =Me.PopupPictureTrimmer1.ClientID %>'); return false;" />
                    
                    &nbsp;&nbsp;<asp:Button runat="server" ID="btnPostBack" Text="PostBack &raquo;" CausesValidation="false" />
                </div>
                
                <div id="popupExtParent" style="display:none;">
                    <div id="popupExt1" style="height: 40px; line-height: 40px; vertical-align: middle; padding:5px; background-color: #999; color:#fff;">
                        <div style="float:right; text-align:right; width:500px;">
                            <input type="button" value="   Select whole image (client-side)   " style="height:35px;" class="DoNotApplyButtonStyle" onclick="clientSideSelectWholeImage(); return false;" />
                            <asp:Button runat="server" ID="btnSelectWholeImage" Text="   Select whole image (server-side)   " CausesValidation="false" style="height:35px;" CssClass="DoNotApplyButtonStyle" OnClick="btnSelectWholeImage_Click" OnClientClick="return serverSideSelectWholeImage();" />
                        </div>
                        <div id="selectedRectangleSize" style="font-weight:bold; background: #c00; color: #fff;">
                        </div>
                        <div style="clear:both;">
                        </div>
                    </div>

                    <div id="popupExt2" style="height: 50px; padding:10px;">
                        <strong>Instructions</strong><br />
                        Drag the cropping handles to select a portion of the image.<br />
                        If you select a rectangle smaller than 400x300 pixels a warning message will be displayed.<br />
                    </div>
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel> 

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


