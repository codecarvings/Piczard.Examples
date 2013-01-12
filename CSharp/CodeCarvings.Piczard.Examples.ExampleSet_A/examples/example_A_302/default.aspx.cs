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
using System.Globalization;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Helpers;

public partial class examples_example_A_302_default : System.Web.UI.Page
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

        this.phCropMode_Fixed.Visible = false;
        this.phCropMode_FixedAspectRatio.Visible = false;
        this.phCropMode_Free.Visible = false;
        this.UpdatePanel1.FindControl("phCropMode_" + this.ddlCropMode.SelectedValue).Visible = true;

        this.ddlConstraint_FixedAspectRatio_Min.Enabled = this.cb_FixedAspectRatio_Min.Checked;
        this.ddlConstraint_FixedAspectRatio_Max.Enabled = this.cb_FixedAspectRatio_Max.Checked;

        this.ddlConstraint_Free_Width_Min.Enabled = this.cb_Free_Width_Min.Checked;
        this.ddlConstraint_Free_Width_Max.Enabled = this.cb_Free_Width_Max.Checked;
        this.ddlConstraint_Free_Height_Min.Enabled = this.cb_Free_Height_Min.Checked;
        this.ddlConstraint_Free_Height_Max.Enabled = this.cb_Free_Height_Max.Checked;

        if (!this.IsPostBack)
        {
            this.PopulateDropdownLists();
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.PopulateDropdownLists();
    }

    protected void btnLoadImage_Click(object sender, EventArgs e)
    {
        // Update the Output resolution and the UI Unit
        GfxUnit unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlUnit.SelectedValue);
        this.InlinePictureTrimmer1.UIUnit = unit;

        float outputResolution = float.Parse(this.ddlDPI.SelectedValue, CultureInfo.InvariantCulture);
        
        CropConstraint cropConstrant = null;
        switch (this.ddlCropMode.SelectedValue)
        {
            case "Fixed":
                float fixedWidth = float.Parse(this.ddlConstraint_Fixed_Width.SelectedValue, CultureInfo.InvariantCulture);
                float fixedHeight = float.Parse(this.ddlConstraint_Fixed_Height.SelectedValue, CultureInfo.InvariantCulture);
                FixedCropConstraint fixedCropConstraint = new FixedCropConstraint(unit, fixedWidth, fixedHeight);
                cropConstrant = fixedCropConstraint;
                break;
            case "FixedAspectRatio":
                float aspectRatioX = float.Parse(this.ddlConstraint_FixedAspectRatio_X.SelectedValue, CultureInfo.InvariantCulture);
                float aspectRatioY = float.Parse(this.ddlConstraint_FixedAspectRatio_Y.SelectedValue, CultureInfo.InvariantCulture);
                float aspectRatio = aspectRatioX / aspectRatioY;
                FixedAspectRatioCropConstraint fixedAspectRatioCropConstraint = new FixedAspectRatioCropConstraint(aspectRatio);
                fixedAspectRatioCropConstraint.Unit = unit;

                if ((this.cb_FixedAspectRatio_Min.Checked) || (this.cb_FixedAspectRatio_Max.Checked))
                {                   
                    float minValue = float.Parse(this.ddlConstraint_FixedAspectRatio_Min.SelectedValue, CultureInfo.InvariantCulture);
                    float maxValue = float.Parse(this.ddlConstraint_FixedAspectRatio_Max.SelectedValue, CultureInfo.InvariantCulture);

                    // Ensure that Min value is not greater than Max value
                    if ((this.cb_FixedAspectRatio_Min.Checked) && (this.cb_FixedAspectRatio_Max.Checked))
                    {
                        if (maxValue < minValue)
                        {
                            // ERROR
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "FixedAspectRatioCropConstraintError1", "alert(\"Error: Min value cannot be greater than Max value.\");", true);
                            return;
                        }
                    }

                    fixedAspectRatioCropConstraint.LimitedDimension = (SizeDimension)Enum.Parse(typeof(SizeDimension), this.ddlConstraint_FixedAspectRatio_LimitedDimension.SelectedValue);
                    if (this.cb_FixedAspectRatio_Min.Checked)
                    {
                        fixedAspectRatioCropConstraint.Min = minValue;
                    }
                    if (this.cb_FixedAspectRatio_Max.Checked)
                    {
                        fixedAspectRatioCropConstraint.Max = maxValue;
                    }
                }

                cropConstrant = fixedAspectRatioCropConstraint;
                break;
            case "Free":
                FreeCropConstraint freeCropConstraint = new FreeCropConstraint();
                freeCropConstraint.Unit = unit;

                if ((this.cb_Free_Width_Min.Checked) || (this.cb_Free_Width_Max.Checked))
                {
                    float minWidth = float.Parse(this.ddlConstraint_Free_Width_Min.SelectedValue, CultureInfo.InvariantCulture);
                    float maxWidth = float.Parse(this.ddlConstraint_Free_Width_Max.SelectedValue, CultureInfo.InvariantCulture);

                    // Ensure that Min width is not greater than Max width
                    if ((this.cb_Free_Width_Min.Checked) && (this.cb_Free_Width_Max.Checked))
                    {
                        if (maxWidth < minWidth)
                        {
                            // ERROR
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "FreeCropConstraintError1", "alert(\"Error: Min width cannot be greater than Max width.\");", true);
                            return;
                        }
                    }

                    if (this.cb_Free_Width_Min.Checked)
                    {
                        freeCropConstraint.MinWidth = minWidth;
                    }
                    if (this.cb_Free_Width_Max.Checked)
                    {
                        freeCropConstraint.MaxWidth = maxWidth;
                    }
                }

                if ((this.cb_Free_Height_Min.Checked) || (this.cb_Free_Height_Max.Checked))
                {
                    float minHeight = float.Parse(this.ddlConstraint_Free_Height_Min.SelectedValue, CultureInfo.InvariantCulture);
                    float maxHeight = float.Parse(this.ddlConstraint_Free_Height_Max.SelectedValue, CultureInfo.InvariantCulture);

                    // Ensure that Min height is not greater than Max height
                    if ((this.cb_Free_Height_Min.Checked) && (this.cb_Free_Height_Max.Checked))
                    {
                        if (maxHeight < minHeight)
                        {
                            // ERROR
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "FreeCropConstraintError2", "alert(\"Error: Min height cannot be greater than Max height.\");", true);
                            return;
                        }
                    }

                    if (this.cb_Free_Height_Min.Checked)
                    {
                        freeCropConstraint.MinHeight = minHeight;
                    }
                    if (this.cb_Free_Height_Max.Checked)
                    {
                        freeCropConstraint.MaxHeight = maxHeight;
                    }
                }

                cropConstrant = freeCropConstraint;
                break;
        }

        // Setup the margins
        if (this.ddlMarginsH.SelectedValue == "")
        {
            // Horizontal margin = automatic
            cropConstrant.Margins.Horizontal = null;
        }
        else
        {
            // Hortizontal margin - custom value
            cropConstrant.Margins.Horizontal = float.Parse(this.ddlMarginsH.SelectedValue, CultureInfo.InvariantCulture);
        }
        if (this.ddlMarginsV.SelectedValue == "")
        {
            // Horizontal margin = automatic
            cropConstrant.Margins.Vertical = null;
        }
        else
        {
            // Hortizontal margin - custom value
            cropConstrant.Margins.Vertical = float.Parse(this.ddlMarginsV.SelectedValue, CultureInfo.InvariantCulture);
        }

        // Setup the DefaultImageSelectionStrategy
        cropConstrant.DefaultImageSelectionStrategy = (CropConstraintImageSelectionStrategy)Enum.Parse(typeof(CropConstraintImageSelectionStrategy), this.ddlImageSelectionStrategy.SelectedValue);

        // Load the image
        this.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", outputResolution, cropConstrant);

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

    #region Display the code
    protected void DisplayCode()
    {
        System.Text.StringBuilder sbCode = new System.Text.StringBuilder();
        string crlf = "\r\n";

        GfxUnit unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlUnit.SelectedValue);
        if (unit != GfxUnit.Pixel)
        {
            // Default value: Pixel
            sbCode.Append("InlinePictureTrimmer1.UIUnit = GfxUnit." + unit.ToString() + ";" + crlf);
        }

        switch (this.ddlCropMode.SelectedValue)
        {
            case "Fixed":
                if (unit == GfxUnit.Pixel)
                {
                    sbCode.Append("FixedCropConstraint cropConstrant = new FixedCropConstraint(" + this.ddlConstraint_Fixed_Width.SelectedValue + ", " + this.ddlConstraint_Fixed_Height.SelectedValue + ");" + crlf);
                }
                else
                {
                    sbCode.Append("FixedCropConstraint cropConstrant = new FixedCropConstraint(GfxUnit." + unit.ToString() + ", " + this.ddlConstraint_Fixed_Width.SelectedValue + "F, " + this.ddlConstraint_Fixed_Height.SelectedValue + "F);" + crlf);
                }
                break;
            case "FixedAspectRatio":
                sbCode.Append("FixedAspectRatioCropConstraint cropConstrant = new FixedAspectRatioCropConstraint(" + this.ddlConstraint_FixedAspectRatio_X.SelectedValue + "F/" + this.ddlConstraint_FixedAspectRatio_Y.SelectedValue + "F);" + crlf);
                if ((this.cb_FixedAspectRatio_Min.Checked) || (this.cb_FixedAspectRatio_Max.Checked))
                {
                    if (unit != GfxUnit.Pixel)
                    {
                        sbCode.Append("cropConstrant.Unit = GfxUnit." + unit.ToString() + ";" + crlf);
                    }

                    SizeDimension limitedDimension = (SizeDimension)Enum.Parse(typeof(SizeDimension), this.ddlConstraint_FixedAspectRatio_LimitedDimension.SelectedValue);
                    if (limitedDimension != SizeDimension.Width)
                    {
                        // Default value = Width
                        sbCode.Append("cropConstrant.LimitedDimension = SizeDimension." + limitedDimension.ToString() + ";" + crlf);
                    }

                    if (this.cb_FixedAspectRatio_Min.Checked)
                    {
                        sbCode.Append("cropConstrant.Min = " + this.ddlConstraint_FixedAspectRatio_Min.SelectedValue + "F;" + crlf);
                    }
                    if (this.cb_FixedAspectRatio_Max.Checked)
                    {
                        sbCode.Append("cropConstrant.Max = " + this.ddlConstraint_FixedAspectRatio_Max.SelectedValue + "F;" + crlf);
                    }
                }

                break;
            case "Free":
                sbCode.Append("FreeCropConstraint cropConstrant = new FreeCropConstraint();" + crlf);

                if ((this.cb_Free_Width_Min.Checked) || (this.cb_Free_Width_Max.Checked) || (this.cb_Free_Height_Min.Checked) || (this.cb_Free_Height_Max.Checked))
                {
                    if (unit != GfxUnit.Pixel)
                    {
                        sbCode.Append("cropConstrant.Unit = GfxUnit." + unit.ToString() + ";" + crlf);
                    }

                    if (this.cb_Free_Width_Min.Checked)
                    {
                        sbCode.Append("cropConstrant.MinWidth = " + this.ddlConstraint_Free_Width_Min.SelectedValue + "F;" + crlf);
                    }
                    if (this.cb_Free_Width_Max.Checked)
                    {
                        sbCode.Append("cropConstrant.MaxWidth = " + this.ddlConstraint_Free_Width_Max.SelectedValue + "F;" + crlf);
                    }
                    if (this.cb_Free_Height_Min.Checked)
                    {
                        sbCode.Append("cropConstrant.MinHeight = " + this.ddlConstraint_Free_Height_Min.SelectedValue + "F;" + crlf);
                    }
                    if (this.cb_Free_Height_Max.Checked)
                    {
                        sbCode.Append("cropConstrant.MaxHeight = " + this.ddlConstraint_Free_Height_Max.SelectedValue + "F;" + crlf);
                    }
                }
                break;
        }

        if (this.ddlMarginsH.SelectedValue != "")
        {
            // Default value = Auto
            sbCode.Append("cropConstrant.Margins.Horizontal = " + this.ddlMarginsH.SelectedValue + "F;" + crlf);
        }
        if (this.ddlMarginsV.SelectedValue != "")
        {
            // Default value = Auto
            sbCode.Append("cropConstrant.Margins.Vertical = " + this.ddlMarginsV.SelectedValue + "F;" + crlf);
        }
                
        CropConstraintImageSelectionStrategy defaultImageSelectionStrategy = (CropConstraintImageSelectionStrategy)Enum.Parse(typeof(CropConstraintImageSelectionStrategy), this.ddlImageSelectionStrategy.SelectedValue);
        if (defaultImageSelectionStrategy != CropConstraintImageSelectionStrategy.Slice)
        {
            // Default value: Slice
            sbCode.Append("cropConstrant.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy." + defaultImageSelectionStrategy.ToString() + ";" + crlf);
        }

        if (this.ddlDPI.SelectedValue == "96")
        {
            // Default value = 96
            sbCode.Append("InlinePictureTrimmer1.LoadImageFromFileSystem(\"~/repository/source/donkey1.jpg\", cropConstrant);" + crlf);
        }
        else
        {
            sbCode.Append("InlinePictureTrimmer1.LoadImageFromFileSystem(\"~/repository/source/donkey1.jpg\", " + this.ddlDPI.SelectedValue + "F, cropConstrant);" + crlf);
        }

        this.phCodeContainer.Visible = true;
        this.litCode.Text = sbCode.ToString();
    }
    #endregion

    #region Populate the drop down lists
    protected void PopulateDropdownLists()
    {
        float minValue = 1F;
        float stepValue = 1f;
        int totSteps = 25;
        ListItem item;

        GfxUnit unit = (GfxUnit)Enum.Parse(typeof(GfxUnit), this.ddlUnit.SelectedValue);
        switch (unit)
        {
            case GfxUnit.Pixel:
                stepValue = 20F;
                break;
            case GfxUnit.Point:
                stepValue = 15F;
                break;
            case GfxUnit.Pica:
                stepValue = 1F;
                break;
            case GfxUnit.Inch:
                stepValue = 0.2F;
                break;
            case GfxUnit.Mm:
                stepValue = 5F;
                break;
            case GfxUnit.Cm:
                stepValue = 0.5F;
                break;
        }
        minValue = stepValue;

        this.ddlConstraint_Fixed_Width.Items.Clear();
        this.ddlConstraint_Fixed_Height.Items.Clear();
        for (int i = 0; i < totSteps; i++)
        {
            float value = minValue + Convert.ToSingle(i) * stepValue;
            string valueString = StringConversionHelper.SingleToString(value);

            item = new ListItem(valueString, valueString);
            if (i == 14)
            {
                item.Selected = true;
            }
            this.ddlConstraint_Fixed_Width.Items.Add(item);

            item = new ListItem(valueString, valueString);
            if (i == 9)
            {
                item.Selected = true;
            }
            this.ddlConstraint_Fixed_Height.Items.Add(item);
        }

        this.ddlConstraint_FixedAspectRatio_Min.Items.Clear();
        this.ddlConstraint_FixedAspectRatio_Max.Items.Clear();
        for (int i = 0; i < totSteps; i++)
        {
            float value = minValue + Convert.ToSingle(i) * stepValue;
            string valueString = StringConversionHelper.SingleToString(value);

            item = new ListItem(valueString, valueString);
            if (i == 0)
            {
                item.Selected = true;
            }
            this.ddlConstraint_FixedAspectRatio_Min.Items.Add(item);

            item = new ListItem(valueString, valueString);
            if (i == (totSteps - 1))
            {
                item.Selected = true;
            }
            this.ddlConstraint_FixedAspectRatio_Max.Items.Add(item);
        }

        this.ddlConstraint_Free_Width_Min.Items.Clear();
        this.ddlConstraint_Free_Width_Max.Items.Clear();
        this.ddlConstraint_Free_Height_Min.Items.Clear();
        this.ddlConstraint_Free_Height_Max.Items.Clear();
        for (int i = 0; i < totSteps; i++)
        {
            float value = minValue + Convert.ToSingle(i) * stepValue;
            string valueString = StringConversionHelper.SingleToString(value);

            item = new ListItem(valueString, valueString);
            if (i == 0)
            {
                item.Selected = true;
            }
            this.ddlConstraint_Free_Width_Min.Items.Add(item);

            item = new ListItem(valueString, valueString);
            if (i == (totSteps - 1))
            {
                item.Selected = true;
            }
            this.ddlConstraint_Free_Width_Max.Items.Add(item);

            item = new ListItem(valueString, valueString);
            if (i == 0)
            {
                item.Selected = true;
            }
            this.ddlConstraint_Free_Height_Min.Items.Add(item);

            item = new ListItem(valueString, valueString);
            if (i == (totSteps - 1))
            {
                item.Selected = true;
            }
            this.ddlConstraint_Free_Height_Max.Items.Add(item);
        }

        minValue = 0F;
        totSteps = 101;
        this.ddlMarginsH.Items.Clear();
        item = new ListItem("Auto", "");
        item.Selected = true;
        this.ddlMarginsH.Items.Add(item);
        this.ddlMarginsV.Items.Clear();
        item = new ListItem("Auto", "");
        item.Selected = true;
        this.ddlMarginsV.Items.Add(item);
        for (int i = 0; i < totSteps; i++)
        {
            float value = minValue + Convert.ToSingle(i) * stepValue;
            string valueString = StringConversionHelper.SingleToString(value);

            item = new ListItem(valueString, valueString);
            this.ddlMarginsH.Items.Add(item);
            item = new ListItem(valueString, valueString);
            this.ddlMarginsV.Items.Add(item);
        }

        if (this.ddlConstraint_FixedAspectRatio_X.Items.Count == 0)
        {
            totSteps = 100;
            for (int i = 1; i <= totSteps; i++)
            {
                string valueString = StringConversionHelper.Int32ToString(i);

                item = new ListItem(valueString, valueString);
                if (i == 16)
                {
                    item.Selected = true;
                }
                this.ddlConstraint_FixedAspectRatio_X.Items.Add(item);

                item = new ListItem(valueString, valueString);
                if (i == 9)
                {
                    item.Selected = true;
                }
                this.ddlConstraint_FixedAspectRatio_Y.Items.Add(item);
            }
        }
    }
    #endregion

}
