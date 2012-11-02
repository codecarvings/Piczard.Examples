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
using System.Globalization;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;

public partial class examples_example_A_223_default : System.Web.UI.Page
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

        if (!this.IsPostBack)
        {
            this.PopulateDropdownLists();
            this.txtText.Text = "Piczard - " + DateTime.Now.ToString();
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
        string outputImageFileName = "~/repository/output/Ex_A_223.jpg";

        TextWatermark textWatermark = new TextWatermark();
        textWatermark.Text = this.txtText.Text;
        textWatermark.ContentAlignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), this.ddlContentAlignment.SelectedValue);
        textWatermark.Unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlMainUnit.SelectedValue);
        textWatermark.ContentDisplacement = new Point(int.Parse(this.ddlContentDisplacementX.SelectedValue, CultureInfo.InvariantCulture), int.Parse(this.ddlContentDisplacementY.SelectedValue, CultureInfo.InvariantCulture));
        textWatermark.ForeColor = Color.FromArgb(int.Parse(this.ddlForeColorAlpha.SelectedValue, CultureInfo.InvariantCulture), ColorTranslator.FromHtml(this.txtForeColor.Text));
        textWatermark.Font.Name = this.ddlFontName.SelectedValue;
        textWatermark.Font.Size = float.Parse(this.ddlFontSize.SelectedValue, CultureInfo.InvariantCulture);
        textWatermark.Font.Bold = this.cbFontBold.Checked;
        textWatermark.Font.Italic = this.cbFontItalic.Checked;
        textWatermark.Font.Underline = this.cbFontUnderline.Checked;
        textWatermark.Font.Strikeout = this.cbFontStrikeout.Checked;
        textWatermark.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)Enum.Parse(typeof(System.Drawing.Text.TextRenderingHint), this.ddlTextRenderingHint.SelectedValue);
        textWatermark.TextContrast = int.Parse(this.ddlTextContrast.SelectedValue, CultureInfo.InvariantCulture);

        using (StringFormat stringFormat = new StringFormat())
        {
            // Setup the string format parameters
            if (this.cbStringFormatFlags_DirectionVertical.Checked)
            {
                stringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
            }

            textWatermark.StringFormat = stringFormat;

            // Process the image
            textWatermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);
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
        sbCode.Append("string outputImage = \"~/repository/output/Ex_A_223.jpg\";" + crlf);
        sbCode.Append(crlf);

        sbCode.Append("// Setup the TextWatermark" + crlf);
        sbCode.Append("TextWatermark textWatermark = new TextWatermark();" + crlf);
        sbCode.Append("textWatermark.Text = \"" + this.txtText.Text + "\";" + crlf);
        if (this.ddlContentAlignment.SelectedValue != "MiddleCenter")
        {
            // Default value = MiddleCenter
            sbCode.Append("textWatermark.ContentAlignment = ContentAlignment." + this.ddlContentAlignment.SelectedValue + ";" + crlf);
        }
        if (this.ddlMainUnit.SelectedValue != "Pixel")
        {
            // Default value = Pixel
            sbCode.Append("textWatermark.Unit = GfxUnit." + this.ddlMainUnit.SelectedValue + ";" + crlf);
        }
        if ((this.ddlContentDisplacementX.SelectedValue != "0") || (this.ddlContentDisplacementY.SelectedValue != "0"))
        {
            // Default value = 0:0
            sbCode.Append("textWatermark.ContentDisplacement = new Point(" + this.ddlContentDisplacementX.SelectedValue + ", " + this.ddlContentDisplacementY.SelectedValue + ");" + crlf);
        }
        if ((this.txtForeColor.Text != "#606060") || (this.ddlForeColorAlpha.SelectedValue != "255"))
        {
            // Default value = #606060 - Alpha:255
            sbCode.Append("textWatermark.ForeColor = Color.FromArgb(" + this.ddlForeColorAlpha.SelectedValue + ", ColorTranslator.FromHtml(\"" + this.txtForeColor.Text + "\"));" + crlf);
        }
        if ((this.ddlFontName.SelectedValue != "GenericSansSerif"))
        {
            // Default value = GenericSansSerif
            sbCode.Append("textWatermark.Font.Name = \"" + this.ddlFontName.SelectedValue + "\";" + crlf);
        }
        if (this.ddlFontSize.SelectedValue != "12")
        {
            // Default value = 12
            sbCode.Append("textWatermark.Font.Size = " + this.ddlFontSize.SelectedValue + "F;" + crlf);
        }
        if (this.cbFontBold.Checked)
        {
            // Default value = false
            sbCode.Append("textWatermark.Font.Bold = true;" + crlf);
        }
        if (this.cbFontItalic.Checked)
        {
            // Default value = false
            sbCode.Append("textWatermark.Font.Italic = true;" + crlf);
        }
        if (this.cbFontUnderline.Checked)
        {
            // Default value = false
            sbCode.Append("textWatermark.Font.Underline = true;" + crlf);
        }
        if (this.cbFontStrikeout.Checked)
        {
            // Default value = false
            sbCode.Append("textWatermark.Font.Strikeout = true;" + crlf);
        }
        if (this.ddlTextRenderingHint.SelectedValue != "ClearTypeGridFit")
        {
            // Default value = ClearTypeGridFit
            sbCode.Append("textWatermark.TextRenderingHint = System.Drawing.Text.TextRenderingHint." + this.ddlTextRenderingHint.SelectedValue + ";" + crlf);
        }
        if (this.ddlTextContrast.SelectedValue != "4")
        {
            // Default value = 4
            sbCode.Append("textWatermark.TextContrast = " + this.ddlTextContrast.SelectedValue + ";" + crlf);
        }

        if (this.cbStringFormatFlags_DirectionVertical.Checked)
        {
            // The StringFormat must be disposed
            sbCode.Append("using (StringFormat stringFormat = new StringFormat())" + crlf);
            sbCode.Append("{" + crlf);
            sbCode.Append("   // Vertical text" + crlf);
            sbCode.Append("   stringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;" + crlf);
            sbCode.Append("   textWatermark.StringFormat = stringFormat;" + crlf);
            sbCode.Append(crlf);
            sbCode.Append("   // Process the image" + crlf);
            sbCode.Append("   textWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);
            sbCode.Append("}" + crlf);
        }
        else
        {
            sbCode.Append(crlf);
            sbCode.Append("// Process the image" + crlf);
            sbCode.Append("textWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);
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

        for (int i = 0; i <= 255; i++)
        {
            ListItem item = new ListItem(i.ToString(), i.ToString());
            this.ddlForeColorAlpha.Items.Add(item);

            if (i == 255)
            {
                // Default value = 255
                item.Selected = true;
            }
        }

        for (int i = 6; i <= 32; i++)
        {
            ListItem item = new ListItem(i.ToString(), i.ToString());
            this.ddlFontSize.Items.Add(item);

            if (i == 12)
            {
                // Default value = 12
                item.Selected = true;
            }
        }

        for (int i = 0; i <= 12; i++)
        {
            ListItem item = new ListItem(i.ToString(), i.ToString());
            this.ddlTextContrast.Items.Add(item);

            if (i == 4)
            {
                // Default value = 4
                item.Selected = true;
            }
        }
    }
}
