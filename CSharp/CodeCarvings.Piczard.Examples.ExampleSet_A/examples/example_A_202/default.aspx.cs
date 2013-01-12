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

using CodeCarvings.Piczard;

public partial class examples_example_A_202_default : System.Web.UI.Page
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
            // Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(this);
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
        this.phOutputContainer.Visible = false;
        this.phCodeContainer.Visible = false;

        this.phResizeMode_Fixed.Visible = false;
        this.phResizeMode_Scaled.Visible = false;
        this.UpdatePanel1.FindControl("phResizeMode_" + this.ddlResizeMode.SelectedValue).Visible = true;
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
        string outputImageFileName = "~/repository/output/Ex_A_202.jpg";

        // Setup the resize filter
        ResizeConstraint resizeFilter = null;
        switch (this.ddlResizeMode.SelectedValue)
        {
            case "Fixed":
                FixedResizeConstraint fixedResizeFilter = null;
                switch (this.ddlConstraints_Fixed.SelectedIndex)
                {
                    case 0:
                        // 100 x 100 Pixel
                        fixedResizeFilter = new FixedResizeConstraint(100, 100);
                        break;
                    case 1:
                        // 150 x 250 Pixel
                        fixedResizeFilter = new FixedResizeConstraint(150, 250);
                        break;
                    case 2:
                        // 580 x 500 Pixel
                        fixedResizeFilter = new FixedResizeConstraint(580, 500);
                        break;
                    case 3:
                        // 2 x 2 Inch
                        fixedResizeFilter = new FixedResizeConstraint(GfxUnit.Inch, 2F, 2F);
                        break;
                }

                // Default value = Fit
                if (this.ddlFixedImagePosition.SelectedValue != "Fit")
                {
                    fixedResizeFilter.ImagePosition = (FixedResizeConstraintImagePosition)Enum.Parse(typeof(FixedResizeConstraintImagePosition), this.ddlFixedImagePosition.SelectedValue);
                }
                
                // Default value = White
                Color canvasColor = System.Drawing.ColorTranslator.FromHtml(this.txtFixedCanvasColor.Text);
                if (canvasColor != Color.FromArgb(255, 255, 255, 255))
                {
                    fixedResizeFilter.CanvasColor = canvasColor;
                }

                // Default value = EnlargeSmallImages:true
                if (!this.cbFixedEnlargeSmallImages.Checked)
                {
                    fixedResizeFilter.EnlargeSmallImages = this.cbFixedEnlargeSmallImages.Checked;
                }
                resizeFilter = fixedResizeFilter;
                break;
            case "Scaled":
                ScaledResizeConstraint scaledResizeFilter = null;
                switch (this.ddlConstraints_Scaled.SelectedIndex)
                {
                    case 0:
                        // 100 x 100 Pixel
                        scaledResizeFilter = new ScaledResizeConstraint(100, 100);
                        break;
                    case 1:
                        // 150 x 250 Pixel
                        scaledResizeFilter = new ScaledResizeConstraint(150, 250);
                        break;
                    case 2:
                        // 580 x 500 Pixel
                        scaledResizeFilter = new ScaledResizeConstraint(580, 500);
                        break;
                    case 3:
                        // 2 x 2 Inch
                        scaledResizeFilter = new ScaledResizeConstraint(GfxUnit.Inch, 2F, 2F);
                        break;
                }

                // Default value = EnlargeSmallImages:true
                if (!this.cbScaledEnlargeSmallImages.Checked)
                {
                    scaledResizeFilter.EnlargeSmallImages = this.cbScaledEnlargeSmallImages.Checked;
                }

                resizeFilter = scaledResizeFilter;
                break;
        }

        // Process the image
        resizeFilter.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

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
        sbCode.Append("string sourceImage = \"~/repository/source/trevi1.jpg\";" + crlf);
        sbCode.Append("string outputImage = \"~/repository/output/Ex_A_202.jpg\";" + crlf);
        sbCode.Append(crlf);

        sbCode.Append("// Setup the resize filter" + crlf);

        switch (this.ddlResizeMode.SelectedValue)
        {
            case "Fixed":
                switch (this.ddlConstraints_Fixed.SelectedIndex)
                {
                    case 0:
                        // 100 x 100 Pixel
                        sbCode.Append("FixedResizeConstraint resizeFilter = new FixedResizeConstraint(100, 100);" + crlf);
                        break;
                    case 1:
                        // 150 x 250 Pixel
                        sbCode.Append("FixedResizeConstraint resizeFilter = new FixedResizeConstraint(150, 250);" + crlf);
                        break;
                    case 2:
                        // 580 x 500 Pixel
                        sbCode.Append("FixedResizeConstraint resizeFilter = new FixedResizeConstraint(580, 500);" + crlf);
                        break;
                    case 3:
                        // 2 x 2 Inch
                        sbCode.Append("FixedResizeConstraint resizeFilter = new FixedResizeConstraint(GfxUnit.Inch, 2F, 2F);" + crlf);
                        break;
                }

                // Default value = Fit
                if (this.ddlFixedImagePosition.SelectedValue != "Fit")
                {
                    sbCode.Append("resizeFilter.ImagePosition = FixedResizeConstraintImagePosition." + this.ddlFixedImagePosition.SelectedValue + ";" + crlf);
                }

                // Default color = White
                Color canvasColor = System.Drawing.ColorTranslator.FromHtml(this.txtFixedCanvasColor.Text);
                if (canvasColor != Color.FromArgb(255, 255, 255, 255))
                {
                    sbCode.Append("resizeFilter.CanvasColor = System.Drawing.ColorTranslator.FromHtml(\"" + this.txtFixedCanvasColor.Text + "\");" + crlf);
                }

                // Default value = EnlargeSmallImages:true
                if (!this.cbFixedEnlargeSmallImages.Checked)
                {
                    sbCode.Append("resizeFilter.EnlargeSmallImages = " + this.cbFixedEnlargeSmallImages.Checked.ToString(System.Globalization.CultureInfo.InvariantCulture).ToLower() + ";" + crlf);
                }
                break;
            case "Scaled":
                switch (this.ddlConstraints_Scaled.SelectedIndex)
                {
                    case 0:
                        // 100 x 100 Pixel
                        sbCode.Append("ScaledResizeConstraint resizeFilter = new ScaledResizeConstraint(100, 100);" + crlf);
                        break;
                    case 1:
                        // 150 x 250 Pixel
                        sbCode.Append("ScaledResizeConstraint resizeFilter = new ScaledResizeConstraint(150, 250);" + crlf);
                        break;
                    case 2:
                        // 580 x 500 Pixel
                        sbCode.Append("ScaledResizeConstraint resizeFilter = new ScaledResizeConstraint(580, 500);" + crlf);
                        break;
                    case 3:
                        // 2 x 2 Inch
                        sbCode.Append("ScaledResizeConstraint resizeFilter = new ScaledResizeConstraint(GfxUnit.Inch, 2F, 2F);" + crlf);
                        break;
                }

                // Default value = EnlargeSmallImages:true
                if (!this.cbScaledEnlargeSmallImages.Checked)
                {
                    sbCode.Append("resizeFilter.EnlargeSmallImages = " + this.cbScaledEnlargeSmallImages.Checked.ToString(System.Globalization.CultureInfo.InvariantCulture).ToLower() + ";" + crlf);
                }
                break;
        }

        sbCode.Append(crlf);
        sbCode.Append("// Process the image" + crlf);
        sbCode.Append("resizeFilter.SaveProcessedImageToFileSystem(sourceImage, outputImage);                " + crlf);

        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }
}
