<%@ Page Title="Piczard Examples | Web - PictureTrimmer | Overview" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_301_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.301 - Overview" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function initializeExampleUI()
            {
                var hexValue = $("#<% =Me.txtCanvasColor.ClientID %>").val();
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
                            <% =Me.ClientScript.GetPostBackEventReference(Me.txtCanvasColor, "OnTextChanged", false) %>
                        });
                        return false;
                    },
                    onChange: function(hsb, hex, rgb)
                    {
                        var hexValue = "#" + hex;
                        $("#<% =Me.txtCanvasColor.ClientID %>").val(hexValue);
                        $("#divCanvasColor div").css("backgroundColor", hexValue);
                    }
                });
            }

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
            
            $(function()
            {
                initializeExampleUI();
            });
            
            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <asp:DropDownList runat="server" ID="ddlImageSelectionStrategy" CausesValidation="false">
                        <asp:ListItem Value="Slice" Text="No margins"></asp:ListItem>
                        <asp:ListItem Value="WholeImage" Text="Select whole image" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="DoNotResize" Text="Do not resize"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlOutputResolution" AutoPostBack="true" CausesValidation="false">
                        <asp:ListItem Value="72" Text="72 DPI"></asp:ListItem>
                        <asp:ListItem Value="96" Text="96 DPI (defalut)" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="300" Text="300 DPI"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button runat="server" ID="btnLoadImage" Text="Load image" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnProcessImage" Text="Process image &raquo;" Enabled="false" CausesValidation="false" />                                              
                </div>
            
                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="500px" 
                ShowImageAdjustmentsPanel="true" Culture="en" AutoFreezeOnFormSubmit="true" /><br />

                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px; height:36px;">
                    <div style="float:left;">
                        Interface: 
                        <asp:DropDownList runat="server" ID="ddlInterfaceMode" AutoPostBack="true" CausesValidation="false">
                            <asp:ListItem Value="Full" Text="Full" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="Standard" Text="Standard"></asp:ListItem>
                            <asp:ListItem Value="Easy" Text="Easy"></asp:ListItem>
                            <asp:ListItem Value="Minimal" Text="Minimal"></asp:ListItem>
                            <asp:ListItem Value="Poor" Text="Poor"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlGfxUnit" AutoPostBack="true" CausesValidation="false">
                            <asp:ListItem Value="Pixel" Text="Pixel" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="Point" Text="Point"></asp:ListItem>
                            <asp:ListItem Value="Pica" Text="Pica"></asp:ListItem>
                            <asp:ListItem Value="Inch" Text="Inch"></asp:ListItem>
                            <asp:ListItem Value="Mm" Text="Mm"></asp:ListItem>
                            <asp:ListItem Value="Cm" Text="Cm"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
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
                            <asp:TextBox runat="server" ID="txtCanvasColor" style="width:70px; text-align:center;" Enabled="false">#ffffff</asp:TextBox>
                        </div>
                        <br style="clear:both;" />
                    </div>
                    <br style="clear:both;" />                     
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


