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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.IO;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;

/// <summary>
/// Utility functions.</summary>
public static class ExamplesHelper
{

    /// <summary>
    /// Gets a new open DB Connection.</summary>
    /// <returns>
    /// The DB Connection (open).</returns>
    public static OleDbConnection GetNewOpenDbConnection()
    {
        string connectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=;Data Source=\"" + System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.ConnectionStrings[Db1ConnectionStringName].ConnectionString) + "\";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=\"Microsoft.Jet.OLEDB.4.0\";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
        OleDbConnection connection = new OleDbConnection(connectionString);
        connection.Open();
        return connection;
    }

    /// <summary>
    /// Gets a new open SQL Server DB Connection.</summary>
    /// <returns>
    /// The SQL Server DB Connection (open).</returns>
    public static SqlConnection GetNewOpenDbConnection_SqlServer()
    {
        SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[SqlServerConnectionStringName].ConnectionString);
        connection.Open();
        return connection;
    }

    /// <summary>
    /// Checks the SQL Server DB configuration.</summary>
    /// <param name="skipCSCheck">If True does not check the connection string.</param>
    public static void CheckDbConnection_SqlServer(bool skipCSCheck)
    {
        if (!skipCSCheck)
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings[SqlServerConnectionStringName].ConnectionString == DefaultSqlServerConnectionString)
            {
                // Connection string non set
                throw new Exception("Please configure the MS SQL Server database connection string \"" + SqlServerConnectionStringName + "\" in the Web.Config file.");
            }
        }

        int totRecords;

        // Test the connection string
        using (SqlConnection connection = GetNewOpenDbConnection_SqlServer())
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                // Test the DB by reading the first record of the Ex_A_306 table (used by the example A.306 - it always contain 1 record)
                command.CommandText = "SELECT COUNT(*) AS TOT FROM [Ex_A_306] WHERE [Id]=@Id";
                command.Parameters.AddWithValue("@Id", 1);
                totRecords = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        if (totRecords == 0)
        {
            // DB not initialized... Initialize it!
            InitializeDb_SqlServer();
        }
    }

    private static void InitializeDb_SqlServer()
    {
        using (SqlConnection connection = GetNewOpenDbConnection_SqlServer())
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                // *** Initialize the [Ex_A_306] TABLE ******
                command.CommandText = "INSERT INTO [Ex_A_306] ([ID], [PictureTrimmerValue], [SourceImage], [OutputImage]) VALUES (@Id, @PictureTrimmerValue, @SourceImage, @OutputImage)";

                command.Parameters.AddWithValue("@Id", 1);

                string sourceImageFilePath = HttpContext.Current.Server.MapPath("~/repository/source/trevi1.jpg");
                command.Parameters.AddWithValue("@SourceImage", File.ReadAllBytes(sourceImageFilePath));

                string pictureTrimmerValueString = "{\"imageSelection\":{\"transformation\":{\"resizeFactor\":102.4734,\"rotationAngle\":0,\"flipH\":true,\"flipV\":false},\"crop\":{\"rectangle\":{\"x\":34,\"y\":37,\"width\":320,\"height\":180},\"canvasColor\":{\"value\":{\"a\":255,\"r\":255,\"g\":255,\"b\":255},\"autoUseTransparentColor\":true}}},\"imageAdjustments\":{\"brightness\":-1.973684,\"contrast\":5.921052,\"hue\":-180,\"saturation\":-13.1579},\"imageBackColorApplyMode\":1}";
                command.Parameters.AddWithValue("@PictureTrimmerValue", pictureTrimmerValueString);

                PictureTrimmerValue value = PictureTrimmerValue.FromJSON(pictureTrimmerValueString);
                command.Parameters.AddWithValue("@OutputImage", value.SaveProcessedImageToByteArray(sourceImageFilePath, new JpegFormatEncoderParams()));

                command.ExecuteNonQuery();
                // *********
            }
        }
    }

    private const string Db1ConnectionStringName = "Db1ConnectionString";
    private const string SqlServerConnectionStringName = "SqlServerConnectionString";
    private const string DefaultSqlServerConnectionString = @"Server=localhost\SQLExpress; Database=CodeCarvings_Piczard_V1_0_ES_A; uid=myUsername; pwd=myPassword; Persist Security Info=False";

    #region LoadLibrary

    /// <summary>
    /// Loads the Galleria Script.</summary>
    /// <param name="page">The web page.</param>
    public static void LoadLibrary_Galleria(System.Web.UI.Page page)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl galleriaScript = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
        galleriaScript.Attributes["type"] = "text/javascript";
        galleriaScript.Attributes["src"] = page.ResolveUrl("~/design/libraries/jquery/aino-galleria/galleria.js");
        page.Header.FindControl("phHeader").Controls.Add(galleriaScript);
    }

    /// <summary>
    /// Loads the colorpicker script.</summary>
    /// <param name="page">The web page.</param>
    public static void LoadLibrary_ColorPicker(System.Web.UI.Page page)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl colorpickerScript = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
        colorpickerScript.Attributes["type"] = "text/javascript";
        colorpickerScript.Attributes["src"] = page.ResolveUrl("~/design/libraries/jquery/colorpicker/js/colorpicker.js");
        page.Header.FindControl("phHeader").Controls.Add(colorpickerScript);

        System.Web.UI.HtmlControls.HtmlLink colorpickerCSS = new System.Web.UI.HtmlControls.HtmlLink();
        colorpickerCSS.Href = page.ResolveUrl("~/design/libraries/jquery/colorpicker/css/colorpicker.css");
        colorpickerCSS.Attributes.Add("rel", "stylesheet");
        colorpickerCSS.Attributes.Add("type", "text/css");
        page.Header.FindControl("phHeader").Controls.Add(colorpickerCSS);
    }

    /// <summary>
    /// Loads the fancybox script.</summary>
    /// <param name="page">The web page.</param>
    public static void LoadLibrary_FancyBox(System.Web.UI.Page page)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl mousewheelScript = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
        mousewheelScript.Attributes["type"] = "text/javascript";
        mousewheelScript.Attributes["src"] = page.ResolveUrl("~/design/libraries/jquery/fancybox/jquery.mousewheel-3.0.2.pack.js");
        page.Header.FindControl("phHeader").Controls.Add(mousewheelScript);

        System.Web.UI.HtmlControls.HtmlGenericControl fancyboxScript = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
        fancyboxScript.Attributes["type"] = "text/javascript";
        fancyboxScript.Attributes["src"] = page.ResolveUrl("~/design/libraries/jquery/fancybox/jquery.fancybox-1.3.1.pack.js");
        page.Header.FindControl("phHeader").Controls.Add(fancyboxScript);

        System.Web.UI.HtmlControls.HtmlLink fancyboxSCSS = new System.Web.UI.HtmlControls.HtmlLink();
        fancyboxSCSS.Href = page.ResolveUrl("~/design/libraries/jquery/fancybox/jquery.fancybox-1.3.1.css");
        fancyboxSCSS.Attributes.Add("rel", "stylesheet");
        fancyboxSCSS.Attributes.Add("type", "text/css");
        fancyboxSCSS.Attributes.Add("media", "screen");
        page.Header.FindControl("phHeader").Controls.Add(fancyboxSCSS);
    }


    #endregion

}
