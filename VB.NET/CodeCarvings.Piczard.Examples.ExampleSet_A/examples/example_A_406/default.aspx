<%@ Page Title="Piczard Examples | Customize Piczard | Real time crop preview" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_406_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="4" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="4" Title="A.406 - Real time crop preview" />
        
        <script type="text/javascript">
            //<![CDATA[

            window.updatePreviewTimeout = -1;

            function InlinePictureTrimmer1_onUserStateChanged(sender, args)
            {
                if (window.updatePreviewTimeout == -1)
                {
                    // Initialization process -> Refresh now the preview
                    window.updatePreviewTimeout = null;
                    updatePreview();
                }
                else
                {   
                    // Use a timeout so the preview is not refreshed too many times
                    clearUpdatePreviewTimeout();
                    window.updatePreviewTimeout = window.setTimeout(updatePreview, 500);
                }
            }
            
            function updatePreview()
            {
                // Update the preview
            
                // Get the UserState
                var oPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =Me.InlinePictureTrimmer1.ClientID %>");
                var userState = oPictureTrimmer.get_userState();

                var x = userState.value.imageSelection.crop.rectangle.x;
                var y = userState.value.imageSelection.crop.rectangle.y;
                var resizeFactor = userState.value.imageSelection.transformation.resizeFactor;
                var rotationAngle = userState.value.imageSelection.transformation.rotationAngle;
                
                var sourceImageWidth = <% =Me.InlinePictureTrimmer1.SourceImageSize.Width.ToString() %>;
                var sourceImageHeight = <% =Me.InlinePictureTrimmer1.SourceImageSize.Height.ToString() %>;
                
                // Calculate the size of the preview
                var imgWidth;
                var imgHeight;
                if ((rotationAngle == 0) || (rotationAngle == 180))
                {
                    // Rotate 0 or 180 degrees
                    imgWidth = Math.round(sourceImageWidth / 100 * resizeFactor);
                    imgHeight = Math.round(sourceImageHeight / 100 * resizeFactor);
                }
                else
                {
                    // Rotate 90 or 270 degrees
                    imgWidth = Math.round(sourceImageHeight / 100 * resizeFactor);
                    imgHeight = Math.round(sourceImageWidth / 100 * resizeFactor);
                }
                // Calculate the x-y coordinates of the preview
                var imgLeft = (0-x);
                var imgTop = (0-y);
                
                var oPreviewImage = $("#previewImage");
                
                var animationDuration = 600;
                
                // Since rotation, flip, and color adjustments cannot be manipulated on the client side, we use
                // a generic handler to apply these effects
                var previewData = CodeCarvings.Wcs.Piczard.Shared.Externals.JSON.stringify({
                    image: "<% =CodeCarvings.Piczard.Helpers.SecurityHelper.EncryptString(Me.SourceImageFilePath) %>",
                    rotationAngle: rotationAngle,
                    flipH: userState.value.imageSelection.transformation.flipH,
                    flipV: userState.value.imageSelection.transformation.flipV,
                    brightness: userState.value.imageAdjustments.brightness,
                    contrast: userState.value.imageAdjustments.contrast,
                    hue: userState.value.imageAdjustments.hue,
                    saturation: userState.value.imageAdjustments.saturation
                });
                
                if (previewData != window.lastPreviewData)
                {
                    // Something has been changed -> Update the preview
                    window.lastPreviewData = previewData;
                    oPreviewImage.html("<img id=\"previewImage0\" alt=\"Preview\" src=\"Preview.ashx?data=" + encodeURIComponent(previewData) + "\" style=\"width:100%; height:100%; border:none;\" />");
                    
                    // No animation
                    animationDuration = 0;
                }
                
                // Refresh the x-y / width-height of the preview (use a jquery animation)
                oPreviewImage.animate({
                    width: imgWidth,
                    height: imgHeight,
                    left: imgLeft,
                    top: imgTop
                  },{
                    duration: animationDuration,
                    easing: "easeInOutCubic"});               
            }
            
            function getRandomNumber(lowLimit, hiLimit)
            {
                // Get a random number between lowLimit and hiLimit
                return lowLimit + Math.random() * (hiLimit - lowLimit + 1);
            }
            
            function getRotatedImageSize(userState)
            {
                var rotationAngle = userState.value.imageSelection.transformation.rotationAngle;
                var sourceImageWidth = <% =Me.InlinePictureTrimmer1.SourceImageSize.Width.ToString() %>;
                var sourceImageHeight = <% =Me.InlinePictureTrimmer1.SourceImageSize.Height.ToString() %>;
                
                // Get the (rotated) image size
                var imgWidth;
                var imgHeight;
                if ((rotationAngle == 0) || (rotationAngle == 180))
                {
                    // Rotate 0 or 180 degrees
                    imgWidth = sourceImageWidth;
                    imgHeight = sourceImageHeight;
                }
                else
                {
                    // Rotate 90 or 270 degrees
                    imgWidth = sourceImageHeight;
                    imgHeight = sourceImageWidth;
                }
                
                return {width: imgWidth, height: imgHeight};
            }
            
            function InlinePictureTrimmer1_SetUserState(mode)
            {
                // Get the PictureTrimmer instance
                var oPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =Me.InlinePictureTrimmer1.ClientID %>");

                // Get a COPY of the current UserState
                var userState = oPictureTrimmer.get_userState();
                var rotatedImageSize = getRotatedImageSize(userState); 
                var resizeFactor;
                var x, y;
                          
                // Calculate the new resize factor
                switch(mode)
                {
                    case 1:
                    case 2:
                        // Whole image o slice  
                        if ((rotatedImageSize.width / rotatedImageSize.height) > (userState.value.imageSelection.crop.rectangle.width / userState.value.imageSelection.crop.rectangle.height))
                        {
                            // landscape
                            if (mode == 1)
                            {
                                resizeFactor = userState.value.imageSelection.crop.rectangle.width / rotatedImageSize.width * 100;
                            }
                            else
                            {
                                resizeFactor = userState.value.imageSelection.crop.rectangle.height / rotatedImageSize.height * 100;                           
                            }
                        }
                        else
                        {
                            // Portrait
                            if (mode == 1)
                            {
                                resizeFactor = userState.value.imageSelection.crop.rectangle.height / rotatedImageSize.height * 100;                           
                            }
                            else
                            {
                                resizeFactor = userState.value.imageSelection.crop.rectangle.width / rotatedImageSize.width * 100;
                            }
                        }
                        break;
                    case 3:
                        // Do not resize
                        resizeFactor = 100;
                        break;
                    case 4: 
                        // Random
                        resizeFactor = Math.floor(getRandomNumber(50, 150));
                        break;
                }
                
                // Calcolate the x - y coordinates
                if (mode == 4)
                {
                    // Random x - y
                    x = Math.floor(getRandomNumber(-100, 300));
                    y = Math.floor(getRandomNumber(-100, 300));
                }
                else
                {
                    // Center x - y
                    x = Math.round(Math.round(rotatedImageSize.width / 100 * resizeFactor) - userState.value.imageSelection.crop.rectangle.width) / 2;
                    y = Math.round(Math.round(rotatedImageSize.height / 100 * resizeFactor) - userState.value.imageSelection.crop.rectangle.height) / 2;
                }

                // Set the resize factor
                userState.value.imageSelection.transformation.resizeFactor = resizeFactor;

                // Set the rectangle
                userState.value.imageSelection.crop.rectangle.x = x;
                userState.value.imageSelection.crop.rectangle.y = y;

                // Auto-zoom and auto-center the view
                userState.uiParams.zoomFactor = null;
                userState.uiParams.pictureScrollH = null;
                userState.uiParams.pictureScrollV = null;

                // Set the new UserState
                oPictureTrimmer.set_userState(userState);

                // Dispay the new user state
                clearUpdatePreviewTimeout();
                window.updatePreviewTimeout = -1;
            }          
                       
            function clearUpdatePreviewTimeout()
            {
                if (window.updatePreviewTimeout)
                {
                    // Do not update the image
                    window.clearTimeout(window.updatePreviewTimeout);
                    window.updatePreviewTimeout = null;
                }
            }     

            //]]>
        </script>               
        
        <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="440px" 
          Culture="en" AutoFreezeOnFormSubmit="true" ShowImageAdjustmentsPanel="true"
          OnClientUserStateChangedFunction="InlinePictureTrimmer1_onUserStateChanged"
           />        

        <div class="InlinePanel2" style="margin-top:20px;">
            <div style="float:left; width:230px; text-align:center; padding-right: 10px;">
                <div style="font-weight:bold; margin-bottom:10px; color: #000;">
                    Change Selection:
                </div>
                <div style="margin-bottom:5px;">
                    <input type="button" value="Whole Image" style="width:165px;" onclick="InlinePictureTrimmer1_SetUserState(1); return false;" />
                </div>
                <div style="margin-bottom:5px;">
                    <input type="button" value="Slice" style="width:165px;" onclick="InlinePictureTrimmer1_SetUserState(2); return false;" />
                </div>
                <div style="margin-bottom:5px;">
                    <input type="button" value="Don't Resize" style="width:165px;" onclick="InlinePictureTrimmer1_SetUserState(3); return false;"  />
                </div>
                <div style="margin-bottom:5px;">
                    <input type="button" value="Random" style="width:165px;" onclick="InlinePictureTrimmer1_SetUserState(4); return false;" />
                </div>
            </div>        
            <div style="margin-left:240px; width:350px; height:250px; overflow:hidden; position:relative; border:solid 5px #80A232; background-color:#fff;">
                <div id="previewImage" style="position:absolute; z-index:101;">
                </div>
                <div id="previewImageCover" style="position:absolute; z-index:102; top:0; left:0; width:100%; height:100%; background-image:url('overlay.png'); background-repeat:no-repeat;">
                </div>
            </div>
            <div style="clear:both;">
            </div>            
        </div>                  

    </div>
</asp:Content>


