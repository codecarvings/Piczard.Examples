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
using CodeCarvings.Piczard.Filters.Watermarks;

public partial class examples_example_A_101_default : System.Web.UI.Page
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
        this.ProcessImage();

        // Display the source code
        this.phCodeContainer.Visible = true;
        for (int i = 0; i < 4; i++)
        {
            PlaceHolder phCode = (PlaceHolder)this.phCodeContainer.FindControl("phCode_" + i.ToString());
            phCode.Visible = i == this.ddlFilter.SelectedIndex;
        }
    }

    protected void ProcessImage()
    {
        // Setup the source file name and the output file name
        string sourceImageFileName = this.imgSource.ImageUrl;
        string outputImageFileName = "~/repository/output/Ex_A_101.jpg";

        switch (this.ddlFilter.SelectedIndex)
        {
            case 0:
                // Fixed Size Crop Constraint
                int cropWidth = 160;
                int cropHeight = 350;
                new FixedCropConstraint(cropWidth, cropHeight).SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);
                break;
            case 1:
                // Scaled Resize Constraint
                int maxWidth = 160;
                int maxHeight = 350;
                new ScaledResizeConstraint(maxWidth, maxHeight).SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);
                break;
            case 2:
                // Grayscale Color Filter
                DefaultColorFilters.Grayscale.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);
                break;
            case 3:
                // Text Watermark
                TextWatermark watermark = new TextWatermark();
                watermark.Text = DateTime.Now.ToString();
                watermark.ContentAlignment = ContentAlignment.TopRight;
                watermark.Font.Name = "Arial";
                watermark.Font.Size = 20;
                watermark.ForeColor = Color.Yellow;
                watermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);
                break;
        }

        // Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        this.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();

        // Display the generated image
        this.phOutputContainer.Visible = true;
    }
}
