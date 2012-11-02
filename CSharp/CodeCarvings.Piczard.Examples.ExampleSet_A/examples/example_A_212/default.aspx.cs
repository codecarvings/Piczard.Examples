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

public partial class examples_example_A_212_default : System.Web.UI.Page
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

        this.phCropMode_Fixed.Visible = false;
        this.phCropMode_FixedAspectRatio.Visible = false;
        this.phCropMode_Free.Visible = false;
        this.UpdatePanel1.FindControl("phCropMode_" + this.ddlCropMode.SelectedValue).Visible = true;
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
        string outputImageFileName = "~/repository/output/Ex_A_212.jpg";

        // Setup the crop filter
        CropConstraint cropFilter = null;
        switch (this.ddlCropMode.SelectedValue)
        {
            case "Fixed":
                FixedCropConstraint fixedCropConstraint = null;
                switch (this.ddlConstraints_Fixed.SelectedIndex)
                {
                    case 0:
                        // 180 x 100 Pixel
                        fixedCropConstraint = new FixedCropConstraint(180, 100);
                        break;
                    case 1:
                        // 50 x 100 Mm (96 DPI)
                        fixedCropConstraint = new FixedCropConstraint(GfxUnit.Mm, 50F, 100F);
                        break;
                }
                cropFilter = fixedCropConstraint;
                break;
            case "FixedAspectRatio":
                FixedAspectRatioCropConstraint fixedAspectRatioCropFilter = null;
                switch (this.ddlAspectRatio.SelectedIndex)
                {
                    case 0:
                        fixedAspectRatioCropFilter = new FixedAspectRatioCropConstraint(1F / 1F);
                        break;
                    case 1:
                        fixedAspectRatioCropFilter = new FixedAspectRatioCropConstraint(2F / 1F);
                        break;
                    case 2:
                        fixedAspectRatioCropFilter = new FixedAspectRatioCropConstraint(1F / 2F);
                        break;
                    case 3:
                        fixedAspectRatioCropFilter = new FixedAspectRatioCropConstraint(16F / 9F);
                        break;
                }
                switch (this.ddlConstraints_FixedAspectRatio.SelectedIndex)
                {
                    case 0:
                        // No constraint
                        break;
                    case 1:
                        // Min width: 500px
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Width;
                        fixedAspectRatioCropFilter.Min = 500;
                        break;
                    case 2:
                        // Min height: 5.2 inch (96 DPI)
                        fixedAspectRatioCropFilter.Unit = GfxUnit.Inch;
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Height;
                        fixedAspectRatioCropFilter.Min = 5.2F;
                        break;
                    case 3:
                        // Max width: 200px
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Width;
                        fixedAspectRatioCropFilter.Max = 200;
                        break;
                    case 4:
                        // Max height: 5 cm (96 DPI)
                        fixedAspectRatioCropFilter.Unit = GfxUnit.Cm;
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Height;
                        fixedAspectRatioCropFilter.Max = 5F;
                        break;
                }
                cropFilter = fixedAspectRatioCropFilter;
                break;
            case "Free":
                FreeCropConstraint freeCropFilter = null;
                switch (this.ddlConstraints_Free.SelectedIndex)
                {
                    case 0:
                        // No constraints
                        freeCropFilter = new FreeCropConstraint();
                        break;
                    case 1:
                        // Max width: 200px
                        freeCropFilter = new FreeCropConstraint(null, 200, null, null);
                        break;
                    case 2:
                        // Max height: 5 cm (96 DPI)
                        freeCropFilter = new FreeCropConstraint(GfxUnit.Cm, null, null, null, 5F);
                        break;
                    case 3:
                        // Fixed width: 4.1 inch (96 DPI)
                        freeCropFilter = new FreeCropConstraint(GfxUnit.Inch, 4.1F, 4.1F, null, null);
                        break;
                    case 4:
                        // Fixed height: 400px
                        freeCropFilter = new FreeCropConstraint(null, null, 400, 400);
                        break;
                }
                cropFilter = freeCropFilter;
                break;
        }

        cropFilter.DefaultImageSelectionStrategy = (CropConstraintImageSelectionStrategy)Enum.Parse(typeof(CropConstraintImageSelectionStrategy), this.ddlImageSelectionStrategy.SelectedValue);
        if (this.ddlMargins.SelectedValue != "Auto")
        {
            // Disabled both the margins (horizontal and vertical).
            // Note: By default the margins are automatically calculated
            cropFilter.Margins.SetZero();
        }

        // Set the canvas color
        cropFilter.CanvasColor = System.Drawing.ColorTranslator.FromHtml(this.txtCanvasColor.Text);
        
        // Process the image
        cropFilter.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

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
        sbCode.Append("string outputImage = \"~/repository/output/Ex_A_212.jpg\";" + crlf);
        sbCode.Append(crlf);

        sbCode.Append("// Create the cropping filter" + crlf);
        switch (this.ddlCropMode.SelectedValue)
        {
            case "Fixed":
                switch (this.ddlConstraints_Fixed.SelectedIndex)
                {
                    case 0:
                        sbCode.Append("FixedCropConstraint cropFilter = new FixedCropConstraint(180, 100);" + crlf);
                        break;
                    case 1:
                        sbCode.Append("FixedCropConstraint cropFilter = new FixedCropConstraint(GfxUnit.Mm, 50F, 100F);" + crlf);
                        break;
                }
                break;
            case "FixedAspectRatio":
                switch (this.ddlAspectRatio.SelectedIndex)
                {
                    case 0:
                        sbCode.Append("FixedAspectRatioCropConstraint cropFilter = new FixedAspectRatioCropConstraint(1F / 1F);" + crlf);
                        break;
                    case 1:
                        sbCode.Append("FixedAspectRatioCropConstraint cropFilter = new FixedAspectRatioCropConstraint(2F / 1F);" + crlf);
                        break;
                    case 2:
                        sbCode.Append("FixedAspectRatioCropConstraint cropFilter = new FixedAspectRatioCropConstraint(1F / 2F);" + crlf);
                        break;
                    case 3:
                        sbCode.Append("FixedAspectRatioCropConstraint cropFilter = new FixedAspectRatioCropConstraint(16F / 9F);" + crlf);
                        break;
                }
                switch (this.ddlConstraints_FixedAspectRatio.SelectedIndex)
                {
                    case 1:
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Width;" + crlf);
                        sbCode.Append("cropFilter.Min = 500;" + crlf);
                        break;
                    case 2:
                        sbCode.Append("cropFilter.Unit = GfxUnit.Inch;" + crlf);
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Height;" + crlf);
                        sbCode.Append("cropFilter.Min = 5.2F;" + crlf);
                        break;
                    case 3:
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Width;" + crlf);
                        sbCode.Append("cropFilter.Max = 200;" + crlf);
                        break;
                    case 4:
                        sbCode.Append("cropFilter.Unit = GfxUnit.Cm;" + crlf);
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Height;" + crlf);
                        sbCode.Append("cropFilter.Max = 5F;" + crlf);
                        break;
                }
                break;
            case "Free":
                switch (this.ddlConstraints_Free.SelectedIndex)
                {
                    case 0:
                        sbCode.Append("FreeCropConstraint cropFilter = new FreeCropConstraint();" + crlf);
                        break;
                    case 1:
                        sbCode.Append("FreeCropConstraint cropFilter = new FreeCropConstraint(null, 200, null, null);" + crlf);
                        break;
                    case 2:
                        sbCode.Append("FreeCropConstraint cropFilter = new FreeCropConstraint(GfxUnit.Cm, null, null, null, 5F);" + crlf);
                        break;
                    case 3:
                        sbCode.Append("FreeCropConstraint cropFilter = new FreeCropConstraint(GfxUnit.Inch, 4.1F, 4.1F, null, null);" + crlf);
                        break;
                    case 4:
                        sbCode.Append("FreeCropConstraint cropFilter = new FreeCropConstraint(null, null, 400, 400);" + crlf);
                        break;
                }
                break;
        }

        if (this.ddlImageSelectionStrategy.SelectedValue != "Slice")
        {
            // Default value: Slice
            sbCode.Append("cropFilter.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy." + this.ddlImageSelectionStrategy.SelectedValue + ";" + crlf);
        }
        if (this.ddlMargins.SelectedValue != "Auto")
        {
            // Default value: Auto margins
            sbCode.Append("cropFilter.Margins.SetZero();" + crlf);
        }
        sbCode.Append("cropFilter.CanvasColor = System.Drawing.ColorTranslator.FromHtml(\"" + this.txtCanvasColor.Text + "\");" + crlf);

        sbCode.Append(crlf);
        sbCode.Append("// Process the image" + crlf);
        sbCode.Append("cropFilter.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);

        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }

}
