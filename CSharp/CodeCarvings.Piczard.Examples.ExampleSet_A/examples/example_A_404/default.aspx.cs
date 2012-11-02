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
using System.Globalization;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Serialization;
using CodeCarvings.Piczard.Filters.Colors;
using CodeCarvings.Piczard.Filters.Watermarks;

public partial class examples_example_A_404_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", true);
        }
        else
        {
            // Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(this);

            // Load the fancybox script
            ExamplesHelper.LoadLibrary_FancyBox(this);
        }
    
        if (!this.Page.IsPostBack)
        {
            this.PopulateDropdownLists();

            // Load the image
            this.LoadImageIntoPictureTrimmer(true);

            // Initialize the zoom factor
            this.InlinePictureTrimmer1.UserState.UIParams.ZoomFactor = float.Parse(this.ddlZoomFactor.SelectedValue, System.Globalization.CultureInfo.InvariantCulture);
        }

        // Update the canvas color and the image back color
        this.InlinePictureTrimmer1.CanvasColor = System.Drawing.ColorTranslator.FromHtml(this.txtCanvasColor.Text);
        this.InlinePictureTrimmer1.ImageBackColor = this.InlinePictureTrimmer1.CanvasColor.Clone();

        // Update the UI Unit
        this.InlinePictureTrimmer1.UIUnit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlGfxUnit.SelectedValue);

        // Reset the OnClientControlLoad event hanlder (used to display the preview when the btnPreview is clicked
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "";

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
    }

    protected const string SourceImageFileName = "~/repository/source/donkey1.jpg";
    protected const string OutputImageFileName = "~/repository/output/Ex_A_404.jpg";

    protected void LoadImageIntoPictureTrimmer(bool resetSelection)
    {
        // Pre-process the image
        ImageProcessingJob job = this.GetImagePreProcessingJob();
        using (System.Drawing.Bitmap preProcessedImage = job.GetProcessedImage(SourceImageFileName))
        {
            // Backup the previous userState
            PictureTrimmerUserState previousUserState = null;
            if (this.InlinePictureTrimmer1.ImageLoaded)
            {
                previousUserState = this.InlinePictureTrimmer1.UserState;
            }

            // Load the pre-processed image into the PictureTrimmer control
            this.InlinePictureTrimmer1.LoadImage(preProcessedImage, new FreeCropConstraint(null, 600, null, 600));

            if (previousUserState != null)
            {
                if (resetSelection)
                {
                    // Re-calculate the cropping rectangle
                    previousUserState.Value.ImageSelection.Crop.Rectangle = null;
                    // Re-center the image
                    previousUserState.UIParams.PictureScrollH = null;
                    previousUserState.UIParams.PictureScrollV = null;
                }

                // Restore the previous user state
                this.InlinePictureTrimmer1.UserState = previousUserState;
            }
        }
    }

    protected ImageProcessingJob GetImagePreProcessingJob()
    {
        ImageProcessingJob job = new ImageProcessingJob();

        // Set the back color
        job.ImageBackColor.Value = this.InlinePictureTrimmer1.ImageBackColor.Clone();

        // Add the color filter
        switch (this.ddlDefaultColorFilters.SelectedIndex)
        {
            case 0:
                // No color filter
                break;
            case 1:
                // Grayscale
                job.Filters.Add(DefaultColorFilters.Grayscale);
                break;
            case 2:
                // Sepia
                job.Filters.Add(DefaultColorFilters.Sepia);
                break;
            case 3:
                // Invert
                job.Filters.Add(DefaultColorFilters.Invert);
                break;
        }

        // Add the rotation filter
        float rotationAngle = float.Parse(this.ddlRotationAngle.SelectedValue, System.Globalization.CultureInfo.InvariantCulture);
        job.Filters.Add(new MyRotationFilter(rotationAngle));

        return job;
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        // Process the image
        this.InlinePictureTrimmer1.SaveProcessedImageToFileSystem(OutputImageFileName);

        // Display the output image
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayPreview";
    }

    protected void ddlDefaultColorFilters_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Update the image
        this.LoadImageIntoPictureTrimmer(false);
    }

    protected void ddlRotationAngle_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Update the image
        this.LoadImageIntoPictureTrimmer(true);
    }

    protected void PopulateDropdownLists()
    {
        for (int i = 0; i <= 360; i+=5)
        {
            ListItem item = new ListItem(i.ToString() + "°", i.ToString());
            this.ddlRotationAngle.Items.Add(item);

            if (i == 20)
            {
                // Default value for this example = 20°
                item.Selected = true;
            }
        }
    }

}
