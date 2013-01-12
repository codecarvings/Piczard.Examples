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
using System.Drawing;
using System.Globalization;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;

public partial class examples_example_A_224_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI();", true);
        }

        if (!this.IsPostBack)
        {
            this.PopulateDropdownLists();
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
        this.phOutputContainer.Visible = false;
        this.phCodeContainer.Visible = false;
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
        string outputImageFileName = "~/repository/output/Ex_A_224.jpg";

        ImageWatermark imageWatermark = null;
        switch (this.ddlImageSource.SelectedIndex)
        {
            case 0:
                // piczardWatermark1.png
                // In this demo the image is automatically loaded/disposed by the ImageWatermark class
                imageWatermark = new ImageWatermark("~/repository/watermark/piczardWatermark1.png");

                imageWatermark.ContentAlignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), this.ddlContentAlignment.SelectedValue);
                imageWatermark.Unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlMainUnit.SelectedValue);
                imageWatermark.ContentDisplacement = new Point(int.Parse(this.ddlContentDisplacementX.SelectedValue, CultureInfo.InvariantCulture), int.Parse(this.ddlContentDisplacementY.SelectedValue, CultureInfo.InvariantCulture));
                imageWatermark.Alpha = int.Parse(this.ddlAlpha.SelectedValue, CultureInfo.InvariantCulture);

                // Process the image
                imageWatermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

                break;
            case 1:
                // codeCarvingsWatermark1.gif
                // In this demo the image is manually loaded/disposed (useful when you need to apply the same
                // watermark to multiple images)
                using (LoadedImage image = ImageArchiver.LoadImage("~/repository/watermark/codeCarvingsWatermark1.gif"))
                {
                    imageWatermark = new ImageWatermark(image.Image);

                    imageWatermark.ContentAlignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), this.ddlContentAlignment.SelectedValue);
                    imageWatermark.Unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlMainUnit.SelectedValue);
                    imageWatermark.ContentDisplacement = new Point(int.Parse(this.ddlContentDisplacementX.SelectedValue, CultureInfo.InvariantCulture), int.Parse(this.ddlContentDisplacementY.SelectedValue, CultureInfo.InvariantCulture));
                    imageWatermark.Alpha = int.Parse(this.ddlAlpha.SelectedValue, CultureInfo.InvariantCulture);

                    // Process the image
                    imageWatermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);
                }
                break;
        }

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
        sbCode.Append("string sourceImage = \"~/repository/source/temple1.jpg\";" + crlf);
        sbCode.Append("string outputImage = \"~/repository/output/Ex_A_224.jpg\";" + crlf);

        switch (this.ddlImageSource.SelectedIndex)
        {
            case 0:
                // piczardWatermark1.png
                sbCode.Append("string wmImage = \"~/repository/watermark/piczardWatermark1.png\";" + crlf);
                sbCode.Append(crlf);

                sbCode.Append("// Setup the ImageWatermark" + crlf);
                sbCode.Append("ImageWatermark imageWatermark = new ImageWatermark(wmImage);" + crlf);

                if (this.ddlContentAlignment.SelectedValue != "MiddleCenter")
                {
                    // Default value = MiddleCenter
                    sbCode.Append("imageWatermark.ContentAlignment = ContentAlignment." + this.ddlContentAlignment.SelectedValue + ";" + crlf);
                }
                if (this.ddlMainUnit.SelectedValue != "Pixel")
                {
                    // Default value = Pixel
                    sbCode.Append("imageWatermark.Unit = GfxUnit." + this.ddlMainUnit.SelectedValue + ";" + crlf);
                }
                if ((this.ddlContentDisplacementX.SelectedValue != "0") || (this.ddlContentDisplacementY.SelectedValue != "0"))
                {
                    // Default value = 0:0
                    sbCode.Append("imageWatermark.ContentDisplacement = new Point(" + this.ddlContentDisplacementX.SelectedValue + ", " + this.ddlContentDisplacementY.SelectedValue + ");" + crlf);
                }
                if (this.ddlAlpha.SelectedValue != "100")
                {
                    // Default value = 100%
                    sbCode.Append("imageWatermark.Alpha = " + this.ddlAlpha.SelectedValue + ";" + crlf);
                }

                sbCode.Append(crlf);
                sbCode.Append("// Process the image" + crlf);
                sbCode.Append("imageWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);
                break;
            case 1:
                // codeCarvingsWatermark1.gif
                sbCode.Append("string wmImage = \"~/repository/watermark/codeCarvingsWatermark1.gif\";" + crlf);
                sbCode.Append(crlf);

                sbCode.Append("using (LoadedImage image = ImageArchiver.LoadImage(wmImage))" + crlf);
                sbCode.Append("{" + crlf);
                sbCode.Append("   // Setup the ImageWatermark" + crlf);
                sbCode.Append("   ImageWatermark imageWatermark = new ImageWatermark(image.Image);" + crlf);

                if (this.ddlContentAlignment.SelectedValue != "MiddleCenter")
                {
                    // Default value = MiddleCenter
                    sbCode.Append("   imageWatermark.ContentAlignment = ContentAlignment." + this.ddlContentAlignment.SelectedValue + ";" + crlf);
                }
                if (this.ddlMainUnit.SelectedValue != "Pixel")
                {
                    // Default value = Pixel
                    sbCode.Append("   imageWatermark.Unit = GfxUnit." + this.ddlMainUnit.SelectedValue + ";" + crlf);
                }
                if ((this.ddlContentDisplacementX.SelectedValue != "0") || (this.ddlContentDisplacementY.SelectedValue != "0"))
                {
                    // Default value = 0:0
                    sbCode.Append("   imageWatermark.ContentDisplacement = new Point(" + this.ddlContentDisplacementX.SelectedValue + ", " + this.ddlContentDisplacementY.SelectedValue + ");" + crlf);
                }
                if (this.ddlAlpha.SelectedValue != "100")
                {
                    // Default value = 100%
                    sbCode.Append("   imageWatermark.Alpha = " + this.ddlAlpha.SelectedValue + ";" + crlf);
                }

                sbCode.Append(crlf);
                sbCode.Append("   // Process the image" + crlf);
                sbCode.Append("   imageWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);
                
                sbCode.Append("}" + crlf);
                break;
        }
          
        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }

    protected void PopulateDropdownLists()
    {
        for (int i = -30; i <= 30; i++)
        {
            ListItem item = new ListItem("X: " + i.ToString(), i.ToString());
            this.ddlContentDisplacementX.Items.Add(item);

            if (i == 0)
            {
                // Default value = 0
                item.Selected = true;
            }
        }
        for (int i = -30; i <= 30; i++)
        {
            ListItem item = new ListItem("Y: " + i.ToString(), i.ToString());
            this.ddlContentDisplacementY.Items.Add(item);

            if (i == 0)
            {
                // Default value = 0
                item.Selected = true;
            }
        }

        for (int i = 0; i <= 100; i++)
        {
            ListItem item = new ListItem(i.ToString() + "%", i.ToString());
            this.ddlAlpha.Items.Add(item);

            if (i == 100)
            {
                // Default value = 100
                item.Selected = true;
            }
        }
    }

}
