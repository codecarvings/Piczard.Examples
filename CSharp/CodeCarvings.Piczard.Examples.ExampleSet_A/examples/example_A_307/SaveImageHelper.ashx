<%@ WebHandler Language="C#" Class="SaveImageHelper" %>
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

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Serialization;

public class SaveImageHelper : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        // Load the picture trimmer core
        PictureTrimmerCore pictureTrimmer = PictureTrimmerCore.FromJSON(context.Request["coreJSONString"]);
        string outputImageFileName;
        bool imageProcessed = false;
        
        if (pictureTrimmer is PopupPictureTrimmerCore)
        {
            // *** POPUP PICTURETRIMMER ***
            outputImageFileName = "~/repository/output/Ex_A_307_2.jpg";
            bool saveChanges = bool.Parse(context.Request["saveChanges"]);

            if (saveChanges)
            {
                // Save the processed image
                pictureTrimmer.SaveProcessedImageToFileSystem(outputImageFileName);
                imageProcessed = true;
            }

            // Unload the image
            pictureTrimmer.UnloadImage();
        }
        else
        {
            // *** INLINE PICTURETRIMMER ***
            outputImageFileName = "~/repository/output/Ex_A_307_1.jpg";
            
            // Save the processed image
            pictureTrimmer.SaveProcessedImageToFileSystem(outputImageFileName);
            imageProcessed = true;
        }
        
        // Return a JSON string containing the url of the image
        context.Response.ContentType = "application/json";
        JSONObject result = new JSONObject();
        if (imageProcessed)
        {
            string imageUrl = VirtualPathUtility.ToAbsolute(outputImageFileName) + "?timestamp=" + DateTime.UtcNow.Ticks.ToString();
            result.SetStringValue("imageUrl", imageUrl);
        }
        context.Response.Write(result.Encode());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}