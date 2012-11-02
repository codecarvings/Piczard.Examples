<%@ Page Title="Piczard Examples | Image manipulation | Apply a text watermark" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_223_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="2" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="2" Title="A.223 - Apply a text watermark" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <div class="ExampleTableContainer">
        
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Image<br />
                </span>
                <asp:Image runat="server" ID="imgSource" ImageUrl="~/repository/source/temple1.jpg" AlternateText="Piczard Source Image" CssClass="ExampleTableImage" />
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
                            <td align="center" valign="top" class="ExampleTableProcessBottomCell">
                                <br />
                                <div style="text-align: left; width: 500px;">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                        <tr>
                                            <td align="left" valign="middle" style="width:210px" class="DefaultTableFormCellHeight">
                                                Text:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:TextBox runat="server" ID="txtText" MaxLength="30" Width="220px">Piczard</asp:TextBox><br />                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Alignment:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlContentAlignment" AutoPostBack="true">
                                                    <asp:ListItem Value="TopLeft" Text="Top - Left" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="TopCenter" Text="Top - Center"></asp:ListItem>
                                                    <asp:ListItem Value="TopRight" Text="Top - Right"></asp:ListItem>
                                                    <asp:ListItem Value="MiddleLeft" Text="Middle - Left"></asp:ListItem>
                                                    <asp:ListItem Value="MiddleCenter" Text="Middle - Center"></asp:ListItem>
                                                    <asp:ListItem Value="MiddleRight" Text="Middle - Right"></asp:ListItem>
                                                    <asp:ListItem Value="BottomLeft" Text="Bottom - Left"></asp:ListItem>
                                                    <asp:ListItem Value="BottomCenter" Text="Bottom - Center"></asp:ListItem>
                                                    <asp:ListItem Value="BottomRight" Text="Bottom - Right"></asp:ListItem>
                                                </asp:DropDownList><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Unit (96 DPI):
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlMainUnit" AutoPostBack="true">
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
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Displacement:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlContentDisplacementX" AutoPostBack="true">
                                                </asp:DropDownList>
                                                -
                                                <asp:DropDownList runat="server" ID="ddlContentDisplacementY" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>                                       
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Foreground color:
                                            </td>
                                            <td align="left" valign="middle">
                                                <div style="float:left;">
                                                    <div id="divForeColor" class="ColorSelector"><div> </div></div>
                                                </div>
                                                <div style="float:left; margin-top:2px;">
                                                    <asp:TextBox runat="server" ID="txtForeColor" style="width:70px; text-align:center;" Enabled="false">#606060</asp:TextBox>
                                                    - Alpha:
                                                    <asp:DropDownList runat="server" ID="ddlForeColorAlpha" AutoPostBack="true">
                                                    </asp:DropDownList>                                               
                                                </div>
                                                <br style="clear:both;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Font:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlFontName" AutoPostBack="true">
                                                    <asp:ListItem Value="GenericSansSerif" Text="Generic Sans Serif" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="GenericSerif" Text="Generic Serif"></asp:ListItem>
                                                    <asp:ListItem Value="GenericMonospace" Text="Generic Monospace"></asp:ListItem>
                                                    <asp:ListItem Value="Arial" Text="Arial"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList runat="server" ID="ddlFontSize" AutoPostBack="true">
                                                </asp:DropDownList><br />
                                                <asp:CheckBox runat="server" ID="cbFontBold" Checked="false" AutoPostBack="true" Text="Bold" />
                                                &nbsp;
                                                <asp:CheckBox runat="server" ID="cbFontItalic" Checked="false" AutoPostBack="true" Text="Italic" />
                                                &nbsp;
                                                <asp:CheckBox runat="server" ID="cbFontUnderline" Checked="false" AutoPostBack="true" Text="Underline" />
                                                &nbsp;
                                                <asp:CheckBox runat="server" ID="cbFontStrikeout" Checked="false" AutoPostBack="true" Text="Strikeout" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Text rendering hint (quality):
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlTextRenderingHint" AutoPostBack="true">
                                                    <asp:ListItem Value="SystemDefault" Text="SystemDefault"></asp:ListItem>
                                                    <asp:ListItem Value="SingleBitPerPixelGridFit" Text="SingleBitPerPixelGridFit"></asp:ListItem>
                                                    <asp:ListItem Value="SingleBitPerPixel" Text="SingleBitPerPixel"></asp:ListItem>
                                                    <asp:ListItem Value="AntiAliasGridFit" Text="AntiAliasGridFit"></asp:ListItem>
                                                    <asp:ListItem Value="AntiAlias" Text="AntiAlias"></asp:ListItem>
                                                    <asp:ListItem Value="ClearTypeGridFit" Text="ClearTypeGridFit" Selected="True"></asp:ListItem>
                                                </asp:DropDownList><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Text contrast (gamma correction):
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlTextContrast" AutoPostBack="true">
                                                </asp:DropDownList><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                String format (other options):
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:CheckBox runat="server" ID="cbStringFormatFlags_DirectionVertical" Checked="false" AutoPostBack="true" Text="Vertical" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                
                                <br />
                                <asp:Button runat="server" ID="btnProcess" Text="Apply &raquo;" CausesValidation="false" OnClick="btnProcess_Click" CssClass="ButtonText" /><br />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" class="ExampleTableProcessBottomCellSpacer">
                            </td>
                        </tr>   
                        
                        <asp:PlaceHolder runat="server" ID="phOutputContainer">
                            <tr>
                                <td align="center" valign="middle">
                                    <span class="ExampleTableCellTitle">
                                        3 - Output Image<br />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle">
                                    <br />
                                    <div style="width: 600px; overflow-x: auto;">
                                        <asp:Image runat="server" ID="imgOutput" AlternateText="Piczard Output Image" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" />                        
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
                        <pre id="shCode1" class="brush: c-sharp"><asp:Literal runat="server" ID="litCode"></asp:Literal></pre>
                    </asp:PlaceHolder> 
                    
                </ContentTemplate>
            </asp:UpdatePanel>                
        </div>  

        <script type="text/javascript">
            //<![CDATA[

            function initializeExampleUI()
            {
                var hexValue = $("#<% =this.txtForeColor.ClientID %>").val();
                $("#divForeColor div").css("backgroundColor", hexValue);
                $("#divForeColor").ColorPicker({
                    color: hexValue,
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


