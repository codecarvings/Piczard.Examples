<%@ Page Title="Piczard Examples | Web - Image Upload Demos | Programmatic Image Processing | SimpleImageUpload Control | Overview" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_501_default" %>

<%@ Register src="~/piczardUserControls/simpleImageUploadUserControl/SimpleImageUpload.ascx" tagname="SimpleImageUpload" tagprefix="ccPiczardUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.501 - SimpleImageUpload Control - Overview" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>            
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div>
                    <ccPiczardUC:SimpleImageUpload ID="ImageUpload1" runat="server" /><br />
                </div>        
                <div style="padding-top:10px;">
                    <div style="float:right; width:400px; text-align:right; padding-bottom: 10px;">
                        <strong>
                            Presets:
                        </strong>
                        <asp:Button runat="server" ID="btnPreset_1" Text="Interactive crop" />
                        <asp:Button runat="server" ID="btnPreset_2" Text="Automatic resize" />
                    </div>
                    <div style="margin-right:410px; padding-top:10px">
                        <span class="ExampleTableCellTitle">
                            Settings
                        </span>                       
                    </div>
                    <div style="clear:both;">
                    </div>
                </div>
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <div>
                        <div>
                            <strong>
                                Interactive image processing
                            </strong><br />                    
                            <asp:CheckBox runat="server" ID="cbEnableInteractiveImageProcessing" Checked="true" Text="Enable interactive image processing" AutoPostBack="true" /><br />
                            <div style="padding: 10px 0 0 20px;">
                                <asp:DropDownList runat="server" ID="ddlCropConstraintMode" AutoPostBack="true">
                                    <asp:ListItem Selected="True">Crop enabled: Fixed size</asp:ListItem>
                                    <asp:ListItem>Crop enabled: Fixed aspect ratio</asp:ListItem>
                                    <asp:ListItem>Crop enabled: Free size</asp:ListItem>
                                    <asp:ListItem>Crop disabled</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox runat="server" ID="cbAutoOpenImageEditPopupAfterUpload" Checked="true" Text="Auto open image edit window after upload" AutoPostBack="true" /><br />
                            </div>
                            <div style="padding-top:5px;">
                                <asp:TextBox runat="server" ID="txtInteractiveImageProcessing" style="width:95%; margin-left:5px; font-family: 'Courier New', Courier, monospace; font-size:12px;" BackColor="Black" ForeColor="LightGreen" BorderStyle="Solid" BorderWidth="2px" BorderColor="Green" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div style="margin-top:20px; border-top:solid 1px #80A232; padding-top:5px;">
                            <strong>
                                Post processing
                            </strong><br />                    
                            <asp:CheckBox runat="server" ID="cbPostProcessing_Resize" Text="Resize image"  AutoPostBack="true" />                        
                            <asp:CheckBox runat="server" ID="cbPostProcessing_Grayscale" Text="Grayscale" AutoPostBack="true" />
                            <asp:CheckBox runat="server" ID="cbPostProcessing_Watermark" Text="Apply watermark" AutoPostBack="true" /><br />
                            <div style="padding-top:5px;">
                                <asp:TextBox runat="server" ID="txtPostProcessing" style="width:95%; margin-left:5px; font-family: 'Courier New', Courier, monospace; font-size:12px;" BackColor="Black" ForeColor="LightGreen" BorderStyle="Solid" BorderWidth="2px" BorderColor="Green" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div style="margin-top:20px; border-top:solid 1px #80A232; padding-top:5px;">
                            <strong>
                                Preview options
                            </strong><br />                    
                            <asp:CheckBox runat="server" ID="cbPreviewConstriant" Checked="true" Text="Scale preview" AutoPostBack="true" /><br /> 
                            <div style="padding-top:5px;">
                                <asp:TextBox runat="server" ID="txtPreviewConstriant" style="width:95%; margin-left:5px; font-family: 'Courier New', Courier, monospace; font-size:12px;" BackColor="Black" ForeColor="LightGreen" BorderStyle="Solid" BorderWidth="2px" BorderColor="Green" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div style="margin-top:20px; border-top:solid 1px #80A232; padding-top:5px; margin-bottom:10px;">
                            <strong>
                                Other options
                            </strong><br />                    
                            <asp:CheckBox runat="server" ID="cbValidateImageSize" Text="Validate minimum width of uploaded images" AutoPostBack="true" /><br />
                            <div style="padding-top:5px;">
                                <asp:TextBox runat="server" ID="txtValidateImageSize" style="width:95%; margin-left:5px; font-family: 'Courier New', Courier, monospace; font-size:12px;" BackColor="Black" ForeColor="LightGreen" BorderStyle="Solid" BorderWidth="2px" BorderColor="Green" TextMode="MultiLine" Rows="9">Protected Sub ImageUpload1_ImageUpload(ByVal sender As Object, ByVal args As SimpleImageUpload.ImageUploadEventArgs) Handles ImageUpload1.ImageUpload
  If (ImageUpload1.SourceImageSize.Width < 250) Then
      ' The uploaded image is too small
      ImageUpload1.UnloadImage()
      ImageUpload1.SetCurrentStatusMessage("Image width must be at least 250 px")
      Return
  End If
End Sub</asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="InlinePanel2" style="margin-top:30px;">
            Usage examples:<br />
            <ul>                         
                <li>
                    <strong>
                        Image crop:
                    </strong><br />
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_502/default.aspx">A.502 - Web - Image Upload Demos / SimpleImageUpload Control / Usage example #1 (image crop)</asp:HyperLink><br />
                </li>
                <li>
                    <strong>
                        Image resize:
                    </strong><br />
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_507/default.aspx">A.507 - Web - Image Upload Demos / SimpleImageUpload Control / Usage example #6 (image resize + watermark)</asp:HyperLink><br />
                    <br />
                </li>                  
            </ul>
        </div>        

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


