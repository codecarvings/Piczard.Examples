<%@ WebHandler Language="C#" Class="ImageFromDB_Content" %>
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
using System.Data;
using System.Data.OleDb;
using System.Drawing;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;

public class ImageFromDB_Content : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        // Get the parameters
        int id = int.Parse(context.Request["id"]);
        Color imageBackColorValue = CodeCarvings.Piczard.Helpers.StringConversionHelper.StringToColor(context.Request["imageBackColorValue"]);
        PictureTrimmerImageBackColorApplyMode imageBackColorApplyMode = (PictureTrimmerImageBackColorApplyMode)int.Parse(context.Request["imageBackColorApplyMode"]);
        
        using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
        {
            using (OleDbCommand command = connection.CreateCommand())
            {
                // Load the image bytes
                command.CommandText = "SELECT [SourceImage] FROM [Ex_A_305] WHERE [Id]=@Id";
                
                command.Parameters.AddWithValue("@Id", id);
                byte[] buffer = (byte[])command.ExecuteScalar();

                // Generate and transmit the content image
                ImageProcessingJob ipj = PictureTrimmerCore.GetContentImageJob(imageBackColorValue, imageBackColorApplyMode);
                ipj.TransmitProcessedImageToWebResponse(buffer, context.Response, PictureTrimmerCore.GetContentImageFormatEncoderParams(imageBackColorApplyMode));
            }
        }
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

}