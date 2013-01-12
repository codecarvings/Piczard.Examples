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

Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing

Imports CodeCarvings.Piczard

Partial Class examples_example_A_502_editRecord
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

            ' Setup the Picture1 CropConstraint and Preview filter
            Me.Picture1.CropConstraint = New FixedCropConstraint(350, 350)
            Me.Picture1.PreviewFilter = New FixedResizeConstraint(100, 100)

            ' Load the Record
            If (Me.RecordId <> 0) Then
                ' UPDATE
                Me.labelRecordId.Text = Me.RecordId.ToString()

                ' Load the database data
                Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                    Using command As OleDbCommand = connection.CreateCommand()
                        command.CommandType = CommandType.Text
                        command.CommandText = "SELECT [Title] FROM [Ex_A_502] WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Id", Me.RecordId)

                        Using reader As OleDbDataReader = command.ExecuteReader()
                            If (reader.Read()) Then
                                ' Record found

                                ' Get the title
                                Me.txtTitle.Text = Convert.ToString(reader("Title"))

                                Dim picture1FileName As String = String.Format("{0}.jpg", Me.RecordId)
                                Dim picture1FilePath_main As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/main/"), picture1FileName)
                                If (System.IO.File.Exists(picture1FilePath_main)) Then
                                    ' Image exists... Load the picture1 main image
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

        ' Delete the previous image files
        If (Me.RecordId <> 0) Then
            ' UPDATE...

            If (Not Me.Picture1.HasImage) Then
                ' Image removed -> Delete the old files

                ' Get the picture file name
                Dim picture1FileName As String = String.Format("{0}.jpg", Me.RecordId)

                ' Delete the main image
                Dim picture1FilePath_main As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/main/"), picture1FileName)
                If (System.IO.File.Exists(picture1FilePath_main)) Then
                    System.IO.File.Delete(picture1FilePath_main)
                End If

                ' Delete the thumbnail
                Dim picture1FilePath_thumbnail As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/thumbnail/"), picture1FileName)
                If (System.IO.File.Exists(picture1FilePath_thumbnail)) Then
                    System.IO.File.Delete(picture1FilePath_thumbnail)
                End If
            End If
        End If

        ' Save the Record into the DB
        If (Me.RecordId <> 0) Then
            ' UPDATE...

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Update the record
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "UPDATE [Ex_A_502] SET [Title]=@Title WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)
                    command.Parameters.AddWithValue("@Id", Me.RecordId)

                    command.ExecuteNonQuery()
                End Using
            End Using
        Else
            ' INSERT...

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Insert the new record
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "INSERT INTO [Ex_A_502] ([Title]) VALUES (@Title)"
                    command.Parameters.AddWithValue("@Title", Me.txtTitle.Text)

                    command.ExecuteNonQuery()
                End Using

                ' Get the new record id
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "SELECT @@IDENTITY"

                    Me.RecordId = Convert.ToInt32(command.ExecuteScalar())
                End Using
            End Using
        End If

        ' Save the new image files (using the record id as file name)
        If (Me.Picture1.HasNewImage) Then
            ' Get the picture file name
            Dim picture1FileName As String = String.Format("{0}.jpg", Me.RecordId)

            ' Save the main image
            Dim picture1FilePath_main As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/main/"), picture1FileName)
            Me.Picture1.SaveProcessedImageToFileSystem(picture1FilePath_main)

            ' Genereate the thumbnail image (Resize -> Fixed size: 48x48 Pixel, Jpeg 80% quality)
            Dim picture1FilePath_thumbnail As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/thumbnail/"), picture1FileName)
            Dim job As ImageProcessingJob = Me.Picture1.GetImageProcessingJob()
            job.Filters.Add(New FixedResizeConstraint(48, 48))
            job.SaveProcessedImageToFileSystem(Me.Picture1.TemporarySourceImageFilePath, picture1FilePath_thumbnail, New JpegFormatEncoderParams(80))
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
