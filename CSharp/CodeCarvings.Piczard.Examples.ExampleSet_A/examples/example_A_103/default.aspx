<%@ Page Title="Piczard Examples | Basics | Load & save image files" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_103_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="1" Title="A.103 - Load & save image files" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <div class="ExampleTableContainer">  
                        
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
    
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">    
                        <tr>
                            <td align="center" valign="middle">
                                <span class="ExampleTableCellTitle">
                                    1 - Load Image<br />
                                </span>
                                <br />
                                <strong>Source Type:</strong>
                                &nbsp;
                                <asp:DropDownList runat="server" ID="ddlSourceType" CausesValidation="false">
                                    <asp:ListItem Text="File System" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Byte Array"></asp:ListItem>
                                    <asp:ListItem Text="Stream"></asp:ListItem>
                                </asp:DropDownList>

                                <div class="SeparatorLine1" style="margin:10px 0px;">
                                </div>

                                <span class="ExampleTableCellTitle">
                                    2 - Save Image<br />
                                </span>
                                <br />
                                <strong>Output Type:</strong>
                                &nbsp;
                                <asp:DropDownList runat="server" ID="ddlOutputType" CausesValidation="false">
                                    <asp:ListItem Text="File System" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Byte Array"></asp:ListItem>
                                    <asp:ListItem Text="Stream"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <strong>Image Format:</strong>
                                &nbsp;
                                <asp:DropDownList runat="server" ID="ddlImageFormat" CausesValidation="false">
                                    <asp:ListItem Value="Auto" Text="Default"></asp:ListItem>
                                    <asp:ListItem Value="JPEG" Text="JPEG" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="GIF" Text="GIF"></asp:ListItem>
                                    <asp:ListItem Value="PNG" Text="PNG"></asp:ListItem>
                                </asp:DropDownList><br />
                                <br />
                                
                                <div id="outputImageFormat_JPEG" class="InlinePanel1" style="display:none; width:300px; padding:10px; text-align:center;">
                                    Image Quality:
                                    <asp:TextBox runat="server" ID="txtJpegQuality" Enabled="false" style="width:40px; text-align:center;" Text="92"></asp:TextBox>
                                    % <br />
                                    <table border="0" cellspacing="0" cellpadding="0" style="width:100%">
                                        <tr>
                                            <td align="center" valign="middle" style="width:70px; font-size:11px;">
                                                Low Quality<br />
                                                <div class="SeparatorLine2" style="width:50px;">
                                                </div>
                                                Small file
                                            </td>
                                            <td align="center" valign="middle">
                                                <div id="jpegQualitySlider"></div>
                                            </td>
                                            <td align="center" valign="middle" style="width:70px; font-size:11px;">
                                                Hi Quality<br />
                                                <div class="SeparatorLine2" style="width:50px;">
                                                </div>
                                                Large file
                                            </td>
                                        </tr>
                                    </table>                                    
                                </div>
                                <div id="outputImageFormat_GIF" class="InlinePanel1" style="display:none; width:300px; padding:10px; text-align:center;">
                                    <asp:CheckBox runat="server" ID="cbGifQuantize" Text="Quantize image" TextAlign ="Right" Checked="true" AutoPostBack="true" CausesValidation="false" />
                                    &nbsp;&nbsp;&nbsp;<a href="http://en.wikipedia.org/wiki/Color_quantization" onclick="window.open('http://en.wikipedia.org/wiki/Color_quantization'); return false;">What's image quantization?</a><br />
                                    
                                    <br />
                                    Maximum number of colors:
                                    <asp:DropDownList runat="server" ID="ddlGifMaxColors" CausesValidation="false">
                                        <asp:ListItem Value="256" Selected="True">256</asp:ListItem>
                                        <asp:ListItem Value="128">128</asp:ListItem>
                                        <asp:ListItem Value="64">64</asp:ListItem>
                                        <asp:ListItem Value="32">32</asp:ListItem>
                                        <asp:ListItem Value="16">16</asp:ListItem>
                                    </asp:DropDownList><br />                       
                                </div>
                                <div id="outputImageFormat_PNG" class="InlinePanel1" style="display:none; width:300px; padding:10px; text-align:center;">
                                    <asp:CheckBox runat="server" ID="cbPngConvertToIndex" Text="Convert to indexed" TextAlign ="Right" Checked="false" AutoPostBack="true" CausesValidation="false" /><br />
                                    
                                    <br />
                                    Maximum number of colors:
                                    <asp:DropDownList runat="server" ID="ddlPngMaxColors" CausesValidation="false">
                                        <asp:ListItem Value="256" Selected="True">256</asp:ListItem>
                                        <asp:ListItem Value="128">128</asp:ListItem>
                                        <asp:ListItem Value="64">64</asp:ListItem>
                                        <asp:ListItem Value="32">32</asp:ListItem>
                                        <asp:ListItem Value="16">16</asp:ListItem>
                                    </asp:DropDownList><br />          
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" class="ExampleTableProcessBottomCell">
                                <asp:Image runat="server" ImageUrl="~/design/gfx/saTop.jpg" AlternateText="Processing1" CssClass="ExampleTableProcessTopCellImage" />
                                <br />
                                <br />
                                <asp:Button runat="server" ID="btnProcess" Text="Preview &raquo;" CausesValidation="false" OnClick="btnProcess_Click" /><br />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle">
                                <asp:PlaceHolder runat="server" ID="phOutputPreview" Visible="false">
                                    <div style="width:580px;">
                                        <div style="float:left; margin: 10px;">
                                            <span class="ExampleTableCellTitle">
                                                Loaded Image<br />
                                            </span>
                                            <br />
                                            <div style="width:260px; height:173px; overflow:hidden; border: solid 1px #cccccc; background-image:url('<% =this.ResolveUrl("~/design/gfx/check.gif") %>');">    
                                                <asp:Image runat="server" ID="imgLoaded" AlternateText="Loaded Image" ImageUrl="~/repository/source/flowers2.png" Width="260" Height="173" />                              
                                            </div>
                                            <br />
                                            <asp:Literal runat="server" ID="litLoadedImageFileDetails"></asp:Literal>
                                        </div>
                                        <div style="float:left; margin: 10px;">
                                            <span class="ExampleTableCellTitle">
                                                Saved Image<br />
                                            </span>              
                                            <br />                              
                                            <div style="width:260px; height:173px; overflow:hidden; border: solid 1px #cccccc; background-image:url('<% =this.ResolveUrl("~/design/gfx/check.gif") %>');">                                    
                                                <asp:Image runat="server" ID="imgSaved" AlternateText="Saved Image" Width="260" Height="173" />                              
                                            </div>
                                            <br />
                                            <asp:Literal runat="server" ID="litSavedImageFileDetails"></asp:Literal>
                                        </div>
                                        <br style="clear: both;" />
                                    </div>
                                </asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>

                    <asp:PlaceHolder runat="server" ID="phCodeContainer" Visible="false">                   
                        <div class="ExampleTableCell">               
                            <br />
                            <br />
                            <br />
                            <span class="ExampleTableCellTitle">
                                --- Source Code ---<br />
                            </span>
                        </div>                 
                        <pre id="shCode1" class="brush: c-sharp"><asp:Literal runat="server" ID="litCode"></asp:Literal></pre>
                    </asp:PlaceHolder> 

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <script type="text/javascript">
            //<![CDATA[

            function getDisplayedImageFormatDiv()
            {
                var result = null;
                $("#<% =this.ddlImageFormat.ClientID %>").children("option").each(function()
                {
                    var ext = $(this).val();
                    var oDiv = $("#outputImageFormat_" + ext);
                    if (oDiv.length)
                    {
                        if (oDiv.is(":visible"))
                        {
                            result = oDiv;
                            return false;
                        }
                    }
                });
                
                return result;
            }

            function imageFormatChange(useAnimation)
            {
                var oCurrentImageFormatDiv = getDisplayedImageFormatDiv();
                var oNewImageFormatDiv = $("#outputImageFormat_" + $("#<% =this.ddlImageFormat.ClientID %>").val());
                if (!oNewImageFormatDiv.length)
                {
                    oNewImageFormatDiv = null;
                }

                if (oNewImageFormatDiv != oCurrentImageFormatDiv)
                {
                    if (oCurrentImageFormatDiv != null)
                    {
                        if (useAnimation)
                        {
                            oCurrentImageFormatDiv.animate({ height: "toggle" }, "fast", function()
                            {
                                if (oNewImageFormatDiv != null)
                                {
                                    oNewImageFormatDiv.delay(150).animate({ height: "toggle" }, "fast");
                                }
                            });
                        }
                        else
                        {
                            oCurrentImageFormatDiv.hide();
                        }
                    }
                    else
                    {
                        if (oNewImageFormatDiv != null)
                        {
                            if (useAnimation)
                            {
                                oNewImageFormatDiv.animate({ height: "toggle" }, "fast");
                            }
                            else
                            {
                                oNewImageFormatDiv.show();
                            }
                        }
                    }
                }        
            }

            function initializeExampleUI()
            {
                $("#jpegQualitySlider").slider({
                    min: 1,
                    max: 100,
                    slide: function(event, ui)
                    {
                        $("#<% =this.txtJpegQuality.ClientID %>").val(ui.value);
                    },
                    animate: true,
                    value: parseInt($("#<% =this.txtJpegQuality.ClientID %>").val())
                });

                $("#<% =this.ddlImageFormat.ClientID %>").change(function()
                {
                    imageFormatChange(true);
                });

                imageFormatChange();
            }

            $(function()
            {
                initializeExampleUI();
            });
            //]]>
        </script>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>

