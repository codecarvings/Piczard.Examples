<%@ Page Title="Piczard Examples | Customize Piczard | Custom filters" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_401_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="4" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="4" Title="A.401 - Custom filters" />
               
        <div class="ExampleTableContainer">
        
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Image<br />
                </span>
                <asp:Image runat="server" ID="imgSource" ImageUrl="~/repository/source/temple1.jpg" AlternateText="Piczard Source Image" CssClass="ExampleTableImage" />
            </div>
               
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
                    <td align="center" valign="top" class="ExampleTableProcessBottomCell ExampleTableText">
                        <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" CausesValidation="false">
                            <asp:ListItem Text="Aggretated filters" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Inherited filter"></asp:ListItem>
                            <asp:ListItem Text="Totally custom filter"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button runat="server" ID="btnProcess" Text="Apply &raquo;" CausesValidation="false" OnClick="btnProcess_Click" CssClass="ButtonText" /><br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle" class="ExampleTableProcessBottomCellSpacer">
                    </td>
                </tr>   
                
                <asp:PlaceHolder runat="server" ID="phOutputContainer">
                    <tr>
                        <td align="center" valign="middle">
                            <span class="ExampleTableCellTitle">
                                3 - Output Image<br />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <asp:Image runat="server" ID="imgOutput" AlternateText="Piczard Output Image" CssClass="ExampleTableImage" />                        
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>

            <asp:PlaceHolder runat="server" ID="phCodeContainer">                   
                <div class="ExampleTableCell">               
                    <br />
                    <br />
                    <br />
                    <span class="ExampleTableCellTitle">
                        --- Source Code ---<br />
                    </span>
                </div>                  
                <pre id="shCode1" class="brush: c-sharp">
<asp:PlaceHolder runat="server" ID="phCode_0">
// Prepare the parameters
string sourceImage = "~/repository/source/temple1.jpg";
string outputImage = "~/repository/output/Ex_A_401.png";
string text = "Crop + TextWatermark + Sepia (" + DateTime.Now.ToShortDateString() + ")";

// Process the image
new MyAggretatedFilters(text).SaveProcessedImageToFileSystem(sourceImage, outputImage);
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="phCode_1">
// Prepare the parameters
string sourceImage = "~/repository/source/temple1.jpg";
string outputImage = "~/repository/output/Ex_A_401.png";
string text = "Extended TextWatermark " + DateTime.Now.ToString("s");

// Process the image
new MyInheritedFilter(text).SaveProcessedImageToFileSystem(sourceImage, outputImage);
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="phCode_2">
// Prepare the parameters
string sourceImage = "~/repository/source/temple1.jpg";
string outputImage = "~/repository/output/Ex_A_401.png";

// Process the image
new MyCustomFilter1().SaveProcessedImageToFileSystem(sourceImage, outputImage);     
</asp:PlaceHolder>
</pre>
            </asp:PlaceHolder>                     
                    
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
</asp:Content>


