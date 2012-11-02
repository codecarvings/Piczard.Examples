<%@ Page Title="Piczard Examples | Image manipulation | Apply an image watermark" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_224_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="2" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="2" Title="A.224 - Apply an image watermark" />
        
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
                                <div style="text-align: left; width: 310px;">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                        <tr>
                                            <td align="left" valign="middle" style="width:110px" class="DefaultTableFormCellHeight">
                                                Image:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlImageSource" AutoPostBack="true">
                                                    <asp:ListItem Text="piczardWatermark1.png" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="codeCarvingsWatermark1.gif"></asp:ListItem>
                                                </asp:DropDownList><br />                                                
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
                                                Alpha:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlAlpha" AutoPostBack="true">
                                                </asp:DropDownList>  
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

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


