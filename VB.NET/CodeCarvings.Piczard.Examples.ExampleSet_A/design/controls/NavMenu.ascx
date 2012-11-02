<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NavMenu.ascx.vb" Inherits="design_controls_NavMenu" %>
<%@ Register Tagprefix="CommonUC" Tagname="NavMenuList" Src="NavMenuList.ascx" %>

<div class="NavMenu_Container">
    <div id="navMenu">
	    <div>
		    <h3 style="font-size:14px;"><a href="#">Piczard: The Basics</a></h3>
		    <div style="padding:0px 0px 0px 16px;">
		        <ul class="NavMenu_UL0">
		            <li>
                        <CommonUC:NavMenuList runat="server" ID="NavMenuList1" MacroAreaID="1" />
                    </li>
                </ul>
		    </div>
	    </div>
	    <div>
		    <h3 style="font-size:14px;"><a href="#">Image manipulation</a></h3>
		    <div style="padding:0px 0px 0px 16px;">
		        <ul class="NavMenu_UL0">
		            <li>
		                <CommonUC:NavMenuList runat="server" ID="NavMenuList2" MacroAreaID="2" />
                    </li>
                </ul>
		    </div>
	    </div>
	    <div>
		    <h3 style="font-size:14px;"><a href="#">Web - PictureTrimmer</a></h3>
		    <div style="padding:0px 0px 0px 16px;">
		        <ul class="NavMenu_UL0">
		            <li>
                        <CommonUC:NavMenuList runat="server" ID="NavMenuList3" MacroAreaID="3" />
                    </li>
                </ul>
		    </div>
	    </div>
	    <div>
		    <h3 style="font-size:14px;"><a href="#">Customize Piczard</a></h3>
		    <div style="padding:0px 0px 0px 16px;">
		        <ul class="NavMenu_UL0">
		            <li>
                        <CommonUC:NavMenuList runat="server" ID="NavMenuList4" MacroAreaID="4" />
                    </li>
                </ul>
		    </div>
	    </div>
	    <div>
		    <h3 style="font-size:14px;"><a href="#">Web - Image Upload Demos</a></h3>
		    <div style="padding:0px 0px 0px 16px;">
		        <ul class="NavMenu_UL0">
		            <li>
                        <CommonUC:NavMenuList runat="server" ID="NavMenuList5" MacroAreaID="5" />
                    </li>
                </ul>
		    </div>
	    </div>
	    <div>
		    <h3 style="font-size:14px;"><a href="#">Third Party Plugins</a></h3>
		    <div style="padding:0px 0px 0px 16px;">
		        <ul class="NavMenu_UL0">
		            <li>
                        <CommonUC:NavMenuList runat="server" ID="NavMenuList6" MacroAreaID="6" />
                    </li>
                </ul>
		    </div>
	    </div>	 	    
    </div>
</div>
<div class="NavMenu_Container2">
   <button id="btnHome" onclick="document.location.href='<% =Me.ResolveUrl("~/default.aspx") %>'; return false;">Back to the Index</button>
</div>

<script type="text/javascript">
    //<![CDATA[
    $(function()
    {
        // Initialize the navigation menu
        $("#navMenu").accordion({ header: "h3", fillSpace: true, active: <% =Me.RenderAccordianActive() %> });

        $("#btnHome").button({
            icons: {
                primary: "ui-icon-circle-triangle-w"
            }
        });        
    });
    //]]>
</script>