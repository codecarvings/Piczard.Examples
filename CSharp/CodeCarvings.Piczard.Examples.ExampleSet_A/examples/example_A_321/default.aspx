<%@ Page Title="Piczard Examples | Web - PictureTrimmer | PopupPictureTrimmer | Overview" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_321_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.321 - PopupPictureTrimmer - Overview" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <script type="text/javascript">
            //<![CDATA[

            function openPopupPictureTrimmer1Window()
            {
                var popupSizeMode = $("#<% =this.ddlClientSidePopupSizeMode.ClientID %>").val();
                var windowWidth, windowHeight;
            
                switch(popupSizeMode)
                {
                    case "0":
                        // Default size
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =this.PopupPictureTrimmer1.ClientID %>');
                        break;
                    case "1":
                        // Custom size #1
                        windowWidth = 900;
                        windowHeight = 540;
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =this.PopupPictureTrimmer1.ClientID %>', windowWidth, windowHeight);
                        break;
                    case "2":
                        // Custom size #2
                        windowWidth = $(window).width() - 50;
                        windowHeight = $(window).height() - 50;
                        CodeCarvings.Wcs.Piczard.PictureTrimmer.popup_open('<% =this.PopupPictureTrimmer1.ClientID %>', windowWidth, windowHeight);
                        break;
                }
            }
           
            //]]>
        </script>        
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
            
                <ccPiczard:PopupPictureTrimmer ID="PopupPictureTrimmer1" runat="server" Culture="en"
                ShowImageAdjustmentsPanel="true" />

                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                        <tr>
                            <td align="left" valign="middle" style="width:250px; height:38px;">
                                <span class="ExampleTableText">
                                    Open popup (client side):<br />
                                </span>
                            </td>
                            <td align="left" valign="middle">
                                <asp:DropDownList runat="server" ID="ddlClientSidePopupSizeMode">
                                    <asp:ListItem Value="0" Text="Default window size" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Custom window size #1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Custom window size #2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button runat="server" ID="btnClientSideOpenPopup" Text="Open popup" CausesValidation="false" OnClientClick="openPopupPictureTrimmer1Window(); return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height:38px;">
                                <span class="ExampleTableText">
                                    Open popup (server side):<br />
                                </span>
                            </td>
                            <td align="left" valign="middle">
                                <asp:DropDownList runat="server" ID="ddlServerSidePopupSizeMode">
                                    <asp:ListItem Value="0" Text="Default window size" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Custom window size"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button runat="server" ID="btnServerSideOpenPopup" Text="Open popup" CausesValidation="false" OnClick="btnServerSideOpenPopup_Click" />
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


