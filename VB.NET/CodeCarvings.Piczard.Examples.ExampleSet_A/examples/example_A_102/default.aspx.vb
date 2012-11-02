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

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks

Partial Class examples_example_A_102_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID

        ' Re-process the image after every postback...
        Me.ProcessImage()
        Me.GenerateExampleCode()
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_102.jpg"

        ' Setup a collection and add filters selected by the user
        Dim filters As ImageProcessingFilterCollection = New ImageProcessingFilterCollection()

        Dim sortedFilterIDS As String() = Me.hfFilterList.Value.Split(",")
        ' The filterIDS contains the sorted filters that we have to apply...
        For i As Integer = 0 To sortedFilterIDS.Length - 1
            Dim filterID As String = sortedFilterIDS(i)
            Select Case filterID
                Case "filterRotate"
                    If (Me.cbFilterRotate.Checked) Then
                        ' Rotate the image by 90°
                        filters.Add(ImageTransformation.Rotate90)
                    End If
                Case "filterResize"
                    If (Me.cbFilterResize.Checked) Then
                        ' Resize the image so that it can be contained within a 280 x 280 square
                        filters.Add(New ScaledResizeConstraint(280, 280))
                    End If
                Case "filterChangeColors"
                    If (Me.cbFilterChangeColors.Checked) Then
                        ' Change hue, saturation, brightness and contrast of the image
                        filters.Add(New ImageAdjustmentsFilter(30, 50, 20, -50))
                    End If
                Case "filterWatermark"
                    If (Me.cbFilterWatermark.Checked) Then
                        ' Apply an image watermak
                        Dim watermark As ImageWatermark = New ImageWatermark("~/repository/watermark/piczardWatermark1.png")
                        watermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight
                        filters.Add(watermark)
                    End If
            End Select
        Next

        ' Process the image
        filters.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()
    End Sub

    Protected Sub GenerateExampleCode()
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

        sb.Append("' Setup a collection and add filters selected by the user" + ControlChars.CrLf)
        sb.Append("Dim filters As ImageProcessingFilterCollection = New ImageProcessingFilterCollection()" + ControlChars.CrLf)

        Dim sortedFilterIDS As String() = Me.hfFilterList.Value.Split(",")
        For i As Integer = 0 To sortedFilterIDS.Length - 1

            Dim filterID As String = sortedFilterIDS(i)
            Select Case filterID
                Case "filterRotate"
                    If (Me.cbFilterRotate.Checked) Then
                        sb.Append(ControlChars.CrLf)
                        sb.Append("' Rotate the image by 90°" + ControlChars.CrLf)
                        sb.Append("filters.Add(ImageTransformation.Rotate90)" + ControlChars.CrLf)
                    End If
                Case "filterResize"
                    If (Me.cbFilterResize.Checked) Then
                        sb.Append(ControlChars.CrLf)
                        sb.Append("' Resize the image so that it can be contained within a 280 x 280 square" + ControlChars.CrLf)
                        sb.Append("filters.Add(New ScaledResizeConstraint(280, 280))" + ControlChars.CrLf)
                    End If
                Case "filterChangeColors"
                    If (Me.cbFilterChangeColors.Checked) Then
                        sb.Append(ControlChars.CrLf)
                        sb.Append("' Change hue, saturation, brightness and contrast of the image" + ControlChars.CrLf)
                        sb.Append("filters.Add(New ImageAdjustmentsFilter(30, 50, 20, -50))" + ControlChars.CrLf)
                    End If
                Case "filterWatermark"
                    If (Me.cbFilterWatermark.Checked) Then
                        sb.Append(ControlChars.CrLf)
                        sb.Append("' Apply an image watermak" + ControlChars.CrLf)
                        sb.Append("Dim watermark As ImageWatermark = New ImageWatermark(""~/repository/watermark/piczardWatermark1.png"")" + ControlChars.CrLf)
                        sb.Append("watermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight" + ControlChars.CrLf)
                        sb.Append("filters.Add(watermark)" + ControlChars.CrLf)
                    End If
            End Select
        Next

        sb.Append(ControlChars.CrLf)
        sb.Append("' Process the image" + ControlChars.CrLf)
        sb.Append("Dim sourceImageFileName As String = ""~/repository/source/valencia1.jpg""" + ControlChars.CrLf)
        sb.Append("Dim outputImageFileName As String = ""~/repository/output/Ex_A_102.jpg""" + ControlChars.CrLf)
        sb.Append("filters.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)" + ControlChars.CrLf)

        Me.litCode.Text = sb.ToString()
    End Sub

End Class
