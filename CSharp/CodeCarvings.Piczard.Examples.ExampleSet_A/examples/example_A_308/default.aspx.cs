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

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Colors;
using CodeCarvings.Piczard.Filters.Watermarks;
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_308_default : System.Web.UI.Page
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

        if (!this.IsPostBack)
        {
            // Load the image
            FixedCropConstraint cropConstraint = new FixedCropConstraint(250, 150);
            cropConstraint.Margins.SetZero();
            this.InlinePictureTrimmer1.LoadImageFromFileSystem(SourceImageFileName, cropConstraint);
        }
    }

    protected const string SourceImageFileName = "~/repository/source/donkey1.jpg";
    protected const string OutputImageFileName = "~/repository/output/Ex_A_308.jpg";
    protected const string WatermarkImageFileName = "~/repository/watermark/piczardWatermark1.png";

    protected void btnProcessImage_Click(object sender, EventArgs e)
    {
        // Process the image

        // Get the image processing job
        ImageProcessingJob job = this.InlinePictureTrimmer1.GetImageProcessingJob();
        
        // Add the filters
        job.Filters.Add(DefaultColorFilters.Grayscale); // Grayscale
        job.Filters.Add(new ImageWatermark(WatermarkImageFileName, System.Drawing.ContentAlignment.BottomRight)); // Watermark

        // Save the image
        job.SaveProcessedImageToFileSystem(SourceImageFileName, OutputImageFileName);

        // Display the output image
        this.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayOutputImage";
    }



}
