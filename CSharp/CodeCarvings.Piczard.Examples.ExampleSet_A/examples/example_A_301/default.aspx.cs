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

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_301_default : System.Web.UI.Page
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

            // Load the fancybox script
            ExamplesHelper.LoadLibrary_FancyBox(this);
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;

        switch (this.ddlInterfaceMode.SelectedValue)
        {
            case "Full":
                this.InlinePictureTrimmer1.ShowRulers = true;
                this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;
                this.InlinePictureTrimmer1.ShowDetailsPanel = true;
                this.InlinePictureTrimmer1.ShowZoomPanel = true;
                this.InlinePictureTrimmer1.ShowResizePanel = true;
                this.InlinePictureTrimmer1.ShowRotatePanel = true;
                this.InlinePictureTrimmer1.ShowFlipPanel = true;
                this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = true;
                break;
            case "Standard":
                this.InlinePictureTrimmer1.ShowRulers = true;
                this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;
                this.InlinePictureTrimmer1.ShowDetailsPanel = true;
                this.InlinePictureTrimmer1.ShowZoomPanel = true;
                this.InlinePictureTrimmer1.ShowResizePanel = true;
                this.InlinePictureTrimmer1.ShowRotatePanel = true;
                this.InlinePictureTrimmer1.ShowFlipPanel = true;
                this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                break;
            case "Easy":
                this.InlinePictureTrimmer1.ShowRulers = true;
                this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;
                this.InlinePictureTrimmer1.ShowDetailsPanel = false;
                this.InlinePictureTrimmer1.ShowZoomPanel = false;
                this.InlinePictureTrimmer1.ShowResizePanel = true;
                this.InlinePictureTrimmer1.ShowRotatePanel = true;
                this.InlinePictureTrimmer1.ShowFlipPanel = true;
                this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                break;
            case "Minimal":
                this.InlinePictureTrimmer1.ShowRulers = true;
                this.InlinePictureTrimmer1.ShowCropAlignmentLines = true;
                this.InlinePictureTrimmer1.ShowDetailsPanel = false;
                this.InlinePictureTrimmer1.ShowZoomPanel = false;
                this.InlinePictureTrimmer1.ShowResizePanel = false;
                this.InlinePictureTrimmer1.ShowRotatePanel = false;
                this.InlinePictureTrimmer1.ShowFlipPanel = false;
                this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                break;
            case "Poor":
                this.InlinePictureTrimmer1.ShowRulers = false;
                this.InlinePictureTrimmer1.ShowCropAlignmentLines = false;
                this.InlinePictureTrimmer1.ShowDetailsPanel = false;
                this.InlinePictureTrimmer1.ShowZoomPanel = false;
                this.InlinePictureTrimmer1.ShowResizePanel = false;
                this.InlinePictureTrimmer1.ShowRotatePanel = false;
                this.InlinePictureTrimmer1.ShowFlipPanel = false;
                this.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = false;
                break;
        }

        this.InlinePictureTrimmer1.UIUnit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlGfxUnit.SelectedValue);
        this.InlinePictureTrimmer1.CanvasColor = System.Drawing.ColorTranslator.FromHtml(this.txtCanvasColor.Text);
        // Reset the OnClientControlLoad event hanlder (used to display the output image when the btnProcessImage is clicked
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "";
    }

    protected const string SourceImageFileName = "~/repository/source/donkey1.jpg";
    protected const string OutputImageFileName = "~/repository/output/Ex_A_301.jpg";

    protected void btnProcessImage_Click(object sender, EventArgs e)
    {
        if (!this.InlinePictureTrimmer1.ImageLoaded)
        {
            // Image not loaded !!!
            return;
        }

        // Process the image
        this.InlinePictureTrimmer1.SaveProcessedImageToFileSystem(OutputImageFileName);

        // Display the output image
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayOutputImage";
    }

    protected void btnLoadImage_Click(object sender, EventArgs e)
    {
        if (this.InlinePictureTrimmer1.ImageLoaded)
        {
            // Unload the image
            this.InlinePictureTrimmer1.UnloadImage();
            this.btnProcessImage.Enabled = false;
            this.ddlImageSelectionStrategy.Enabled = true;
            this.ddlOutputResolution.Enabled = true;
            this.btnLoadImage.Text = "Load image";
        }
        else
        {
            // Load the image
            float outputResolution = float.Parse(this.ddlOutputResolution.SelectedValue, System.Globalization.CultureInfo.InvariantCulture);

            CropConstraint cropConstrant = new FixedCropConstraint(GfxUnit.Pixel, 350, 250);
            cropConstrant.DefaultImageSelectionStrategy = (CropConstraintImageSelectionStrategy)Enum.Parse(typeof(CropConstraintImageSelectionStrategy), this.ddlImageSelectionStrategy.SelectedValue);
            if (cropConstrant.DefaultImageSelectionStrategy == CropConstraintImageSelectionStrategy.Slice)
            {
                cropConstrant.Margins.SetZero();
            }

            this.InlinePictureTrimmer1.AllowResize = cropConstrant.DefaultImageSelectionStrategy != CropConstraintImageSelectionStrategy.DoNotResize;

            this.InlinePictureTrimmer1.LoadImageFromFileSystem(SourceImageFileName, outputResolution, cropConstrant);

            this.btnProcessImage.Enabled = true;
            this.ddlImageSelectionStrategy.Enabled = false;
            this.ddlOutputResolution.Enabled = false;
            this.btnLoadImage.Text = "Unload image";
        }
    }

    protected void ddlInterfaceMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.InlinePictureTrimmer1.ImageLoaded)
        {
            // Reset the zoom factor to force the interface to center the image
            this.InlinePictureTrimmer1.UserState.UIParams.ZoomFactor = null;
            this.InlinePictureTrimmer1.UserState.UIParams.PictureScrollH = null;
            this.InlinePictureTrimmer1.UserState.UIParams.PictureScrollV = null;
        }
    }

}
