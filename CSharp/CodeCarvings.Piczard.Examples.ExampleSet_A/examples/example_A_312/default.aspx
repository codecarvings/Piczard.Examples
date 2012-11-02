<%@ Page Title="Piczard Examples | Web - PictureTrimmer | InlinePictureTrimmer | Server side events" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_312_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.312 - InlinePictureTrimmer - Server side events" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <asp:Button runat="server" ID="btnLoadImage" Text="Load image" OnClick="btnLoadImage_Click" CausesValidation="false" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnPostBack" Text="PostBack &raquo;" CausesValidation="false" />
                    &nbsp;&nbsp;
                    <asp:CheckBox runat="server" ID="cbAutoUndoChanges" Text="Auto UNDO changes" Checked="false" />
                </div>
            
                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="440px" 
                  Culture="en" AutoFreezeOnFormSubmit="true" OnValueChanged="InlinePictureTrimmer1_ValueChanged"  />

                <div class="InlinePanel2" style="margin-top:20px;">
                    <span class="ExampleTableText">
                        Event log:<br />
                    </span>
                    <asp:TextBox runat="server" ID="txtMyLog" TextMode="MultiLine" ReadOnly="true" style="width:605px; height:200px;"></asp:TextBox>
                </div>
                         
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


