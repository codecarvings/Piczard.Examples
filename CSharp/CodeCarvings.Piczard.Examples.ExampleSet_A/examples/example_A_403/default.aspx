<%@ Page Title="Piczard Examples | Customize Piczard | Customize layout" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_403_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="4" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="4" Title="A.403 - Customize layout" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function openPopupPictureTrimmer1Window()
            {
                var popupSizeMode = $("#<% =this.ddlPopupSizeMode.ClientID %>").val();
                var windowWidth, windowHeight;
            
                switch(popupSizeMode)
                {
                    case "0":
                        // Default size
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =this.PopupPictureTrimmer1.ClientID %>');
                        break;
                    case "1":
                        // Custom size #1
                        windowWidth = 900;
                        windowHeight = 540;
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =this.PopupPictureTrimmer1.ClientID %>', windowWidth, windowHeight);
                        break;
                    case "2":
                        // Custom size #2
                        windowWidth = $(window).width() - 50;
                        windowHeight = $(window).height() - 50;
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =this.PopupPictureTrimmer1.ClientID %>', windowWidth, windowHeight);
                        break;
                }
                
            }
            
            function initializeExampleUI()
            {
                // Background color
                var hexValue = $("#<% =this.txtBackColor.ClientID %>").val();
                $("#divBackColor div").css("backgroundColor", hexValue);
                $("#divBackColor").ColorPicker({
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
                            <% =this.ClientScript.GetPostBackEventReference(this.txtBackColor, "OnTextChanged", false) %>
                        });
                        return false;
                    },
                    onChange: function(hsb, hex, rgb)
                    {
                        var hexValue = "#" + hex;
                        $("#<% =this.txtBackColor.ClientID %>").val(hexValue);
                        $("#divBackColor div").css("backgroundColor", hexValue);
                    }
                });            
                
                // Foreground color
                hexValue = $("#<% =this.txtForeColor.ClientID %>").val();
                $("#divForeColor div").css("backgroundColor", hexValue);
                $("#divForeColor").ColorPicker({
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
                            <% =this.ClientScript.GetPostBackEventReference(this.txtForeColor, "OnTextChanged", false) %>
                        });
                        return false;
                    },
                    onChange: function(hsb, hex, rgb)
                    {
                        var hexValue = "#" + hex;
                        $("#<% =this.txtForeColor.ClientID %>").val(hexValue);
                        $("#divForeColor div").css("backgroundColor", hexValue);
                    }
                });
            
                // Canvas color
                hexValue = $("#<% =this.txtCanvasColor.ClientID %>").val();
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
                
                // Image back color
                hexValue = $("#<% =this.txtImageBackColor.ClientID %>").val();
                $("#divImageBackColor div").css("backgroundColor", hexValue);
                $("#divImageBackColor").ColorPicker({
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
                            <% =this.ClientScript.GetPostBackEventReference(this.txtImageBackColor, "OnTextChanged", false) %>
                        });
                        return false;
                    },
                    onChange: function(hsb, hex, rgb)
                    {
                        var hexValue = "#" + hex;
                        $("#<% =this.txtImageBackColor.ClientID %>").val(hexValue);
                        $("#divImageBackColor div").css("backgroundColor", hexValue);
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
                           
                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="500px" 
                AutoFreezeOnFormSubmit="true"              
                BorderStyle="None" BorderColor="#ffffff" BorderWidth="0px"
                 /><br />

                <ccPiczard:PopupPictureTrimmer ID="PopupPictureTrimmer1" runat="server" />
                
                <div class="InlinePanel1" style="padding:5px; margin-top:10px;">      
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td align="left" valign="middle" style="width:180px; height:1px;"></td>
                            <td align="left" valign="middle" style="width:34px; height:1px;"></td>
                            <td align="left" valign="middle"></td>
                        </tr>
                        
                        <tr>
                            <td align="left" valign="middle">
                                Show details panel: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowDetailsPanel" Checked="true" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Show zoom panel: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowZoomPanel" Checked="false" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Allow resize: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbAllowResize" Checked="false" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Show resize panel: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowResizePanel" Checked="false" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Show rotate panel: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowRotatePanel" Checked="true" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Show flip panel: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowFlipPanel" Checked="true" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Show adjustments panel: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowImageAdjustmentsPanel" Checked="true" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Show crop alignment lines: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowCropAlignmentLines" Checked="true" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Enable snapping: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbEnableSnapping" Checked="true" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Show rulers: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:CheckBox runat="server" id="cbShowRulers" Checked="true" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                UI unit: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:DropDownList runat="server" ID="ddlUIUnit" AutoPostBack="true">
                                    <asp:ListItem Value="Pixel" Text="Pixel" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Point" Text="Point"></asp:ListItem>
                                    <asp:ListItem Value="Pica" Text="Pica"></asp:ListItem>
                                    <asp:ListItem Value="Inch" Text="Inch"></asp:ListItem>
                                    <asp:ListItem Value="Mm" Text="Mm"></asp:ListItem>
                                    <asp:ListItem Value="Cm" Text="Cm"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>                   

                        <tr>
                            <td align="left" valign="middle">
                                Background color: 
                            </td>
                            <td align="left" valign="middle">
                                <div id="divBackColor" class="ColorSelector"><div> </div></div>
                            </td>
                            <td align="left" valign="middle">
                               <asp:TextBox runat="server" ID="txtBackColor" style="width:70px; text-align:center;" Enabled="false">#e6e6e6</asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Foreground color: 
                            </td>
                            <td align="left" valign="middle">
                                <div id="divForeColor" class="ColorSelector"><div> </div></div>
                            </td>
                            <td align="left" valign="middle">
                               <asp:TextBox runat="server" ID="txtForeColor" style="width:70px; text-align:center;" Enabled="false">#000000</asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Canvas color: 
                            </td>
                            <td align="left" valign="middle">
                                <div id="divCanvasColor" class="ColorSelector"><div> </div></div>
                            </td>
                            <td align="left" valign="middle">
                               <asp:TextBox runat="server" ID="txtCanvasColor" style="width:70px; text-align:center;" Enabled="false">#ffffff</asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Image back color: 
                            </td>
                            <td align="left" valign="middle">
                                <div id="divImageBackColor" class="ColorSelector"><div> </div></div>
                            </td>
                            <td align="left" valign="middle">
                               <asp:TextBox runat="server" ID="txtImageBackColor" style="width:70px; text-align:center;" Enabled="false">#ffffff</asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Crop shadow mode: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:DropDownList runat="server" ID="ddlCropShadowMode" AutoPostBack="true">
                                    <asp:ListItem Value="Standard" Text="Standard" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Flat" Text="Flat"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr> 
                        
                        <tr>
                            <td align="left" valign="middle" colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height:36px;">
                                Popup Css class: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:DropDownList runat="server" ID="ddlPopupLightBoxCssClass" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Default LightBox Css Class"></asp:ListItem>
                                    <asp:ListItem Value="ccPiczard_LightBox_Custom1" Text="Custom LightBox Css Class # 1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="ccPiczard_LightBox_Custom2" Text="Custom LightBox Css Class # 2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height:36px;">
                                Popup size: 
                            </td>
                            <td align="left" valign="middle" colspan="2">
                                <asp:DropDownList runat="server" ID="ddlPopupSizeMode">
                                    <asp:ListItem Value="0" Text="Default window size"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Custom window size #1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Custom window size #2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height:36px;">
                            </td>
                            <td align="left" valign="middle" colspan="2">                                                            
                                <asp:Button runat="server" ID="btnOpenPopupPictureTrimmer1" Text="Open PopupPictureTrimmer Window..." CausesValidation="false"
                                OnClientClick="openPopupPictureTrimmer1Window(); return false;" />
                            </td>
                        </tr>
                    </table>                               
                </div>  
                <br />
                <br />                
                               
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>

