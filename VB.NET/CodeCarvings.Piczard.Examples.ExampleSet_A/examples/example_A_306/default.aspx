<%@ Page Title="Piczard Examples | Web - PictureTrimmer | Load & save images and values (SQL Server)" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_306_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.306 - Load & save images and values (SQL Server)" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
                
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
            
                <div>                                     
                    <span class="ExampleTableCellTitle">
                        Image stored in a SQL Server DB<br />
                    </span>

                    <br />
                    <asp:LinkButton runat="server" ID="lbEdit1" CausesValidation="false">
                        <asp:Image runat="server" ID="img1" AlternateText="Image stored in a DB" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" /><br />
                    </asp:LinkButton>

                    <ccPiczard:PopupPictureTrimmer runat="server" id="popupPictureTrimmer1"
                    ShowImageAdjustmentsPanel="true" Culture="en" AutoFreezeOnFormSubmit="true"
                    AutoPostBackOnPopupClose="Always" />                         
                    <asp:Button runat="server" ID="btnEdit1" Text="  Edit Picture  " CausesValidation="false" />                              
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>
