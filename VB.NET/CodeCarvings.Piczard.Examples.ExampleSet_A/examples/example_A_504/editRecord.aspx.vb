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
Imports System.IO

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web
Imports CodeCarvings.Piczard.Serialization

Partial Class examples_example_A_504_editRecord
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

            ' Setup the Picture1 preview filter
            Dim previewFilter As ScaledResizeConstraint = New ScaledResizeConstraint(300, 200)
            previewFilter.EnlargeSmallImages = False
            Me.Picture1.PreviewFilter = previewFilter

            ' Load the Record
            If (Me.RecordId <> 0) Then
                ' UPDATE
                Me.labelRecordId.Text = Me.RecordId.ToString()

                ' Load the database data
                Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                    Using command As OleDbCommand = connection.CreateCommand()
                        command.CommandType = CommandType.Text
                        command.CommandText = "SELECT [Title], [Picture1_file_main] FROM [Ex_A_504] WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Id", Me.RecordId)

                        Using reader As OleDbDataReader = command.ExecuteReader()
                            If (reader.Read()) Then
                                ' Record found

                                ' Get the title
                                Me.txtTitle.Text = Convert.ToString(reader("Title"))

                                ' Get the picture1 main image bytes
                                Dim picture1_file_main As Byte() = DirectCast(reader("Picture1_file_main"), Byte())
                                If (picture1_file_main.Length > 0) Then
                                    ' Load the image into the SimpleImageUpload ASCX control
                                    Me.Picture1.LoadImageFromByteArray(picture1_file_main)
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

#Region "Event handlers"

    Protected Sub Picture1_ImageUpload(ByVal sender As Object, ByVal args As SimpleImageUpload.ImageUploadEventArgs) Handles Picture1.ImageUpload
        Me.MyLogEvent("New image uploaded.")

        If ((Me.Picture1.SourceImageSize.Width < 100) Or (Me.Picture1.SourceImageSize.Height < 150)) Then
            ' The uploaded image image is too small
            Me.Picture1.UnloadImage()
            Me.Picture1.SetCurrentStatusMessage("<span style=""color:#cc0000;"">The uploaded Image is too small.</span>")
            Return
        End If

        If ((Me.Picture1.SourceImageSize.Width > 1500) Or (Me.Picture1.SourceImageSize.Height > 1600)) Then
            ' The uploaded image image is too large
            Me.Picture1.UnloadImage()
            Me.Picture1.SetCurrentStatusMessage("<span style=""color:#cc0000;"">The uploaded Image is too large.</span>")
            Return
        End If
    End Sub

    Protected Sub Picture1_UploadError(ByVal sender As Object, ByVal e As System.EventArgs) Handles Picture1.UploadError
        Me.MyLogEvent("Upload error.")
    End Sub

    Protected Sub Picture1_ImageEdit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Picture1.ImageEdit
        Me.MyLogEvent("Image edited.")
    End Sub

    Protected Sub Picture1_ImageRemove(ByVal sender As Object, ByVal e As System.EventArgs) Handles Picture1.ImageRemove
        Me.MyLogEvent("Image removed.")
    End Sub

#End Region

    Protected Sub MyLogEvent(ByVal message As String)
        Dim newEvent As String = DateTime.Now.ToString("s") + " - " + message + ControlChars.CrLf
        Me.txtMyLog.Text = newEvent + Me.txtMyLog.Text
    End Sub

    Protected Sub ReturnToList()
        Response.Redirect("default.aspx", True)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ' Save the record

        If (Not Me.IsValid) Then
            Return
        End If

        ' Prepare the byte arrays to store in the DB (lenght=0)
        Dim picture1_file_main(-1) As Byte
        Dim picture1_file_thumbnail(-1) As Byte

        If (Me.Picture1.HasNewImage) Then
            ' New image to save

            ' Ensure that the temporary file exists
            If (File.Exists(Me.Picture1.TemporarySourceImageFilePath)) Then
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

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Update the record
                Using command As OleDbCommand = connection.CreateCommand()
                    If ((Not Me.Picture1.HasImage) Or (Me.Picture1.HasNewImage)) Then
                        ' ### IMAGE NOT SELECTED -OR- NEW IMAGE
                        command.CommandText = "UPDATE [Ex_A_504] SET [Title]=@Title, [Picture1_file_main]=@Picture1_file_main, [Picture1_file_thumbnail]=@Picture1_file_thumbnail WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                        ' Store the files
                        command.Parameters.AddWithValue("@Picture1_file_main", picture1_file_main)
                        command.Parameters.AddWithValue("@Picture1_file_thumbnail", picture1_file_thumbnail)

                        command.Parameters.AddWithValue("@Id", Me.RecordId)
                    Else
                        ' ### IMAGE NOT UPDATED
                        command.CommandText = "UPDATE [Ex_A_504] SET [Title]=@Title WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                        command.Parameters.AddWithValue("@Id", Me.RecordId)
                    End If

                    command.ExecuteNonQuery()
                End Using
            End Using
        Else
            ' INSERT...

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Insert the new record
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "INSERT INTO [Ex_A_504] ([Title], [Picture1_file_main], [Picture1_file_thumbnail]) VALUES (@Title, @Picture1_file_main, @Picture1_file_thumbnail)"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                    ' Store the files
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
