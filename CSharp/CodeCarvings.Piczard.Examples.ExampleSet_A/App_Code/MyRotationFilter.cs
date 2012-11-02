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
using System.Drawing;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Processing;
using CodeCarvings.Piczard.Helpers;
using CodeCarvings.Piczard.Serialization;

// Custom Rotation ImageProcessingFilter (Used by Example A.404)
// NOTE: This filter implements JSON serialziation
[Serializable]
public class MyRotationFilter
    : ImageProcessingFilter, IJSONSerializable
{

    #region Constructors

    public MyRotationFilter(float rotationAngle)
        : base()
    {
        this.RotationAngle = rotationAngle;
    }

    public MyRotationFilter()
        : this(0F)
    {
    }

    #endregion

    #region Overriedes

    protected override void Apply(ImageProcessingActionExecuteArgs args)
    {
        float normalizedRotationAngle = this._RotationAngle % 360F;
        if (normalizedRotationAngle == 0F)
        {
            // No need to rotate the image
            return;
        }

        Bitmap result = null;
        try
        {
            // Intial calculations
            double t;
            double t1;
            double b1;
            double t2;
            double b2;
            this.CalculateValues(args.Image.Size, out t, out t1, out b1, out t2, out b2);

            // Calculate the new image size
            Size outputImageSize = this.GetOutputImageSize(t1, b1, t2, b2);
           
            // Create the result image
            result = new Bitmap(outputImageSize.Width, outputImageSize.Height, CodeCarvings.Piczard.CommonData.DefaultPixelFormat);

            // Set the right image resolution (DPI)
            ImageHelper.SetImageResolution(result, args.ImageProcessingJob.OutputResolution);

            using (Graphics g = Graphics.FromImage(result))
            {
                // Use the max quality
                ImageHelper.SetGraphicsMaxQuality(g);

                if ((args.IsLastAction) && (!args.AppliedImageBackColorValue.HasValue))
                {
                    // Optimization (not mandatory)
                    // This is the last filter action and the ImageBackColor has not been yet applied...
                    // Apply the ImageBackColor now to save RAM & CPU
                    args.ApplyImageBackColor(g);
                }

                // Calculate the points for the DrawImage method
                Point[] shapePoints = new Point[3];

                if ((t >= 0D) && (t < (Math.PI / 2D)))
                {
                    shapePoints[0] = new Point(Convert.ToInt32(Math.Round(b2)), 0);
                    shapePoints[1] = new Point(outputImageSize.Width, Convert.ToInt32(Math.Round(t2)));
                    shapePoints[2] = new Point(0, Convert.ToInt32(Math.Round(b1)));
                }
                else if ((t >= (Math.PI / 2D)) && (t < Math.PI))
                {
                    shapePoints[0] = new Point(outputImageSize.Width, Convert.ToInt32(Math.Round(t2)));
                    shapePoints[1] = new Point(Convert.ToInt32(Math.Round(t1)), outputImageSize.Height);
                    shapePoints[2] = new Point(Convert.ToInt32(Math.Round(b2)), 0);
                }
                else if ((t >= Math.PI) && (t < (Math.PI + (Math.PI / 2D))))
                {
                    shapePoints[0] = new Point(Convert.ToInt32(Math.Round(t1)), outputImageSize.Height);
                    shapePoints[1] = new Point(0, Convert.ToInt32(Math.Round(b1)));
                    shapePoints[2] = new Point(outputImageSize.Width, Convert.ToInt32(Math.Round(t2)));
                }
                else
                {
                    shapePoints[0] = new Point(0, Convert.ToInt32(Math.Round(b1)));
                    shapePoints[1] = new Point(Convert.ToInt32(Math.Round(b2)), 0);
                    shapePoints[2] = new Point(Convert.ToInt32(Math.Round(t1)), outputImageSize.Height);
                }

                // Draw the image
                g.DrawImage(args.Image, shapePoints);
            }

            // Return the image
            args.Image = result;
        }
        catch
        {
            // An error has occurred...

            // Release the resources
            if (result != null)
            {
                result.Dispose();
                result = null;
            }

            // Re-throw the exception
            throw;
        }
    }

    #endregion

    #region Methods

    private void CalculateValues(Size imageSize, out double t, out double t1, out double b1, out double t2, out double b2)
    {
        // Convert image size to double
        double imageWidth = Convert.ToDouble(imageSize.Width);
        double imageHeight = Convert.ToDouble(imageSize.Height);

        // Convert the rotation angle
        t = Convert.ToDouble(this._RotationAngle) * Math.PI / 180D;
        while (t < 0D)
        {
            t += (Math.PI * 2D);
        }

        if (
            ((t >= 0D) && (t < (Math.PI / 2D)))
            ||
            ((t >= Math.PI) && (t < (Math.PI + (Math.PI / 2D)))))
        {
            t1 = Math.Abs(Math.Cos(t)) * imageWidth;
            b1 = Math.Abs(Math.Cos(t)) * imageHeight;

            t2 = Math.Abs(Math.Sin(t)) * imageWidth;
            b2 = Math.Abs(Math.Sin(t)) * imageHeight;
        }
        else
        {
            t1 = Math.Abs(Math.Sin(t)) * imageHeight;
            b1 = Math.Abs(Math.Sin(t)) * imageWidth;

            t2 = Math.Abs(Math.Cos(t)) * imageHeight;
            b2 = Math.Abs(Math.Cos(t)) * imageWidth;
        }
    }

    private Size GetOutputImageSize(double t1, double b1, double t2, double b2)
    {
        int width = Convert.ToInt32(Math.Ceiling(t1 + b2));
        int height = Convert.ToInt32(Math.Ceiling(b1 + t2));
        return new Size(width, height);
    }

    public Size GetOutputImageSize(Size imageSize)
    {
        double t;
        double t1;
        double b1;
        double t2;
        double b2;
        this.CalculateValues(imageSize, out t, out t1, out b1, out t2, out b2);

        return this.GetOutputImageSize(t1, b1, t2, b2);
    }

    #endregion

    #region Properties

    private float _RotationAngle;
    public float RotationAngle
    {
        get
        {
            return this._RotationAngle;
        }
        set
        {
            this._RotationAngle = value;
        }
    }

    #endregion

    #region Serialization

    int IJSONSerializable.SerializationVersion
    {
        get
        {
            // Default version = 1
            return 1;
        }
    }

    JSONSerializationException IJSONSerializable.GetSerializationException()
    {
        // No error
        return null;
    }

    JSONObject IJSONSerializable.ToJSONObject(JSONSerializationOptions options)
    {
        // Options cannot be null
        if (options == null)
        {
            options = JSONSerializationOptions.Default;
        }

        JSONObject result = options.GetNewJSONObject(this);
        result.SetNumberValue("rotationAngle", this._RotationAngle);

        return result;
    }

    private static MyRotationFilter FromJSON_1(JSONObject jsonObject)
    {
        if (jsonObject == null)
        {
            return null;
        }

        MyRotationFilter result = new MyRotationFilter();
        result.RotationAngle = jsonObject.GetNumberSingleValue("rotationAngle").Value;

        return result;
    }

    public static MyRotationFilter FromJSON(JSONObject jsonObject)
    {
        if (jsonObject == null)
        {
            return null;
        }

        int version = jsonObject.GetSerializationVersion();
        switch (version)
        {
            case 1:
                return FromJSON_1(jsonObject);
        }

        throw new JSONInvalidObjectException(typeof(MyRotationFilter));
    }

    public static MyRotationFilter FromJSON(string jsonString)
    {
        return FromJSON(JSONObject.Decode(jsonString));
    }

    #endregion

}
