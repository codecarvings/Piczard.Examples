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
using System.Data.OleDb;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Serialization;

public partial class examples_example_A_504_editRecord : System.Web.UI.Page
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

            // Setup the Picture1 preview filter
            ScaledResizeConstraint previewFilter = new ScaledResizeConstraint(300, 200);
            previewFilter.EnlargeSmallImages = false;
            this.Picture1.PreviewFilter = previewFilter;

            #region Load the Record
            if (this.RecordId != 0)
            {
                // UPDATE
                this.labelRecordId.Text = this.RecordId.ToString();

                // Load the database data
                using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
                {
                    using (OleDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "SELECT [Title], [Picture1_file_main] FROM [Ex_A_504] WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Id", this.RecordId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Record found

                                // Get the title
                                this.txtTitle.Text = Convert.ToString(reader["Title"]);

                                // Get the picture1 main image bytes
                                byte[] picture1_file_main = (byte[])(reader["Picture1_file_main"]);
                                if (picture1_file_main.Length > 0)
                                {
                                    // Load the image into the SimpleImageUpload ASCX control
                                    this.Picture1.LoadImageFromByteArray(picture1_file_main);
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

    #region Event handlers

    protected void Picture1_ImageUpload(object sender, SimpleImageUpload.ImageUploadEventArgs args)
    {
        this.MyLogEvent("New image uploaded.");

        if ((this.Picture1.SourceImageSize.Width < 100) || (this.Picture1.SourceImageSize.Height < 150))
        {
            // The uploaded image is too small
            this.Picture1.UnloadImage();
            this.Picture1.SetCurrentStatusMessage("<span style=\"color:#cc0000;\">The uploaded Image is too small.</span>");
            return;
        }

        if ((this.Picture1.SourceImageSize.Width > 1500) || (this.Picture1.SourceImageSize.Height > 1600))
        {
            // The uploaded image is too large
            this.Picture1.UnloadImage();
            this.Picture1.SetCurrentStatusMessage("<span style=\"color:#cc0000;\">The uploaded Image is too large.</span>");
            return;
        }
    }

    protected void Picture1_UploadError(object sender, EventArgs e)
    {
        this.MyLogEvent("Upload error.");
    }

    protected void Picture1_ImageEdit(object sender, EventArgs e)
    {
        this.MyLogEvent("Image edited.");
    }

    protected void Picture1_ImageRemove(object sender, EventArgs e)
    {
        this.MyLogEvent("Image removed.");
    }

    #endregion

    protected void MyLogEvent(string message)
    {
        string newEvent = DateTime.Now.ToString("s") + " - " + message + "\r\n";
        this.txtMyLog.Text = newEvent + this.txtMyLog.Text;
    }

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

        #region Prepare the byte arrays to store in the DB

        byte[] picture1_file_main = new byte[0];
        byte[] picture1_file_thumbnail = new byte[0];

        if (this.Picture1.HasNewImage)
        {
            // New image to save

            // Ensure that the temporary file exists
            if (File.Exists(this.Picture1.TemporarySourceImageFilePath))
            {
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

            using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
            {
                // Update the record
                using (OleDbCommand command = connection.CreateCommand())
                {
                    if ((!this.Picture1.HasImage) || (this.Picture1.HasNewImage))
                    {
                        // ### IMAGE NOT SELECTED -OR- NEW IMAGE
                        command.CommandText = "UPDATE [Ex_A_504] SET [Title]=@Title, [Picture1_file_main]=@Picture1_file_main, [Picture1_file_thumbnail]=@Picture1_file_thumbnail WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                        // Store the files
                        command.Parameters.AddWithValue("@Picture1_file_main", picture1_file_main);
                        command.Parameters.AddWithValue("@Picture1_file_thumbnail", picture1_file_thumbnail);

                        command.Parameters.AddWithValue("@Id", this.RecordId);
                    }
                    else
                    {
                        // ### IMAGE NOT UPDATED
                        command.CommandText = "UPDATE [Ex_A_504] SET [Title]=@Title WHERE [Id]=@Id";
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

            using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
            {
                // Insert the new record
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Ex_A_504] ([Title], [Picture1_file_main], [Picture1_file_thumbnail]) VALUES (@Title, @Picture1_file_main, @Picture1_file_thumbnail)";
                    command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                    // Store the files
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
