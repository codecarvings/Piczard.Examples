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

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web

Partial Class examples_example_A_306_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        If (Not Me.IsPostBack) Then
            ' Check the SQL Server DB configuration.
            ExamplesHelper.CheckDbConnection_SqlServer(Request("skipcscheck") = "1")

            ' Display the picture
            Me.displayImage1()
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
    End Sub

    Protected Const RecordID1 As Integer = 1

    Protected Sub lbEdit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEdit1.Click
        Me.EditImage1()
    End Sub

    Protected Sub btnEdit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit1.Click
        Me.EditImage1()
    End Sub

    Protected Sub EditImage1()
        ' Load the image and the value from the DB
        Using connection As SqlConnection = ExamplesHelper.GetNewOpenDbConnection_SqlServer()
            Using command As SqlCommand = connection.CreateCommand()
                command.CommandText = "SELECT [SourceImage],[PictureTrimmerValue] FROM [Ex_A_306] WHERE [Id]=@Id"
                command.Parameters.AddWithValue("@Id", RecordID1)
                Using reader As SqlDataReader = command.ExecuteReader()
                    reader.Read()

                    Me.popupPictureTrimmer1.LoadImageFromByteArray(DirectCast(reader("SourceImage"), Byte()), New FixedCropConstraint(320, 180))
                    Me.popupPictureTrimmer1.Value = PictureTrimmerValue.FromJSON(DirectCast(reader("PictureTrimmerValue"), String))
                End Using
            End Using
        End Using

        ' Open the image edit popup
        Me.popupPictureTrimmer1.OpenPopup(800, 510)
    End Sub

    Protected Sub popupPictureTrimmer1_PopupClose(ByVal sender As Object, ByVal e As CodeCarvings.Piczard.Web.PictureTrimmerPopupCloseEventArgs) Handles popupPictureTrimmer1.PopupClose
        If (e.SaveChanges) Then
            ' User clicked the "Ok" button

            ' Save the PictureTrimmer value and the output image in the DB
            Using connection As SqlConnection = ExamplesHelper.GetNewOpenDbConnection_SqlServer()
                Using command As SqlCommand = connection.CreateCommand()
                    command.CommandText = "UPDATE [Ex_A_306] SET [PictureTrimmerValue]=@PictureTrimmerValue, [OutputImage]=@OutputImage  WHERE [Id]=@Id"
                    command.Parameters.AddWithValue("@PictureTrimmerValue", CodeCarvings.Piczard.Serialization.JSONSerializer.SerializeToString(Me.popupPictureTrimmer1.Value))
                    command.Parameters.AddWithValue("@OutputImage", Me.popupPictureTrimmer1.SaveProcessedImageToByteArray(New JpegFormatEncoderParams()))
                    command.Parameters.AddWithValue("@Id", RecordID1)
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Display the new image
            Me.displayImage1()
        End If

        ' Unload the image from the control
        Me.popupPictureTrimmer1.UnloadImage()
    End Sub

    Protected Sub displayImage1()
        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.img1.ImageUrl = "ImageFromDB.ashx?id=" + RecordID1.ToString() + "&timestamp=" + DateTime.UtcNow.Ticks.ToString()
    End Sub

End Class
