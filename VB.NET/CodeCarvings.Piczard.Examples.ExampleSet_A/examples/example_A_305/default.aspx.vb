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
Imports CodeCarvings.Piczard.Web

Partial Class examples_example_A_305_default
    Inherits System.Web.UI.Page

    ' IMPORTANT NOTE ***
    ' In order to completely disable the creation of temporary files you have to set the attribute
    ' useTemporaryFiles to false in the Web.Config file (codeCarvings.piczard/webSettings/pictureTrimmer)
    ' *********

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        If (Not Me.IsPostBack) Then
            ' Display the pictures
            Me.displayOutputImage1()
            Me.displayOutputImage2()
            Me.displayOutputImage3()
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
    End Sub

    Protected Const RecordID1 As Integer = 1
    Protected Const RecordID2 As Integer = 2
    Protected Const RecordID3 As Integer = 3

#Region "Image stored in the file system"

    Protected Sub lbEdit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEdit1.Click
        Me.EditImage1()
    End Sub

    Protected Sub btnEdit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit1.Click
        Me.EditImage1()
    End Sub

    Protected Sub EditImage1()
        ' Load the image in the control (the image file is stored in the file system)
        Me.popupPictureTrimmer1.LoadImageFromFileSystem(String.Format("~/repository/store/ex_A_305/source/{0}.jpg", RecordID1), New FixedCropConstraint(320, 180))

        ' Load the value (stored in the DB)
        Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
            Using command As OleDbCommand = connection.CreateCommand()
                command.CommandText = "SELECT [PictureTrimmerValue] FROM [Ex_A_305] WHERE [Id]=@Id"
                command.Parameters.AddWithValue("@Id", RecordID1)
                Me.popupPictureTrimmer1.Value = PictureTrimmerValue.FromJSON(DirectCast(command.ExecuteScalar(), String))
            End Using
        End Using

        ' Open the image edit popup
        Me.popupPictureTrimmer1.OpenPopup(800, 510)
    End Sub

    Protected Sub popupPictureTrimmer1_PopupClose(ByVal sender As Object, ByVal e As CodeCarvings.Piczard.Web.PictureTrimmerPopupCloseEventArgs) Handles popupPictureTrimmer1.PopupClose
        If (e.SaveChanges) Then
            ' User clicked the "Ok" button

            ' Save the cropped image in the file system
            Me.popupPictureTrimmer1.SaveProcessedImageToFileSystem(String.Format("~/repository/store/ex_A_305/output/{0}.jpg", RecordID1))

            ' Save the PictureTrimmer value in the DB
            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "UPDATE [Ex_A_305] SET [PictureTrimmerValue]=@PictureTrimmerValue  WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(Me.popupPictureTrimmer1.Value))
                    command.Parameters.AddWithValue("@Id", RecordID1)
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Display the new output image
            Me.displayOutputImage1()
        End If

        ' Unload the image from the control
        Me.popupPictureTrimmer1.UnloadImage()
    End Sub

    Protected Sub displayOutputImage1()
        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.img1.ImageUrl = String.Format("~/repository/store/ex_A_305/output/{0}.jpg?timestamp={1}", RecordID1, DateTime.UtcNow.Ticks)
    End Sub

#End Region

#Region "Image stored in a DB"

    Protected Function GetImageUrl_Output(ByVal id As Integer) As String
        ' Add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Return "ImageFromDB_Output.ashx?id=" + id.ToString() + "&timestamp=" + DateTime.UtcNow.Ticks.ToString()
    End Function

    Protected Function GetImageUrl_Content(ByVal id As Integer, ByVal imageBackColorValue As Color, ByVal imageBackColorApplyMode As PictureTrimmerImageBackColorApplyMode) As String
        ' Add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Dim result As String = "ImageFromDB_Content.ashx?id=" + id.ToString() + "&imageBackColorValue=" + HttpUtility.UrlEncode(CodeCarvings.Piczard.Helpers.StringConversionHelper.ColorToString(imageBackColorValue, True))
        result += "&imageBackColorApplyMode=" + HttpUtility.UrlEncode(DirectCast(imageBackColorApplyMode, Integer).ToString()) + "&timestamp=" + DateTime.UtcNow.Ticks.ToString()

        Return result
    End Function

#Region "With temporary files"

    Protected Sub lbEdit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEdit2.Click
        Me.EditImage2()
    End Sub

    Protected Sub btnEdit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit2.Click
        Me.EditImage2()
    End Sub

    Protected Sub EditImage2()
        ' Load the image and the value from the DB
        Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
            Using command As OleDbCommand = connection.CreateCommand()
                command.CommandText = "SELECT [SourceImage],[PictureTrimmerValue] FROM [Ex_A_305] WHERE [Id]=@Id"
                command.Parameters.AddWithValue("@Id", RecordID2)
                Using reader As OleDbDataReader = command.ExecuteReader()
                    reader.Read()

                    Dim imageBytes As Byte() = DirectCast(reader("SourceImage"), Byte())
                    Me.popupPictureTrimmer2.LoadImageFromByteArray(imageBytes, New FixedCropConstraint(320, 180))
                    Me.popupPictureTrimmer2.Value = PictureTrimmerValue.FromJSON(DirectCast(reader("PictureTrimmerValue"), String))
                End Using
            End Using
        End Using

        ' Open the image edit popup
        Me.popupPictureTrimmer2.OpenPopup(800, 510)
    End Sub

    Protected Sub popupPictureTrimmer2_PopupClose(ByVal sender As Object, ByVal e As CodeCarvings.Piczard.Web.PictureTrimmerPopupCloseEventArgs) Handles popupPictureTrimmer2.PopupClose
        If (e.SaveChanges) Then
            ' User clicked the "Ok" button

            ' Save the PictureTrimmer value and the output image in the DB
            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "UPDATE [Ex_A_305] SET [PictureTrimmerValue]=@PictureTrimmerValue, [OutputImage]=@OutputImage  WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(Me.popupPictureTrimmer2.Value))
                    Dim outputImageBytes As Byte() = Me.popupPictureTrimmer2.SaveProcessedImageToByteArray(New JpegFormatEncoderParams())
                    command.Parameters.AddWithValue("@OutputImage", outputImageBytes)
                    command.Parameters.AddWithValue("@Id", RecordID2)
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Display the new output image
            Me.displayOutputImage2()
        End If

        ' Unload the image from the control
        Me.popupPictureTrimmer2.UnloadImage()
    End Sub

    Protected Sub displayOutputImage2()
        Me.img2.ImageUrl = GetImageUrl_Output(RecordID2)
    End Sub

#End Region

#Region "Without temporary files"

    Protected Sub lbEdit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEdit3.Click
        Me.EditImage3()
    End Sub

    Protected Sub btnEdit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit3.Click
        Me.EditImage3()
    End Sub

    Protected Sub EditImage3()
        ' Load the image and the value from the DB
        Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
            Using command As OleDbCommand = connection.CreateCommand()
                command.CommandText = "SELECT [SourceImage],[PictureTrimmerValue] FROM [Ex_A_305] WHERE [Id]=@Id"
                command.Parameters.AddWithValue("@Id", RecordID3)
                Using reader As OleDbDataReader = command.ExecuteReader()
                    reader.Read()

                    Dim imageBytes As Byte() = DirectCast(reader("SourceImage"), Byte())
                    Dim contentImageUrl As String = GetImageUrl_Content(RecordID3, Me.popupPictureTrimmer3.ImageBackColor.Value, Me.popupPictureTrimmer3.ImageBackColorApplyMode)
                    Me.popupPictureTrimmer3.LoadImageFromByteArray(imageBytes, contentImageUrl, New FixedCropConstraint(320, 180))

                    ' NOTE:
                    ' The content image URL can be setted also after image load.
                    ' This is useful to generate dynamic content images.
                    ' Example:
                    ' Me.popupPictureTrimmer1.ContentImageUrl = "..."

                    Me.popupPictureTrimmer3.Value = PictureTrimmerValue.FromJSON(DirectCast(reader("PictureTrimmerValue"), String))
                End Using
            End Using
        End Using

        ' Open the image edit popup
        Me.popupPictureTrimmer3.OpenPopup(800, 510)
    End Sub

    Protected Sub popupPictureTrimmer3_PopupClose(ByVal sender As Object, ByVal e As CodeCarvings.Piczard.Web.PictureTrimmerPopupCloseEventArgs) Handles popupPictureTrimmer3.PopupClose
        If (e.SaveChanges) Then
            ' User clicked the "Ok" button

            Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
                ' Load the source image (this operation is required when temporary files are disabled!)
                Dim sourceImageBytes As Byte()
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "SELECT [SourceImage] FROM [Ex_A_305] WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@Id", RecordID3)
                    sourceImageBytes = DirectCast(command.ExecuteScalar(), Byte())
                End Using

                ' Save the PictureTrimmer value and the output image in the DB
                Using command As OleDbCommand = connection.CreateCommand()
                    command.CommandText = "UPDATE [Ex_A_305] SET [PictureTrimmerValue]=@PictureTrimmerValue, [OutputImage]=@OutputImage  WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(Me.popupPictureTrimmer3.Value))
                    Dim outputImageBytes As Byte() = Me.popupPictureTrimmer3.SaveProcessedImageToByteArray(sourceImageBytes, New JpegFormatEncoderParams())
                    command.Parameters.AddWithValue("@OutputImage", outputImageBytes)
                    command.Parameters.AddWithValue("@Id", RecordID3)
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Display the new output image
            Me.displayOutputImage3()
        End If

        ' Unload the image from the control
        Me.popupPictureTrimmer3.UnloadImage()
    End Sub

    Protected Sub displayOutputImage3()
        Me.img3.ImageUrl = GetImageUrl_Output(RecordID3)
    End Sub

#End Region

#End Region

End Class
