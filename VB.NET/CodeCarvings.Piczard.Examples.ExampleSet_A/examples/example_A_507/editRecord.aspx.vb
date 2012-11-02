' -------------------------------------------------------
' Piczard Examples | ExampleSet -A- VB.NET
' Copyright 2011-2012 Sergio Turolla - All Rights Reserved.
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

Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web
Imports CodeCarvings.Piczard.Serialization
Imports CodeCarvings.Piczard.Filters.Watermarks

Partial Class examples_example_A_507_editRecord
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        If (Not Me.IsPostBack) Then
            ' Get the record id passed as query parameter
            If (Not String.IsNullOrEmpty(Request.QueryString("id"))) Then
                Me.RecordId = Integer.Parse(Request.QueryString("id"))
            End If

            ' Setup the Picture1 Post Processing Filter (Resize + Watermark)
            Me.Picture1.PostProcessingFilter = New ScaledResizeConstraint(250, 250) + New ImageWatermark("~/repository/watermark/piczardWatermark1.png", ContentAlignment.BottomRight)

            ' Load the Record
            If (Me.RecordId <> 0) Then
                ' UPDATE
                Me.labelRecordId.Text = Me.RecordId.ToString()

                ' Load the database data
                Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                    Using command As OleDbCommand = connection.CreateCommand()
                        command.CommandType = CommandType.Text
                        command.CommandText = "SELECT [Title], [Picture1_FileName_main], [Picture1_FileName_thumbnail] FROM [Ex_A_507] WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Id", Me.RecordId)

                        Using reader As OleDbDataReader = command.ExecuteReader()
                            If (reader.Read()) Then
                                ' Record found

                                ' Get the title
                                Me.txtTitle.Text = Convert.ToString(reader("Title"))

                                ' Get the picture1 file names
                                Me.Picture1FileName_main = Convert.ToString(reader("Picture1_FileName_main"))
                                Me.Picture1FileName_thumbnail = Convert.ToString(reader("Picture1_FileName_thumbnail"))

                                If (Not String.IsNullOrEmpty(Me.Picture1FileName_main)) Then
                                    ' Load the image into the SimpleImageUpload ASCX control
                                    Dim picture1FilePath_main As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_507/picture1/main/"), Me.Picture1FileName_main)
                                    Me.Picture1.LoadImageFromFileSystem(picture1FilePath_main)
                                End If
                            Else
                                ' Record not found, return to list
                                Me.ReturnToList()
                                Return
                            End If
                        End Using
                    End Using
                End Using
            Else
                ' NEW RECORD
                Me.labelRecordId.Text = "New record"
            End If
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
    End Sub

#Region "Properties"

    Protected Property RecordId() As Integer
        Get
            If (Me.ViewState("RecordId") IsNot Nothing) Then
                Return DirectCast(Me.ViewState("RecordId"), Integer)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            Me.ViewState("RecordId") = value
        End Set
    End Property

    Protected Property Picture1FileName_main() As String
        Get
            If (Me.ViewState("Picture1FileName_main") IsNot Nothing) Then
                Return DirectCast(Me.ViewState("Picture1FileName_main"), String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Me.ViewState("Picture1FileName_main") = value
        End Set
    End Property

    Protected Property Picture1FileName_thumbnail() As String
        Get
            If (Me.ViewState("Picture1FileName_thumbnail") IsNot Nothing) Then
                Return DirectCast(Me.ViewState("Picture1FileName_thumbnail"), String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Me.ViewState("Picture1FileName_thumbnail") = value
        End Set
    End Property

#End Region

    Protected Sub ReturnToList()
        Response.Redirect("default.aspx", True)
    End Sub

    Protected Sub fvPicture1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles fvPicture1.ServerValidate
        ' Validate the Picture1 control (must contain a value)
        args.IsValid = Me.Picture1.HasImage
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ' Save the record

        If (Not Me.IsValid) Then
            Return
        End If

        ' *** Manage the image files ***

        ' Delete the previous image files
        If (Me.RecordId <> 0) Then
            ' UPDATE...

            If ((Not Me.Picture1.HasImage) Or (Me.Picture1.HasNewImage)) Then
                ' Delete the previous image
                If (Not String.IsNullOrEmpty(Me.Picture1FileName_main)) Then
                    ' Delete the main image
                    Dim picture1FilePath_main As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_507/picture1/main/"), Me.Picture1FileName_main)
                    If (System.IO.File.Exists(picture1FilePath_main)) Then
                        System.IO.File.Delete(picture1FilePath_main)
                    End If
                    Me.Picture1FileName_main = ""

                    ' Delete the thumbnail
                    Dim picture1FilePath_thumbnail As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_507/picture1/thumbnail/"), Me.Picture1FileName_thumbnail)
                    If (System.IO.File.Exists(picture1FilePath_thumbnail)) Then
                        System.IO.File.Delete(picture1FilePath_thumbnail)
                    End If
                    Me.Picture1FileName_thumbnail = ""

                End If
            End If
        End If

        ' Save the new image
        If (Me.Picture1.HasNewImage) Then
            ' Generate the main image            
            Dim picture1folderPath_main As String = Server.MapPath("~/repository/store/ex_A_507/picture1/main/")
            ' Get the original file name (but always use the .jpg extension)
            Me.Picture1FileName_main = CodeCarvings.Piczard.Helpers.IOHelper.GetUniqueFileName(picture1folderPath_main, System.IO.Path.GetFileNameWithoutExtension(Me.Picture1.SourceImageClientFileName) + ImageArchiver.GetFileExtensionFromImageFormatId(System.Drawing.Imaging.ImageFormat.Jpeg.Guid))
            Dim picture1FilePath_main As String = System.IO.Path.Combine(picture1folderPath_main, Me.Picture1FileName_main)
            Me.Picture1.SaveProcessedImageToFileSystem(picture1FilePath_main)

            ' Genereate the thumbnail image (Resize -> Fixed size: 48x48 Pixel, Jpeg 80% quality)
            Dim picture1folderPath_thumbnail As String = Server.MapPath("~/repository/store/ex_A_507/picture1/thumbnail/")
            ' Get the original file name (but always use the .jpg extension)
            Me.Picture1FileName_thumbnail = CodeCarvings.Piczard.Helpers.IOHelper.GetUniqueFileName(picture1folderPath_thumbnail, System.IO.Path.GetFileNameWithoutExtension(Me.Picture1.SourceImageClientFileName) + ImageArchiver.GetFileExtensionFromImageFormatId(System.Drawing.Imaging.ImageFormat.Jpeg.Guid))
            Dim picture1FilePath_thumbnail As String = System.IO.Path.Combine(picture1folderPath_thumbnail, Me.Picture1FileName_thumbnail)
            Dim job As ImageProcessingJob = Me.Picture1.GetImageProcessingJob()
            job.Filters.Add(New FixedResizeConstraint(48, 48))
            job.SaveProcessedImageToFileSystem(Me.Picture1.TemporarySourceImageFilePath, picture1FilePath_thumbnail, New JpegFormatEncoderParams(80))
        End If

        ' *********

        ' Save the Record into the DB
        If (Me.RecordId <> 0) Then
            ' UPDATE...

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Update the record
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "UPDATE [Ex_A_507] SET [Title]=@Title, [Picture1_FileName_main]=@Picture1_FileName_main, [Picture1_FileName_thumbnail]=@Picture1_FileName_thumbnail WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                    ' Store the file names
                    command.Parameters.AddWithValue("@Picture1_FileName_main", Me.Picture1FileName_main)
                    command.Parameters.AddWithValue("@Picture1_FileName_thumbnail", Me.Picture1FileName_thumbnail)

                    command.Parameters.AddWithValue("@Id", Me.RecordId)

                    command.ExecuteNonQuery()
                End Using
            End Using
        Else
            ' INSERT...

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Insert the new record
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "INSERT INTO [Ex_A_507] ([Title], [Picture1_FileName_main], [Picture1_FileName_thumbnail]) VALUES (@Title, @Picture1_FileName_main, @Picture1_FileName_thumbnail)"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                    ' Store the file names
                    command.Parameters.AddWithValue("@Picture1_FileName_main", Me.Picture1FileName_main)
                    command.Parameters.AddWithValue("@Picture1_FileName_thumbnail", Me.Picture1FileName_thumbnail)

                    command.ExecuteNonQuery()
                End Using
            End Using
        End If
        ' Clear the temporary files
        Me.Picture1.ClearTemporaryFiles()

        Me.ReturnToList()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ' Clear the temporary files
        Me.Picture1.ClearTemporaryFiles()

        Me.ReturnToList()
    End Sub
End Class
