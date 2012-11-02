<%@ Page Title="Piczard Examples | Web - PictureTrimmer | UserState & PictureTrimmer Value" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_304_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.304 - UserState & PictureTrimmer Value" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function InlinePictureTrimmer1_GetUserState()
            {
                var str = "";
                var crlf = "\r\n";

                // Get the PictureTrimmer instance
                var oPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =Me.InlinePictureTrimmer1.ClientID %>");

                // Get the UserState
                var userState = oPictureTrimmer.get_userState();

                // Get the Value
                var value = userState.value;

                // Get the ImageSelection
                var imageSelection = value.imageSelection;

                // Get the ImageTransformation
                var imageTransformation = imageSelection.transformation
                str = "";
                str += "Resize factor:" + imageTransformation.resizeFactor + "%" + crlf;
                str += "Rotation angle:" + imageTransformation.rotationAngle + "°" + crlf;
                str += "Flip horizontal:" + (imageTransformation.flipH ? "yes" : "no") + crlf;
                str += "Flip vertical:" + (imageTransformation.flipV ? "yes" : "no") + crlf;
                $("#<% =Me.txtUserState_Value_ImageSelection_Transformation.ClientID %>").val(str);

                // Get the ImageCrop
                var imageCrop = imageSelection.crop
                str = "";
                str += "Rectangle:" + (imageCrop.rectangle != null ? ("{x=" + imageCrop.rectangle.x + ",y=" + imageCrop.rectangle.y + ",width=" + imageCrop.rectangle.width + ",height=" + imageCrop.rectangle.height + "}") : "auto") + crlf;
                // Note: in this example imageCrop.CanvasColor is not displayed.
                $("#<% =Me.txtUserState_Value_ImageSelection_Crop.ClientID %>").val(str);

                // Get the ImageAdjustments Filter
                var imageAdjustments = value.imageAdjustments;
                str = "";
                str += "Brightness:" + imageAdjustments.brightness + crlf;
                str += "Contrast:" + imageAdjustments.contrast + crlf;
                str += "Hue:" + imageAdjustments.hue + "°" + crlf;
                str += "Saturation:" + imageAdjustments.saturation + crlf;
                $("#<% =Me.txtUserState_Value_ImageAdjustments.ClientID %>").val(str);

                // Note: In this example value.ImageBackColorApplyMode is not displayed.

                // Get the UIParams
                var uiParams = userState.uiParams;
                str = "";
                str += "Zoom factor:" + (uiParams.zoomFactor != null ? uiParams.zoomFactor + "%" : "auto") + crlf;
                str += "Picture scroll horizontal:" + (uiParams.pictureScrollH != null ? uiParams.pictureScrollH : "auto") + crlf;
                str += "Picture scroll vertical:" + (uiParams.pictureScrollV != null ? uiParams.pictureScrollV : "auto") + crlf;
                str += "Command panel scroll vertical:" + uiParams.commandPanelScrollV + crlf;
                $("#<% =Me.txtUserState_UIParams.ClientID %>").val(str);
            }

            function InlinePictureTrimmer1_SetUserState()
            {
                // Get the PictureTrimmer instance
                var oPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =Me.InlinePictureTrimmer1.ClientID %>");

                // Get a COPY of the current UserState
                var userState = oPictureTrimmer.get_userState();

                // Set the resize factor
                userState.value.imageSelection.transformation.resizeFactor = Math.floor(getRandomNumber(50, 150));

                // Horizontal flip
                userState.value.imageSelection.transformation.flipH = getRandomNumber(0, 4) < 2;

                // Set the rectangle
                userState.value.imageSelection.crop.rectangle.x = Math.floor(getRandomNumber(-100, 300));
                userState.value.imageSelection.crop.rectangle.y = Math.floor(getRandomNumber(-100, 300));
                userState.value.imageSelection.crop.rectangle.width = Math.floor(getRandomNumber(50, 400));
                userState.value.imageSelection.crop.rectangle.height = Math.floor(getRandomNumber(50, 400));

                // Set the Saturation
                userState.value.imageAdjustments.saturation = getRandomNumber(-100, 100);

                // Auto-zoom and auto-center the view
                userState.uiParams.zoomFactor = null;
                userState.uiParams.pictureScrollH = null;
                userState.uiParams.pictureScrollV = null;

                // Set the new UserState
                oPictureTrimmer.set_userState(userState);

                // Dispay the new user state
                InlinePictureTrimmer1_GetUserState();
            }

            function getRandomNumber(lowLimit, hiLimit)
            {
                return lowLimit + Math.random() * (hiLimit - lowLimit + 1);
            }

            function InlinePictureTrimmer1_onUserStateChanged(sender, args)
            {
                // Dispay the new user state
                InlinePictureTrimmer1_GetUserState();
            }

            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>           
                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="500px" 
                  ShowImageAdjustmentsPanel="true" Culture="en" AutoFreezeOnFormSubmit="true" />

                <asp:PlaceHolder runat="server" ID="phInteractiveCommands">
                    <div class="InlinePanel1" style="padding:5px; margin-top:10px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                            <tr>
                                <td align="left" valign="middle" style="width:120px; height:38px;">
                                    <span class="ExampleTableText">
                                        Server side:<br />
                                    </span>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:Button runat="server" ID="btnServerSideGetUserState" Text="Get UserState" CausesValidation="false" />&nbsp;
                                    <asp:Button runat="server" ID="btnServerSideSetUserState" Text="Set UserState" CausesValidation="false" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" style="height:38px;">
                                    <span class="ExampleTableText">
                                        Client side:<br />
                                    </span>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:Button runat="server" ID="btnClientSideGetUserState" Text="Get UserState" CausesValidation="false" OnClientClick="InlinePictureTrimmer1_GetUserState(); return false;" />&nbsp;
                                    <asp:Button runat="server" ID="btnClientSideSetUserState" Text="Set UserState" CausesValidation="false" OnClientClick="InlinePictureTrimmer1_SetUserState(); return false;" />
                                    &nbsp;&nbsp;&nbsp;<asp:CheckBox runat="server" ID="cbAutoUpdateUserState" AutoPostBack="true" Checked="false" Text="Auto Update" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />

                        <div class="GroupBox_Div">
                            <div class="GroupBox_Header">
                                UserState
                            </div>
                            <div class="GroupBox_Body">
                                <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                                    <tr>
                                        <td align="left" valign="top" style="width:60%">
                                            <div class="GroupBox_Div">
                                                <div class="GroupBox_Header">
                                                    Value
                                                </div>
                                                <div class="GroupBox_Body">
                                                
                                                    <div class="GroupBox_Div">
                                                        <div class="GroupBox_Header">
                                                            ImageSelection
                                                        </div>
                                                        <div class="GroupBox_Body">
                                                                                                            
                                                            <div class="GroupBox_Div">
                                                                <div class="GroupBox_Header">
                                                                    ImageTransformation
                                                                </div>
                                                                <div class="GroupBox_Body">                                                                                                                                                                                   
                                                                    <asp:TextBox runat="server" ID="txtUserState_Value_ImageSelection_Transformation" TextMode="MultiLine" Width="325px" Height="60px" ReadOnly="true" Font-Size="10px"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="GroupBox_Div" style="margin-top:5px;">
                                                                <div class="GroupBox_Header">
                                                                    ImageCrop
                                                                </div>
                                                                <div class="GroupBox_Body">                                                                
                                                                    <asp:TextBox runat="server" ID="txtUserState_Value_ImageSelection_Crop" TextMode="MultiLine" Width="325px" Height="22px" ReadOnly="true" Font-Size="10px"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        
                                                        </div>
                                                    </div>

                                                    <div class="GroupBox_Div" style="margin-top:5px;">
                                                        <div class="GroupBox_Header">
                                                            ImageAdjustments
                                                        </div>
                                                        <div class="GroupBox_Body">
                                                            <asp:TextBox runat="server" ID="txtUserState_Value_ImageAdjustments" TextMode="MultiLine" Width="337px" Height="52px" ReadOnly="true" Font-Size="10px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                
                                                </div>
                                            </div>
                                        </td>
                                        <td align="left" valign="top" style="width:40%">
                                            <div class="GroupBox_Div" style="margin-left:5px;">
                                                <div class="GroupBox_Header">
                                                    UIParams
                                                </div>
                                                <div class="GroupBox_Body">                                    
                                                    <asp:TextBox runat="server" ID="txtUserState_UIParams" TextMode="MultiLine" Width="220px" Height="272px" ReadOnly="true" Font-Size="10px"></asp:TextBox>                                   
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                        
                    </div>                
                </asp:PlaceHolder>
                         
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


