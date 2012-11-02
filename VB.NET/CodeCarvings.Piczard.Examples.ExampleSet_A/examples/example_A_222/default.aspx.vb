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

Partial Class examples_example_A_222_default
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

        Me.ddlDefaultColorFilters.Enabled = Me.cbDefaultColorFilters.Checked
        Me.ddlImageAdjustmentsFilter.Enabled = Me.cbImageAdjustmentsFilter.Checked
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.ProcessImage()
        Me.DisplayCode()
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_222.jpg"

        ' Use an image processing job since we are using 2 filters
        Dim job As ImageProcessingJob = New ImageProcessingJob()

        If (Me.cbDefaultColorFilters.Checked) Then
            Select Case Me.ddlDefaultColorFilters.SelectedIndex
                Case 0
                    ' Grayscale
                    job.Filters.Add(DefaultColorFilters.Grayscale)
                Case 1
                    ' Sepia
                    job.Filters.Add(DefaultColorFilters.Sepia)
                Case 2
                    ' Invert
                    job.Filters.Add(DefaultColorFilters.Invert)
            End Select
        End If

        If (Me.cbImageAdjustmentsFilter.Checked) Then
            Select Case Me.ddlImageAdjustmentsFilter.SelectedIndex
                Case 0
                    ' Decrease brightness
                    job.Filters.Add(New ImageAdjustmentsFilter(-60, 0))
                Case 1
                    ' Decrease contrast
                    job.Filters.Add(New ImageAdjustmentsFilter(0, -50))
                Case 2
                    ' Decrease saturation
                    job.Filters.Add(New ImageAdjustmentsFilter(0, 0, 0, -80))
                Case 3
                    ' Increase brightness
                    job.Filters.Add(New ImageAdjustmentsFilter(50, 0))
                Case 4
                    ' Increase contrast
                    job.Filters.Add(New ImageAdjustmentsFilter(0, 40))
                Case 5
                    ' Increase saturation
                    job.Filters.Add(New ImageAdjustmentsFilter(0, 0, 0, 70))
                Case 6
                    ' Change hue
                    job.Filters.Add(New ImageAdjustmentsFilter(0, 0, 60, 0))
                Case 7
                    ' Color combination #1
                    job.Filters.Add(New ImageAdjustmentsFilter(42, 42, 0, -72))
                Case 8
                    ' Color combination #2
                    job.Filters.Add(New ImageAdjustmentsFilter(0, 22, 180, 20))
            End Select
        End If

        ' Process the image
        job.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

        ' Display the generated image
        Me.phOutputContainer.Visible = True
    End Sub

    Protected Sub DisplayCode()
        Dim sbCode As System.Text.StringBuilder = New System.Text.StringBuilder()

        Dim crlf As String = ControlChars.CrLf
        sbCode.Append("' Prepare the parameters" + crlf)
        sbCode.Append("Dim sourceImage As String = ""~/repository/source/valencia2.jpg""" + crlf)
        sbCode.Append("Dim outputImage As String = ""~/repository/output/Ex_A_222.jpg""" + crlf)
        sbCode.Append(crlf)

        sbCode.Append("' Setup an image processing job" + crlf)
        sbCode.Append("Dim job As ImageProcessingJob = New ImageProcessingJob()" + crlf)

        If (Me.cbDefaultColorFilters.Checked) Then
            Select Case Me.ddlDefaultColorFilters.SelectedIndex
                Case 0
                    ' Grayscale
                    sbCode.Append("job.Filters.Add(DefaultColorFilters.Grayscale)" + crlf)
                Case 1
                    ' Sepia
                    sbCode.Append("job.Filters.Add(DefaultColorFilters.Sepia)" + crlf)
                Case 2
                    ' Invert
                    sbCode.Append("job.Filters.Add(DefaultColorFilters.Invert)" + crlf)
            End Select
        End If

        If (Me.cbImageAdjustmentsFilter.Checked) Then
            Select Me.ddlImageAdjustmentsFilter.SelectedIndex
                Case 0
                    ' Decrease brightness
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(-60, 0))" + crlf)
                Case 1
                    ' Decrease contrast
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(0, -50))" + crlf)
                Case 2
                    ' Decrease saturation
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(0, 0, 0, -80))" + crlf)
                Case 3
                    ' Increase brightness
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(50, 0))" + crlf)
                Case 4
                    ' Increase contrast
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(0, 40))" + crlf)
                Case 5
                    ' Increase saturation
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(0, 0, 0, 70))" + crlf)
                Case 6
                    ' Change hue
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(0, 0, 60, 0))" + crlf)
                Case 7
                    ' Color combination #1
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(42, 42, 0, -72))" + crlf)
                Case 8
                    ' Color combination #2
                    sbCode.Append("job.Filters.Add(New ImageAdjustmentsFilter(0, 22, 180, 20))" + crlf)
            End Select
        End If

        sbCode.Append(crlf)
        sbCode.Append("' Process the image" + crlf)
        sbCode.Append("job.SaveProcessedImageToFileSystem(sourceImage, outputImage)" + crlf)

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub

End Class
