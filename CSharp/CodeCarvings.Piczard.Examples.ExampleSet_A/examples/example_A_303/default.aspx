<%@ Page Title="Piczard Examples | Web - PictureTrimmer | Crop disabled" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_303_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.303 - Crop disabled" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <asp:PlaceHolder runat="server" ID="phBeforeLoad">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr>
                                <td align="left" valign="middle" style="width:130px" class="DefaultTableFormCellHeight">
                                    Unit (96 DPI):
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList runat="server" ID="ddlUnit">
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
                                <td align="left" valign="middle" colspan="2" class="DefaultTableFormCellSeparator">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                    Enable resize:
                                </td>
                                <td align="left" valign="middle">
                                    <asp:CheckBox runat="server" ID="cbEnableResize" Checked="true" /><br />                                                
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button runat="server" ID="btnLoadImage" Text="Load image" OnClick="btnLoadImage_Click" CausesValidation="false" />
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="phAfterLoad" Visible="false">
                        <asp:Button runat="server" ID="btnUnloadImage" Text="Unload image" OnClick="btnUnloadImage_Click" CausesValidation="false" />
                    </asp:PlaceHolder>
                </div>
            
                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="500px" 
                 ShowImageAdjustmentsPanel="true" ShowZoomPanel="false" Culture="en" AutoFreezeOnFormSubmit="true" Visible="false" />

                <asp:PlaceHolder runat="server" ID="phCodeContainer" Visible="false">                   
                    <div style="text-align:center;">               
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


