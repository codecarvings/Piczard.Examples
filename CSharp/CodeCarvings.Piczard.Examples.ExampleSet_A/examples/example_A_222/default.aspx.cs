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
using CodeCarvings.Piczard.Filters.Colors;

public partial class examples_example_A_222_default : System.Web.UI.Page
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

        this.ddlDefaultColorFilters.Enabled = this.cbDefaultColorFilters.Checked;
        this.ddlImageAdjustmentsFilter.Enabled = this.cbImageAdjustmentsFilter.Checked;
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
        string outputImageFileName = "~/repository/output/Ex_A_222.jpg";

         // Use an image processing job since we are using 2 filters
        ImageProcessingJob job = new ImageProcessingJob();

        if (this.cbDefaultColorFilters.Checked)
        {
            switch (this.ddlDefaultColorFilters.SelectedIndex)
            {
                case 0:
                    // Grayscale
                    job.Filters.Add(DefaultColorFilters.Grayscale);
                    break;
                case 1:
                    // Sepia
                    job.Filters.Add(DefaultColorFilters.Sepia);
                    break;
                case 2:
                    // Invert
                    job.Filters.Add(DefaultColorFilters.Invert);
                    break;
            }
        }

        if (this.cbImageAdjustmentsFilter.Checked)
        {
            switch (this.ddlImageAdjustmentsFilter.SelectedIndex)
            {
                case 0:
                    // Decrease brightness
                    job.Filters.Add(new ImageAdjustmentsFilter(-60, 0));
                    break;
                case 1:
                    // Decrease contrast
                    job.Filters.Add(new ImageAdjustmentsFilter(0, -50));
                    break;
                case 2:
                    // Decrease saturation
                    job.Filters.Add(new ImageAdjustmentsFilter(0, 0, 0, -80));
                    break;
                case 3:
                    // Increase brightness
                    job.Filters.Add(new ImageAdjustmentsFilter(50, 0));
                    break;
                case 4:
                    // Increase contrast
                    job.Filters.Add(new ImageAdjustmentsFilter(0, 40));
                    break;
                case 5:
                    // Increase saturation
                    job.Filters.Add(new ImageAdjustmentsFilter(0, 0, 0, 70));
                    break;
                case 6:
                    // Change hue
                    job.Filters.Add(new ImageAdjustmentsFilter(0, 0, 60, 0));
                    break;
                case 7:
                    // Color combination #1
                    job.Filters.Add(new ImageAdjustmentsFilter(42, 42, 0, -72));
                    break;
                case 8:
                    // Color combination #2
                    job.Filters.Add(new ImageAdjustmentsFilter(0, 22, 180, 20));
                    break;
            }
        }

        // Process the image
        job.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

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
        sbCode.Append("string sourceImage = \"~/repository/source/valencia2.jpg\";" + crlf);
        sbCode.Append("string outputImage = \"~/repository/output/Ex_A_222.jpg\";" + crlf);
        sbCode.Append(crlf);

        sbCode.Append("// Setup an image processing job" + crlf);
        sbCode.Append("ImageProcessingJob job = new ImageProcessingJob();" + crlf);

        if (this.cbDefaultColorFilters.Checked)
        {
            switch (this.ddlDefaultColorFilters.SelectedIndex)
            {
                case 0:
                    // Grayscale
                    sbCode.Append("job.Filters.Add(DefaultColorFilters.Grayscale);" + crlf);
                    break;
                case 1:
                    // Sepia
                    sbCode.Append("job.Filters.Add(DefaultColorFilters.Sepia);" + crlf);
                    break;
                case 2:
                    // Invert
                    sbCode.Append("job.Filters.Add(DefaultColorFilters.Invert);" + crlf);
                    break;
            }
        }

        if (this.cbImageAdjustmentsFilter.Checked)
        {
            switch (this.ddlImageAdjustmentsFilter.SelectedIndex)
            {
                case 0:
                    // Decrease brightness
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(-60, 0));" + crlf);
                    break;
                case 1:
                    // Decrease contrast
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(0, -50));" + crlf);
                    break;
                case 2:
                    // Decrease saturation
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(0, 0, 0, -80));" + crlf);
                    break;
                case 3:
                    // Increase brightness
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(50, 0));" + crlf);
                    break;
                case 4:
                    // Increase contrast
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(0, 40));" + crlf);
                    break;
                case 5:
                    // Increase saturation
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(0, 0, 0, 70));" + crlf);
                    break;
                case 6:
                    // Change hue
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(0, 0, 60, 0));" + crlf);
                    break;
                case 7:
                    // Color combination #1
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(42, 42, 0, -72));" + crlf);
                    break;
                case 8:
                    // Color combination #2
                    sbCode.Append("job.Filters.Add(new ImageAdjustmentsFilter(0, 22, 180, 20));" + crlf);
                    break;
            }
        }

        sbCode.Append(crlf);
        sbCode.Append("// Process the image" + crlf);
        sbCode.Append("job.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);

        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }
}
