<%@ WebHandler Language="C#" Class="Preview" %>
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
using System.Web;
using System.Drawing;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Serialization;
using CodeCarvings.Piczard.Helpers;
using CodeCarvings.Piczard.Filters.Colors;

public class Preview : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string data = context.Request["data"];
        if (string.IsNullOrEmpty(data))
        {
            // Invalid data
            context.Response.ContentType = "text/plain";
            context.Response.Write("Invalid data");
            context.Response.End();
            return;
        }

        // Parse the JSON data
        JSONObject oData = JSONObject.Decode(data);

        // Get an image processing job to apply the required filters
        ImageProcessingJob ipj = new ImageProcessingJob();
        
        // Apply the transformations
        ImageTransformation transformation = new ImageTransformation();
        transformation.RotationAngle = oData.GetNumberSingleValue("rotationAngle").Value;
        transformation.FlipH = oData.GetBoolValue("flipH").Value;
        transformation.FlipV = oData.GetBoolValue("flipV").Value;
        ipj.Filters.Add(transformation);
        
        // Apply the image adjustments
        ImageAdjustmentsFilter adjustments = new ImageAdjustmentsFilter();
        adjustments.Brightness = oData.GetNumberSingleValue("brightness").Value;
        adjustments.Contrast = oData.GetNumberSingleValue("contrast").Value;
        adjustments.Hue = oData.GetNumberSingleValue("hue").Value;
        adjustments.Saturation = oData.GetNumberSingleValue("saturation").Value;
        ipj.Filters.Add(adjustments);

        // Process the image and trasmit it to the browser
        string sourceImageFilePath = SecurityHelper.DecryptString(oData.GetStringValue("image"));
        ipj.TransmitProcessedImageToWebResponse(sourceImageFilePath, context.Response, new JpegFormatEncoderParams(92));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}