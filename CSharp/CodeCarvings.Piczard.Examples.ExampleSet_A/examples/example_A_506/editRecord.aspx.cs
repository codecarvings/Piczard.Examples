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
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;
using CodeCarvings.Piczard.Serialization;

public partial class examples_example_A_506_editRecord : System.Web.UI.Page
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

            // Setup the size of the preview image displayed in the Picture1 control (fixed size: 200x200 pixel)
            this.Picture1.PreviewFilter = new FixedResizeConstraint(200, 200);

            this.Picture1.Text_ConfigurationLabel = "<strong>Image orientation:&nbsp;</strong>";
            this.Picture1.Configurations = new string[] {"Landscape", "Portrait"};

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
                        command.CommandText = "SELECT [Title], [Picture1_Configuration], [Picture1_PictureTrimmerValue], [Picture1_FileName_upload], [Picture1_FileName_main], [Picture1_FileName_thumbnail] FROM [Ex_A_506] WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Id", this.RecordId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Record found

                                // Get the title
                                this.txtTitle.Text = Convert.ToString(reader["Title"]);

                                // Get the configuration index (0=landscape, 1=portrait) 
                                // and apply the right crop constrait depending on the configuration
                                this.Picture1.SelectedConfigurationIndex = Convert.ToInt32(reader["Picture1_Configuration"]);
                                this.Picture1.CropConstraint = this.GetCropConstraint(this.Picture1.SelectedConfigurationIndex.Value);

                                // Get the PictureTrimmer previous state
                                PictureTrimmerValue picture1Value = PictureTrimmerValue.FromJSON(Convert.ToString(reader["Picture1_PictureTrimmerValue"]));

                                // Get the picture1 file names
                                this.Picture1FileName_upload = Convert.ToString(reader["Picture1_FileName_upload"]);
                                this.Picture1FileName_main = Convert.ToString(reader["Picture1_FileName_main"]);
                                this.Picture1FileName_thumbnail = Convert.ToString(reader["Picture1_FileName_thumbnail"]);

                                if (!string.IsNullOrEmpty(this.Picture1FileName_upload))
                                {
                                    // Load the image into the SimpleImageUpload ASCX control
                                    string picture1FilePath_upload = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/upload/"), this.Picture1FileName_upload);
                                    this.Picture1.LoadImageFromFileSystem(picture1FilePath_upload, picture1Value);
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

    #region Picture1 events

    protected void Picture1_ImageUpload(object sender, SimpleImageUpload.ImageUploadEventArgs args)
    {
        // Auto-Apply the right crop constraint depending on the source image size
        if (this.Picture1.SourceImageSize.Width > this.Picture1.SourceImageSize.Height)
        {
            // Landscape -> Configuration = 0
            this.Picture1.SelectedConfigurationIndex = 0;
        }
        else
        {
            // Portrait -> Configuration = 1
            this.Picture1.SelectedConfigurationIndex = 1;
        }
        args.CropConstraint = this.GetCropConstraint(this.Picture1.SelectedConfigurationIndex.Value);
    }

    protected void Picture1_SelectedConfigurationIndexChanged(object sender, SimpleImageUpload.SelectedConfigurationIndexChangedEventArgs args)
    {
        // Apply the new crop constraint
        args.CropConstraint = this.GetCropConstraint(this.Picture1.SelectedConfigurationIndex.Value);
    }

    #endregion

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

    protected string Picture1FileName_upload
    {
        get
        {
            if (this.ViewState["Picture1FileName_upload"] != null)
            {
                return (string)this.ViewState["Picture1FileName_upload"];
            }
            else
            {
                return "";
            }
        }
        set
        {
            this.ViewState["Picture1FileName_upload"] = value;
        }
    }

    protected string Picture1FileName_main
    {
        get
        {
            if (this.ViewState["Picture1FileName_main"] != null)
            {
                return (string)this.ViewState["Picture1FileName_main"];
            }
            else
            {
                return "";
            }
        }
        set
        {
            this.ViewState["Picture1FileName_main"] = value;
        }
    }

    protected string Picture1FileName_thumbnail
    {
        get
        {
            if (this.ViewState["Picture1FileName_thumbnail"] != null)
            {
                return (string)this.ViewState["Picture1FileName_thumbnail"];
            }
            else
            {
                return "";
            }
        }
        set
        {
            this.ViewState["Picture1FileName_thumbnail"] = value;
        }
    }

    #endregion

    private CropConstraint GetCropConstraint(int configuration)
    {
        FixedCropConstraint result;
        if (configuration == 0)
        {
            // Landscape crop constraint
            result = new FixedCropConstraint(300, 200);
        }
        else
        {
            // Portrait crop constraint
            result = new FixedCropConstraint(200, 300);
        }
        return result;
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

        #region Manage the image files

        #region Delete the previous image files
        if (this.RecordId != 0)
        {
            // UPDATE...
               
            if ((!this.Picture1.HasImage) || (this.Picture1.HasNewImage))
            {
                // Delete the previous image
                if (!string.IsNullOrEmpty(this.Picture1FileName_upload))
                {
                    // Delete the uploaded image
                    string picture1FilePath_upload = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/upload/"), this.Picture1FileName_upload);
                    if (System.IO.File.Exists(picture1FilePath_upload))
                    {
                        System.IO.File.Delete(picture1FilePath_upload);
                    }
                    this.Picture1FileName_upload = "";

                    // Delete the main image
                    string picture1FilePath_main = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/main/"), this.Picture1FileName_main);
                    if (System.IO.File.Exists(picture1FilePath_main))
                    {
                        System.IO.File.Delete(picture1FilePath_main);
                    }
                    this.Picture1FileName_main = "";

                    // Delete the thumbnail
                    string picture1FilePath_thumbnail = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/thumbnail/"), this.Picture1FileName_thumbnail);
                    if (System.IO.File.Exists(picture1FilePath_thumbnail))
                    {
                        System.IO.File.Delete(picture1FilePath_thumbnail);
                    }
                    this.Picture1FileName_thumbnail = "";

                }
            }
        }
        #endregion

        #region Save the new image
        if (this.Picture1.HasNewImage)
        {
            // Save the uploaded image           
            string picture1folderPath_upload = Server.MapPath("~/repository/store/ex_A_506/picture1/upload/");
            this.Picture1FileName_upload = CodeCarvings.Piczard.Helpers.IOHelper.GetUniqueFileName(picture1folderPath_upload, this.Picture1.SourceImageClientFileName);
            string picture1FilePath_upload = System.IO.Path.Combine(picture1folderPath_upload, this.Picture1FileName_upload);
            System.IO.File.Copy(this.Picture1.TemporarySourceImageFilePath, picture1FilePath_upload, true);

            // Generate the main image            
            string picture1folderPath_main = Server.MapPath("~/repository/store/ex_A_506/picture1/main/");
            // Get the original file name (but always use the .jpg extension)
            this.Picture1FileName_main = CodeCarvings.Piczard.Helpers.IOHelper.GetUniqueFileName(picture1folderPath_main, System.IO.Path.GetFileNameWithoutExtension(this.Picture1.SourceImageClientFileName) + ImageArchiver.GetFileExtensionFromImageFormatId(System.Drawing.Imaging.ImageFormat.Jpeg.Guid));
            string picture1FilePath_main = System.IO.Path.Combine(picture1folderPath_main, this.Picture1FileName_main);
            this.Picture1.SaveProcessedImageToFileSystem(picture1FilePath_main);

            // Genereate the thumbnail image (Resize -> Fixed size: 48x48 Pixel, Jpeg 80% quality)
            string picture1folderPath_thumbnail = Server.MapPath("~/repository/store/ex_A_506/picture1/thumbnail/");
            // Get the original file name (but always use the .jpg extension)
            this.Picture1FileName_thumbnail = CodeCarvings.Piczard.Helpers.IOHelper.GetUniqueFileName(picture1folderPath_thumbnail, System.IO.Path.GetFileNameWithoutExtension(this.Picture1.SourceImageClientFileName) + ImageArchiver.GetFileExtensionFromImageFormatId(System.Drawing.Imaging.ImageFormat.Jpeg.Guid));
            string picture1FilePath_thumbnail = System.IO.Path.Combine(picture1folderPath_thumbnail, this.Picture1FileName_thumbnail);
            ImageProcessingJob job = this.Picture1.GetImageProcessingJob();
            job.Filters.Add(new FixedResizeConstraint(48, 48));
            job.SaveProcessedImageToFileSystem(this.Picture1.TemporarySourceImageFilePath, picture1FilePath_thumbnail, new JpegFormatEncoderParams(80));
        }
        #endregion

        #endregion

        #region Save the Record into the DB
        if (this.RecordId != 0)
        {
            // UPDATE...

            using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
            {
                // Update the record
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Ex_A_506] SET [Title]=@Title, [Picture1_Configuration]=@Picture1_Configuration, [Picture1_PictureTrimmerValue]=@Picture1_PictureTrimmerValue, [Picture1_FileName_upload]=@Picture1_FileName_upload, [Picture1_FileName_main]=@Picture1_FileName_main, [Picture1_FileName_thumbnail]=@Picture1_FileName_thumbnail WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                    // Store the selected configuration index (landscape / portrait)
                    command.Parameters.AddWithValue("@Picture1_Configuration", this.Picture1.SelectedConfigurationIndex);

                    // Store the picture trimmer vale
                    command.Parameters.AddWithValue("@Picture1_PictureTrimmerValue", JSONSerializer.SerializeToString(this.Picture1.Value));

                    // Store the file names
                    command.Parameters.AddWithValue("@Picture1_FileName_upload", this.Picture1FileName_upload);
                    command.Parameters.AddWithValue("@Picture1_FileName_main", this.Picture1FileName_main);
                    command.Parameters.AddWithValue("@Picture1_FileName_thumbnail", this.Picture1FileName_thumbnail);

                    command.Parameters.AddWithValue("@Id", this.RecordId);

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
                    command.CommandText = "INSERT INTO [Ex_A_506] ([Title], [Picture1_Configuration], [Picture1_PictureTrimmerValue], [Picture1_FileName_upload], [Picture1_FileName_main], [Picture1_FileName_thumbnail]) VALUES (@Title, @Picture1_Configuration, @Picture1_PictureTrimmerValue, @Picture1_FileName_upload, @Picture1_FileName_main, @Picture1_FileName_thumbnail)";
                    command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                    // Store the selected configuration index (landscape / portrait)
                    command.Parameters.AddWithValue("@Picture1_Configuration", this.Picture1.SelectedConfigurationIndex);

                    // Store the picture trimmer vale
                    command.Parameters.AddWithValue("@Picture1_PictureTrimmerValue", JSONSerializer.SerializeToString(this.Picture1.Value));

                    // Store the file names
                    command.Parameters.AddWithValue("@Picture1_FileName_upload", this.Picture1FileName_upload);
                    command.Parameters.AddWithValue("@Picture1_FileName_main", this.Picture1FileName_main);
                    command.Parameters.AddWithValue("@Picture1_FileName_thumbnail", this.Picture1FileName_thumbnail);

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
