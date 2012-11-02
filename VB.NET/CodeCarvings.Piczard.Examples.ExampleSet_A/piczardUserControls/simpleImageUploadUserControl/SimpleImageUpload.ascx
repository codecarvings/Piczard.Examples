﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SimpleImageUpload.ascx.vb" Inherits="SimpleImageUpload" %>
<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>
<asp:PlaceHolder runat="server" ID="phDesignTimeStart" EnableViewState="false">
    <div style="display:none;">
</asp:PlaceHolder> 
<div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("container0")) %>" class="ccpz_siu_container0<% =If(Not string.IsNullOrEmpty(Me.CssClass), " " + HttpUtility.HtmlAttributeEncode(Me.CssClass), "") %>" style="<% =Me.GetRenderStyle_container0() %>">
    <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("container1")) %>" class="ccpz_siu_container1" style="padding:5px;">
    
        <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("content")) %>" class="ccpz_siu_content" style="overflow: auto; <% =Me.GetRenderStyle_content() %>">
            <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("content_statusMessage")) %>" class="ccpz_siu_content_statusMessage" style="display:<% =if(Me.HasImage, "none", "inline") %>">
                <div style="padding:5px;">
                    <asp:Literal runat="server" id="litStatusMessage" EnableViewState="false">
                        No image selected.
                    </asp:Literal>
                </div>
            </div>
            <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("content_preview")) %>" class="ccpz_siu_content_preview" style="display:<% =if(Me.HasImage, "inline", "none") %>">
            <asp:PlaceHolder runat="server" ID="phImageContainer" EnableViewState="false">
                <div style="padding:5px; float:left;"><asp:HyperLink runat="server" ID="hlPictureImageEdit" NavigateUrl="#" EnableViewState="false" style="display:block;"><asp:Image runat="server" ID="imgPreview" AlternateText="Preview" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" EnableViewState="false" Width="1px" Height="1px" style="display:block;" /></asp:HyperLink></div>
                <div style="clear:both;"></div>
            </asp:PlaceHolder>
            </div>
        </div>
        
        <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("commands")) %>" class="ccpz_siu_commands" style="padding: 5px 0px 0px 0px;">
            <asp:PlaceHolder runat="server" ID="phEditCommands" EnableViewState="false">
                <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("editCommands")) %>" class="ccpz_siu_editCommands" style="display:inline; float: left;">
                    <asp:Button runat="server" ID="btnEdit" CausesValidation="false" Text="Edit..." Enabled="false" CssClass="DoNotApplyButtonStyle" style="padding:0; margin:0;" EnableViewState="false" />
                    <asp:Button runat="server" ID="btnRemove" CausesValidation="false" Text="Remove" Enabled="false" CssClass="DoNotApplyButtonStyle" style="padding:0; margin:0;" EnableViewState="false" />
                </div>
                <div style="display:inline; float: left; width:10px; height:10px;">
                </div>
            </asp:PlaceHolder>
            
            <asp:PlaceHolder runat="server" ID="phUploadCommands" EnableViewState="false">
                <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("uploadCommands")) %>" class="ccpz_siu_uploadCommands" style="display:inline; float: left;">
                    <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("uploadContainer_0")) %>" class="ccpz_siu_uploadContainer_0" style="position: relative; width:<% =(Me.ButtonSize.Width + 25).ToString() %>px; height:<% =(Me.ButtonSize.Height).ToString() %>px; overflow: hidden; display:none;">
                        <asp:Button runat="server" ID="btnBrowseDisabled" CausesValidation="false" Text="Browse..." Enabled="false" CssClass="DoNotApplyButtonStyle" style="display:inline; padding:0; margin:0;" EnableViewState="false" />
                        <asp:Button runat="server" ID="btnCancelUpload" CausesValidation="false" Text="Cancel upload" CssClass="DoNotApplyButtonStyle" style="display:none; padding:0; margin:0;" EnableViewState="false" />
                        <asp:Image runat="server" AlternateText="Uploading file..." ImageUrl="wait.gif?v=4" style="width:16px; height:16px; margin-left:5px; vertical-align:middle;" EnableViewState="false" />
                    </div>
                    <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("uploadContainer_1")) %>" class="ccpz_siu_uploadContainer_1" style="position: relative; width:<% =(Me.ButtonSize.Width + 25).ToString() %>px; height:<% =(Me.ButtonSize.Height).ToString() %>px; overflow: hidden; display:inline;">
                        <asp:Button runat="server" ID="btnBrowse" CausesValidation="false" Text="Browse..." OnClientClick="return false;" Enabled="false" CssClass="DoNotApplyButtonStyle" style="padding:0; margin:0;" EnableViewState="false" />
                        <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("uploadPlaceHolder")) %>" class="ccpz_siu_uploadPlaceHolder"
                            style="opacity: 0; filter:alpha(opacity: 0);  position: absolute; top:0px; left:0px;">
                        </div>
                    </div>    
                </div>
            </asp:PlaceHolder>
            
            <br style="clear:both;" />
        </div>
        
        <div style="display:none;">
            <asp:Button runat="server" ID="btnPostBack" CausesValidation="false" Text="PostBack" CssClass="DoNotApplyButtonStyle" EnableViewState="false" />
        </div>
        
        <asp:Literal runat="server" ID="litScript" EnableViewState="false">
        </asp:Literal>
        
        <asp:HiddenField runat="server" ID="hfAct" Value="" EnableViewState="false" />
        
        <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("popupExtContainer")) %>" class="ccpz_siu_popupExtContainer" style="display: none;">
            <div id="<% =HttpUtility.HtmlAttributeEncode(Me.GetSubElementId("popupExt")) %>" class="ccpz_siu_popupExt" style="background-color:#999; color:#fff; height: 44px; line-height:44px; vertical-align:middle; padding-right:5px; overflow: hidden; text-align:right;">
                <asp:Literal runat="server" ID="litSelectConfiguration" EnableViewState="false"></asp:Literal>
                <asp:DropDownList runat="server" ID="ddlConfigurations" EnableViewState="false"></asp:DropDownList>
            </div>
        </div>

        <ccPiczard:PopupPictureTrimmer runat="server" id="popupPictureTrimmer1"
        ShowZoomPanel="true"
        ShowImageAdjustmentsPanel="true" 
        AutoFreezeOnFormSubmit="true" AutoPostBackOnPopupClose="Never"
        OnClientBeforePopupOpenFunction="CodeCarvings.Wcs.Piczard.Upload.SimpleImageUpload.onImageEditBeforePopupOpen"
        OnClientAfterPopupCloseFunction="CodeCarvings.Wcs.Piczard.Upload.SimpleImageUpload.onImageEditAfterPopupClose"
        EnableViewState="false" />
             
    </div>
</div>
<asp:PlaceHolder runat="server" ID="phDesignTimeEnd" EnableViewState="false">
    </div>
    <div style="margin:5px; padding: 5px; border: solid 1px #c6c6c6; background-color: #d6d6d6; font-size: 12px; font-family:Arial,Hevetica,sans-serif;text-align:left;">
        <span style="font-weight:bold;">Piczard</span> - .NET Image Manipulation Library by Sergio Turolla<br />
         SimpleImageUpload User Control &nbsp;v 3.0.0
    </div>
</asp:PlaceHolder>  
        