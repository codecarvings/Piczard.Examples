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

public partial class examples_example_A_502_editRecord : System.Web.UI.Page
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

            // Setup the Picture1 CropConstraint and Preview 
            this.Picture1.CropConstraint = new FixedCropConstraint(350, 350);
            this.Picture1.PreviewFilter = new FixedResizeConstraint(100, 100);

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
                        command.CommandText = "SELECT [Title] FROM [Ex_A_502] WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Id", this.RecordId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Record found

                                // Get the title
                                this.txtTitle.Text = Convert.ToString(reader["Title"]);

                                string picture1FileName = string.Format("{0}.jpg", this.RecordId);
                                string picture1FilePath_main = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/main/"), picture1FileName);
                                if (System.IO.File.Exists(picture1FilePath_main))
                                {
                                    // Image exists... Load the picture1 main image
                                    this.Picture1.LoadImageFromFileSystem(picture1FilePath_main);
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

    protected void fvPicture1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Validate the Picture1 control (must contain a value)
        args.IsValid = this.Picture1.HasImage;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Save the record

        if (!this.IsValid)
        {
            return;
        }

        #region Delete the previous image files
        if (this.RecordId != 0)
        {
            // UPDATE...
               
            if (!this.Picture1.HasImage)
            {
                // Image removed -> Delete the old files

                // Get the picture file name
                string picture1FileName = string.Format("{0}.jpg", this.RecordId);

                // Delete the main image
                string picture1FilePath_main = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/main/"), picture1FileName);
                if (System.IO.File.Exists(picture1FilePath_main))
                {
                    System.IO.File.Delete(picture1FilePath_main);
                }

                // Delete the thumbnail
                string picture1FilePath_thumbnail = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/thumbnail/"), picture1FileName);
                if (System.IO.File.Exists(picture1FilePath_thumbnail))
                {
                    System.IO.File.Delete(picture1FilePath_thumbnail);
                }
            }
        }
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
                    command.CommandText = "UPDATE [Ex_A_502] SET [Title]=@Title WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Title", this.txtTitle.Text);
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
                    command.CommandText = "INSERT INTO [Ex_A_502] ([Title]) VALUES (@Title)";
                    command.Parameters.AddWithValue("@Title", this.txtTitle.Text);

                    command.ExecuteNonQuery();
                }

                // Get the new record id
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT @@IDENTITY";

                    this.RecordId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
        #endregion

        #region Save the new image files (using the record id as file name)
        if (this.Picture1.HasNewImage)
        {
            // Get the picture file name
            string picture1FileName = string.Format("{0}.jpg", this.RecordId);

            // Save the main image
            string picture1FilePath_main = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/main/"), picture1FileName);
            this.Picture1.SaveProcessedImageToFileSystem(picture1FilePath_main);

            // Genereate the thumbnail image (Resize -> Fixed size: 48x48 Pixel, Jpeg 80% quality)
            string picture1FilePath_thumbnail = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/thumbnail/"), picture1FileName);
            ImageProcessingJob job = this.Picture1.GetImageProcessingJob();
            job.Filters.Add(new FixedResizeConstraint(48, 48));
            job.SaveProcessedImageToFileSystem(this.Picture1.TemporarySourceImageFilePath, picture1FilePath_thumbnail, new JpegFormatEncoderParams(80));
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
