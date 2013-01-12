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

public partial class examples_example_A_103_default : System.Web.UI.Page
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
        this.ddlGifMaxColors.Enabled = this.cbGifQuantize.Checked;
        this.ddlPngMaxColors.Enabled = this.cbPngConvertToIndex.Checked;
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        // Save the image and display the source code
        this.SaveImage();
        this.GenerateExampleCode();
    }

    protected void SaveImage()
    {
        string savedImageFileName = "";
        FormatEncoderParams formatEncoderParams = null;

        object source = null;
        switch (this.ddlSourceType.SelectedIndex)
        {
            case 0:
                // File system
                source = this.imgLoaded.ImageUrl;
                break;
            case 1:
                // Byte array
                source = System.IO.File.ReadAllBytes(Server.MapPath(this.imgLoaded.ImageUrl));
                break;
            case 2:
                // Stream
                source = System.IO.File.OpenRead(Server.MapPath(this.imgLoaded.ImageUrl));
                // Note: The stream will be automatically closed/disposed by the LoadedImage class
                break;
        }
        using (LoadedImage loadedImage = ImageArchiver.LoadImage(source))
        {           
            switch(this.ddlImageFormat.SelectedValue)
            {
                case "Auto":
                    formatEncoderParams = ImageArchiver.GetDefaultFormatEncoderParams();
                    break;
                case "JPEG":
                    formatEncoderParams = new JpegFormatEncoderParams(int.Parse(this.txtJpegQuality.Text));
                    break;
                case "GIF":
                    GifFormatEncoderParams gifFormatEncoderParams = new GifFormatEncoderParams();
                    formatEncoderParams = gifFormatEncoderParams;
                    gifFormatEncoderParams.QuantizeImage = this.cbGifQuantize.Checked;
                    if (this.cbGifQuantize.Checked)
                    {
                        gifFormatEncoderParams.MaxColors = int.Parse(this.ddlGifMaxColors.SelectedValue);
                    }
                    break;
                case "PNG":
                    PngFormatEncoderParams pngformatEncoderParams = new PngFormatEncoderParams();
                    formatEncoderParams = pngformatEncoderParams;
                    pngformatEncoderParams.ConvertToIndexed = this.cbPngConvertToIndex.Checked;
                    if (this.cbPngConvertToIndex.Checked)
                    {
                        pngformatEncoderParams.MaxColors = int.Parse(this.ddlPngMaxColors.SelectedValue);
                    }
                    break;
            }

            savedImageFileName = "~/repository/output/Ex_A_103" + formatEncoderParams.FileExtension;

            // IMPORTANT NOTE:  Apply a Noop (No Operation) filter to prevent a known GDI+ problem with transparent images
            // Example: http://forums.asp.net/t/1235100.aspx
            switch (this.ddlOutputType.SelectedIndex)
            {
                case 0:
                    // File system
                    new NoopFilter().SaveProcessedImageToFileSystem(loadedImage.Image, savedImageFileName, formatEncoderParams);
                    break;
                case 1:
                    // Byte array
                    byte[] imageBytes = new NoopFilter().SaveProcessedImageToByteArray(loadedImage.Image, formatEncoderParams);
                    System.IO.File.WriteAllBytes(Server.MapPath(savedImageFileName), imageBytes);
                    break;
                case 2:
                    // Stream
                    using (System.IO.Stream stream = System.IO.File.OpenWrite(Server.MapPath(savedImageFileName)))
                    {
                        new NoopFilter().SaveProcessedImageToStream(loadedImage.Image, stream, formatEncoderParams);
                    }
                    break;
            }
        }

        // Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        this.imgSaved.ImageUrl = savedImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();
        this.phOutputPreview.Visible = true;

        // Display extension and size of both files
        this.litLoadedImageFileDetails.Text = System.IO.Path.GetExtension(this.imgLoaded.ImageUrl).ToUpper() + " file (" + (new System.IO.FileInfo(Server.MapPath(this.imgLoaded.ImageUrl))).Length.ToString() + " bytes)";
        this.litSavedImageFileDetails.Text = System.IO.Path.GetExtension(savedImageFileName).ToUpper() + " file (" + (new System.IO.FileInfo(Server.MapPath(savedImageFileName))).Length.ToString() + " bytes)";
    }

    protected void GenerateExampleCode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("// Load the image\r\n");

        switch (this.ddlSourceType.SelectedIndex)
        {
            case 0:
                // File system
                sb.Append("string source = Server.MapPath(\"~/repository/source/flowers2.png\");\r\n");
                break;
            case 1:
                // Byte array
                sb.Append("byte[] source = System.IO.File.ReadAllBytes(Server.MapPath(\"~/repository/source/flowers2.png\"));\r\n");
                break;
            case 2:
                // Stream
                sb.Append("System.IO.Stream source = System.IO.File.OpenRead(Server.MapPath(\"~/repository/source/flowers2.png\"));\r\n");
                sb.Append("// Note: The stream will be automatically closed/disposed by the LoadedImage class\r\n");
                break;
        }

        sb.Append("using (LoadedImage loadedImage = ImageArchiver.LoadImage(source))\r\n");
        sb.Append("{\r\n");

        if (this.ddlImageFormat.SelectedValue != "Auto")
        {
            sb.Append("    // Setup the format params\r\n");
        }

        string fileExtension = "";
        switch (this.ddlImageFormat.SelectedValue)
        {
            case "Auto":
                fileExtension = ".jpg";
                break;
            case "JPEG":
                sb.Append("    JpegFormatEncoderParams formatEncoderParams = new JpegFormatEncoderParams(" + this.txtJpegQuality.Text + ");\r\n");
                fileExtension = ".jpg";
                break;
            case "GIF":
                sb.Append("    GifFormatEncoderParams formatEncoderParams = new GifFormatEncoderParams();\r\n");
                sb.Append("    formatEncoderParams.QuantizeImage = " + (this.cbGifQuantize.Checked ? "true" : "false") + ";\r\n");
                if (this.cbGifQuantize.Checked)
                {
                    sb.Append("    formatEncoderParams.MaxColors = " + this.ddlGifMaxColors.SelectedValue + ";\r\n");
                }
                fileExtension = ".gif";
                break;
            case "PNG":
                sb.Append("    PngFormatEncoderParams formatEncoderParams = new PngFormatEncoderParams();\r\n");
                sb.Append("    formatEncoderParams.ConvertToIndexed = " + (this.cbPngConvertToIndex.Checked ? "true" : "false") + ";\r\n");
                if (this.cbPngConvertToIndex.Checked)
                {
                    sb.Append("    formatEncoderParams.MaxColors = " + this.ddlPngMaxColors.SelectedValue + ";\r\n");
                }
                fileExtension = ".png";
                break;
        }

        if (this.ddlImageFormat.SelectedValue != "Auto")
        {
            sb.Append("\r\n");
        }

        sb.Append("    // Save the image\r\n");
        sb.Append("    string outputFilePath = Server.MapPath(\"~/repository/output/Ex_A_103" + fileExtension + "\");\r\n");

        switch (this.ddlOutputType.SelectedIndex)
        {
            case 0:
                // File system
                sb.Append("    ImageArchiver.SaveImageToFileSystem(loadedImage.Image, outputFilePath" + (this.ddlImageFormat.SelectedValue != "Auto" ? ", formatEncoderParams" : "") + ");\r\n");    
                break;
            case 1:
                // Byte array
                sb.Append("    byte[] imageBytes = ImageArchiver.SaveImageToByteArray(loadedImage.Image" + (this.ddlImageFormat.SelectedValue != "Auto" ? ", formatEncoderParams" : "") + ");\r\n");
                sb.Append("    System.IO.File.WriteAllBytes(outputFilePath, imageBytes);\r\n");
                break;
            case 2:
                // Stream
                sb.Append("    using (System.IO.Stream stream = System.IO.File.OpenWrite(outputFilePath))\r\n");
                sb.Append("    {\r\n");
                sb.Append("        ImageArchiver.SaveImageToStream(loadedImage.Image, stream" + (this.ddlImageFormat.SelectedValue != "Auto" ? ", formatEncoderParams" : "") + ");\r\n");
                sb.Append("    }\r\n");
                break;
        }
                    
        sb.Append("}\r\n");

        this.litCode.Text = sb.ToString();
        this.phCodeContainer.Visible = true;
    }
}
