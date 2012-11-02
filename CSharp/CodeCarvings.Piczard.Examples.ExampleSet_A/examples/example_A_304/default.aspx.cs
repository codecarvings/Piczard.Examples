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
using System.Drawing;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Helpers;
using CodeCarvings.Piczard.Filters.Colors;

public partial class examples_example_A_304_default : System.Web.UI.Page
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
        if (this.cbAutoUpdateUserState.Checked)
        {
            // Auto update the user state
            this.InlinePictureTrimmer1.OnClientUserStateChangedFunction = "InlinePictureTrimmer1_onUserStateChanged";
        }
        else
        {
            this.InlinePictureTrimmer1.OnClientUserStateChangedFunction = "";
        }

        if (!this.IsPostBack)
        {
            this.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", new FreeCropConstraint());
        }
    }

    protected void btnServerSideGetUserState_Click(object sender, EventArgs e)
    {
        this.DisplayUserState();
    }

    protected void btnServerSideSetUserState_Click(object sender, EventArgs e)
    {
        // Set some random values
        Random rnd = new Random();

        PictureTrimmerUserState userState = this.InlinePictureTrimmer1.UserState;

        // Set the resize factor
        userState.Value.ImageSelection.Transformation.ResizeFactor = rnd.Next(50, 150);

        // Horizontal flip
        userState.Value.ImageSelection.Transformation.FlipH = rnd.Next(0, 4) < 2;

        // Set the rectangle
        int x = rnd.Next(-100, 300);
        int y = rnd.Next(-100, 300);
        int w = rnd.Next(50, 400);
        int h = rnd.Next(50, 400);
        userState.Value.ImageSelection.Crop.Rectangle = new Rectangle(x, y, w, h);

        // Set the Saturation
        userState.Value.ImageAdjustments.Saturation = rnd.Next(-100, 100);

        // Auto-zoom and auto-center the view
        userState.UIParams.ZoomFactor = null;
        userState.UIParams.PictureScrollH = null;
        userState.UIParams.PictureScrollV = null;

        this.DisplayUserState();
    }

    protected void DisplayUserState()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string crlf = "\r\n";

        #region UserState
        PictureTrimmerUserState userState = this.InlinePictureTrimmer1.UserState;

        #region Value
        PictureTrimmerValue value = userState.Value;

        #region ImageSelection
        ImageSelection imageSelection = value.ImageSelection;

        #region ImageTransformation
        ImageTransformation imageTransformation = imageSelection.Transformation;

        sb.Length = 0;
        sb.Append("Resize factor:" + imageTransformation.ResizeFactor.ToString(CultureInfo.InvariantCulture) + "%" + crlf);
        sb.Append("Rotation angle:" + imageTransformation.RotationAngle.ToString(CultureInfo.InvariantCulture) + "°" + crlf);
        sb.Append("Flip horizontal:" + (imageTransformation.FlipH ? "yes" : "no") + crlf);
        sb.Append("Flip vertical:" + (imageTransformation.FlipV ? "yes" : "no") + crlf);
        this.txtUserState_Value_ImageSelection_Transformation.Text = sb.ToString();
        #endregion

        #region ImageCrop
        ImageCrop imageCrop = imageSelection.Crop;

        sb.Length = 0;
        sb.Append("Rectangle:" + (imageCrop.Rectangle.HasValue ? imageCrop.Rectangle.Value.ToString() : "Auto") + crlf);
        // Note: in this example imageCrop.CanvasColor is not displayed.
        this.txtUserState_Value_ImageSelection_Crop.Text = sb.ToString();
        #endregion

        #endregion

        #region ImageAdjustments
        ImageAdjustmentsFilter imageAdjustments = value.ImageAdjustments;

        sb.Length = 0;
        sb.Append("Brightness:" + imageAdjustments.Brightness.ToString(CultureInfo.InvariantCulture) + crlf);
        sb.Append("Contrast:" + imageAdjustments.Contrast.ToString(CultureInfo.InvariantCulture) + crlf);
        sb.Append("Hue:" + imageAdjustments.Hue.ToString(CultureInfo.InvariantCulture) + "°" + crlf);
        sb.Append("Saturation:" + imageAdjustments.Saturation.ToString(CultureInfo.InvariantCulture) + crlf);
        this.txtUserState_Value_ImageAdjustments.Text = sb.ToString();
        #endregion

        // Note: In this example value.ImageBackColorApplyMode is not displayed.

        #endregion

        #region UIParams
        PictureTrimmerUIParams uiParams = userState.UIParams;

        sb.Length = 0;
        sb.Append("Zoom factor:" + (uiParams.ZoomFactor.HasValue ? uiParams.ZoomFactor.Value.ToString(CultureInfo.InvariantCulture) + "%" : "auto") + crlf);
        sb.Append("Picture scroll horizontal:" + (uiParams.PictureScrollH.HasValue ? uiParams.PictureScrollH.Value.ToString(CultureInfo.InvariantCulture) : "auto") + crlf);
        sb.Append("Picture scroll vertical:" + (uiParams.PictureScrollH.HasValue ? uiParams.PictureScrollV.Value.ToString(CultureInfo.InvariantCulture) : "auto") + crlf);
        sb.Append("Command panel scroll vertical:" + uiParams.CommandPanelScrollV.ToString(CultureInfo.InvariantCulture) + crlf);
        this.txtUserState_UIParams.Text = sb.ToString();

        #endregion

        #endregion
    }

}
