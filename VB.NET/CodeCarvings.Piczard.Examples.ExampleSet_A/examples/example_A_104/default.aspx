<%@ Page Title="Piczard Examples | Basics | Batch process multiple images" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_104_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="1" Title="A.104 - Batch process multiple images" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <div class="ExampleTableContainer">      
          
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Images<br />
                </span>
                <div id="sourceImagesGalleria" style="text-align:left; width:400px; height:400px; margin: 10px auto;">
                    <asp:Repeater runat="server" ID="rptSourceImages">
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgSource" />                         
                        </ItemTemplate>
                    </asp:Repeater>
                </div>      
            </div>
        
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
    
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">                        
                        <tr>
                            <td align="center" valign="top" class="ExampleTableProcessTopCell">
                                <asp:Image runat="server" ImageUrl="~/design/gfx/saTop.jpg" AlternateText="Processing1" CssClass="ExampleTableProcessTopCellImage" />
                                <div class="ExampleTableProcessTopCellTitle">
                                    2 - Image Processing
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" class="ExampleTableProcessBottomCell ExampleTableText">
                                <br />
            
                                <div style="width:170px; text-align:left;">
                                    <asp:CheckBox ID="cbFilterCrop" runat="server" Text="Automatic crop" CausesValidation="false" Checked="true"  AutoPostBack="true" /><br />
                                    <div class="LineSpacer1">
                                    </div>
                                    <asp:CheckBox ID="cbFilterColor" runat="server" Text="Sepia tone" CausesValidation="false" Checked="true" AutoPostBack="true" /><br />
                                    <div class="LineSpacer1">
                                    </div>
                                    <asp:CheckBox ID="cbFilterWatermark" runat="server" Text="Apply watermark" CausesValidation="false" Checked="true" AutoPostBack="true" /><br />
                                </div>
                                
                                <br />                                
                                <br />
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right" valign="middle" style="width:50%">
                                            <div style="margin:5px;">
                                                Output image format:
                                            </div>
                                        </td>
                                        <td align="left" valign="middle" style="width:50%">
                                            <div style="margin:5px;">
                                                <asp:RadioButton runat="server" ID="rbFormatCustom" Text="Custom format:" CausesValidation="false" GroupName="OutputFormat" AutoPostBack="true" Checked="true" />
                                                <asp:DropDownList runat="server" ID="ddlImageFormat" CausesValidation="false" AutoPostBack="true">
                                                    <asp:ListItem Value=".JPG" Text="JPEG" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value=".GIF" Text="GIF"></asp:ListItem>
                                                    <asp:ListItem Value=".PNG" Text="PNG"></asp:ListItem>
                                                </asp:DropDownList><br />
                                                <asp:RadioButton runat="server" ID="rbFormatAuto" Text="Same format as source image" CausesValidation="false" GroupName="OutputFormat" AutoPostBack="true" /><br />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                
                                <br />
                                <br />
                                <asp:Button runat="server" ID="btnProcess" Text="Batch process images &raquo;" CausesValidation="false" CssClass="ButtonText" /><br />
                                
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" class="ExampleTableProcessBottomCellSpacer">
                            </td>
                        </tr>                        
                        <asp:PlaceHolder runat="server" ID="phOutputContainer" Visible="false">
                            <tr>
                                <td align="center" valign="middle">
                                    <span class="ExampleTableCellTitle">
                                        3 - Output Images<br />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle">
                                    <div id="outputImagesGalleria" style="text-align:left; width:400px; height:400px; margin: 10px auto;">
                                        <asp:Repeater runat="server" ID="rptOutputImages">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="imgOutput" />                         
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>  
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                    </table>

                    <asp:PlaceHolder runat="server" ID="phCodeContainer">    
                        <div class="ExampleTableCell">               
                            <br />
                            <br />
                            <br />
                            <span class="ExampleTableCellTitle">
                                --- Source Code ---<br />
                            </span>
                        </div>
                        <pre id="shCode1" class="brush: vb.net"><asp:Literal runat="server" ID="litCode"></asp:Literal></pre>
                    </asp:PlaceHolder> 
            
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <script type="text/javascript">
            //<![CDATA[

            // Load the classic theme
            Galleria.loadTheme('<% =HttpUtility.HtmlAttributeEncode(Me.ResolveUrl("~/design/libraries/jquery/aino-galleria/themes/classic/galleria.classic.js")) %>');

            function initializeExampleUI()
            {
                var oOutputImagesGalleria = $("#outputImagesGalleria");
                if (oOutputImagesGalleria.length)
                {
                    oOutputImagesGalleria.galleria({
                        show_info: false
                    });
                }
            }

            $(function()
            {
                $("#sourceImagesGalleria").galleria({
                    show_info: false
                });
            });
            //]]>
        </script>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>

