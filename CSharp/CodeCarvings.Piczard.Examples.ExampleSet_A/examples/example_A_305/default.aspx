<%@ Page Title="Piczard Examples | Web - PictureTrimmer | Load & save images and values" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_305_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.305 - Load & save images and values" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
                
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
            
                <div>
                    <span class="ExampleTableCellTitle">
                        Image stored in the file system<br />
                    </span>
                    
                    <br />
                    <asp:LinkButton runat="server" ID="lbEdit1" CausesValidation="false" OnClick="lbEdit1_Click">
                        <asp:Image runat="server" ID="img1" AlternateText="Image stored in the file system" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" /><br />
                    </asp:LinkButton>
                    
                    <ccPiczard:PopupPictureTrimmer runat="server" id="popupPictureTrimmer1"
                    ShowImageAdjustmentsPanel="true" Culture="en" AutoFreezeOnFormSubmit="true"
                    AutoPostBackOnPopupClose="Always" OnPopupClose="popupPictureTrimmer1_PopupClose" />
                    <asp:Button runat="server" ID="btnEdit1" Text="  Edit Picture  " CausesValidation="false" OnClick="btnEdit1_Click" />                    

                    
                    <div class="SeparatorLine1" style="margin:30px 0px;">
                    </div>
                    
                    
                    <span class="ExampleTableCellTitle">
                        Image stored in a DB (With temporary files)<br />
                    </span>

                    <br />
                    <asp:LinkButton runat="server" ID="lbEdit2" CausesValidation="false" OnClick="lbEdit2_Click">
                        <asp:Image runat="server" ID="img2" AlternateText="Image stored in a DB" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" /><br />
                    </asp:LinkButton>

                    <ccPiczard:PopupPictureTrimmer runat="server" id="popupPictureTrimmer2"
                    ShowImageAdjustmentsPanel="true" Culture="en" AutoFreezeOnFormSubmit="true"
                    AutoPostBackOnPopupClose="Always" OnPopupClose="popupPictureTrimmer2_PopupClose" />                         
                    <asp:Button runat="server" ID="btnEdit2" Text="  Edit Picture  " CausesValidation="false" OnClick="btnEdit2_Click" />                              


                    <div class="SeparatorLine1" style="margin:30px 0px;">
                    </div>
                    
                    
                    <span class="ExampleTableCellTitle">
                        Image stored in a DB (Without temporary files)<br />
                    </span>

                    <br />
                    <asp:LinkButton runat="server" ID="lbEdit3" CausesValidation="false" OnClick="lbEdit3_Click">
                        <asp:Image runat="server" ID="img3" AlternateText="Image stored in a DB" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" /><br />
                    </asp:LinkButton>

                    <ccPiczard:PopupPictureTrimmer runat="server" id="popupPictureTrimmer3"
                    ShowImageAdjustmentsPanel="true" Culture="en" AutoFreezeOnFormSubmit="true"
                    AutoPostBackOnPopupClose="Always" OnPopupClose="popupPictureTrimmer3_PopupClose" />                         
                    <asp:Button runat="server" ID="btnEdit3" Text="  Edit Picture  " CausesValidation="false" OnClick="btnEdit3_Click" />  

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>
