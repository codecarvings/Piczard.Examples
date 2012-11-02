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

Imports System.Drawing

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks

Partial Class examples_example_A_101_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
        Me.phOutputContainer.Visible = False
        Me.phCodeContainer.Visible = False
    End Sub


    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.ProcessImage()

        ' Display the source code
        Me.phCodeContainer.Visible = True
        For i As Integer = 0 To 3
            Dim phCode As PlaceHolder = DirectCast(Me.phCodeContainer.FindControl("phCode_" + i.ToString()), PlaceHolder)
            phCode.Visible = (i = Me.ddlFilter.SelectedIndex)
        Next
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_101.jpg"

        Select Case Me.ddlFilter.SelectedIndex
            Case 0
                ' Fixed Size Crop Constraint
                Dim cropWidth As Integer = 160
                Dim cropHeight As Integer = 350
                Call New FixedCropConstraint(cropWidth, cropHeight).SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
            Case 1
                ' Scaled Resize Constraint
                Dim maxWidth As Integer = 160
                Dim maxHeight As Integer = 350
                Call New ScaledResizeConstraint(maxWidth, maxHeight).SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
            Case 2
                ' Grayscale Color Filter
                DefaultColorFilters.Grayscale.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
            Case 3
                ' Text Watermark
                Dim watermark As TextWatermark = New TextWatermark()
                watermark.Text = DateTime.Now.ToString()
                watermark.ContentAlignment = ContentAlignment.TopRight
                watermark.Font.Name = "Arial"
                watermark.Font.Size = 20
                watermark.ForeColor = Color.Yellow
                watermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
        End Select

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

        ' Display the generated image
        Me.phOutputContainer.Visible = True
    End Sub

End Class
