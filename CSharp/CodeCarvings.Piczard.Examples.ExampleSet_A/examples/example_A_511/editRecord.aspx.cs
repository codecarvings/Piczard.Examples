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

using CodeCarvings.Piczard;

public partial class examples_example_A_511_editRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            // Get the record id passed as query parameter
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.RecordId = int.Parse(Request.QueryString["id"]);
            }

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
                        command.CommandText = "SELECT [Title] FROM [Ex_A_511] WHERE [Id]=@Id";
                        command.Parameters.AddWithValue("@Id", this.RecordId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Record found

                                this.txtTitle.Text = Convert.ToString(reader["Title"]);
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

                // Picture 1 preview visible
                this.phPicture1Preview.Visible = true;
                this.imgPicture1Preview.ImageUrl = string.Format("~/repository/store/ex_A_511/picture1/main/{0}.jpg", this.RecordId);

                // Picture 1 not required (alrady exists an image...)
                // Hide the required field validator
                this.fvPicture1.Visible = false;
            }
            else
            {
                // NEW RECORD
                this.labelRecordId.Text = "New record";

                // Picture 1 preview not visible
                this.phPicture1Preview.Visible = false;

                // Picture 1 required
                // Show the required field validator
                this.fvPicture1.Visible = true;
            }
        }
    }

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
        if (this.RecordId != 0)
        {
            // UPDATE...

            using (OleDbConnection connection = ExamplesHelper.GetNewOpenDbConnection())
            {
                // Update the record
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Ex_A_511] SET [Title]=@Title WHERE [Id]=@Id";
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
                    command.CommandText = "INSERT INTO [Ex_A_511] ([Title]) VALUES (@Title)";
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

        // Save the picture 1 files
        // *** USE THE RECORD ID AS FILE NAME ***
        if (this.fuPicture1.HasFile)
        {
            // Get the source file bytes
            byte[] sourceFileBytes = this.fuPicture1.FileBytes;

            // Genereate the main image (Resize -> MaxSize: 400x250, Jpeg 92% quality)
            new ScaledResizeConstraint(250, 100).SaveProcessedImageToFileSystem(sourceFileBytes, string.Format("~/repository/store/ex_A_511/picture1/main/{0}.jpg", this.RecordId), new JpegFormatEncoderParams(92));

            // Genereate the thumbnail image (Resize -> Fixed size: 48x48 Pixel, Jpeg 80% quality)
            new FixedResizeConstraint(48, 48).SaveProcessedImageToFileSystem(sourceFileBytes, string.Format("~/repository/store/ex_A_511/picture1/thumbnail/{0}.jpg", this.RecordId), new JpegFormatEncoderParams(80));
        }

        this.ReturnToList();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ReturnToList();
    }
}
