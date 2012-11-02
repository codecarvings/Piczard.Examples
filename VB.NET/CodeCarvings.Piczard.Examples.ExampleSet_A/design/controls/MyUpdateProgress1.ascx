<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MyUpdateProgress1.ascx.vb" Inherits="design_controls_MyUpdateProgress1" %>
<%@ Register Tagprefix="CommonUC" Tagname="PageBlockMessage" Src="PageBlockMessage.ascx" %>

<asp:UpdateProgress runat="server" ID="UpdateProgress1">
    <ProgressTemplate>
        <div class="blockElem blockOverlay MyUpdateProgress1Background"></div>
        <div class="blockElem blockMsg MyUpdateProgress1Window"> 
            <CommonUC:PageBlockMessage runat="server" ID="PageBlockMessage1" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>