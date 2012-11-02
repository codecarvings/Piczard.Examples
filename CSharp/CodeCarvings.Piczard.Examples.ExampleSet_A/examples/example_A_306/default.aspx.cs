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
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_306_default : System.Web.UI.Page
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
            // Check the SQL Server DB configuration.
            ExamplesHelper.CheckDbConnection_SqlServer();

            // Display the picture
            this.displayImage1();
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
    }

    protected const int RecordID1 = 1;

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
        // Load the image and the value from the DB
        using (SqlConnection connection = ExamplesHelper.GetNewOpenDbConnection_SqlServer())
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [SourceImage],[PictureTrimmerValue] FROM [Ex_A_306] WHERE [Id]=@Id";
                command.Parameters.AddWithValue("@Id", RecordID1);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    this.popupPictureTrimmer1.LoadImageFromByteArray((byte[])reader["SourceImage"], new FixedCropConstraint(320, 180));
                    this.popupPictureTrimmer1.Value = PictureTrimmerValue.FromJSON((string)reader["PictureTrimmerValue"]);
                }
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

            // Save the PictureTrimmer value and the output image in the DB
            using (SqlConnection connection = ExamplesHelper.GetNewOpenDbConnection_SqlServer())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Ex_A_306] SET [PictureTrimmerValue]=@PictureTrimmerValue, [OutputImage]=@OutputImage  WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(this.popupPictureTrimmer1.Value));
                    command.Parameters.AddWithValue("@OutputImage", this.popupPictureTrimmer1.SaveProcessedImageToByteArray(new JpegFormatEncoderParams()));
                    command.Parameters.AddWithValue("@Id", RecordID1);
                    command.ExecuteNonQuery();
                }
            }

            // Display the new image
            this.displayImage1();
        }

        // Unload the image from the control
        this.popupPictureTrimmer1.UnloadImage();
    }

    protected void displayImage1()
    {
        // Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        this.img1.ImageUrl = "ImageFromDB.ashx?id=" + RecordID1.ToString() + "&timestamp=" + DateTime.UtcNow.Ticks.ToString();
    }

}
