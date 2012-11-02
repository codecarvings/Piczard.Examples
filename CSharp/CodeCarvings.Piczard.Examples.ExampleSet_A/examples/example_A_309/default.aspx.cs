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
using CodeCarvings.Piczard.Filters.Colors;
using CodeCarvings.Piczard.Filters.Watermarks;
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_309_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI();", true);
        }

        if (!this.ScriptManager1.IsInAsyncPostBack)
        {
            // Load the fancybox script
            ExamplesHelper.LoadLibrary_FancyBox(this);
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;

        // Reset the OnClientControlLoad event hanlder (used to display the output image when the btnProcessImage is clicked
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "";
    }

    protected const string SourceImageFileName_Landscape = "~/repository/source/trevi1.jpg";
    protected const string SourceImageFileName_Portrait = "~/repository/source/valencia2.jpg";
    protected const string OutputImageFileName = "~/repository/output/Ex_A_309.jpg";

    protected void btnLoadLandscape_Click(object sender, EventArgs e)
    {
        // Load the landscape image
        this.loadImage(SourceImageFileName_Landscape);
    }

    protected void btnLoadPortrait_Click(object sender, EventArgs e)
    {
        // Load the portrait image
        this.loadImage(SourceImageFileName_Portrait);
    }

    private string lastLoadImagePath
    {
        get
        {
            return (string)this.ViewState["lastLoadImagePath"];
        }
        set
        {
            this.ViewState["lastLoadImagePath"] = value;
        }
    }

    protected void loadImage(string sourceImagePath)
    {
        // Store the source image path (it's used in the ddlCropOrientation_SelectedIndexChanged event handler)
        this.lastLoadImagePath = sourceImagePath;

        // Get the source iamge size
        System.Drawing.Size sourceImageSize;
        using (LoadedImage image = ImageArchiver.LoadImage(sourceImagePath))
        {
            sourceImageSize = image.Size;
        }

        // Auto detect the image orientation
        if (sourceImageSize.Width > sourceImageSize.Height)
        {
            // Is a landscape image
            this.ddlCropOrientation.SelectedIndex = 0;
        }
        else
        {
            // Is a portrait image
            this.ddlCropOrientation.SelectedIndex = 1;
        }

        // Load the image
        CropConstraint cropConstraint = this.getCropConstraint();
        this.InlinePictureTrimmer1.LoadImageFromFileSystem(sourceImagePath, cropConstraint);

        if (!this.ddlCropOrientation.Enabled)
        {
            // This is the first time an image is loaded...
            // Refresh some UI elements

            // Enable the drop down  list control
            this.ddlCropOrientation.Enabled = true;
            // Enable the "Preview" button
            this.btnProcessImage.Enabled = true;
        }
    }

    protected CropConstraint getCropConstraint()
    {
        // Get the crop constraint associated with the image orientation currently selected

        FixedCropConstraint result;
        if (this.ddlCropOrientation.SelectedIndex == 0)
        {
            // Return the landscape crop constraint
            result = new FixedCropConstraint(250, 150);
            result.Margins.SetZero();
        }
        else
        {
            // Return the portrait crop constraint
            result = new FixedCropConstraint(150, 250);
            result.Margins.SetZero();
        }

        return result;
    }

    protected void ddlCropOrientation_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Image orientation changed...

        // Re-Load the image wit the new Crop constraint
        CropConstraint cropConstraint = this.getCropConstraint();
        this.InlinePictureTrimmer1.LoadImageFromFileSystem(this.lastLoadImagePath, cropConstraint);
    }

    protected void btnProcessImage_Click(object sender, EventArgs e)
    {
        // Process the image
        this.InlinePictureTrimmer1.SaveProcessedImageToFileSystem(OutputImageFileName);        

        // Display the output image
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayOutputImage";
    }

}
