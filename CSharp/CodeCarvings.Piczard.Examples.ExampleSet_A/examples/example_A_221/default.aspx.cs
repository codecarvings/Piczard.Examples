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

public partial class examples_example_A_221_default : System.Web.UI.Page
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
        this.DisplayCode();
    }

    protected void ProcessImage()
    {
        // Setup the source file name and the output file name
        string sourceImageFileName = this.imgSource.ImageUrl;
        string outputImageFileName = "~/repository/output/Ex_A_221.jpg";

        // Setup the image selection filter
        ImageTransformation imageTransformation = new ImageTransformation();
        imageTransformation.RotationAngle = float.Parse(this.ddlRotationAngle.SelectedValue, System.Globalization.CultureInfo.InvariantCulture);
        imageTransformation.FlipH = this.cbFlipH.Checked;
        imageTransformation.FlipV = this.cbFlipV.Checked;

        // Process the image
        imageTransformation.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName);

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
        sbCode.Append("string outputImage = \"~/repository/output/Ex_A_221.jpg\";" + crlf);
        sbCode.Append(crlf);

        sbCode.Append("// Setup the image transformation filter" + crlf);
        sbCode.Append("ImageTransformation imageTransformation = new ImageTransformation();" + crlf);
        if ((float.Parse(this.ddlRotationAngle.SelectedValue, System.Globalization.CultureInfo.InvariantCulture) != 0F)
            ||
            ((!this.cbFlipH.Checked) && (!this.cbFlipV.Checked)))
        {
            sbCode.Append("imageTransformation.RotationAngle = " + this.ddlRotationAngle.SelectedValue + "F;" + crlf);
        }
        if (this.cbFlipH.Checked)
        {
            sbCode.Append("imageTransformation.FlipH = true;" + crlf);
        }
        if (this.cbFlipV.Checked)
        {
            sbCode.Append("imageTransformation.FlipV = true;" + crlf);
        }

        sbCode.Append(crlf);
        sbCode.Append("// Process the image" + crlf);
        sbCode.Append("imageTransformation.SaveProcessedImageToFileSystem(sourceImage, outputImage);" + crlf);

        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }
}
