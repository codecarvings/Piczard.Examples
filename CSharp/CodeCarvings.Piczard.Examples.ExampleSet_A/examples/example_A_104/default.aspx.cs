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

public partial class examples_example_A_104_default : System.Web.UI.Page
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
            // Load the Galleria Script
            ExamplesHelper.LoadLibrary_Galleria(this);
        }

        if (!this.IsPostBack)
        {
            // Display the source images
            this.displaySourceImages();
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
        this.ddlImageFormat.Enabled = this.rbFormatCustom.Checked;
        this.phOutputContainer.Visible = false;
        this.phCodeContainer.Visible = false;
    }

    protected const string SourceImagesFolder = "~/repository/source/set1/";
    protected const string OutputImagesFolder = "~/repository/output/Ex_A_104/";

    protected void processImages()
    {
        // Delete the previously generated files
        string[] oldOutputImages = System.IO.Directory.GetFiles(Server.MapPath(OutputImagesFolder));
        foreach (string oldOutputImage in oldOutputImages)
        {
            System.IO.File.Delete(oldOutputImage);
        }

        // Create a new image processing job
        ImageProcessingJob job = new ImageProcessingJob();

        if (this.cbFilterCrop.Checked)
        {
            // Add the crop filter
            job.Filters.Add(new FixedCropConstraint(380, 320));
        }

        if (this.cbFilterColor.Checked)
        {
            job.Filters.Add(DefaultColorFilters.Sepia);
        }

        if (this.cbFilterWatermark.Checked)
        {
            // Add the watermark
            TextWatermark textWatermark = new TextWatermark();
            textWatermark.ContentAlignment = ContentAlignment.BottomRight;
            textWatermark.ForeColor = Color.FromArgb(230, Color.Black);
            textWatermark.Text = "Image processed by Piczard";
            textWatermark.Font.Size = 12;
            job.Filters.Add(textWatermark);
        }

        // Get the files to process
        string[] sourceImages = System.IO.Directory.GetFiles(Server.MapPath(SourceImagesFolder));

        for (int i = 0; i < sourceImages.Length; i++)
        {
            string sourceFilePath = sourceImages[i];
            string sourceFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(sourceFilePath);

            // Get the output image format
            FormatEncoderParams outputFormat;
            if (this.rbFormatCustom.Checked)
            {
                // Custom format
                outputFormat = ImageArchiver.GetFormatEncoderParamsFromFileExtension(this.ddlImageFormat.SelectedValue);
            }
            else
            {
                // Same format as the source image
                outputFormat = ImageArchiver.GetFormatEncoderParamsFromFilePath(sourceFilePath);
            }

            string outputFileName = sourceFileNameWithoutExtension + outputFormat.FileExtension;
            string outputFilePath = System.IO.Path.Combine(OutputImagesFolder, outputFileName);

            // Process the image
            job.SaveProcessedImageToFileSystem(sourceFilePath, outputFilePath);            
        }
    }

    protected void displaySourceImages()
    {
        // Dispaly all the source images
        string[] sourceImages = System.IO.Directory.GetFiles(Server.MapPath(SourceImagesFolder));

        this.rptSourceImages.DataSource = sourceImages;
        this.rptSourceImages.ItemDataBound += new RepeaterItemEventHandler(rptSourceImages_ItemDataBound);
        this.rptSourceImages.DataBind();
    }

    protected void rptSourceImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            // Display the source image
            string filePath = (string)e.Item.DataItem;
            string fileName = System.IO.Path.GetFileName(filePath);

            System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSource");
            image.AlternateText = "Source image '" + fileName + "'";
            image.ImageUrl = SourceImagesFolder + fileName;
        }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        // Process the images and display the result
        this.processImages();        
        this.displayOutputImages();
        this.phOutputContainer.Visible = true;

        // Generate the source code
        this.GenerateExampleCode();
        this.phCodeContainer.Visible = true;
    }

    protected void displayOutputImages()
    {
        // Dispaly all the output images
        string[] outputImages = System.IO.Directory.GetFiles(Server.MapPath(OutputImagesFolder));

        this.rptOutputImages.DataSource = outputImages;
        this.rptOutputImages.ItemDataBound += new RepeaterItemEventHandler(rptOutputImages_ItemDataBound);
        this.rptOutputImages.DataBind();
    }

    protected void rptOutputImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            // Display the image
            string filePath = (string)e.Item.DataItem;
            string fileName = System.IO.Path.GetFileName(filePath);

            System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgOutput");
            image.AlternateText = "Output image '" + fileName + "'";
            // Use a timestamp to force the reloading of the image
            image.ImageUrl = OutputImagesFolder + fileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();
        }
    }

    protected void GenerateExampleCode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("// Prepare the parameters\r\n");
        sb.Append("string sourceImagesFolder = \"~/repository/source/set1/\";\r\n");
        sb.Append("string outputImagesFolder = \"~/repository/output/Ex_A_104/\";\r\n");
        
        sb.Append("\r\n");
        sb.Append("// Create a new image processing job\r\n");
        sb.Append("ImageProcessingJob job = new ImageProcessingJob();\r\n");
        
        if (this.cbFilterCrop.Checked)
        {
            sb.Append("\r\n");
            sb.Append("// Add the crop filter\r\n");
            sb.Append("job.Filters.Add(new FixedCropConstraint(380, 320));\r\n");
        }

        if (this.cbFilterColor.Checked)
        {
            sb.Append("\r\n");
            sb.Append("// Add the sepia tone filter\r\n");
            sb.Append("job.Filters.Add(DefaultColorFilters.Sepia);\r\n");
        }

        if (this.cbFilterWatermark.Checked)
        {
            sb.Append("\r\n");
            sb.Append("// Add the watermark\r\n");
            sb.Append("TextWatermark textWatermark = new TextWatermark();\r\n");
            sb.Append("textWatermark.ContentAlignment = ContentAlignment.BottomRight;\r\n");
            sb.Append("textWatermark.ForeColor = Color.FromArgb(230, 0, 32, 0);\r\n");
            sb.Append("textWatermark.Text = \"Image processed by Piczard\";\r\n");
            sb.Append("textWatermark.Font.Size = 12;\r\n");
            sb.Append("job.Filters.Add(textWatermark);\r\n");
        }

        sb.Append("\r\n");
        sb.Append("// Get the files to process\r\n");
        sb.Append("string[] sourceImages = System.IO.Directory.GetFiles(Server.MapPath(sourceImagesFolder));\r\n");

        sb.Append("for (int i = 0; i < sourceImages.Length; i++)\r\n");
        sb.Append("{\r\n");
        sb.Append("    string sourceFilePath = sourceImages[i];\r\n");
        sb.Append("    string sourceFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(sourceFilePath);\r\n");
        sb.Append("\r\n");
        if (this.rbFormatCustom.Checked)
        {
            sb.Append("    // Get the output image format (custom - " + this.ddlImageFormat.SelectedValue + ")\r\n");
            switch (this.ddlImageFormat.SelectedValue)
            {
                case ".JPG":
                    sb.Append("    FormatEncoderParams outputFormat = new JpegFormatEncoderParams();\r\n");
                    break;
                case ".GIF":
                    sb.Append("    FormatEncoderParams outputFormat = new GifFormatEncoderParams();\r\n");
                    break;
                case ".PNG":
                    sb.Append("    FormatEncoderParams outputFormat = new PngFormatEncoderParams();\r\n");
                    break;
            }
        }
        else
        {
            sb.Append("    // Get the output image format (same format as the source image)\r\n");
            sb.Append("    FormatEncoderParams outputFormat = ImageArchiver.GetFormatEncoderParamsFromFilePath(sourceFilePath);\r\n");
        }

        sb.Append("\r\n");
        sb.Append("    string outputFileName = sourceFileNameWithoutExtension + outputFormat.FileExtension;\r\n");
        sb.Append("    string outputFilePath = System.IO.Path.Combine(outputImagesFolder, outputFileName);\r\n");

        sb.Append("\r\n");
        sb.Append("    // Process the image\r\n");
        sb.Append("    job.SaveProcessedImageToFileSystem(sourceFilePath, outputFilePath);\r\n");
        sb.Append("}\r\n");

        this.litCode.Text = sb.ToString();
    }

}
