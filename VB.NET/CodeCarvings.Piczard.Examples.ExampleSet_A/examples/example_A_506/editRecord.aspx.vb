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

Partial Class examples_example_A_506_editRecord
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

            ' Setup the size of the preview image displayed in the Picture1 control (fixed size: 200x200 pixel)
            Me.Picture1.PreviewFilter = New FixedResizeConstraint(200, 200)

            Me.Picture1.Text_ConfigurationLabel = "<strong>Image orientation:&nbsp;</strong>"
            Me.Picture1.Configurations = New String() {"Landscape", "Portrait"}

            ' Load the Record
            If (Me.RecordId <> 0) Then
                ' UPDATE
                Me.labelRecordId.Text = Me.RecordId.ToString()

                ' Load the database data
                Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                    Using command As OleDbCommand = connection.CreateCommand()
                        command.CommandType = CommandType.Text
                        command.CommandText = "SELECT [Title], [Picture1_Configuration], [Picture1_PictureTrimmerValue], [Picture1_FileName_upload], [Picture1_FileName_main], [Picture1_FileName_thumbnail] FROM [Ex_A_506] WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Id", Me.RecordId)

                        Using reader As OleDbDataReader = command.ExecuteReader()
                            If (reader.Read()) Then
                                ' Record found

                                ' Get the title
                                Me.txtTitle.Text = Convert.ToString(reader("Title"))

                                ' Get the configuration index (0=landscape, 1=portrait) 
                                ' and apply the right crop constrait depending on the configuration
                                Me.Picture1.SelectedConfigurationIndex = Convert.ToInt32(reader("Picture1_Configuration"))
                                Me.Picture1.CropConstraint = Me.GetCropConstraint(Me.Picture1.SelectedConfigurationIndex.Value)

                                ' Get the PictureTrimmer previous state
                                Dim picture1Value As PictureTrimmerValue = PictureTrimmerValue.FromJSON(Convert.ToString(reader("Picture1_PictureTrimmerValue")))

                                ' Get the picture1 file names
                                Me.Picture1FileName_upload = Convert.ToString(reader("Picture1_FileName_upload"))
                                Me.Picture1FileName_main = Convert.ToString(reader("Picture1_FileName_main"))
                                Me.Picture1FileName_thumbnail = Convert.ToString(reader("Picture1_FileName_thumbnail"))

                                If (Not String.IsNullOrEmpty(Me.Picture1FileName_upload)) Then
                                    ' Load the image into the SimpleImageUpload ASCX control
                                    Dim picture1FilePath_upload As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/upload/"), Me.Picture1FileName_upload)
                                    Me.Picture1.LoadImageFromFileSystem(picture1FilePath_upload, picture1Value)
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

#Region "Picture1 events"

    Protected Sub Picture1_ImageUpload(ByVal sender As Object, ByVal args As SimpleImageUpload.ImageUploadEventArgs) Handles Picture1.ImageUpload
        ' Auto-Apply the right crop constraint depending on the source image size
        If (Me.Picture1.SourceImageSize.Width > Me.Picture1.SourceImageSize.Height) Then
            ' Landscape -> Configuration = 0
            Me.Picture1.SelectedConfigurationIndex = 0
        Else
            ' Portrait -> Configuration = 1
            Me.Picture1.SelectedConfigurationIndex = 1
        End If
        args.CropConstraint = Me.GetCropConstraint(Me.Picture1.SelectedConfigurationIndex.Value)
    End Sub

    Protected Sub Picture1_SelectedConfigurationIndexChanged(ByVal sender As Object, ByVal args As SimpleImageUpload.SelectedConfigurationIndexChangedEventArgs) Handles Picture1.SelectedConfigurationIndexChanged
        ' Apply the new crop constraint
        args.CropConstraint = Me.GetCropConstraint(Me.Picture1.SelectedConfigurationIndex.Value)
    End Sub

#End Region

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

    Protected Property Picture1FileName_upload() As String
        Get
            If (Me.ViewState("Picture1FileName_upload") IsNot Nothing) Then
                Return DirectCast(Me.ViewState("Picture1FileName_upload"), String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Me.ViewState("Picture1FileName_upload") = value
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

    Private Function GetCropConstraint(ByVal configuration As Integer) As CropConstraint
        Dim result As FixedCropConstraint
        If (configuration = 0) Then
            ' Landscape crop constraint
            result = New FixedCropConstraint(300, 200)
        Else
            ' Portrait crop constraint
            result = New FixedCropConstraint(200, 300)
        End If
        Return result
    End Function

    Protected Sub ReturnToList()
        Response.Redirect("default.aspx", True)
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
                If (Not String.IsNullOrEmpty(Me.Picture1FileName_upload)) Then
                    ' Delete the uploaded image
                    Dim picture1FilePath_upload As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/upload/"), Me.Picture1FileName_upload)
                    If (System.IO.File.Exists(picture1FilePath_upload)) Then
                        System.IO.File.Delete(picture1FilePath_upload)
                    End If
                    Me.Picture1FileName_upload = ""

                    ' Delete the main image
                    Dim picture1FilePath_main As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/main/"), Me.Picture1FileName_main)
                    If (System.IO.File.Exists(picture1FilePath_main)) Then
                        System.IO.File.Delete(picture1FilePath_main)
                    End If
                    Me.Picture1FileName_main = ""

                    ' Delete the thumbnail
                    Dim picture1FilePath_thumbnail As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_506/picture1/thumbnail/"), Me.Picture1FileName_thumbnail)
                    If (System.IO.File.Exists(picture1FilePath_thumbnail)) Then
                        System.IO.File.Delete(picture1FilePath_thumbnail)
                    End If
                    Me.Picture1FileName_thumbnail = ""

                End If
            End If
        End If

        ' Save the new image
        If (Me.Picture1.HasNewImage) Then
            ' Save the uploaded image           
            Dim picture1folderPath_upload As String = Server.MapPath("~/repository/store/ex_A_506/picture1/upload/")
            Me.Picture1FileName_upload = CodeCarvings.Piczard.Helpers.IOHelper.GetUniqueFileName(picture1folderPath_upload, Me.Picture1.SourceImageClientFileName)
            Dim picture1FilePath_upload As String = System.IO.Path.Combine(picture1folderPath_upload, Me.Picture1FileName_upload)
            System.IO.File.Copy(Me.Picture1.TemporarySourceImageFilePath, picture1FilePath_upload, True)

            ' Generate the main image            
            Dim picture1folderPath_main As String = Server.MapPath("~/repository/store/ex_A_506/picture1/main/")
            ' Get the original file name (but always use the .jpg extension)
            Me.Picture1FileName_main = CodeCarvings.Piczard.Helpers.IOHelper.GetUniqueFileName(picture1folderPath_main, System.IO.Path.GetFileNameWithoutExtension(Me.Picture1.SourceImageClientFileName) + ImageArchiver.GetFileExtensionFromImageFormatId(System.Drawing.Imaging.ImageFormat.Jpeg.Guid))
            Dim picture1FilePath_main As String = System.IO.Path.Combine(picture1folderPath_main, Me.Picture1FileName_main)
            Me.Picture1.SaveProcessedImageToFileSystem(picture1FilePath_main)

            ' Genereate the thumbnail image (Resize -> Fixed size: 48x48 Pixel, Jpeg 80% quality)
            Dim picture1folderPath_thumbnail As String = Server.MapPath("~/repository/store/ex_A_506/picture1/thumbnail/")
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
                    command.CommandText = "UPDATE [Ex_A_506] SET [Title]=@Title, [Picture1_Configuration]=@Picture1_Configuration, [Picture1_PictureTrimmerValue]=@Picture1_PictureTrimmerValue, [Picture1_FileName_upload]=@Picture1_FileName_upload, [Picture1_FileName_main]=@Picture1_FileName_main, [Picture1_FileName_thumbnail]=@Picture1_FileName_thumbnail WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                    ' Store the selected configuration index (landscape / portrait)
                    command.Parameters.AddWithValue("@Picture1_Configuration", Me.Picture1.SelectedConfigurationIndex)

                    ' Store the picture trimmer vale
                    command.Parameters.AddWithValue("@Picture1_PictureTrimmerValue", JSONSerializer.SerializeToString(Me.Picture1.Value))

                    ' Store the file names
                    command.Parameters.AddWithValue("@Picture1_FileName_upload", Me.Picture1FileName_upload)
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
                    command.CommandText = "INSERT INTO [Ex_A_506] ([Title], [Picture1_Configuration], [Picture1_PictureTrimmerValue], [Picture1_FileName_upload], [Picture1_FileName_main], [Picture1_FileName_thumbnail]) VALUES (@Title, @Picture1_Configuration, @Picture1_PictureTrimmerValue, @Picture1_FileName_upload, @Picture1_FileName_main, @Picture1_FileName_thumbnail)"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                    ' Store the selected configuration index (landscape / portrait)
                    command.Parameters.AddWithValue("@Picture1_Configuration", Me.Picture1.SelectedConfigurationIndex)

                    ' Store the picture trimmer vale
                    command.Parameters.AddWithValue("@Picture1_PictureTrimmerValue", JSONSerializer.SerializeToString(Me.Picture1.Value))

                    ' Store the file names
                    command.Parameters.AddWithValue("@Picture1_FileName_upload", Me.Picture1FileName_upload)
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
