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

public partial class examples_example_A_502_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void EditRecord(int id)
    {
        Response.Redirect("editRecord.aspx?Id=" + id.ToString(), true);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        // Go to the editRecord page
        this.EditRecord(0);
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Edit the record

        // Get the record id
        int id = (int)this.GridView1.DataKeys[e.NewEditIndex].Value;

        this.EditRecord(id);
    }

    protected void AccessDataSource1_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        // Delete the image files

        // Get the record id
        int id = (int)e.Command.Parameters["Id"].Value;

        // Get the picture file name
        string picture1FileName = string.Format("{0}.jpg", id);

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
