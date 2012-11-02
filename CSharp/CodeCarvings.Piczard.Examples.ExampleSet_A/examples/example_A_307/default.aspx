<%@ Page Title="Piczard Examples | Web - PictureTrimmer | Client side load, unload and save images" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_307_default" %>

<%@ Register assembly="CodeCarvings.Piczard" namespace="CodeCarvings.Piczard.Web" tagprefix="ccPiczard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="3" Title="A.307 - Client side load, unload and save images" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <script type="text/javascript">
            //<![CDATA[

            function InlinePictureTrimmer1_LoadImage()
            {
                // Get the PictureTrimmer instance
                var oInlinePictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =this.InlinePictureTrimmer1.ClientID %>");
                
                var url = "<% =this.ResolveUrl("LoadImageHelper.ashx") %>?timestamp=" + CodeCarvings.Wcs.Piczard.Shared.Helpers.TimeHelper.getTimeStamp();

				var dataToBeSent = {
					coreJSONString: oInlinePictureTrimmer.get_coreJSONString(),
					imageToLoad: $("#<% =this.ddlInlineImageToLoad.ClientID %>").val()
				};
				
				$.post(url, dataToBeSent, function(data)
                {  
                                       
                    // Load the JSON Attach Data
                    oInlinePictureTrimmer.load(data);
                }, "json");
            }

            function InlinePictureTrimmer1_UnloadImage()
            {
                // Get the PictureTrimmer instance
                var oInlinePictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =this.InlinePictureTrimmer1.ClientID %>");

                // Unload the image
                oInlinePictureTrimmer.unloadImage();
            }

            function InlinePictureTrimmer1_SaveImage()
            {
                // Get the PictureTrimmer instance
                var oInlinePictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =this.InlinePictureTrimmer1.ClientID %>");
                
                var url = "<% =this.ResolveUrl("SaveImageHelper.ashx") %>?timestamp=" + CodeCarvings.Wcs.Piczard.Shared.Helpers.TimeHelper.getTimeStamp();

				var dataToBeSent = {
					coreJSONString: oInlinePictureTrimmer.get_coreJSONString()
				};

				$.post(url, dataToBeSent, function(data)
                {                   
                    // Hide the PictureTrimmer UI since the flash wmode is set to window and there is z-index problem
                    oInlinePictureTrimmer.ui.set_frozen(true);
            
                    // Display the image
		            $.fancybox({
			            "href" : data.imageUrl,
			            "padding" : 0,
			            "title" : "Output image",
			            "transitionIn" : "elastic",
			            "transitionOut" : "elastic",
			            "hideOnContentClick": true,
			            "onClosed" : function()
			            {
			                // Display the PictureTrimmer UI
			                oInlinePictureTrimmer.ui.set_frozen(false);
			            }
		            });                    
                }, "json");
            }

            function InlinePictureTrimmer1_onControlLoad(sender, args)
            {
                // NOTE: The onControlLoad event handler is raised:
                // 1 - When the control.load function is invoked
                // 2 - When the control.unloadImage function is invoked
                            
                // Check if the image has been loaded
                if (sender.get_imageLoaded())
                {
                    $("#spanInlineCommands1").css("display", "none");
                    $("#spanInlineCommands2").css("display", "inline");
                }
                else
                {
                    $("#spanInlineCommands1").css("display", "inline");
                    $("#spanInlineCommands2").css("display", "none");
                }
            }
            
            function PopupPictureTrimmer1_LoadImage()
            {
                // Get the PictureTrimmer instance
                var oPopupPictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =this.PopupPictureTrimmer1.ClientID %>");
                
                var url = "<% =this.ResolveUrl("LoadImageHelper.ashx") %>?timestamp=" + CodeCarvings.Wcs.Piczard.Shared.Helpers.TimeHelper.getTimeStamp();

				var dataToBeSent = {
					coreJSONString: oPopupPictureTrimmer.get_coreJSONString(),
					imageToLoad: $("#<% =this.ddlPopupImageToLoad.ClientID %>").val()
				};
				
				$.post(url, dataToBeSent, function(data)
                {                                 
                    // Load the JSON Attach Data
                    oPopupPictureTrimmer.load(data);
                }, "json");
            }
            
            function PopupPictureTrimmer1_onAfterPopupClose(sender, args)
            {
                // Get the PictureTrimmer instances
                var oPopupPictureTrimmer = sender;
                var oInlinePictureTrimmer = CodeCarvings.Wcs.Piczard.PictureTrimmer.getControl("<% =this.InlinePictureTrimmer1.ClientID %>");
                
                var url = "<% =this.ResolveUrl("SaveImageHelper.ashx") %>?timestamp=" + CodeCarvings.Wcs.Piczard.Shared.Helpers.TimeHelper.getTimeStamp();
				
				var dataToBeSent = {
					saveChanges: args.saveChanges,
					coreJSONString: oPopupPictureTrimmer.get_coreJSONString()
				};

				$.post(url, dataToBeSent, function(data)
                {
                    if (data.imageUrl)
                    {
						// User clicked "Ok"
					
                        // Hide the InlinePictureTrimmer UI since the flash wmode is set to window and there is z-index problem
                        oInlinePictureTrimmer.ui.set_frozen(true);

                        // Display the image
		                $.fancybox({
			                "href" : data.imageUrl,
			                "padding" : 0,
			                "title" : "Output image",
			                "transitionIn" : "elastic",
			                "transitionOut" : "elastic",
			                "hideOnContentClick": true,
			                "onClosed" : function()
			                {
			                    // Display the InlinePictureTrimmer UI
			                    oInlinePictureTrimmer.ui.set_frozen(false);
			                }
		                });
		            }
                }, "json");
            }

            //]]>
        </script>
        
        <div class="ExampleTableCellTitle" style="margin-bottom:10px;">
            InlinePictureTrimmer<br />
        </div>       
        <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%" Height="410px" 
          Culture="en" AutoFreezeOnFormSubmit="true" OnClientControlLoadFunction="InlinePictureTrimmer1_onControlLoad" />

        <div class="InlinePanel1" style="padding:5px; margin-top:10px;">
            <span id="spanInlineCommands1" style="<% =(this.InlinePictureTrimmer1.ImageLoaded ? "none" : "inline") %>">
                Image to load: 
                <asp:DropDownList runat="server" id="ddlInlineImageToLoad">
                <asp:ListItem Value="0" Text="Temple" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Flowers"></asp:ListItem>
                <asp:ListItem Value="2" Text="Donkey"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:Button runat="server" Text="Load image" CausesValidation="false" OnClientClick="InlinePictureTrimmer1_LoadImage(); return false;" />
            </span>
            <span id="spanInlineCommands2" style="<% =(this.InlinePictureTrimmer1.ImageLoaded ? "inline" : "none") %>">
                <asp:Button runat="server" Text="Unload image" CausesValidation="false" OnClientClick="InlinePictureTrimmer1_UnloadImage(); return false;" />
                <asp:Button runat="server" Text="Save image" CausesValidation="false" OnClientClick="InlinePictureTrimmer1_SaveImage(); return false;" />
            </span>

            &nbsp;&nbsp;<asp:Button runat="server" ID="btnPostback" Text="Postback" CausesValidation="false"/>                       
        </div>                
        
        <br />
        <br />
        <br />
        <div class="ExampleTableCellTitle">
            PopupPictureTrimmer<br />
        </div>        
        <ccPiczard:PopupPictureTrimmer ID="PopupPictureTrimmer1" runat="server" 
            Culture="en" AutoFreezeOnFormSubmit="true" OnClientAfterPopupCloseFunction="PopupPictureTrimmer1_onAfterPopupClose"  />

        <div class="InlinePanel1" style="padding:5px; margin-top:10px;">
            Image to load: 
            <asp:DropDownList runat="server" id="ddlPopupImageToLoad">
                <asp:ListItem Value="0" Text="Temple" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Flowers"></asp:ListItem>
                <asp:ListItem Value="2" Text="Donkey"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:Button runat="server" Text="Edit image" CausesValidation="false" OnClientClick="PopupPictureTrimmer1_LoadImage(); return false;" />
        </div>                
                         
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
</asp:Content>


