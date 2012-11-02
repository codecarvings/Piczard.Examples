<%@ Page Title="Piczard Examples | Basics | Working with different image resolutions" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_106_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="1" Title="A.106 - Working with different image resolutions" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <div class="ExampleTableContainer">
        
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Image<br />
                </span>
                <asp:Image runat="server" ID="imgSource" ImageUrl="~/repository/source/greece1.jpg" AlternateText="Piczard Source Image" CssClass="ExampleTableImage" />
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
                                <div style="width:300px; text-align:left;">
                                    <br />

                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                        <tr>
                                            <td align="left" valign="middle" style="width:150px;" class="DefaultTableFormCellHeight">
                                                Crop size:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlCropSize" AutoPostBack="true" CausesValidation="false">
                                                    <asp:ListItem Text="200 x 300 Pixel"></asp:ListItem>
                                                    <asp:ListItem Text="50 x 75 Points"></asp:ListItem>
                                                    <asp:ListItem Text="20 x 30 Mm"></asp:ListItem>
                                                    <asp:ListItem Text="0.6 x 0.9 Inch" Selected="True"></asp:ListItem>
                                                </asp:DropDownList><br />                                      
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                                Output resolution:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlOutputResolution" AutoPostBack="true" CausesValidation="false">
                                                    <asp:ListItem Value="72" Text="72 DPI"></asp:ListItem>
                                                    <asp:ListItem Value="96" Text="96 DPI (defalut)"></asp:ListItem>
                                                    <asp:ListItem Value="300" Text="300 DPI" Selected="True"></asp:ListItem>
                                                </asp:DropDownList><br />                                   
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
                                    <asp:Image runat="server" ID="imgOutput" AlternateText="Piczard Output Image" CssClass="ExampleTableImage" />                        
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

