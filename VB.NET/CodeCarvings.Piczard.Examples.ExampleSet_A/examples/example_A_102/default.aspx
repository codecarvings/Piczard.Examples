<%@ Page Title="Piczard Examples | Basics | Use multiple image filters" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_102_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="1" Title="A.102 - Use multiple image filters" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>

        <div class="ExampleTableContainer">      
          
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Image
                </span>
                <button id="btnShowHideSourceImage" onclick="showHideSourceImage(); return false;" class="ButtonText">Show / hide</button><br />

                <div id="slidingSourceImageDiv" style="height:392px;">
                    <asp:Image runat="server" ID="imgSource" ImageUrl="~/repository/source/valencia1.jpg" AlternateText="Piczard Source Image" CssClass="ExampleTableImage" />                        
                </div>
            </div>


            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
        
                    <div class="ExampleTable">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">                       
                            <tr>
                                <td align="center" valign="top" class="ExampleTableProcessTopCell">
                                    <asp:Image runat="server" ImageUrl="~/design/gfx/saTop.jpg" AlternateText="Processing1" CssClass="ExampleTableProcessTopCellImage" />
                                    <div class="ExampleTableProcessTopCellTitle">
                                        2 - Image Processing
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" class="ExampleTableProcessBottomCell">
                                    Enable/Disable filters - and - change the filter order by <span style="text-decoration:underline;">dragging</span> the items<br />
                                    <br />
                                    <asp:HiddenField runat="server" ID="hfFilterList" Value="filterRotate,filterResize,filterChangeColors,filterWatermark" />

                                    <div style="display:none;">
                                        <asp:LinkButton runat="server" ID="lbDragRefresh" CausesValidation="false"></asp:LinkButton>
                                    </div>
                                    <div style="width:350px;">
                                        <ul id="sortableFilters" class="SortableList">
                                            <li id="filterRotate" class="ui-state-default" style="text-align:right;"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span><asp:CheckBox ID="cbFilterRotate" runat="server" Text="Rotate 90°" TextAlign="Left" AutoPostBack="true" CausesValidation="false" Checked="true" /></li>
                                            <li id="filterResize" class="ui-state-default" style="text-align:right;"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span><asp:CheckBox ID="cbFilterResize" runat="server" Text="Resize" TextAlign="Left" AutoPostBack="true" CausesValidation="false" Checked="true" /></li>
                                            <li id="filterChangeColors" class="ui-state-default" style="text-align:right;"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span><asp:CheckBox ID="cbFilterChangeColors" runat="server" Text="Change Colors" TextAlign="Left" AutoPostBack="true" CausesValidation="false" Checked="true" /></li>
                                            <li id="filterWatermark" class="ui-state-default" style="text-align:right;"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span><asp:CheckBox ID="cbFilterWatermark" runat="server" Text="Watermark" TextAlign="Left" AutoPostBack="true" CausesValidation="false" Checked="true" /></li>
                                        </ul>
                                    </div>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" class="ExampleTableProcessBottomCellSpacer">
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="center" valign="middle">
                                    <span class="ExampleTableCellTitle">
                                        3 - Output Image<br />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="height:480px;">
                                    <asp:Image runat="server" ID="imgOutput" AlternateText="Piczard Output Image" CssClass="ExampleTableImage" />                        
                                </td>
                            </tr>
                        </table>
                    </div>
                   
                   <asp:PlaceHolder runat="server" ID="phCodeContainer">   
                        <div class="ExampleTableCell">               
                            <br />
                            <br />
                            <br />
                            <span class="ExampleTableCellTitle">
                                --- Source Code ---<br />
                            </span>
                        </div>
                        <pre id="shCode1" class="brush: vb.net"><asp:Literal runat="server" ID="litCode"></asp:Literal></pre>
                    </asp:PlaceHolder> 
                
                </ContentTemplate>
            </asp:UpdatePanel>

            <script type="text/javascript">
                //<![CDATA[

                function showHideSourceImage()
                {
                    var icon;
                    if ($("#slidingSourceImageDiv").is(":visible"))
                    {
                        icon = "ui-icon-circle-triangle-s";
                    }
                    else
                    {
                        icon = "ui-icon-circle-triangle-n";
                    }
                    
                    $("#btnShowHideSourceImage").button({
                        icons: {
                            primary: icon
                        }
                    });              
                
                    $("#slidingSourceImageDiv").animate({
                        height: "toggle"
                        }, 1000, "easeOutBounce");
                }

                function initializeExampleUI()
                {            
                    // Setup the sortable list
                    $("#sortableFilters").sortable({
                        update: function(event, ui)
                        {
                            // Save the current state (filter position / scroll)
                            var result = $("#sortableFilters").sortable("toArray");
                            $("#<% =Me.hfFilterList.ClientID %>").val(result.join(","));
                            
                            <% =Me.ClientScript.GetPostBackEventReference(Me.lbDragRefresh, "", false) %>
                        }
                    });
                    $("#sortableFilters").disableSelection();
                    
                    // Restore the filter order
                    var itemIDS = $("#<% =Me.hfFilterList.ClientID %>").val().split(",");                          
                    for (var i = 0; i < itemIDS.length; i++)
                    {
	                    var itemID = itemIDS[i];
        	            
	                    var item = $("#" + itemID);   
	                    item.remove();         
	                    $("#sortableFilters").append(item);
                    }  
                }

                $(function()
                {            
                    initializeExampleUI();

                    <% if (Not Me.IsPostBack) Then %>
                        // Hide the source image
                        window.setTimeout(function() {
                        showHideSourceImage();
                        }, 200);
                    <% End If %>
                });
                //]]>
            </script>
        </div>        
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>

