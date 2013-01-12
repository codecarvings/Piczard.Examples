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

Partial Class examples_example_A_511_editRecord
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me.IsPostBack) Then
            ' Get the record id passed as query parameter
            If (Not String.IsNullOrEmpty(Request.QueryString("id"))) Then
                Me.RecordId = Integer.Parse(Request.QueryString("id"))
            End If

            If (Me.RecordId <> 0) Then
                ' UPDATE
                Me.labelRecordId.Text = Me.RecordId.ToString()

                ' Load the database data
                Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                    Using command As OleDbCommand = connection.CreateCommand()
                        command.CommandType = CommandType.Text
                        command.CommandText = "SELECT [Title] FROM [Ex_A_511] WHERE [Id]=@Id"
                        command.Parameters.AddWithValue("@Id", Me.RecordId)

                        Using reader As OleDbDataReader = command.ExecuteReader()
                            If (reader.Read()) Then
                                ' Record found

                                Me.txtTitle.Text = Convert.ToString(reader("Title"))
                            Else
                                ' Record not found, return to list
                                Me.ReturnToList()
                                Return
                            End If
                        End Using
                    End Using
                End Using

                ' Picture 1 preview visible
                Me.phPicture1Preview.Visible = True
                Me.imgPicture1Preview.ImageUrl = String.Format("~/repository/store/ex_A_511/picture1/main/{0}.jpg", Me.RecordId)

                ' Picture 1 not required (alrady exists an image...)
                ' Hide the required field validator
                Me.fvPicture1.Visible = False
            Else
                ' NEW RECORD
                Me.labelRecordId.Text = "New record"

                ' Picture 1 preview not visible
                Me.phPicture1Preview.Visible = False

                ' Picture 1 required
                ' Show the required field validator
                Me.fvPicture1.Visible = True
            End If
        End If
    End Sub

    ' Gets or sets the current record id.
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

    Protected Sub ReturnToList()
        Response.Redirect("default.aspx", True)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ' Save the record

        If (Not Me.IsValid) Then
            Return
        End If

        ' Save the Record into the DB
        If (Me.RecordId <> 0) Then
            ' UPDATE...

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Update the record
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "UPDATE [Ex_A_511] SET [Title]=@Title WHERE [Id]=@Id"
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
                    command.CommandText = "INSERT INTO [Ex_A_511] ([Title]) VALUES (@Title)"
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

        ' Save the picture 1 files
        ' *** USE THE RECORD ID AS FILE NAME ***
        If (Me.fuPicture1.HasFile) Then
            ' Get the source file bytes
            Dim sourceFileBytes As Byte() = Me.fuPicture1.FileBytes

            ' Genereate the main image (Resize -> MaxSize: 400x250, Jpeg 92% quality)
            Call New ScaledResizeConstraint(250, 100).SaveProcessedImageToFileSystem(sourceFileBytes, String.Format("~/repository/store/ex_A_511/picture1/main/{0}.jpg", Me.RecordId), New JpegFormatEncoderParams(92))

            ' Genereate the thumbnail image (Resize -> Fixed size: 48x48 Pixel, Jpeg 80% quality)
            Call New FixedResizeConstraint(48, 48).SaveProcessedImageToFileSystem(sourceFileBytes, String.Format("~/repository/store/ex_A_511/picture1/thumbnail/{0}.jpg", Me.RecordId), New JpegFormatEncoderParams(80))
        End If

        Me.ReturnToList()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.ReturnToList()
    End Sub
End Class
