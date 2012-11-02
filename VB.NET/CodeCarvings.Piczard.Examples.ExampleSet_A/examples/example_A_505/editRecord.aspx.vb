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
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web
Imports CodeCarvings.Piczard.Serialization

Partial Class examples_example_A_505_editRecord
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

            ' Setup the Picture1 Crop constraint and the preview filter
            Me.Picture1.CropConstraint = New FixedCropConstraint(300, 300)
            Me.Picture1.PreviewFilter = New FixedResizeConstraint(100, 100)

            ' Load the Record
            If (Me.RecordId <> 0) Then
                ' UPDATE
                Me.labelRecordId.Text = Me.RecordId.ToString()

                ' Load the database data
                Using connection As SqlConnection = ExamplesHelper.GetNewOpenDbConnection_SqlServer()
                    Using command As SqlCommand = connection.CreateCommand()
                        command.CommandType = CommandType.Text
                        command.CommandText = "SELECT [Title], [Picture1_pictureTrimmerValue], [Picture1_file_upload] FROM [Ex_A_505] WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Id", Me.RecordId)

                        Using reader As SqlDataReader = command.ExecuteReader()
                            If (reader.Read()) Then
                                ' Record found

                                ' Get the title
                                Me.txtTitle.Text = Convert.ToString(reader("Title"))

                                ' Get the picture1 PictureTrimmerValue
                                Dim picture1_pictureTrimmerValue As String = Convert.ToString(reader("Picture1_pictureTrimmerValue"))
                                If (Not String.IsNullOrEmpty(picture1_pictureTrimmerValue)) Then
                                    Dim pictureTrimmerValue As PictureTrimmerValue = pictureTrimmerValue.FromJSON(picture1_pictureTrimmerValue)

                                    ' Get the original file bytes
                                    Dim picture1_file_upload As Byte() = DirectCast(reader("Picture1_file_upload"), Byte())
                                    If (picture1_file_upload.Length > 0) Then
                                        ' Load the image into the SimpleImageUpload ASCX control
                                        Me.Picture1.LoadImageFromByteArray(picture1_file_upload, pictureTrimmerValue)
                                    End If
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

#End Region

    Protected Sub ReturnToList()
        Response.Redirect("default.aspx", True)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ' Save the record

        If (Not Me.IsValid) Then
            Return
        End If

        ' Prepare the byte arrays and the strings to store in the DB (length=0)
        Dim picture1_pictureTrimmerValue As String = String.Empty
        Dim picture1_file_upload(-1) As Byte
        Dim picture1_file_main(-1) As Byte
        Dim picture1_file_thumbnail(-1) As Byte

        If (Me.Picture1.HasNewImage) Then
            ' New image to save

            ' Ensure that the temporary file exists
            If (File.Exists(Me.Picture1.TemporarySourceImageFilePath)) Then
                ' Serialize the value
                picture1_pictureTrimmerValue = JSONSerializer.SerializeToString(Me.Picture1.Value)

                ' Load the original image uploaded by the user
                picture1_file_upload = File.ReadAllBytes(Me.Picture1.TemporarySourceImageFilePath)

                ' Load the main image
                picture1_file_main = Me.Picture1.SaveProcessedImageToByteArray(New JpegFormatEncoderParams())

                ' Generate the thumbnail
                Dim job As ImageProcessingJob = Me.Picture1.GetImageProcessingJob()
                job.Filters.Add(New FixedResizeConstraint(48, 48))
                picture1_file_thumbnail = job.SaveProcessedImageToByteArray(Me.Picture1.TemporarySourceImageFilePath, New JpegFormatEncoderParams(80))
            End If
        End If

        If (Me.RecordId <> 0) Then
            ' UPDATE...

            Using connection As SqlConnection = ExamplesHelper.GetNewOpenDbConnection_SqlServer()
                ' Update the record
                Using command As SqlCommand = connection.CreateCommand()
                    If ((Not Me.Picture1.HasImage) Or (Me.Picture1.HasNewImage)) Then
                        ' ### IMAGE NOT SELECTED -OR- NEW IMAGE
                        command.CommandText = "UPDATE [Ex_A_505] SET [Title]=@Title, [Picture1_pictureTrimmerValue]=@Picture1_pictureTrimmerValue, [Picture1_file_upload]=@Picture1_file_upload, [Picture1_file_main]=@Picture1_file_main, [Picture1_file_thumbnail]=@Picture1_file_thumbnail WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                        ' Store the picture trimmer vale
                        command.Parameters.AddWithValue("@Picture1_pictureTrimmerValue", picture1_pictureTrimmerValue)

                        ' Store the files
                        command.Parameters.AddWithValue("@Picture1_file_upload", picture1_file_upload)
                        command.Parameters.AddWithValue("@Picture1_file_main", picture1_file_main)
                        command.Parameters.AddWithValue("@Picture1_file_thumbnail", picture1_file_thumbnail)

                        command.Parameters.AddWithValue("@Id", Me.RecordId)
                    Else
                        ' ### IMAGE NOT UPDATED
                        command.CommandText = "UPDATE [Ex_A_505] SET [Title]=@Title WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                        command.Parameters.AddWithValue("@Id", Me.RecordId)
                    End If

                    command.ExecuteNonQuery()
                End Using
            End Using
        Else
            ' INSERT...

            Using connection As SqlConnection = ExamplesHelper.GetNewOpenDbConnection_SqlServer()
                ' Insert the new record
                Using command As SqlCommand = connection.CreateCommand()
                    command.CommandText = "INSERT INTO [Ex_A_505] ([Title], [Picture1_pictureTrimmerValue], [Picture1_file_upload], [Picture1_file_main], [Picture1_file_thumbnail]) VALUES (@Title, @Picture1_pictureTrimmerValue, @Picture1_file_upload, @Picture1_file_main, @Picture1_file_thumbnail)"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                    ' Store the picture trimmer vale
                    command.Parameters.AddWithValue("@Picture1_pictureTrimmerValue", picture1_pictureTrimmerValue)

                    ' Store the files
                    command.Parameters.AddWithValue("@Picture1_file_upload", picture1_file_upload)
                    command.Parameters.AddWithValue("@Picture1_file_main", picture1_file_main)
                    command.Parameters.AddWithValue("@Picture1_file_thumbnail", picture1_file_thumbnail)

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
