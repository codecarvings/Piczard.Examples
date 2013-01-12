<%@ WebHandler Language="C#" Class="ImageFromDB_Output" %>
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
using System.Data;
using System.Data.OleDb;

using CodeCarvings.Piczard;

public class ImageFromDB_Output : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        // Get the record id
        int id = int.Parse(context.Request["id"]);
        
        using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
        {
            using (OleDbCommand command = connection.CreateCommand())
            {
                // Load the image bytes
                command.CommandText = "SELECT [OutputImage] FROM [Ex_A_305] WHERE [Id]=@Id";
                
                command.Parameters.AddWithValue("@Id", id);
                byte[] buffer = (byte[])command.ExecuteScalar();
                
                // Write the image bytes
                context.Response.ContentType = ImageArchiver.GetMimeTypeFromImageFormatId(System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
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