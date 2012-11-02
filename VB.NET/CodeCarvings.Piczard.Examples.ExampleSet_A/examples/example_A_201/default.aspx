<%@ Page Title="Piczard Examples | Image manipulation | Resize an image | Manual" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_201_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="2" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="2" Title="A.201 - Resize an image - Manual" />
        
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
                                <div style="text-align: left; width: 180px;">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">                                       
                                        <tr>
                                            <td align="left" valign="middle" style="width:100px" class="DefaultTableFormCellHeight">
                                                Resize factor:
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlResizeFactor" AutoPostBack="true">
                                                        <asp:ListItem Value="10.5" Text="10.5%"></asp:ListItem>
                                                        <asp:ListItem Value="50" Text="50%" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="75" Text="75%"></asp:ListItem>
                                                        <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                                                        <asp:ListItem Value="150" Text="150%"></asp:ListItem>
                                                        <asp:ListItem Value="200" Text="200%"></asp:ListItem>
                                                </asp:DropDownList><br />
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
 
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


