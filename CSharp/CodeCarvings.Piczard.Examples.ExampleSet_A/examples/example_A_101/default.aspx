<%@ Page Title="Piczard Examples | Basics | Use a single image filter" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_101_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="1" Title="A.101 - Use a single image filter" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <div class="ExampleTableContainer">
        
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Image<br />
                </span>
                <asp:Image runat="server" ID="imgSource" ImageUrl="~/repository/source/donkey1.jpg" AlternateText="Piczard Source Image" CssClass="ExampleTableImage" />
            </div>
        
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
        
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
                                    <asp:ListItem Text="Fixed Size Crop Constraint" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Scaled Resize Constraint"></asp:ListItem>
                                    <asp:ListItem Text="Grayscale Color Filter"></asp:ListItem>
                                    <asp:ListItem Text="Text Watermark"></asp:ListItem>
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
string sourceImage = "~/repository/source/donkey1.jpg";
string outputImage = "~/repository/output/Ex_A_101.jpg";
int cropWidth = 160;
int cropHeight = 350;

// Process the image
new FixedCropConstraint(cropWidth, cropHeight).SaveProcessedImageToFileSystem(sourceImage, outputImage);
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="phCode_1">
// Prepare the parameters
string sourceImage = "~/repository/source/donkey1.jpg";
string outputImage = "~/repository/output/Ex_A_101.jpg";
int maxWidth = 160;
int maxHeight = 350;

// Process the image
new ScaledResizeConstraint(maxWidth, maxHeight).SaveProcessedImageToFileSystem(sourceImage, outputImage);
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="phCode_2">
// Prepare the parameters
string sourceImage = "~/repository/source/donkey1.jpg";
string outputImage = "~/repository/output/Ex_A_101.jpg";

// Process the image
DefaultColorFilters.Grayscale.SaveProcessedImageToFileSystem(sourceImage, outputImage);
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="phCode_3">
// Prepare the parameters
string sourceImage = "~/repository/source/donkey1.jpg";
string outputImage = "~/repository/output/Ex_A_101.jpg";

// Setup the watermark filter
TextWatermark watermark = new TextWatermark();
watermark.Text = DateTime.Now.ToString();
watermark.ContentAlignment = ContentAlignment.TopRight;
watermark.Font.Name = "Arial";
watermark.Font.Size = 20;
watermark.ForeColor = Color.Yellow;

// Process the image
watermark.SaveProcessedImageToFileSystem(sourceImage, outputImage);
</asp:PlaceHolder>
</pre>
                    </asp:PlaceHolder> 
                
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageBG" Runat="Server">
    <CommonUC:MyUpdateProgress1 runat="server" ID="MyUpdateProgress1" />
</asp:Content>


