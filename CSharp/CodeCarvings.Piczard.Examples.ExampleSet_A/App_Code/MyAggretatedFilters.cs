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
using System.Drawing;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Processing;
using CodeCarvings.Piczard.Filters.Colors;
using CodeCarvings.Piczard.Filters.Watermarks;

// Custom ImageProcessingFilter (Used by Example A.401)
// NOTE: This filter does not implement JSON serialziation
// (Please see the the "MyRotationFilter" class for a JSON serialization example)
[Serializable]
public class MyAggretatedFilters
    : ImageProcessingFilter
{

    #region Constructors

    public MyAggretatedFilters(string text)
	{
        this.Text = text;
    }

    public MyAggretatedFilters()
        : this(String.Empty)
    {
    }

    #endregion

    #region Overriedes

    protected override void LoadImageProcessingActions(ImageProcessingActionLoadArgs args)
    {
        // Filter #1
        args.LoadImageProcessingActions(new FixedCropConstraint(300, 200));

        // Filter #2
        TextWatermark watermark = new TextWatermark(this.Text, ContentAlignment.TopCenter);
        watermark.Font.Size = 12;
        watermark.ForeColor = Color.Black;
        args.LoadImageProcessingActions(watermark);

        // Filter #3
        args.LoadImageProcessingActions(DefaultColorFilters.Sepia);
    }

    protected override void Apply(ImageProcessingActionExecuteArgs args)
    {
        // This is only a container for multiple filters
        throw new Exception("Cannot invoke the Apply method.");
    }

    #endregion

    #region Propertites

    public string Text {get; set;}

    #endregion

}
