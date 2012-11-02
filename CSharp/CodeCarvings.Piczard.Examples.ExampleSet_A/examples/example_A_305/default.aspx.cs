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
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_305_default : System.Web.UI.Page
{

    // IMPORTANT NOTE ***
    // In order to completely disable the creation of temporary files you have to set the attribute
    // useTemporaryFiles to false in the Web.Config file (codeCarvings.piczard/webSettings/pictureTrimmer)
    // *********

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI();", true);
        }

        if (!this.IsPostBack)
        {
            // Display the pictures
            this.displayOutputImage1();
            this.displayOutputImage2();
            this.displayOutputImage3();
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
    }

    protected const int RecordID1 = 1;
    protected const int RecordID2 = 2;
    protected const int RecordID3 = 3;

    #region Image stored in the file system

    protected void lbEdit1_Click(object sender, EventArgs e)
    {
        this.EditImage1();
    }

    protected void btnEdit1_Click(object sender, EventArgs e)
    {
        this.EditImage1();
    }

    protected void EditImage1()
    {
        // Load the image in the control (the image file is stored in the file system)
        this.popupPictureTrimmer1.LoadImageFromFileSystem(string.Format("~/repository/store/ex_A_305/source/{0}.jpg", RecordID1), new FixedCropConstraint(320, 180));

        // Load the value (stored in the DB)
        using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
        {
            using (OleDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [PictureTrimmerValue] FROM [Ex_A_305] WHERE [Id]=@Id";
                command.Parameters.AddWithValue("@Id", RecordID1);
                this.popupPictureTrimmer1.Value = PictureTrimmerValue.FromJSON((string)command.ExecuteScalar());
            }
        }

        // Open the image edit popup
        this.popupPictureTrimmer1.OpenPopup(800, 510);
    }

    protected void popupPictureTrimmer1_PopupClose(object sender, PictureTrimmerPopupCloseEventArgs e)
    {
        if (e.SaveChanges)
        {
            // User clicked the "Ok" button

            // Save the cropped image in the file system
            this.popupPictureTrimmer1.SaveProcessedImageToFileSystem(string.Format("~/repository/store/ex_A_305/output/{0}.jpg", RecordID1));

            // Save the PictureTrimmer value in the DB
            using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
            {
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Ex_A_305] SET [PictureTrimmerValue]=@PictureTrimmerValue  WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(this.popupPictureTrimmer1.Value));
                    command.Parameters.AddWithValue("@Id", RecordID1);
                    command.ExecuteNonQuery();
                }
            }

            // Display the new output image
            this.displayOutputImage1();
        }

        // Unload the image from the control
        this.popupPictureTrimmer1.UnloadImage();
    }

    protected void displayOutputImage1()
    {
        // Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        this.img1.ImageUrl = string.Format("~/repository/store/ex_A_305/output/{0}.jpg?timestamp={1}", RecordID1, DateTime.UtcNow.Ticks);
    }

    #endregion

    #region Image stored in a DB

    private static string GetImageUrl_Output(int id)
    {
        // Add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        return "ImageFromDB_Output.ashx?id=" + id.ToString() + "&timestamp=" + DateTime.UtcNow.Ticks.ToString();
    }

    private static string GetImageUrl_Content(int id, Color imageBackColorValue, PictureTrimmerImageBackColorApplyMode imageBackColorApplyMode)
    {
        // Add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        string result = "ImageFromDB_Content.ashx?id=" + id.ToString() + "&imageBackColorValue=" + HttpUtility.UrlEncode(CodeCarvings.Piczard.Helpers.StringConversionHelper.ColorToString(imageBackColorValue, true));
        result += "&imageBackColorApplyMode=" + HttpUtility.UrlEncode(((int)imageBackColorApplyMode).ToString()) +  "&timestamp=" + DateTime.UtcNow.Ticks.ToString();

        return result;
    }

    #region With temporary files

    protected void lbEdit2_Click(object sender, EventArgs e)
    {
        this.EditImage2();
    }

    protected void btnEdit2_Click(object sender, EventArgs e)
    {
        this.EditImage2();
    }

    protected void EditImage2()
    {
        // Load the image and the value from the DB
        using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
        {
            using (OleDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [SourceImage],[PictureTrimmerValue] FROM [Ex_A_305] WHERE [Id]=@Id";
                command.Parameters.AddWithValue("@Id", RecordID2);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    byte[] imageBytes = (byte[])reader["SourceImage"];
                    this.popupPictureTrimmer2.LoadImageFromByteArray(imageBytes, new FixedCropConstraint(320, 180));
                    this.popupPictureTrimmer2.Value = PictureTrimmerValue.FromJSON((string)reader["PictureTrimmerValue"]);
                }
            }
        }

        // Open the image edit popup
        this.popupPictureTrimmer2.OpenPopup(800, 510);
    }

    protected void popupPictureTrimmer2_PopupClose(object sender, PictureTrimmerPopupCloseEventArgs e)
    {
        if (e.SaveChanges)
        {
            // User clicked the "Ok" button

            // Save the PictureTrimmer value and the output image in the DB
            using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
            {
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Ex_A_305] SET [PictureTrimmerValue]=@PictureTrimmerValue, [OutputImage]=@OutputImage  WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(this.popupPictureTrimmer2.Value));
                    byte[] outputImageBytes = this.popupPictureTrimmer2.SaveProcessedImageToByteArray(new JpegFormatEncoderParams());
                    command.Parameters.AddWithValue("@OutputImage", outputImageBytes);
                    command.Parameters.AddWithValue("@Id", RecordID2);
                    command.ExecuteNonQuery();
                }
            }

            // Display the new output image
            this.displayOutputImage2();
        }

        // Unload the image from the control
        this.popupPictureTrimmer2.UnloadImage();
    }

    protected void displayOutputImage2()
    {
        this.img2.ImageUrl = GetImageUrl_Output(RecordID2);
    }

    #endregion

    #region Without temporary files

    protected void lbEdit3_Click(object sender, EventArgs e)
    {
        this.EditImage3();
    }

    protected void btnEdit3_Click(object sender, EventArgs e)
    {
        this.EditImage3();
    }

    protected void EditImage3()
    {
        // Load the image and the value from the DB
        using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
        {
            using (OleDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [SourceImage],[PictureTrimmerValue] FROM [Ex_A_305] WHERE [Id]=@Id";
                command.Parameters.AddWithValue("@Id", RecordID3);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    byte[] imageBytes = (byte[])reader["SourceImage"];
                    string contentImageUrl = GetImageUrl_Content(RecordID3, this.popupPictureTrimmer3.ImageBackColor.Value, this.popupPictureTrimmer3.ImageBackColorApplyMode);
                    this.popupPictureTrimmer3.LoadImageFromByteArray(imageBytes, contentImageUrl, new FixedCropConstraint(320, 180));
                    
                    // NOTE:
                    // The content image URL can be setted also after image load.
                    // This is useful to generate dynamic content images.
                    // Example:
                    // this.popupPictureTrimmer1.ContentImageUrl = "...";
                    
                    this.popupPictureTrimmer3.Value = PictureTrimmerValue.FromJSON((string)reader["PictureTrimmerValue"]);
                }
            }
        }

        // Open the image edit popup
        this.popupPictureTrimmer3.OpenPopup(800, 510);
    }

    protected void popupPictureTrimmer3_PopupClose(object sender, PictureTrimmerPopupCloseEventArgs e)
    {
        if (e.SaveChanges)
        {
            // User clicked the "Ok" button

            using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
            {
                // Load the source image (this operation is required when temporary files are disabled!)
                byte[] sourceImageBytes;
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [SourceImage] FROM [Ex_A_305] WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", RecordID3);
                    sourceImageBytes = (byte[]) command.ExecuteScalar();
                }

                // Save the PictureTrimmer value and the output image in the DB
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Ex_A_305] SET [PictureTrimmerValue]=@PictureTrimmerValue, [OutputImage]=@OutputImage  WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(this.popupPictureTrimmer3.Value));
                    byte[] outputImageBytes = this.popupPictureTrimmer3.SaveProcessedImageToByteArray(sourceImageBytes, new JpegFormatEncoderParams());
                    command.Parameters.AddWithValue("@OutputImage", outputImageBytes);
                    command.Parameters.AddWithValue("@Id", RecordID3);
                    command.ExecuteNonQuery();
                }
            }

            // Display the new output image
            this.displayOutputImage3();
        }

        // Unload the image from the control
        this.popupPictureTrimmer3.UnloadImage();
    }

    protected void displayOutputImage3()
    {
        this.img3.ImageUrl = GetImageUrl_Output(RecordID3);
    }

    #endregion

    #endregion

}
