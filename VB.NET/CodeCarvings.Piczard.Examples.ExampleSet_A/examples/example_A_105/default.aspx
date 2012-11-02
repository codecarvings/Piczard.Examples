<%@ Page Title="Piczard Examples | Basics | Generate different images from one picture" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_105_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="1" Title="A.105 - Generate different images from one picture" />
        
        <asp:ScriptManager runat="server" ID="ScriptManager1">
        </asp:ScriptManager>
        
        <div class="ExampleTableContainer">      
          
            <div class="ExampleTableCell ExampleTableText">
                <span class="ExampleTableCellTitle">
                    1 - Source Image<br />
                </span>
                <asp:Image runat="server" ID="imgSource" ImageUrl="~/repository/source/flowers1.jpg" AlternateText="Piczard Source Image" CssClass="ExampleTableImage" />
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
                                <asp:Button runat="server" ID="btnProcess" Text="Generate images &raquo;" CausesValidation="false" CssClass="ButtonText" /><br />
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
                                        3 - Output Images<br />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    <br />
                                    <div class="InlinePanel1">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%; padding:10px;">
                                            <tr>
                                                <td align="center" valign="middle" style="width:33%;">
                                                    <asp:Image runat="server" ID="imgOutput1" AlternateText="Output image 1" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" />
                                                </td>
                                                <td align="center" valign="middle" style="width:34%;">
                                                    <asp:Image runat="server" ID="imgOutput2" AlternateText="Output image 2" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" />
                                                </td>
                                                <td align="center" valign="middle" style="width:33%;">
                                                    <asp:Image runat="server" ID="imgOutput3" AlternateText="Output image 3" BorderWidth="2px" BorderColor="#80A232" BorderStyle="Dotted" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
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
                        <pre id="shCode1" class="brush: vb.net"
>Using sourceImage As LoadedImage = ImageArchiver.LoadImage("~/repository/source/flowers1.jpg")
    ' Generate the image #1
    Call New ScaledResizeConstraint(150, 250).SaveProcessedImageToFileSystem(sourceImage, "~/repository/output/Ex_A_105___1.jpg")

    ' Generate the image #2
    Call New FixedCropConstraint(150, 250).SaveProcessedImageToFileSystem(sourceImage, "~/repository/output/Ex_A_105___2.jpg")

    ' Generate the image #3
    Call New ImageProcessingJob(New ImageProcessingFilter() { _
        New FixedResizeConstraint(150, 250), _
        DefaultColorFilters.Grayscale _
    }).SaveProcessedImageToFileSystem(sourceImage, "~/repository/output/Ex_A_105___3.jpg")
End Using
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

