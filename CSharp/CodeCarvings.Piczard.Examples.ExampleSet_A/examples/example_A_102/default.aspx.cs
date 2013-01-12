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

public partial class examples_example_A_102_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", true);
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;

        // Re-process the image after every postback...
        this.ProcessImage();
        this.GenerateExampleCode();
    }

    protected void ProcessImage()
    {
        // Setup the source file name and the output file name
        string sourceImageFileName = this.imgSource.ImageUrl;
        string outputImageFileName = "~/repository/output/Ex_A_102.jpg";

        // Setup a collection and add filters selected by the user
        ImageProcessingFilterCollection filters = new ImageProcessingFilterCollection();

        string[] sortedFilterIDS = this.hfFilterList.Value.Split(',');
        // The filterIDS contains the sorted filters that we have to apply...
        for (int i = 0; i < sortedFilterIDS.Length; i++)
        {
            string filterID = sortedFilterIDS[i];
            switch (filterID)
            {
                case "filterRotate":
                    if (this.cbFilterRotate.Checked)
                    {
                        // Rotate the image by 90°
                        filters.Add(ImageTransformation.Rotate90);
                    }
                    break;
                case "filterResize":
                    if (this.cbFilterResize.Checked)
                    {
                        // Resize the image so that it can be contained within a 280 x 280 square
                        filters.Add(new ScaledResizeConstraint(280, 280));
                    }
                    break;
                case "filterChangeColors":
                    if (this.cbFilterChangeColors.Checked)
                    {
                        // Change hue, saturation, brightness and contrast of the image
                        filters.Add(new ImageAdjustmentsFilter(30, 50, 20, -50));
                    }
                    break;
                case "filterWatermark":
                    if (this.cbFilterWatermark.Checked)
                    {
                        // Apply an image watermak
                        ImageWatermark watermark = new ImageWatermark("~/repository/watermark/piczardWatermark1.png");
                        watermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;
                        filters.Add(watermark);
                    }
                    break;
            }
        }

        // Process the image
        filters.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

        // Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        this.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();
    }

    protected void GenerateExampleCode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("// Setup a collection and add filters selected by the user\r\n");
        sb.Append("ImageProcessingFilterCollection filters = new ImageProcessingFilterCollection();\r\n");

        string[] sortedFilterIDS = this.hfFilterList.Value.Split(',');
        for (int i = 0; i < sortedFilterIDS.Length; i++)
        {
            string filterID = sortedFilterIDS[i];
            switch (filterID)
            {
                case "filterRotate":
                    if (this.cbFilterRotate.Checked)
                    {
                        sb.Append("\r\n");
                        sb.Append("// Rotate the image by 90°\r\n");
                        sb.Append("filters.Add(ImageTransformation.Rotate90);\r\n");
                    }
                    break;
                case "filterResize":
                    if (this.cbFilterResize.Checked)
                    {
                        sb.Append("\r\n");
                        sb.Append("// Resize the image so that it can be contained within a 280 x 280 square\r\n");
                        sb.Append("filters.Add(new ScaledResizeConstraint(280, 280));\r\n");
                    }
                    break;
                case "filterChangeColors":
                    if (this.cbFilterChangeColors.Checked)
                    {
                        sb.Append("\r\n");
                        sb.Append("// Change hue, saturation, brightness and contrast of the image\r\n");
                        sb.Append("filters.Add(new ImageAdjustmentsFilter(30, 50, 20, -50));\r\n");
                    }
                    break;
                case "filterWatermark":
                    if (this.cbFilterWatermark.Checked)
                    {
                        sb.Append("\r\n");
                        sb.Append("// Apply an image watermak\r\n");
                        sb.Append("ImageWatermark watermark = new ImageWatermark(\"~/repository/watermark/piczardWatermark1.png\");\r\n");
                        sb.Append("watermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;\r\n");
                        sb.Append("filters.Add(watermark);\r\n");
                    }
                    break;
            }
        }

        sb.Append("\r\n");
        sb.Append("// Process the image\r\n");
        sb.Append("string sourceImageFileName = \"~/repository/source/valencia1.jpg\";\r\n");
        sb.Append("string outputImageFileName = \"~/repository/output/Ex_A_102.jpg\";\r\n");
        sb.Append("filters.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);\r\n");

        this.litCode.Text = sb.ToString();
    }

}
