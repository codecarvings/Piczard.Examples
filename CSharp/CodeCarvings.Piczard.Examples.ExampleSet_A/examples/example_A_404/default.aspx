<%@ Page Title="Piczard Examples | Customize Piczard | Dynamic content image &amp; GUI expansion" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_404_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="4" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="4" Title="A.404 - Dynamic content image &amp; GUI expansion" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function displayPreview(sender, args)
            {
                // Hide the PictureTrimmer UI since the flash wmode is set to window and there is z-index problem
                sender.ui.set_frozen(true);

                $.fancybox({
                    // Add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser
                    "href": "<% =this.ResolveUrl(OutputImageFileName) %>" + "?timestamp=" + CodeCarvings.Wcs.Piczard.Shared.Helpers.TimeHelper.getTimeStamp(),
                    "padding": 0,
                    "title": "Output image",
                    "transitionIn": "elastic",
                    "transitionOut": "elastic",
                    "hideOnContentClick": true,
                    "onClosed": function()
                    {
                        // Display the PictureTrimmer UI
                        sender.ui.set_frozen(false);
                    }
                });
            }

            function zoomFactorChange()
            {
                // Get the PictureTrimmer instance
                var oPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =this.InlinePictureTrimmer1.ClientID %>");

                // Get a COPY of the current UserState
                var userState = oPictureTrimmer.get_userState();

                // Set the zoom factor
                userState.uiParams.zoomFactor = parseFloat($("#<% =this.ddlZoomFactor.ClientID %>").val());
                
                // Center the image
                userState.uiParams.pictureScrollH = null;
                userState.uiParams.pictureScrollV = null;

                // Update the control
                oPictureTrimmer.set_userState(userState);
            }

            function initializeExampleUI()
            {
                var hexValue = $("#<% =this.txtCanvasColor.ClientID %>").val();
                $("#divCanvasColor div").css("backgroundColor", hexValue);
                $("#divCanvasColor").ColorPicker({
                    color: hexValue,
                    custom_forceBelow: true,
                    onShow: function(colpkr)
                    {
                        $(colpkr).fadeIn(500);
                        return false;
                    },
                    onHide: function(colpkr)
                    {
                        $(colpkr).fadeOut(500, function()
                        {
                            <% =this.ClientScript.GetPostBackEventReference(this.txtCanvasColor, "OnTextChanged", false) %>
                        });
                        return false;
                    },
                    onChange: function(hsb, hex, rgb)
                    {
                        var hexValue = "#" + hex;
                        $("#<% =this.txtCanvasColor.ClientID %>").val(hexValue);
                        $("#divCanvasColor div").css("backgroundColor", hexValue);
                    }
                });

                $("#<% =this.ddlZoomFactor.ClientID %>").change(function()
                {
                    zoomFactorChange(true);
                });
            }

            $(function()
            {
                initializeExampleUI();
            });

            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>           
            
                <div style="border: solid 1px #cccccc; background-color: #e6e6e6; color: #000000; width:600px;">
                    <div style="padding:5px;">
                        Image rotation: 
                        <asp:DropDownList runat="server" ID="ddlRotationAngle" AutoPostBack="true" OnSelectedIndexChanged="ddlRotationAngle_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        Color filter: 
                        <asp:DropDownList runat="server" ID="ddlDefaultColorFilters" AutoPostBack="true" OnSelectedIndexChanged="ddlDefaultColorFilters_SelectedIndexChanged">
                            <asp:ListItem Text="Original"></asp:ListItem>
                            <asp:ListItem Text="Grayscale"></asp:ListItem>
                            <asp:ListItem Text="Sepia" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Invert"></asp:ListItem>
                        </asp:DropDownList>
                        
                        <span style="margin-left:70px;">
                            <asp:Button runat="server" ID="btnPreview" Text="Preview" OnClick="btnPreview_Click" CausesValidation="false" />
                        </span>
                    </div>
                    
                    <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="600px" Height="420px" 
                     BorderStyle="None" Culture="en" AutoFreezeOnFormSubmit="true" 
                     AutoZoomMode="Disabled" ShowRotatePanel="false" ShowFlipPanel="false"
                     AllowResize="false" ShowZoomPanel="false" ShowImageAdjustmentsPanel="true"
                     CropShadowMode="Flat"
                     ImageBackColorApplyMode="DoNotApply"
                      />
                      
                    <div style="padding:5px;">                       
                        <div style="float:left;">
                            <div style="float:left;">
                                <span style="line-height:34px; vertical-align:middle;">
                                    &nbsp;&nbsp;&nbsp;Canvas color: 
                                </span>
                            </div>
                            <div style="float:left;">
                                <div id="divCanvasColor" class="ColorSelector"><div> </div></div>
                            </div>
                            <div style="float:left; margin-top:2px;">
                                <asp:TextBox runat="server" ID="txtCanvasColor" style="width:70px; text-align:center;" Enabled="false">#f9fad2</asp:TextBox>
                            </div>
                            <br style="clear:both;" />
                        </div>

                        <div style="float:left;">
                            &nbsp;&nbsp;
                            Unit:
                            <asp:DropDownList runat="server" ID="ddlGfxUnit" AutoPostBack="true" CausesValidation="false">
                                <asp:ListItem Value="Pixel" Text="Pixel" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Point" Text="Point"></asp:ListItem>
                                <asp:ListItem Value="Pica" Text="Pica"></asp:ListItem>
                                <asp:ListItem Value="Inch" Text="Inch"></asp:ListItem>
                                <asp:ListItem Value="Mm" Text="Mm"></asp:ListItem>
                                <asp:ListItem Value="Cm" Text="Cm"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            Zoom factor:
                            <asp:DropDownList runat="server" ID="ddlZoomFactor">
                                <asp:ListItem Value="40" Text="40%"></asp:ListItem>
                                <asp:ListItem Value="60" Text="60%" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="80" Text="80%"></asp:ListItem>
                                <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                                <asp:ListItem Value="120" Text="120%" ></asp:ListItem>
                                <asp:ListItem Value="140" Text="140%" ></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <br style="clear:both;" /> 
                    </div>
                      
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                         
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


