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
using System.Globalization;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Helpers;

public partial class examples_example_A_303_default : System.Web.UI.Page
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
    }

    protected void btnLoadImage_Click(object sender, EventArgs e)
    {
        // Update the Output resolution and the UI Unit
        GfxUnit unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlUnit.SelectedValue);
        this.InlinePictureTrimmer1.UIUnit = unit;
        
        // By default rotation is enabled
        this.InlinePictureTrimmer1.ShowRotatePanel = true;

        // Load the image
        this.InlinePictureTrimmer1.LoadImageFromFileSystem(Server.MapPath("~/repository/source/flowers1.jpg"));

        this.InlinePictureTrimmer1.ShowResizePanel = this.cbEnableResize.Checked;
        this.InlinePictureTrimmer1.ShowDetailsPanel = this.cbEnableResize.Checked;

        if (this.cbEnableResize.Checked)
        {
            // Resize enabled -> Disable the AutoZoom feature
            this.InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.Disabled;
        }
        else
        {
            // Resize disabled -> Force the AutoZoom
            this.InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.EnabledOnlyForLargeImages;
        }

        this.InlinePictureTrimmer1.Visible = true;
        this.phBeforeLoad.Visible = false;
        this.phAfterLoad.Visible = true;

        this.DisplayCode();
    }

    protected void btnUnloadImage_Click(object sender, EventArgs e)
    {
        this.InlinePictureTrimmer1.UnloadImage();
        this.InlinePictureTrimmer1.Visible = false;
        this.phCodeContainer.Visible = false;
        this.phBeforeLoad.Visible = true;
        this.phAfterLoad.Visible = false;
    }

    protected void DisplayCode()
    {
        System.Text.StringBuilder sbCode = new System.Text.StringBuilder();
        string crlf = "\r\n";

        sbCode.Append("InlinePictureTrimmer1.ShowZoomPanel = false;" + crlf);

        GfxUnit unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlUnit.SelectedValue);
        if (unit != GfxUnit.Pixel)
        {
            // Default value: Pixel
            sbCode.Append("InlinePictureTrimmer1.UIUnit = GfxUnit." + unit.ToString() + ";" + crlf);
        }

        if (this.cbEnableResize.Checked)
        {
            // Default = AutoZoomMode.Standard
            sbCode.Append("InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.Disabled;" + crlf);
        }
        else
        {
            // Default = true
            sbCode.Append("InlinePictureTrimmer1.ShowResizePanel = false;" + crlf);
            sbCode.Append("InlinePictureTrimmer1.ShowDetailsPanel = false;" + crlf);

            sbCode.Append("InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.EnabledOnlyForLargeImages;" + crlf);
        }

        sbCode.Append("InlinePictureTrimmer1.LoadImageFromFileSystem(Server.MapPath(\"~/repository/source/flowers1.jpg\"));" + crlf);

        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }

}
