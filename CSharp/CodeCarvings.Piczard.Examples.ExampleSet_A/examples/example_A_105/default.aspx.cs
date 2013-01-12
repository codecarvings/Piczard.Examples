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

public partial class examples_example_A_105_default : System.Web.UI.Page
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
        this.phOutputContainer.Visible = false;
        this.phCodeContainer.Visible = false;
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        this.processImage();
    }

    protected void processImage()
    {
        using (LoadedImage sourceImage = ImageArchiver.LoadImage(this.imgSource.ImageUrl))
        {
            // Generate the image #1
            string outputImageFileName = "~/repository/output/Ex_A_105___1.jpg";
            new ScaledResizeConstraint(150, 250).SaveProcessedImageToFileSystem(sourceImage, outputImageFileName);
            this.imgOutput1.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();

            // Generate the image #2
            outputImageFileName = "~/repository/output/Ex_A_105___2.jpg";
            new FixedCropConstraint(150, 250).SaveProcessedImageToFileSystem(sourceImage, outputImageFileName);
            this.imgOutput2.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();

            // Generate the image #3
            outputImageFileName = "~/repository/output/Ex_A_105___3.jpg";
            new ImageProcessingJob(new ImageProcessingFilter[] {
                new FixedResizeConstraint(150, 250),
                DefaultColorFilters.Grayscale
            }).SaveProcessedImageToFileSystem(sourceImage, outputImageFileName);
            this.imgOutput3.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();
        }

        this.phOutputContainer.Visible = true;
        this.phCodeContainer.Visible = true;
    }
}
