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
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_403_default : System.Web.UI.Page
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
            // Load the custom CSS used by the PopupPictureTrimmer
            System.Web.UI.HtmlControls.HtmlLink lightBoxCustomCSS = new System.Web.UI.HtmlControls.HtmlLink();
            lightBoxCustomCSS.Href = this.ResolveUrl("CodeCarvings.Piczard.LightBox.Custom.css");
            lightBoxCustomCSS.Attributes.Add("rel", "stylesheet");
            lightBoxCustomCSS.Attributes.Add("type", "text/css");
            this.Header.FindControl("phHeader").Controls.Add(lightBoxCustomCSS);

            // Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(this);
        }

        if (!this.IsPostBack)
        {
            // Load the image
            CropConstraint cropConstraint = new FixedCropConstraint(100, 100);
            cropConstraint.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy.DoNotResize;

            this.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers2.png", cropConstraint);
            this.PopupPictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers2.png", cropConstraint);
        }

        // Configure the PictureTrimmer instances
        this.SetPictureTrimmerConfiguration(this.InlinePictureTrimmer1);
        this.SetPictureTrimmerConfiguration(this.PopupPictureTrimmer1);
        this.PopupPictureTrimmer1.LightBoxCssClass = this.ddlPopupLightBoxCssClass.SelectedValue;

        this.cbShowResizePanel.Enabled = this.cbAllowResize.Checked;

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
    }

    protected void SetPictureTrimmerConfiguration(PictureTrimmer pictureTrimmer)
    {
        pictureTrimmer.ShowDetailsPanel = this.cbShowDetailsPanel.Checked;
        pictureTrimmer.ShowZoomPanel = this.cbShowZoomPanel.Checked;
        pictureTrimmer.AllowResize = this.cbAllowResize.Checked;
        pictureTrimmer.ShowResizePanel = this.cbShowResizePanel.Checked;
        pictureTrimmer.ShowRotatePanel = this.cbShowRotatePanel.Checked;
        pictureTrimmer.ShowFlipPanel = this.cbShowFlipPanel.Checked;
        pictureTrimmer.ShowImageAdjustmentsPanel = this.cbShowImageAdjustmentsPanel.Checked;
        pictureTrimmer.ShowCropAlignmentLines = this.cbShowCropAlignmentLines.Checked;
        pictureTrimmer.EnableSnapping = this.cbEnableSnapping.Checked;
        pictureTrimmer.ShowRulers = this.cbShowRulers.Checked;
        pictureTrimmer.UIUnit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlUIUnit.SelectedValue);
        pictureTrimmer.BackColor = System.Drawing.ColorTranslator.FromHtml(this.txtBackColor.Text);
        pictureTrimmer.ForeColor = System.Drawing.ColorTranslator.FromHtml(this.txtForeColor.Text);
        pictureTrimmer.CanvasColor.Value = System.Drawing.ColorTranslator.FromHtml(this.txtCanvasColor.Text);
        pictureTrimmer.ImageBackColor.Value = System.Drawing.ColorTranslator.FromHtml(this.txtImageBackColor.Text);
        pictureTrimmer.CropShadowMode = (PictureTrimmerCropShadowMode)Enum.Parse(typeof(PictureTrimmerCropShadowMode), this.ddlCropShadowMode.SelectedValue);
    }

}
