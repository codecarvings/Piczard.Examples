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

Partial Class examples_example_A_106_default
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
        ' Process the image and display the source code
        Me.ProcessImage()
        Me.GenerateExampleCode()
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_106.jpg"

        ' Get the image processing job for the specified resolution
        Dim job As ImageProcessingJob = New ImageProcessingJob(Single.Parse(Me.ddlOutputResolution.SelectedValue, System.Globalization.CultureInfo.InvariantCulture))

        ' Setup a crop constraint
        Select Case Me.ddlCropSize.SelectedIndex
            Case 0
                ' 200 x 300 px
                job.Filters.Add(New FixedCropConstraint(200, 300))
            Case 1
                ' 50 x 75 points
                job.Filters.Add(New FixedCropConstraint(GfxUnit.Point, 50, 75))
            Case 2
                ' 20 x 30 mm
                job.Filters.Add(New FixedCropConstraint(GfxUnit.Mm, 20, 30))
            Case 3
                ' 0.6 x 0.9 inch
                job.Filters.Add(New FixedCropConstraint(GfxUnit.Inch, 0.6F, 0.9F))
        End Select

        ' Proces the image
        job.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

        ' Display the generated image
        Me.phOutputContainer.Visible = True
    End Sub

    Protected Sub GenerateExampleCode()
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

        sb.Append("' Setup an ImageProcessingJob / Output resolution = " + Me.ddlOutputResolution.SelectedValue + " DPI" + ControlChars.CrLf)
        If (Me.ddlOutputResolution.SelectedValue = "96") Then
            ' Default value = 96DPI
            sb.Append("Dim job As ImageProcessingJob = New ImageProcessingJob()" + ControlChars.CrLf)
        Else
            sb.Append("Dim job As ImageProcessingJob = New ImageProcessingJob(" + Me.ddlOutputResolution.SelectedValue + ")" + ControlChars.CrLf)
        End If

        sb.Append(ControlChars.CrLf)
        sb.Append("' Add the crop filter ")
        Select Case Me.ddlCropSize.SelectedIndex
            Case 0
                ' 200 x 300 px
                sb.Append("(200 x 300 px)" + ControlChars.CrLf)
                sb.Append("job.Filters.Add(New FixedCropConstraint(200, 300))" + ControlChars.CrLf)
            Case 1
                ' 50 x 75 points
                sb.Append("(50 x 75 points)" + ControlChars.CrLf)
                sb.Append("job.Filters.Add(New FixedCropConstraint(GfxUnit.Point, 50, 75))" + ControlChars.CrLf)
            Case 2
                ' 20 x 30 mm
                sb.Append("(20 x 30 mm)" + ControlChars.CrLf)
                sb.Append("job.Filters.Add(New FixedCropConstraint(GfxUnit.Mm, 20, 30))" + ControlChars.CrLf)
            Case 3
                ' 0.6 x 0.9 inch
                sb.Append("(0.6 x 0.9 inch)" + ControlChars.CrLf)
                sb.Append("job.Filters.Add(New FixedCropConstraint(GfxUnit.Inch, 0.6F, 0.9F))" + ControlChars.CrLf)
        End Select

        sb.Append(ControlChars.CrLf)
        sb.Append("' Process the image" + ControlChars.CrLf)
        sb.Append("Dim sourceImageFileName As String = ""~/repository/source/greece1.jpg""" + ControlChars.CrLf)
        sb.Append("Dim outputImageFileName As String = ""~/repository/output/Ex_A_106.jpg""" + ControlChars.CrLf)
        sb.Append("job.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)" + ControlChars.CrLf)

        Me.litCode.Text = sb.ToString()
        Me.phCodeContainer.Visible = True
    End Sub

End Class
