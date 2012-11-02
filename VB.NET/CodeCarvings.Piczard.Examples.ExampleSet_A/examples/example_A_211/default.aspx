<%@ Page Title="Piczard Examples | Image manipulation | Crop an image | Manual" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_211_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="2" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="2" Title="A.211 - Crop an image - Manual" />
        
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
                                <div style="text-align: left; width: 450px;">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                        <tr>
                                            <td align="left" valign="middle" style="width:150px" class="DefaultTableFormCellHeight">
                                                Crop rectangle:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlRectangle" AutoPostBack="true">
                                                    <asp:ListItem Text="Whole image" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="x=80, y=80, width=340, height=190 (Pixel)"></asp:ListItem>
                                                    <asp:ListItem Text="x=-20, y=-20, width=540, height=390 (Pixel)"></asp:ListItem>
                                                    <asp:ListItem Text="x=-0.5, y=--1, width=5, height=5 (Inch / 96 DPI)"></asp:ListItem>
                                                    <asp:ListItem Text="x=-1.5, y=--5, width=10.5, height=10.5 (Cm / 96 DPI)"></asp:ListItem>
                                                </asp:DropDownList><br />                                                
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Canvas color:
                                            </td>
                                            <td align="left" valign="middle">
                                                <div style="float:left;">
                                                    <div id="divCanvasColor" class="ColorSelector"><div> </div></div>
                                                </div>
                                                <div style="float:left; margin-top:2px;">
                                                    <asp:TextBox runat="server" ID="txtCanvasColor" style="width:70px; text-align:center;" Enabled="false">#d1d184</asp:TextBox>
                                                </div>
                                                <br style="clear:both;" />
                                            </td>
                                        </tr>
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
                var hexValue = $("#<% =Me.txtCanvasColor.ClientID %>").val();
                $("#divCanvasColor div").css("backgroundColor", hexValue);
                $("#divCanvasColor").ColorPicker({
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


