<%@ Page Title="Piczard Examples | Web - Image Upload Demos | SimpleImageUpload Control | Usage example #1 (image crop) | Edit record" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="editRecord.aspx.vb" Inherits="examples_example_A_502_editRecord" %>

<%@ Register src="~/piczardUserControls/simpleImageUploadUserControl/SimpleImageUpload.ascx" tagname="SimpleImageUpload" tagprefix="ccPiczardUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="5" Title="A.502 - SimpleImageUpload Control - Usage example #1 (image crop) - Edit record" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
    
        <script type="text/javascript">
            //<![CDATA[
            function fvPicture1_Validate(sender, args)
            {
                // Validate the Picture1 (must contain a value)
                args.IsValid = CodeCarvings.Wcs.Piczard.Upload.SimpleImageUpload.get_hasImage("<% =CodeCarvings.Piczard.Web.Helpers.JSHelper.EncodeString(Me.Picture1.ClientID) %>"); ;
            }
            function btnSave_clientClick()
            {
                if (CodeCarvings.Wcs.Piczard.Upload.SimpleImageUpload.get_uploadInProgress())
                {
                    alert("Upload in progress, please wait...");
                    return false;
                }
                
                return true; 
            }
            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>        
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
                                <ccPiczardUC:SimpleImageUpload ID="Picture1" runat="server" Width="414px" />
                            </td>
                            <td align="left" valign="middle">
                                <asp:CustomValidator runat="server" ID="fvPicture1" ClientValidationFunction="fvPicture1_Validate" ErrorMessage="* Required field"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                            </td>
                            <td align="left" valign="top">
                                
                                <br />
                                <br />
                                <asp:Button runat="server" ID="btnSave" Text="Save" CausesValidation="true" OnClientClick="if (!btnSave_clientClick()) return false;" />
                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" CausesValidation="false" />    
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>

