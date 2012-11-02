<%@ Page Title="Piczard Examples | Web - Image Upload Demos | Programmatic Image Processing | Default file upload #1 | Edit record" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="editRecord.aspx.vb" Inherits="examples_example_A_511_editRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="5" Title="A.511 - Programmatic Image Processing - Default file upload #1 - Edit record" />
        
        <div class="EditBox">
            <table border="0" cellpadding="2" cellspacing="0" style="width:100%">
                <tr>
                    <td align="left" valign="middle" style="width:100px; height:35px;">
                        <strong>Record id: <br /></strong>
                    </td>
                    <td align="left" valign="middle">
                        <asp:Label runat="server" ID="labelRecordId"></asp:Label>
                    </td>
                    <td align="left" valign="middle" style="width:100px;">
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="middle" style="height:35px;">
                        <strong>Title:<br /></strong>
                    </td>
                    <td align="left" valign="middle">
                        <asp:TextBox runat="server" ID="txtTitle" MaxLength="255" Width="400px"></asp:TextBox>
                    </td>
                    <td align="left" valign="middle">
                        <asp:RequiredFieldValidator runat="server" ID="fvTitle" ControlToValidate="txtTitle" ErrorMessage="* Required field"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="middle" style="height:35px;">
                        <strong>Picture 1:<br /></strong>
                    </td>
                    <td align="left" valign="middle">
                        <asp:PlaceHolder runat="server" ID="phPicture1Preview">
                            <asp:Image runat="server" ID="imgPicture1Preview" AlternateText="Picture 1 Preview" BorderWidth="1px" BorderColor="#cccccc" BorderStyle="Solid" /><br />
                        </asp:PlaceHolder>
                        <asp:FileUpload runat="server" ID="fuPicture1" />
                    </td>
                    <td align="left" valign="middle">
                        <asp:RequiredFieldValidator runat="server" ID="fvPicture1" ControlToValidate="fuPicture1" ErrorMessage="* Required field"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                    </td>
                    <td align="left" valign="top">
                        <br />
                        <br />
                        <asp:Button runat="server" ID="btnSave" Text="Save" CausesValidation="true" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" CausesValidation="false" />    
                    </td>
                    <td align="left" valign="middle">
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>

