<%@ Page Title="Piczard Examples | Web - PictureTrimmer | InlinePictureTrimmer | Client side events" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_313_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.313 - InlinePictureTrimmer - Client side events" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function InlinePictureTrimmer1_onControlLoad(sender, args)
            {
                // NOTE: The onControlLoad event handler is raised:
                // 1 - When the control.load function is invoked.
                // 2 - When the control.unloadImage function is invoked.

                myLog("Control loaded (image loaded=" + sender.get_imageLoaded() + ").");
            }

            function InlinePictureTrimmer1_onUserStateChanged(sender, args)
            {
                // Get the new userState
                var userState = sender.get_userState();
                var rectangle = userState.value.imageSelection.crop.rectangle;

                // Display the crop rectangle coordinates
                var rectangleString = (rectangle != null ? ("{x=" + rectangle.x + ",y=" + rectangle.y + ",width=" + rectangle.width + ",height=" + rectangle.height + "}") : "auto");
           
                myLog("UserState changed\r\n   (crop rectangle:" + rectangleString + ").");
            }

            function myLog(message)
            {
                var formatDate = function (dt)
                {
                    if (!dt)
                    {
                        dt = new Date();
                    }

                    var format2Digits = function(str)
                    {
                        str = "00" + str;
                        return str.substring(str.length - 2, str.length);
                    }

                    var result = dt.getFullYear() + "-" + format2Digits(dt.getMonth() + 1) + "-" + format2Digits(dt.getDate()) + "T" + format2Digits(dt.getHours()) + ":" + format2Digits(dt.getMinutes()) + ":" + format2Digits(dt.getSeconds());
                    return result;
                }

                var oTxtMyLog = $("#<% =Me.txtMyLog.ClientID %>");
                oTxtMyLog.val(formatDate() + " - " + message + "\r\n" + oTxtMyLog.val());
            }
          
            //]]>
        </script>
        
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="InlinePanel1" style="padding:5px; margin-bottom:10px;">
                    <asp:Button runat="server" ID="btnLoadImage" Text="Load / Unload image" CausesValidation="false" />
                </div>
            
                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="440px" 
                  Culture="en" AutoFreezeOnFormSubmit="true" 
                  OnClientControlLoadFunction="InlinePictureTrimmer1_onControlLoad"
                  OnClientUserStateChangedFunction="InlinePictureTrimmer1_onUserStateChanged"  />

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


