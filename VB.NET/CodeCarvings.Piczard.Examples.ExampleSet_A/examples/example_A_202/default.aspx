<%@ Page Title="Piczard Examples | Image manipulation | Resize an image | Automatic" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_202_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="2" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="2" Title="A.202 - Resize an image - Automatic" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <div class="ExampleTableContainer">
        
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Image<br />
                </span>
                <asp:Image runat="server" ID="imgSource" ImageUrl="~/repository/source/trevi1.jpg" AlternateText="Piczard Source Image" CssClass="ExampleTableImage" />
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
                                <div style="text-align: left; width: 350px;">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                        <tr>
                                            <td align="left" valign="middle" style="width:180px" class="DefaultTableFormCellHeight">
                                                Resize mode:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlResizeMode" AutoPostBack="true">
                                                    <asp:ListItem Value="Fixed" Text="Fixed" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Scaled" Text="Scaled"></asp:ListItem>
                                                </asp:DropDownList><br />                                        
                                            </td>
                                        </tr>
                                        
                                        <asp:PlaceHolder runat="server" ID="phResizeMode_Fixed">
                                            <tr>
                                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                    Size:<br />
                                                </td>
                                                <td align="left" valign="middle"> 
                                                     <asp:DropDownList runat="server" ID="ddlConstraints_Fixed" AutoPostBack="true">
                                                        <asp:ListItem Text="100 x 100 Pixel" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="150 x 250 Pixel"></asp:ListItem>
                                                        <asp:ListItem Text="580 x 500 Pixel"></asp:ListItem>
                                                        <asp:ListItem Text="2 x 2 Inch (96 DPI)"></asp:ListItem>
                                                    </asp:DropDownList><br />                                           
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                    Image position:<br />
                                                </td>
                                                <td align="left" valign="middle"> 
                                                     <asp:DropDownList runat="server" ID="ddlFixedImagePosition" AutoPostBack="true">
                                                        <asp:ListItem Value="Fit" Text="Fit" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="Fill" Text="Fill"></asp:ListItem>
                                                        <asp:ListItem Value="Stretch" Text="Stretch"></asp:ListItem>
                                                    </asp:DropDownList><br />                                           
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                    Back color:
                                                </td>
                                                <td align="left" valign="middle">
                                                    <div style="float:left;">
                                                        <div id="divFixedCanvasColor" class="ColorSelector"><div> </div></div>
                                                    </div>
                                                    <div style="float:left; margin-top:2px;">
                                                        <asp:TextBox runat="server" ID="txtFixedCanvasColor" style="width:70px; text-align:center;" Enabled="false">#ffffff</asp:TextBox>
                                                    </div>
                                                    <br style="clear:both;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                    Enlarge small images:<br />
                                                </td>
                                                <td align="left" valign="middle"> 
                                                    <asp:CheckBox runat="server" ID="cbFixedEnlargeSmallImages" Checked="true" AutoPostBack="true" /><br />                                           
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        
                                        <asp:PlaceHolder runat="server" ID="phResizeMode_Scaled">
                                             <tr>
                                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                    Max size:<br />
                                                </td>
                                                <td align="left" valign="middle">
                                                     <asp:DropDownList runat="server" ID="ddlConstraints_Scaled" AutoPostBack="true">
                                                        <asp:ListItem Text="100 x 100 Pixel" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="150 x 250 Pixel"></asp:ListItem>
                                                        <asp:ListItem Text="580 x 500 Pixel"></asp:ListItem>
                                                        <asp:ListItem Text="2 x 2 Inch (96 DPI)"></asp:ListItem>
                                                    </asp:DropDownList><br />   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                    Enlarge small images:<br />
                                                </td>
                                                <td align="left" valign="middle"> 
                                                    <asp:CheckBox runat="server" ID="cbScaledEnlargeSmallImages" Checked="true" AutoPostBack="true" /><br />                                           
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                    </table>
                                </div>
                                
                                <br />
                                <asp:Button runat="server" ID="btnProcess" Text="Apply &raquo;" CausesValidation="false" CssClass="ButtonText" /><br />
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
                        <pre id="shCode1" class="brush: vb.net"><asp:Literal runat="server" ID="litCode"></asp:Literal></pre>
                    </asp:PlaceHolder> 
                
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <script type="text/javascript">
            //<![CDATA[

            function initializeExampleUI()
            {
                var hexValue = $("#<% =Me.txtFixedCanvasColor.ClientID %>").val();
                $("#divFixedCanvasColor div").css("backgroundColor", hexValue);
                $("#divFixedCanvasColor").ColorPicker({
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
                            <% =Me.ClientScript.GetPostBackEventReference(Me.txtFixedCanvasColor, "OnTextChanged", false) %>
                        });
                        return false;
                    },
                    onChange: function(hsb, hex, rgb)
                    {
                        var hexValue = "#" + hex;
                        $("#<% =Me.txtFixedCanvasColor.ClientID %>").val(hexValue);
                        $("#divFixedCanvasColor div").css("backgroundColor", hexValue);
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


