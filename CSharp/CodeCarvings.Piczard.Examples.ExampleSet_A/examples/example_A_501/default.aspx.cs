/*
 * Piczard Examples | ExampleSet -A- C#
 * Copyright 2011-2013 Sergio Turolla - All Rights Reserved.
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
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Filters.Colors;
using CodeCarvings.Piczard.Filters.Watermarks;

public partial class examples_example_A_501_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI();", true);
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;

        if (!this.IsPostBack)
        {
            // First initialization
            this.SetPreset_1();
        }

        if (this.cbValidateImageSize.Checked)
        {
            // Register the event handler after every post back !
            this.ImageUpload1.ImageUpload += new SimpleImageUpload.ImageUploadEventHandler(this.ImageUpload1_ImageUpload);
        }
    }

    protected void SetPreset_1()
    {
        // Preset: Interactive crop
        this.cbEnableInteractiveImageProcessing.Checked = true;
        this.ddlCropConstraintMode.SelectedIndex = 0;
        this.cbAutoOpenImageEditPopupAfterUpload.Checked = true;

        this.cbPostProcessing_Resize.Checked = false;
        this.cbPostProcessing_Grayscale.Checked = false;
        this.cbPostProcessing_Watermark.Checked = false;

        this.cbPreviewConstriant.Checked = true;

        this.cbValidateImageSize.Checked = false;

        this.UpdateDemoParameters();
    }

    protected void SetPreset_2()
    {
        // Preset: Automatic resize
        this.cbEnableInteractiveImageProcessing.Checked = false;
        this.ddlCropConstraintMode.SelectedIndex = 0;
        this.cbAutoOpenImageEditPopupAfterUpload.Checked = false;

        this.cbPostProcessing_Resize.Checked = true;
        this.cbPostProcessing_Grayscale.Checked = false;
        this.cbPostProcessing_Watermark.Checked = false;

        this.cbPreviewConstriant.Checked = true;

        this.cbValidateImageSize.Checked = false;

        this.UpdateDemoParameters();
    }

    protected void UpdateDemoParameters()
    {
        // Unload the current image if necessary
        if (this.ImageUpload1.HasImage)
        {
            this.ImageUpload1.UnloadImage();
        }

        this.ddlCropConstraintMode.Enabled = this.cbEnableInteractiveImageProcessing.Checked;
        this.cbAutoOpenImageEditPopupAfterUpload.Enabled = this.cbEnableInteractiveImageProcessing.Checked;

        this.ImageUpload1.EnableEdit = this.cbEnableInteractiveImageProcessing.Checked;
        this.txtInteractiveImageProcessing.Text = this.ImageUpload1.EnableEdit ? "" : "// Disable interactive image processing features\r\nImageUpload1.EnableEdit = false;\r\n";
        if (this.ImageUpload1.EnableEdit)
        {
            switch (this.ddlCropConstraintMode.SelectedIndex)
            {
                case 0:
                    // Crop enabled: Fixed size
                    this.ImageUpload1.CropConstraint = new FixedCropConstraint(300, 200);
                    this.txtInteractiveImageProcessing.Text += "// Add a fixed size crop constraint (300 x 200 pixels)\r\n";
                    this.txtInteractiveImageProcessing.Text += "ImageUpload1.CropConstraint = new FixedCropConstraint(300, 200);\r\n";
                    break;
                case 1:
                    // Crop enabled: Fixed aspect ratio
                    this.ImageUpload1.CropConstraint = new FixedAspectRatioCropConstraint(16F / 9F);
                    this.txtInteractiveImageProcessing.Text += "// Add a fixed aspect ratio crop constraint (asperct ratio = 16:9)\r\n";
                    this.txtInteractiveImageProcessing.Text += "ImageUpload1.CropConstraint = new FixedAspectRatioCropConstraint(16F / 9F);\r\n";
                    break;
                case 2:
                    // Crop enabled: Free size
                    this.ImageUpload1.CropConstraint = new FreeCropConstraint();
                    this.txtInteractiveImageProcessing.Text += "// Add a free crop constraint\r\n";
                    this.txtInteractiveImageProcessing.Text += "ImageUpload1.CropConstraint = new FreeCropConstraint();\r\n";
                    break;
                case 3:
                    // Crop disabled
                    this.ImageUpload1.CropConstraint = null;
                    break;
            }
        }
        else
        {
            // Reset the CropConstraint
            this.ImageUpload1.CropConstraint = null;
        }

        this.ImageUpload1.AutoOpenImageEditPopupAfterUpload = this.ddlCropConstraintMode.Enabled ? this.cbAutoOpenImageEditPopupAfterUpload.Checked : false;

        this.txtInteractiveImageProcessing.Text += this.ImageUpload1.AutoOpenImageEditPopupAfterUpload ? "// Automatically open image edit window after upload\r\nImageUpload1.AutoOpenImageEditPopupAfterUpload = true;\r\n" : "";
        this.txtInteractiveImageProcessing.Rows = this.txtInteractiveImageProcessing.Text.Split('\n').Length - 1;
        this.txtInteractiveImageProcessing.Visible = !string.IsNullOrEmpty(this.txtInteractiveImageProcessing.Text);
        

        // Create a filter collection to handle multiple filters
        ImageProcessingFilterCollection postProcessing = new ImageProcessingFilterCollection();
        string postProcessingCode = "";
        if (this.cbPostProcessing_Resize.Checked)
        {
            // Add the resize filter
            postProcessing.Add(new ScaledResizeConstraint(200, 200));
            postProcessingCode += "new ScaledResizeConstraint(200, 200)";
        }
        if (this.cbPostProcessing_Grayscale.Checked)
        {
            // Add the graysacale filter
            postProcessing.Add(CodeCarvings.Piczard.Filters.Colors.DefaultColorFilters.Grayscale);
            if (!string.IsNullOrEmpty(postProcessingCode))
            {
                postProcessingCode += " + ";
            }
            postProcessingCode += "DefaultColorFilters.Grayscale";
        }
        if (this.cbPostProcessing_Watermark.Checked)
        {
            // Add the watermark filter
            postProcessing.Add(new CodeCarvings.Piczard.Filters.Watermarks.ImageWatermark("~/repository/watermark/piczardWatermark1.png", System.Drawing.ContentAlignment.BottomRight));
            if (!string.IsNullOrEmpty(postProcessingCode))
            {
                postProcessingCode += " + ";
            }
            postProcessingCode += "new ImageWatermark(\"~/watermark1.png\", ContentAlignment.BottomRight)";
        }
        this.ImageUpload1.PostProcessingFilter = postProcessing; 

        if (!string.IsNullOrEmpty(postProcessingCode))
        {
            this.txtPostProcessing.Text = "// Add the post-processing filters\r\nImageUpload1.PostProcessingFilter = " + postProcessingCode + ";\r\n";
        }
        else
        {
            this.txtPostProcessing.Text = "";
        }        
        this.txtPostProcessing.Visible = !string.IsNullOrEmpty(this.txtPostProcessing.Text);
        this.txtPostProcessing.Rows = postProcessing.Count + 2;

        if (this.cbPreviewConstriant.Checked)
        {
            this.ImageUpload1.PreviewFilter = new FixedResizeConstraint(200, 200, Color.Black);
        }
        else
        {
            this.ImageUpload1.PreviewFilter = null;
        }

        this.txtPreviewConstriant.Text = this.cbPreviewConstriant.Checked ? "// Set a square resize constraint (200 x 200 pixels, bg:black) for the preview\r\nImageUpload1.PreviewFilter = new FixedResizeConstraint(200, 200, Color.Black);\r\n" : "";
        this.txtPreviewConstriant.Visible = !string.IsNullOrEmpty(this.txtPreviewConstriant.Text);

        this.txtValidateImageSize.Visible = this.cbValidateImageSize.Checked;
    }

    #region Event Handlers

    protected void ImageUpload1_ImageUpload(object sender, SimpleImageUpload.ImageUploadEventArgs args)
    {
        if (this.ImageUpload1.SourceImageSize.Width < 250)
        {
            // The uploaded image is too small
            this.ImageUpload1.UnloadImage();
            this.ImageUpload1.SetCurrentStatusMessage("<span style=\"color:#cc0000;\">Image width must be at least 250 px.</span>");
            return;
        }
    }

    protected void btnPreset_1_Click(object sender, EventArgs e)
    {
        this.SetPreset_1();
    }

    protected void btnPreset_2_Click(object sender, EventArgs e)
    {
        this.SetPreset_2();
    }

    protected void cbEnableInteractiveImageProcessing_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    protected void ddlCropConstraintMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    protected void cbAutoOpenImageEditPopupAfterUpload_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    protected void cbPostProcessing_Resize_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    protected void cbPostProcessing_Grayscale_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    protected void cbPostProcessing_Watermark_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    protected void cbPreviewConstriant_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    protected void cbValidateImageSize_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateDemoParameters();
    }

    #endregion

}
