<%@ Page Title="Piczard Examples | ExampleSet -A- VB.NET" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_default" %>
<%@ Register Tagprefix="CommonUC" Tagname="NavMenuList" Src="~/design/controls/NavMenuList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer" style="margin:0px;">
        <div class="PageTitle">
            <h1 class="PageTitle_SubTitle">PICZARD EXAMPLES<br /></h1>
            <h2 class="PageTitle_MainTitle">EXAMPLESET -A- VB.NET</h2>
        </div>

        <br />
        <br />      

        <div style="width: 290px; float:left;">
            <h3>Piczard: The Basics</h3>
            <div style="margin-left: 20px;">
                <CommonUC:NavMenuList runat="server" ID="NavMenuList1" MacroAreaID="1" ShowExampleID="true" />
            </div>
            
            <br />
            <br />        
            <h3>Image Manipulation</h3>
            <div style="margin-left: 20px;">
                <CommonUC:NavMenuList runat="server" ID="NavMenuList2" MacroAreaID="2" ShowExampleID="true" />
            </div>
        </div>
        
        <div style="width: 290px; float:left;">
            <h3>Web - PictureTrimmer</h3>
            <div style="margin-left: 20px;">
                <CommonUC:NavMenuList runat="server" ID="NavMenuList3" MacroAreaID="3" ShowExampleID="true" />
            </div>    
        </div>
        
        <div style="width: 290px; float:left;">
            <h3>Customize Piczard</h3>
            <div style="margin-left: 20px;">
                <CommonUC:NavMenuList runat="server" ID="NavMenuList4" MacroAreaID="4" ShowExampleID="true" />
            </div>

            <br />
            <br />            
            <h3>Web - Image Upload Demos</h3>
            <div style="margin-left: 20px;">
                <CommonUC:NavMenuList runat="server" ID="NavMenuList5" MacroAreaID="5" ShowExampleID="true" />
            </div>
            
            <br />
            <br />
            <h3>Third Party Plugins</h3>
            <div style="margin-left: 20px;">
                <CommonUC:NavMenuList runat="server" ID="NavMenuList6" MacroAreaID="6" ShowExampleID="true" />
            </div>              
        </div>
        <br style="clear:both;" />
        <br />
        <div style="border: solid 10px #a8d040; padding: 0px 10px 10px 10px; margin-top: 30px;">
            <h3>Examples You Should Not Miss</h3>
            <div style="margin-left: 20px;">
                <ul class="NavMenu_UL1"> 
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_501/default.aspx">A.501 - Web - Image Upload Demos / SimpleImageUpload Control / Overview</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_502/default.aspx">A.502 - Web - Image Upload Demos / SimpleImageUpload Control / Usage example #1 (image crop)</asp:HyperLink>
                    </li>                    
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_507/default.aspx">A.507 - Web - Image Upload Demos / SimpleImageUpload Control / Usage example #6 (image resize + watermark)</asp:HyperLink>
                    </li>                                        
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_301/default.aspx">A.301 - Web - PictureTrimmer / Overview</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_102/default.aspx">A.102 - Piczard: The Basics / Use multiple image filters</asp:HyperLink>
                    </li>
                    <li>
                        <a href="http://digivogue.com/products/Piczard-DeBrand/comparison/" onclick="window.open(this.href, '_blank', 'width=1400,height=800,scrollbars=yes'); return false;">A.601 - Third Party Plugins / DeBrand Plugin By Digivogue</a>
                    </li>                                     
                </ul>
            </div>
        </div>

        <div class="InlinePanel2" style="margin-top:50px;">
            These examples may not be drop-in solutions for your application but may help you find the direction you need to solve your particular issue.<br />
            <br />
            Before running examples you have to make sure that the necessary read-write permissions are set for the folders "~/App_Data" and "~/repository".<br />
            In order to run examples marked for SQL Server, you have also to:<br />
            <ol>
              <li>Create the MS SQL Server database by executing the appropriate SQL Script.</li>
              <li>Configure the MS SQL Server database connection string "SqlServerConnectionString" in the Web.Config file.</li>
            </ol>
        </div>
    </div>
</asp:Content>

