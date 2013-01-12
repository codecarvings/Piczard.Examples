<%@ WebHandler Language="C#" Class="LoadImageHelper" %>
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
using System.Web;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;

public class LoadImageHelper : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) 
    {
        // Load the picture trimmer core
        PictureTrimmerCore pictureTrimmer = PictureTrimmerCore.FromJSON(context.Request["coreJSONString"]);

        string imagePath = null;
        int imageToLoad = int.Parse(context.Request["imageToLoad"]);
        switch (imageToLoad)
        {
            case 0:
                imagePath = "~/repository/source/temple1.jpg";
                break;
            case 1:
                imagePath = "~/repository/source/flowers1.jpg";
                break;
            case 2:
                imagePath = "~/repository/source/donkey1.jpg";
                break;
        }

        // Load the image
        pictureTrimmer.LoadImageFromFileSystem(imagePath, new FreeCropConstraint(null, 500, null, 500));

        if (pictureTrimmer is PopupPictureTrimmerCore)
        {
            // Open the popup
            PopupPictureTrimmerCore popupPictureTrimmer = (PopupPictureTrimmerCore)pictureTrimmer;
            popupPictureTrimmer.OpenPopup();
        }
        
        // Return the JSON
        context.Response.ContentType = "application/json";
        context.Response.Write(pictureTrimmer.GetAttachDataJSON());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}