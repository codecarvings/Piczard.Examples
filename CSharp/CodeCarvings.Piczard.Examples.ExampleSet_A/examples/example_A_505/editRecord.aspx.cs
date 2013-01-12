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
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Serialization;

public partial class examples_example_A_505_editRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI();", true);
        }

        if (!this.IsPostBack)
        {
            // Get the record id passed as query parameter
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.RecordId = int.Parse(Request.QueryString["id"]);
            }

            // Setup the Picture1 Crop constraint and the preview filter
            this.Picture1.CropConstraint = new FixedCropConstraint(300, 300);
            this.Picture1.PreviewFilter = new FixedResizeConstraint(100, 100);

            #region Load the Record
            if (this.RecordId != 0)
            {
                // UPDATE
                this.labelRecordId.Text = this.RecordId.ToString();

                // Load the database data
                using (SqlConnection connection = ExamplesHelper.GetNewOpenDbConnection_SqlServer())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "SELECT [Title], [Picture1_pictureTrimmerValue], [Picture1_file_upload] FROM [Ex_A_505] WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Id", this.RecordId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Record found

                                // Get the title
                                this.txtTitle.Text = Convert.ToString(reader["Title"]);

                                // Get the picture1 PictureTrimmerValue
                                string picture1_pictureTrimmerValue = Convert.ToString(reader["Picture1_pictureTrimmerValue"]);
                                if (!string.IsNullOrEmpty(picture1_pictureTrimmerValue))
                                {
                                    PictureTrimmerValue pictureTrimmerValue = PictureTrimmerValue.FromJSON(picture1_pictureTrimmerValue);

                                    // Get the original file bytes
                                    byte[] picture1_file_upload = (byte[])(reader["Picture1_file_upload"]);
                                    if (picture1_file_upload.Length > 0)
                                    {
                                        // Load the image into the SimpleImageUpload ASCX control
                                        this.Picture1.LoadImageFromByteArray(picture1_file_upload, pictureTrimmerValue);
                                    }
                                }
                            }
                            else
                            {
                                // Record not found, return to list
                                this.ReturnToList();
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                // NEW RECORD
                this.labelRecordId.Text = "New record";
            }
            #endregion
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
    }

    #region Properties

    protected int RecordId
    {
        get
        {
            if (this.ViewState["RecordId"] != null)
            {
                return (int)this.ViewState["RecordId"];
            }
            else
            {
                return 0;
            }
        }
        set
        {
            this.ViewState["RecordId"] = value;
        }
    }

    #endregion

    protected void ReturnToList()
    {
        Response.Redirect("default.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Save the record

        if (!this.IsValid)
        {
            return;
        }

        #region Save the Record into the DB

        #region Prepare the byte arrays and the strings to store in the DB

        string picture1_pictureTrimmerValue = String.Empty;
        byte[] picture1_file_upload = new byte[0];
        byte[] picture1_file_main = new byte[0];
        byte[] picture1_file_thumbnail = new byte[0];

        if (this.Picture1.HasNewImage)
        {
            // New image to save

            // Ensure that the temporary file exists
            if (File.Exists(this.Picture1.TemporarySourceImageFilePath))
            {
                // Serialize the value
                picture1_pictureTrimmerValue = JSONSerializer.SerializeToString(this.Picture1.Value);

                // Load the original image uploaded by the user
                picture1_file_upload = File.ReadAllBytes(this.Picture1.TemporarySourceImageFilePath);

                // Load the main image
                picture1_file_main = this.Picture1.SaveProcessedImageToByteArray(new JpegFormatEncoderParams());

                // Generate the thumbnail
                ImageProcessingJob job = this.Picture1.GetImageProcessingJob();
                job.Filters.Add(new FixedResizeConstraint(48, 48));
                picture1_file_thumbnail = job.SaveProcessedImageToByteArray(this.Picture1.TemporarySourceImageFilePath, new JpegFormatEncoderParams(80));
            }
        }

        #endregion

        if (this.RecordId != 0)
        {
            // UPDATE...

            using (SqlConnection connection = ExamplesHelper.GetNewOpenDbConnection_SqlServer())
            {
                // Update the record
                using (SqlCommand command = connection.CreateCommand())
                {
                    if ((!this.Picture1.HasImage) || (this.Picture1.HasNewImage))
                    {
                        // ### IMAGE NOT SELECTED -OR- NEW IMAGE
                        command.CommandText = "UPDATE [Ex_A_505] SET [Title]=@Title, [Picture1_pictureTrimmerValue]=@Picture1_pictureTrimmerValue, [Picture1_file_upload]=@Picture1_file_upload, [Picture1_file_main]=@Picture1_file_main, [Picture1_file_thumbnail]=@Picture1_file_thumbnail WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                        // Store the picture trimmer vale
                        command.Parameters.AddWithValue("@Picture1_pictureTrimmerValue", picture1_pictureTrimmerValue);

                        // Store the files
                        command.Parameters.AddWithValue("@Picture1_file_upload", picture1_file_upload);
                        command.Parameters.AddWithValue("@Picture1_file_main", picture1_file_main);
                        command.Parameters.AddWithValue("@Picture1_file_thumbnail", picture1_file_thumbnail);

                        command.Parameters.AddWithValue("@Id", this.RecordId);
                    }
                    else
                    {
                        // ### IMAGE NOT UPDATED
                        command.CommandText = "UPDATE [Ex_A_505] SET [Title]=@Title WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                        command.Parameters.AddWithValue("@Id", this.RecordId);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        else
        {
            // INSERT...

            using (SqlConnection connection = ExamplesHelper.GetNewOpenDbConnection_SqlServer())
            {
                // Insert the new record
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Ex_A_505] ([Title], [Picture1_pictureTrimmerValue], [Picture1_file_upload], [Picture1_file_main], [Picture1_file_thumbnail]) VALUES (@Title, @Picture1_pictureTrimmerValue, @Picture1_file_upload, @Picture1_file_main, @Picture1_file_thumbnail)";
                    command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                    // Store the picture trimmer vale
                    command.Parameters.AddWithValue("@Picture1_pictureTrimmerValue", picture1_pictureTrimmerValue);

                    // Store the files
                    command.Parameters.AddWithValue("@Picture1_file_upload", picture1_file_upload);
                    command.Parameters.AddWithValue("@Picture1_file_main", picture1_file_main);
                    command.Parameters.AddWithValue("@Picture1_file_thumbnail", picture1_file_thumbnail);

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        // Clear the temporary files
        this.Picture1.ClearTemporaryFiles();

        this.ReturnToList();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Clear the temporary files
        this.Picture1.ClearTemporaryFiles();

        this.ReturnToList();
    }
}
