<%@ Page Title="Piczard Examples | Web - PictureTrimmer | Crop enabled" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_302_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.302 - Crop enabled" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <asp:PlaceHolder runat="server" ID="phBeforeLoad">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr>
                                <td align="left" valign="middle" style="width:180px" class="DefaultTableFormCellHeight">
                                    Unit / Resolution:
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList runat="server" ID="ddlUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                        <asp:ListItem Value="Pixel" Text="Pixel" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Point" Text="Point"></asp:ListItem>
                                        <asp:ListItem Value="Pica" Text="Pica"></asp:ListItem>
                                        <asp:ListItem Value="Inch" Text="Inch"></asp:ListItem>
                                        <asp:ListItem Value="Mm" Text="Mm"></asp:ListItem>
                                        <asp:ListItem Value="Cm" Text="Cm"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:DropDownList runat="server" ID="ddlDPI">
                                        <asp:ListItem Value="96" Text="96 DPI" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100 DPI"></asp:ListItem>
                                        <asp:ListItem Value="200" Text="200 DPI"></asp:ListItem>
                                        <asp:ListItem Value="300" Text="300 DPI"></asp:ListItem>
                                    </asp:DropDownList> 
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="2" class="DefaultTableFormCellSeparator">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                    Crop mode:
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList runat="server" ID="ddlCropMode" AutoPostBack="true">
                                        <asp:ListItem Value="Fixed" Text="Fixed"></asp:ListItem>
                                        <asp:ListItem Value="FixedAspectRatio" Text="Fixed Aspect Ratio"></asp:ListItem>
                                        <asp:ListItem Value="Free" Text="Free" Selected="True"></asp:ListItem>
                                    </asp:DropDownList><br />                                                
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="phCropMode_Fixed">
                                <tr>
                                    <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                        Size:
                                    </td>
                                    <td align="left" valign="middle">
                                        Width:
                                        <asp:DropDownList runat="server" ID="ddlConstraint_Fixed_Width">
                                        </asp:DropDownList>
                                        - Height:
                                        <asp:DropDownList runat="server" ID="ddlConstraint_Fixed_Height">
                                        </asp:DropDownList>     
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="phCropMode_FixedAspectRatio">
                                <tr>
                                    <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                        Aspect ratio:
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:DropDownList runat="server" ID="ddlConstraint_FixedAspectRatio_X">
                                        </asp:DropDownList> / <asp:DropDownList runat="server" ID="ddlConstraint_FixedAspectRatio_Y">
                                        </asp:DropDownList>     
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                        Constraints:
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:DropDownList runat="server" ID="ddlConstraint_FixedAspectRatio_LimitedDimension">
                                            <asp:ListItem Value="Width" Text="Width" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Height" Text="Height"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                        <asp:CheckBox runat="server" ID="cb_FixedAspectRatio_Min" Checked="false" AutoPostBack="true" Text="Min:" TextAlign="Left" />
                                        <asp:DropDownList runat="server" ID="ddlConstraint_FixedAspectRatio_Min">
                                        </asp:DropDownList>
                                        &nbsp;-&nbsp;
                                        <asp:CheckBox runat="server" ID="cb_FixedAspectRatio_Max" Checked="false" AutoPostBack="true" Text="Max:" TextAlign="Left" />                                        
                                        <asp:DropDownList runat="server" ID="ddlConstraint_FixedAspectRatio_Max">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="phCropMode_Free">
                                <tr>
                                    <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                        Constraints:
                                    </td>
                                    <td align="left" valign="middle">
                                        Width
                                        <asp:CheckBox runat="server" ID="cb_Free_Width_Min" Checked="false" AutoPostBack="true" Text="Min:" TextAlign="Left" />
                                        <asp:DropDownList runat="server" ID="ddlConstraint_Free_Width_Min">
                                        </asp:DropDownList>
                                        &nbsp;-&nbsp;
                                        <asp:CheckBox runat="server" ID="cb_Free_Width_Max" Checked="false" AutoPostBack="true" Text="Max:" TextAlign="Left" />                                        
                                        <asp:DropDownList runat="server" ID="ddlConstraint_Free_Width_Max">
                                        </asp:DropDownList><br />
                                        Height
                                        <asp:CheckBox runat="server" ID="cb_Free_Height_Min" Checked="false" AutoPostBack="true" Text="Min:" TextAlign="Left" />
                                        <asp:DropDownList runat="server" ID="ddlConstraint_Free_Height_Min">
                                        </asp:DropDownList>
                                        &nbsp;-&nbsp;
                                        <asp:CheckBox runat="server" ID="cb_Free_Height_Max" Checked="false" AutoPostBack="true" Text="Max:" TextAlign="Left" />                                        
                                        <asp:DropDownList runat="server" ID="ddlConstraint_Free_Height_Max">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <td align="left" valign="middle" colspan="2" class="DefaultTableFormCellSeparator">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                    Margins:
                                </td>
                                <td align="left" valign="middle">
                                    Horizontal:
                                    <asp:DropDownList runat="server" ID="ddlMarginsH">
                                    </asp:DropDownList>
                                    - Vertical:
                                    <asp:DropDownList runat="server" ID="ddlMarginsV">
                                    </asp:DropDownList>   
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" class="DefaultTableFormCellHeight">
                                    Default selection strategy:
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList runat="server" ID="ddlImageSelectionStrategy" CausesValidation="false">
                                        <asp:ListItem Value="Slice" Text="Slice" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="WholeImage" Text="Select whole image"></asp:ListItem>
                                        <asp:ListItem Value="DoNotResize" Text="Do not resize"></asp:ListItem>
                                    </asp:DropDownList><br />
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
                Culture="en" AutoFreezeOnFormSubmit="true" Visible="false" />

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


