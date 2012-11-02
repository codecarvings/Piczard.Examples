/*
 * Piczard Examples | ExampleSet -A- C#
 * Copyright 2011-2012 Sergio Turolla - All Rights Reserved.
 * Author: Sergio Turolla
 * <codecarvings.com>
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
 * ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT 
 * SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR 
 * ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN 
 * ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
 * OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using CodeCarvings.Piczard;

public partial class examples_example_A_211_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", true);
        }

        if (!this.ScriptManager1.IsInAsyncPostBack)
        {
            // Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(this);
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
        this.phOutputContainer.Visible = false;
        this.phCodeContainer.Visible = false;
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        this.ProcessImage();
        this.DisplayCode();
    }

    protected void ProcessImage()
    {
        // Setup the source file name and the output file name
        string sourceImageFileName = this.imgSource.ImageUrl;
        string outputImageFileName = "~/repository/output/Ex_A_211.jpg";

        // Setup the image crop filter
        ImageCrop imageCrop = null;

        switch (this.ddlRectangle.SelectedIndex)
        {
            case 0:
                // Select whole image
                imageCrop = new ImageCrop();
                break;
            case 1:
                // x=80, y=80, width=340, height=190 (Pixel)
                imageCrop = new ImageCrop(new Rectangle(80, 80, 340, 190));
                break;
            case 2:
                // x=-20, y=-20, width=540, height=390 (Pixel)
                imageCrop = new ImageCrop(new Rectangle(-20, -20, 540, 390));
                break;
            case 3:
                // x=-0.5, y=--1, width=5, height=5 (Inch / 96 DPI)
                imageCrop = ImageCrop.Calculate(GfxUnit.Inch, new RectangleF(-0.5F, -1F, 5F, 5F));
                break;
            case 4:
                // x=-1.5, y=--5, width=10.5, height=10.5 (Cm / 96 DPI)
                imageCrop = ImageCrop.Calculate(GfxUnit.Cm, new RectangleF(-1.5F, -5F, 10.5F, 10.5F), 96F);
                break;
        }

        // Set the canvas color
        imageCrop.CanvasColor = System.Drawing.ColorTranslator.FromHtml(this.txtCanvasColor.Text);

        // Process the image
        imageCrop.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

        // Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        this.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();

        // Display the generated image
        this.phOutputContainer.Visible = true;
    }

    protected void DisplayCode()
    {
        System.Text.StringBuilder sbCode = new System.Text.StringBuilder();

        string crlf = "\r\n";
        sbCode.Append("// Prepare the parameters" + crlf);
        sbCode.Append("string sourceImage = \"~/repository/source/trevi1.jpg\";" + crlf);
        sbCode.Append("string outputImage = \"~/repository/output/Ex_A_211.jpg\";" + crlf);
        sbCode.Append(crlf);

        sbCode.Append("// Setup the image crop filter" + crlf);
        switch (this.ddlRectangle.SelectedIndex)
        {
            case 0:
                // Select whole image
                sbCode.Append("ImageCrop imageCrop = new ImageCrop();" + crlf);
                break;
            case 1:
                // x=80, y=80, width=340, height=190 (Pixel)
                sbCode.Append("ImageCrop imageCrop = new ImageCrop(new Rectangle(80, 80, 340, 190));" + crlf);
                break;
            case 2:
                // x=-20, y=-20, width=540, height=390 (Pixel)
                sbCode.Append("ImageCrop imageCrop = new ImageCrop(new Rectangle(-20, -20, 540, 390));" + crlf);
                break;
            case 3:
                // x=-0.5, y=--1, width=5, height=5 (Inch / 96 DPI)
                sbCode.Append("ImageCrop imageCrop = ImageCrop.Calculate(GfxUnit.Inch, new RectangleF(-0.5F, -1F, 5F, 5F));" + crlf);
                break;
            case 4:
                // x=-1.5, y=--5, width=10.5, height=10.5 (Cm / 96 DPI)
                sbCode.Append("ImageCrop imageCrop = ImageCrop.Calculate(GfxUnit.Cm, new RectangleF(-1.5F, -5F, 10.5F, 10.5F), 96F);" + crlf);
                break;
        }
        sbCode.Append("imageCrop.CanvasColor = System.Drawing.ColorTranslator.FromHtml(\"" + this.txtCanvasColor.Text + "\");" + crlf);

        sbCode.Append(crlf);
        sbCode.Append("// Process the image" + crlf);
        sbCode.Append("imageCrop.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);

        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }
}
