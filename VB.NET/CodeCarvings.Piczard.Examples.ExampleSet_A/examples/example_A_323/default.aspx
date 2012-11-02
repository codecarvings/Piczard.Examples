<%@ Page Title="Piczard Examples | Web - PictureTrimmer | PopupPictureTrimmer | Client side Events" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_323_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.323 - PopupPictureTrimmer - Client side Events" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function openPopupPictureTrimmer1Window()
            {
                var popupSizeMode = $("#<% =Me.ddlClientSidePopupSizeMode.ClientID %>").val();
                var windowWidth, windowHeight;
            
                switch(popupSizeMode)
                {
                    case "0":
                        // Default size
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =Me.PopupPictureTrimmer1.ClientID %>');
                        break;
                    case "1":
                        // Custom size #1
                        windowWidth = 900;
                        windowHeight = 540;
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =Me.PopupPictureTrimmer1.ClientID %>', windowWidth, windowHeight);
                        break;
                    case "2":
                        // Custom size #2
                        windowWidth = $(window).width() - 50;
                        windowHeight = $(window).height() - 50;
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =Me.PopupPictureTrimmer1.ClientID %>', windowWidth, windowHeight);
                        break;
                }
            }

            function myLog(message)
            {
                var formatDate = function(dt)
                {
                    if (!dt)
                    {
                        dt = new Date();
                    }

                    var format2Digits = function(str)
                    {
                        str = "00" + str;
                        return str.substring(str.length - 2, str.length);
                    }

                    var result = dt.getFullYear() + "-" + format2Digits(dt.getMonth() + 1) + "-" + format2Digits(dt.getDate()) + "T" + format2Digits(dt.getHours()) + ":" + format2Digits(dt.getMinutes()) + ":" + format2Digits(dt.getSeconds());
                    return result;
                }

                var oTxtMyLog = $("#<% =Me.txtMyLog.ClientID %>");
                oTxtMyLog.val(formatDate() + " - " + message + "\r\n" + oTxtMyLog.val());
            }

            function PopupPictureTrimmer1_onControlLoad(sender, args)
            {
                // NOTE: The onControlLoad event handler is raised:
                // 1 - When the control.load function is invoked.
                // 2 - When the control.unloadImage function is invoked.

                myLog("Control loaded (image loaded=" + sender.get_imageLoaded() + ").");
            }

            function PopupPictureTrimmer1_onUserStateChanged(sender, args)
            {
                // Checks if the popup is open
                var popupOpen = sender.ui.get_popup_isOpen();
            
                // Get the new userState
                var userState;
                if (!popupOpen)
                {
                    // Popup closed -> use the current userState
                    userState = sender.get_userState();
                }
                else
                {
                    // Popup open -> use the current temporary POPUP userState
                    userState = sender.get_popup_userState();
                }
                
                var rectangle = userState.value.imageSelection.crop.rectangle;

                // Display the crop rectangle coordinates
                var rectangleString = (rectangle != null ? ("{x=" + rectangle.x + ",y=" + rectangle.y + ",width=" + rectangle.width + ",height=" + rectangle.height + "}") : "auto");

                myLog("UserState changed (popup open=" + popupOpen + ")\r\n   (crop rectangle:" + rectangleString + ").");
            }

            function PopupPictureTrimmer1_onBeforePopupOpen(sender, args)
            {
                // *** PARAMETERS ***
                // args.windowWidth | Gets or sets the width of the popup.
                // args.windowHeight | Gets or sets the height of the popup.
                // args.reservedWindowHeight | Gets or sets the height of the popup window reserved for additional custom UI elements (default=0).
                // args.additionalTopElements | Gets or sets an array of DOM elements to be added on the top of the popup window.
                // args.additionalBottomElements | Gets or sets an array of DOM elements to be added on the bottom of the popup window.
                // args.lightBoxCssClass | Gets or sets the css class of the popup.
                // args.freezeOtherPictureTrimmerControls | Gets or sets a value indicating whether to freeze other 
                //                                          PictureTrimmer controls (default=true).
                // args.goOn | If you set this value to false then the popup will NOT open.

                myLog("Before popup open (window size=" + args.windowWidth + "x" + args.windowHeight +  ").");
            }

            function PopupPictureTrimmer1_onAfterPopupOpen(sender, args)
            {
                // *** PARAMETERS ***
                // args.popupWindow | The Popup window HTML DOM element.
			
                myLog("After popup open.");

                alert("Hello from the function PopupPictureTrimmer1_onAfterPopupOpen !");
            }

            function PopupPictureTrimmer1_onBeforePopupClose(sender, args)
            {
                // *** PARAMETERS ***
                // args.saveChanges | Gets or sets a value indicating whether to save the changes or not.
                // args.previousUserState | Gets the previous userState (before popup opens).
                // args.newUserState | Gets or sets the new userState.
                // args.resetUIParams | If you set this value to false, then the uiParameters will NOT be reset. In this case
                //                      the next time the popup is opened the previous uiParams are applied (default value = true).
                // args.performPostback | Gets or sets a value indicating whether to perform a page postback.
                // args.goOn | If you set this value to false then the popup will NOT close.

                if (!args.saveChanges)
                {
                    if (!window.confirm("Are you sure you want to cancel ?"))
                    {
                        args.goOn = false;
                    }
                }

                myLog("Before popup close (saveChanges=" + args.saveChanges + ", goOn=" + args.goOn + ").");
            }

            function PopupPictureTrimmer1_onAfterPopupClose(sender, args)
            {
                // *** PARAMETERS ***
                // args.saveChanges | Gets a value indicating whether the changes has been saved (READONLY VALUE!).
                // args.performPostback | Gets or sets a value indicating whether to perform a page postback.

                if (args.saveChanges)
                {
                    args.performPostback = window.confirm("Do you want to postback to server ?");
                }

                myLog("After popup close (saveChanges=" + args.saveChanges + ", performPostback=" + args.performPostback + ").");
            }

            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
            
                <ccPiczard:PopupPictureTrimmer ID="PopupPictureTrimmer1" runat="server" Culture="en"
                ShowImageAdjustmentsPanel="true" AutoPostBackOnPopupClose="Never"
                
                OnClientControlLoadFunction="PopupPictureTrimmer1_onControlLoad"
                OnClientUserStateChangedFunction="PopupPictureTrimmer1_onUserStateChanged"
                
                OnClientBeforePopupOpenFunction="PopupPictureTrimmer1_onBeforePopupOpen"
                OnClientAfterPopupOpenFunction="PopupPictureTrimmer1_onAfterPopupOpen"
                
                OnClientBeforePopupCloseFunction="PopupPictureTrimmer1_onBeforePopupClose"
                OnClientAfterPopupCloseFunction="PopupPictureTrimmer1_onAfterPopupClose"
                 />

                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                        <tr>
                            <td align="left" valign="middle">
                                <asp:DropDownList runat="server" ID="ddlClientSidePopupSizeMode">
                                    <asp:ListItem Value="0" Text="Default window size" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Custom window size #1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Custom window size #2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button runat="server" ID="btnClientSideOpenPopup" Text="Open popup" CausesValidation="false" OnClientClick="openPopupPictureTrimmer1Window(); return false;" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <span class="ExampleTableText">
                        Event log:<br />
                    </span>
                    <asp:TextBox runat="server" ID="txtMyLog" TextMode="MultiLine" ReadOnly="true" style="width:610px; height:300px;"></asp:TextBox>                    
                </div>
                         
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


