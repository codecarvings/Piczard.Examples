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

public partial class examples_example_A_106_default : System.Web.UI.Page
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
        // Process the image and display the source code
        this.ProcessImage();
        this.GenerateExampleCode();
    }

    protected void ProcessImage()
    {
        // Setup the source file name and the output file name
        string sourceImageFileName = this.imgSource.ImageUrl;
        string outputImageFileName = "~/repository/output/Ex_A_106.jpg";

        // Get the image processing job for the specified resolution
        ImageProcessingJob job = new ImageProcessingJob(float.Parse(this.ddlOutputResolution.SelectedValue, System.Globalization.CultureInfo.InvariantCulture));

        // Setup a crop constraint
        switch (this.ddlCropSize.SelectedIndex)
        {
            case 0:
                // 200 x 300 px
                job.Filters.Add(new FixedCropConstraint(200, 300));
                break;
            case 1:
                // 50 x 75 points
                job.Filters.Add(new FixedCropConstraint(GfxUnit.Point, 50, 75));
                break;
            case 2:
                // 20 x 30 mm
                job.Filters.Add(new FixedCropConstraint(GfxUnit.Mm, 20, 30));
                break;
            case 3:
                // 0.6 x 0.9 inch
                job.Filters.Add(new FixedCropConstraint(GfxUnit.Inch, 0.6F, 0.9F));
                break;
        }

        // Proces the image
        job.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

        // Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        this.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();

        // Display the generated image
        this.phOutputContainer.Visible = true;
    }

    protected void GenerateExampleCode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("// Setup an ImageProcessingJob / Output resolution = " + this.ddlOutputResolution.SelectedValue + " DPI\r\n");
        if (this.ddlOutputResolution.SelectedValue == "96")
        {
            // Default value = 96DPI
            sb.Append("ImageProcessingJob job = new ImageProcessingJob();\r\n");
        }
        else
        {
            sb.Append("ImageProcessingJob job = new ImageProcessingJob(" + this.ddlOutputResolution.SelectedValue + ");\r\n");
        }

        sb.Append("\r\n");
        sb.Append("// Add the crop filter ");
        switch (this.ddlCropSize.SelectedIndex)
        {
            case 0:
                // 200 x 300 px
                sb.Append("(200 x 300 px)\r\n");
                sb.Append("job.Filters.Add(new FixedCropConstraint(200, 300));\r\n");
                break;
            case 1:
                // 50 x 75 points
                sb.Append("(50 x 75 points)\r\n");
                sb.Append("job.Filters.Add(new FixedCropConstraint(GfxUnit.Point, 50, 75));\r\n");
                break;
            case 2:
                // 20 x 30 mm
                sb.Append("(20 x 30 mm)\r\n");
                sb.Append("job.Filters.Add(new FixedCropConstraint(GfxUnit.Mm, 20, 30));\r\n");
                break;
            case 3:
                // 0.6 x 0.9 inch
                sb.Append("(0.6 x 0.9 inch)\r\n");
                sb.Append("job.Filters.Add(new FixedCropConstraint(GfxUnit.Inch, 0.6F, 0.9F));\r\n");
                break;
        }

        sb.Append("\r\n");
        sb.Append("// Process the image\r\n");
        sb.Append("string sourceImageFileName = \"~/repository/source/greece1.jpg\";\r\n");
        sb.Append("string outputImageFileName = \"~/repository/output/Ex_A_106.jpg\";\r\n");
        sb.Append("job.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);\r\n");

        this.litCode.Text = sb.ToString();
        this.phCodeContainer.Visible = true;
    }
}
