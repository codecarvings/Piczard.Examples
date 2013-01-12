' -------------------------------------------------------
' Piczard Examples | ExampleSet -A- VB.NET
' Copyright 2011-2013 Sergio Turolla - All Rights Reserved.
' Author: Sergio Turolla
' <codecarvings.com>
'  
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
' ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
' PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT 
' SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR 
' ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN 
' ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
' OR OTHER DEALINGS IN THE SOFTWARE.
' -------------------------------------------------------

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web

''' <summary>
''' Utility functions.</summary>
Public NotInheritable Class ExamplesHelper

    ''' <summary>
    ''' Gets a new open DB Connection.</summary>
    ''' <returns>
    ''' The DB Connection (open).</returns>
    Public Shared Function GetNewOpenDbConnection() As OleDbConnection
        Dim connectionString As String = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=;Data Source=""" + System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.ConnectionStrings(Db1ConnectionStringName).ConnectionString) + """;Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False"
        Dim connection As OleDbConnection = New OleDbConnection(connectionString)
        connection.Open()
        Return connection
    End Function

    ''' <summary>
    ''' Gets a new open SQL Server DB Connection.</summary>
    ''' <returns>
    ''' The SQL Server DB Connection (open).</returns>
    Public Shared Function GetNewOpenDbConnection_SqlServer() As SqlConnection
        Dim connection As SqlConnection = New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings(SqlServerConnectionStringName).ConnectionString)
        connection.Open()
        Return connection
    End Function

    ''' <summary>
    ''' Checks the SQL Server DB configuration.</summary>
    ''' <param name="skipCSCheck">If True does not check the connection string.</param>
    Public Shared Sub CheckDbConnection_SqlServer(ByVal skipCSCheck As Boolean)
        If (Not skipCSCheck) Then
            If (System.Configuration.ConfigurationManager.ConnectionStrings(SqlServerConnectionStringName).ConnectionString = DefaultSqlServerConnectionString) Then
                ' Connection string non set
                Throw New Exception("Please configure the MS SQL Server database connection string """ + SqlServerConnectionStringName + """ in the Web.Config file.")
            End If
        End If

        Dim totRecords As Integer

        ' Test the connection string
        Using connection As SqlConnection = GetNewOpenDbConnection_SqlServer()
            Using command As SqlCommand = connection.CreateCommand()
                ' Test the DB by reading the first record of the Ex_A_306 table (used by the example A.306 - it always contain 1 record)
                command.CommandText = "SELECT COUNT(*) AS TOT FROM [Ex_A_306] WHERE [Id]=@Id"
                command.Parameters.AddWithValue("@Id", 1)
                totRecords = Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using

        If (totRecords = 0) Then
            ' DB not initialized... Initialize it!
            InitializeDb_SqlServer()
        End If
    End Sub

    Private Shared Sub InitializeDb_SqlServer()
        Using connection As SqlConnection = GetNewOpenDbConnection_SqlServer()
            Using command As SqlCommand = connection.CreateCommand()
                ' *** Initialize the [Ex_A_306] TABLE ******
                command.CommandText = "INSERT INTO [Ex_A_306] ([ID], [PictureTrimmerValue], [SourceImage], [OutputImage]) VALUES (@Id, @PictureTrimmerValue, @SourceImage, @OutputImage)"

                command.Parameters.AddWithValue("@Id", 1)

                Dim sourceImageFilePath As String = HttpContext.Current.Server.MapPath("~/repository/source/trevi1.jpg")
                command.Parameters.AddWithValue("@SourceImage", File.ReadAllBytes(sourceImageFilePath))

                Dim pictureTrimmerValueString As String = "{""imageSelection"":{""transformation"":{""resizeFactor"":102.4734,""rotationAngle"":0,""flipH"":true,""flipV"":false},""crop"":{""rectangle"":{""x"":34,""y"":37,""width"":320,""height"":180},""canvasColor"":{""value"":{""a"":255,""r"":255,""g"":255,""b"":255},""autoUseTransparentColor"":true}}},""imageAdjustments"":{""brightness"":-1.973684,""contrast"":5.921052,""hue"":-180,""saturation"":-13.1579},""imageBackColorApplyMode"":1}"
                command.Parameters.AddWithValue("@PictureTrimmerValue", pictureTrimmerValueString)

                Dim value As PictureTrimmerValue = PictureTrimmerValue.FromJSON(pictureTrimmerValueString)
                command.Parameters.AddWithValue("@OutputImage", value.SaveProcessedImageToByteArray(sourceImageFilePath, New JpegFormatEncoderParams()))

                command.ExecuteNonQuery()
                ' *********
            End Using
        End Using
    End Sub

    Private Const Db1ConnectionStringName As String = "Db1ConnectionString"
    Private Const SqlServerConnectionStringName As String = "SqlServerConnectionString"
    Private Const DefaultSqlServerConnectionString As String = "Server=localhost\SQLExpress; Database=CodeCarvings_Piczard_V1_0_ES_A; uid=myUsername; pwd=myPassword; Persist Security Info=False"

#Region "LoadLibrary"

    ''' <summary>
    ''' Loads the Galleria Script.</summary>
    ''' <param name="page">The web page.</param>
    Public Shared Sub LoadLibrary_Galleria(ByVal page As System.Web.UI.Page)
        Dim galleriaScript As System.Web.UI.HtmlControls.HtmlGenericControl = New System.Web.UI.HtmlControls.HtmlGenericControl("script")
        galleriaScript.Attributes("type") = "text/javascript"
        galleriaScript.Attributes("src") = page.ResolveUrl("~/design/libraries/jquery/aino-galleria/galleria.js")
        page.Header.FindControl("phHeader").Controls.Add(galleriaScript)
    End Sub

    ''' <summary>
    ''' Loads the colorpicker script.</summary>
    ''' <param name="page">The web page.</param>
    Public Shared Sub LoadLibrary_ColorPicker(ByVal page As System.Web.UI.Page)
        Dim colorpickerScript As System.Web.UI.HtmlControls.HtmlGenericControl = New System.Web.UI.HtmlControls.HtmlGenericControl("script")
        colorpickerScript.Attributes("type") = "text/javascript"
        colorpickerScript.Attributes("src") = page.ResolveUrl("~/design/libraries/jquery/colorpicker/js/colorpicker.js")
        page.Header.FindControl("phHeader").Controls.Add(colorpickerScript)

        Dim colorpickerCSS As System.Web.UI.HtmlControls.HtmlLink = New System.Web.UI.HtmlControls.HtmlLink()
        colorpickerCSS.Href = page.ResolveUrl("~/design/libraries/jquery/colorpicker/css/colorpicker.css")
        colorpickerCSS.Attributes.Add("rel", "stylesheet")
        colorpickerCSS.Attributes.Add("type", "text/css")
        page.Header.FindControl("phHeader").Controls.Add(colorpickerCSS)
    End Sub

    ''' <summary>
    ''' Loads the fancybox script.</summary>
    ''' <param name="page">The web page.</param>
    Public Shared Sub LoadLibrary_FancyBox(ByVal page As System.Web.UI.Page)
        Dim mousewheelScript As System.Web.UI.HtmlControls.HtmlGenericControl = New System.Web.UI.HtmlControls.HtmlGenericControl("script")
        mousewheelScript.Attributes("type") = "text/javascript"
        mousewheelScript.Attributes("src") = page.ResolveUrl("~/design/libraries/jquery/fancybox/jquery.mousewheel-3.0.2.pack.js")
        page.Header.FindControl("phHeader").Controls.Add(mousewheelScript)

        Dim fancyboxScript As System.Web.UI.HtmlControls.HtmlGenericControl = New System.Web.UI.HtmlControls.HtmlGenericControl("script")
        fancyboxScript.Attributes("type") = "text/javascript"
        fancyboxScript.Attributes("src") = page.ResolveUrl("~/design/libraries/jquery/fancybox/jquery.fancybox-1.3.1.pack.js")
        page.Header.FindControl("phHeader").Controls.Add(fancyboxScript)

        Dim fancyboxSCSS As System.Web.UI.HtmlControls.HtmlLink = New System.Web.UI.HtmlControls.HtmlLink()
        fancyboxSCSS.Href = page.ResolveUrl("~/design/libraries/jquery/fancybox/jquery.fancybox-1.3.1.css")
        fancyboxSCSS.Attributes.Add("rel", "stylesheet")
        fancyboxSCSS.Attributes.Add("type", "text/css")
        fancyboxSCSS.Attributes.Add("media", "screen")
        page.Header.FindControl("phHeader").Controls.Add(fancyboxSCSS)
    End Sub

#End Region

End Class
