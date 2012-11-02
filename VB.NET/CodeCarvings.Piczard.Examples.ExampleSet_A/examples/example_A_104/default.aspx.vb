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

Partial Class examples_example_A_104_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        End If

        If (Not Me.ScriptManager1.IsInAsyncPostBack) Then
            ' Load the Galleria Script
            ExamplesHelper.LoadLibrary_Galleria(Me)
        End If

        If (Not Me.IsPostBack) Then
            ' Display the source images
            Me.displaySourceImages()
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
        Me.ddlImageFormat.Enabled = Me.rbFormatCustom.Checked
        Me.phOutputContainer.Visible = False
        Me.phCodeContainer.Visible = False
    End Sub

    Protected Const SourceImagesFolder As String = "~/repository/source/set1/"
    Protected Const OutputImagesFolder As String = "~/repository/output/Ex_A_104/"

    Protected Sub processImages()
        ' Delete the previously generated files
        Dim oldOutputImages As String() = System.IO.Directory.GetFiles(Server.MapPath(OutputImagesFolder))
        For Each oldOutputImage As String In oldOutputImages
            System.IO.File.Delete(oldOutputImage)
        Next

        ' Create a new image processing job
        Dim job As ImageProcessingJob = New ImageProcessingJob()

        If (Me.cbFilterCrop.Checked) Then
            ' Add the crop filter
            job.Filters.Add(New FixedCropConstraint(380, 320))
        End If

        If (Me.cbFilterColor.Checked) Then
            job.Filters.Add(DefaultColorFilters.Sepia)
        End If

        If (Me.cbFilterWatermark.Checked) Then
            ' Add the watermark
            Dim textWatermark As TextWatermark = New TextWatermark()
            textWatermark.ContentAlignment = ContentAlignment.BottomRight
            textWatermark.ForeColor = Color.FromArgb(230, Color.Black)
            textWatermark.Text = "Image processed by Piczard"
            textWatermark.Font.Size = 12
            job.Filters.Add(textWatermark)
        End If

        ' Get the files to process
        Dim sourceImages As String() = System.IO.Directory.GetFiles(Server.MapPath(SourceImagesFolder))

        For i As Integer = 0 To sourceImages.Length - 1
            Dim sourceFilePath As String = sourceImages(i)
            Dim sourceFileNameWithoutExtension As String = System.IO.Path.GetFileNameWithoutExtension(sourceFilePath)

            ' Get the output image format
            Dim outputFormat As FormatEncoderParams
            If (Me.rbFormatCustom.Checked) Then
                ' Custom format
                outputFormat = ImageArchiver.GetFormatEncoderParamsFromFileExtension(Me.ddlImageFormat.SelectedValue)
            Else
                ' Same format as the source image
                outputFormat = ImageArchiver.GetFormatEncoderParamsFromFilePath(sourceFilePath)
            End If

            Dim outputFileName As String = sourceFileNameWithoutExtension + outputFormat.FileExtension
            Dim outputFilePath As String = System.IO.Path.Combine(OutputImagesFolder, outputFileName)

            ' Process the image
            job.SaveProcessedImageToFileSystem(sourceFilePath, outputFilePath)
        Next
    End Sub

    Protected Sub displaySourceImages()
        ' Dispaly all the source images
        Dim sourceImages As String() = System.IO.Directory.GetFiles(Server.MapPath(SourceImagesFolder))

        Me.rptSourceImages.DataSource = sourceImages
        Me.rptSourceImages.DataBind()
    End Sub

    Protected Sub rptSourceImages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSourceImages.ItemDataBound
        If (e.Item.DataItem IsNot Nothing) Then
            ' Display the source image
            Dim filePath As String = DirectCast(e.Item.DataItem, String)
            Dim fileName As String = System.IO.Path.GetFileName(filePath)

            Dim image As System.Web.UI.WebControls.Image = DirectCast(e.Item.FindControl("imgSource"), System.Web.UI.WebControls.Image)
            image.AlternateText = "Source image '" + fileName + "'"
            image.ImageUrl = SourceImagesFolder + fileName
        End If
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        ' Process the images and display the result
        Me.processImages()
        Me.displayOutputImages()
        Me.phOutputContainer.Visible = True

        ' Generate the source code
        Me.GenerateExampleCode()
        Me.phCodeContainer.Visible = True
    End Sub

    Protected Sub displayOutputImages()
        ' Dispaly all the output images
        Dim outputImages As String() = System.IO.Directory.GetFiles(Server.MapPath(OutputImagesFolder))

        Me.rptOutputImages.DataSource = outputImages
        Me.rptOutputImages.DataBind()
    End Sub

    Protected Sub rptOutputImages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptOutputImages.ItemDataBound
        If (e.Item.DataItem IsNot Nothing) Then
            ' Display the image
            Dim filePath As String = DirectCast(e.Item.DataItem, String)
            Dim fileName As String = System.IO.Path.GetFileName(filePath)

            Dim image As System.Web.UI.WebControls.Image = DirectCast(e.Item.FindControl("imgOutput"), System.Web.UI.WebControls.Image)
            image.AlternateText = "Output image '" + fileName + "'"
            ' Use a timestamp to force the reloading of the image
            image.ImageUrl = OutputImagesFolder + fileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()
        End If
    End Sub

    Protected Sub GenerateExampleCode()
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

        sb.Append("' Prepare the parameters" + ControlChars.CrLf)
        sb.Append("Dim sourceImagesFolder As String = ""~/repository/source/set1/""" + ControlChars.CrLf)
        sb.Append("Dim outputImagesFolder As String = ""~/repository/output/Ex_A_104/""" + ControlChars.CrLf)

        sb.Append(ControlChars.CrLf)
        sb.Append("' Create a new image processing job" + ControlChars.CrLf)
        sb.Append("Dim job As ImageProcessingJob = New ImageProcessingJob()" + ControlChars.CrLf)

        If (Me.cbFilterCrop.Checked) Then
            sb.Append(ControlChars.CrLf)
            sb.Append("' Add the crop filter" + ControlChars.CrLf)
            sb.Append("job.Filters.Add(New FixedCropConstraint(380, 320))" + ControlChars.CrLf)
        End If

        If (Me.cbFilterColor.Checked) Then
            sb.Append(ControlChars.CrLf)
            sb.Append("' Add the sepia tone filter" + ControlChars.CrLf)
            sb.Append("job.Filters.Add(DefaultColorFilters.Sepia)" + ControlChars.CrLf)
        End If

        If (Me.cbFilterWatermark.Checked) Then
            sb.Append(ControlChars.CrLf)
            sb.Append("' Add the watermark" + ControlChars.CrLf)
            sb.Append("Dim textWatermark As TextWatermark = New TextWatermark()" + ControlChars.CrLf)
            sb.Append("textWatermark.ContentAlignment = ContentAlignment.BottomRight" + ControlChars.CrLf)
            sb.Append("textWatermark.ForeColor = Color.FromArgb(230, 0, 32, 0)" + ControlChars.CrLf)
            sb.Append("textWatermark.Text = ""Image processed by Piczard""" + ControlChars.CrLf)
            sb.Append("textWatermark.Font.Size = 12" + ControlChars.CrLf)
            sb.Append("job.Filters.Add(textWatermark)" + ControlChars.CrLf)
        End If

        sb.Append(ControlChars.CrLf)
        sb.Append("' Get the files to process" + ControlChars.CrLf)
        sb.Append("Dim sourceImages As String() = System.IO.Directory.GetFiles(Server.MapPath(sourceImagesFolder))" + ControlChars.CrLf)

        sb.Append("For i As Integer = 0 To sourceImages.Length" + ControlChars.CrLf)
        sb.Append("    Dim sourceFilePath As String = sourceImages(i)" + ControlChars.CrLf)
        sb.Append("    Dim sourceFileNameWithoutExtension As String = System.IO.Path.GetFileNameWithoutExtension(sourceFilePath)" + ControlChars.CrLf)
        sb.Append(ControlChars.CrLf)
        If (Me.rbFormatCustom.Checked) Then
            sb.Append("    ' Get the output image format (custom - " + Me.ddlImageFormat.SelectedValue + ")" + ControlChars.CrLf)
            Select Case Me.ddlImageFormat.SelectedValue
                Case ".JPG"
                    sb.Append("    Dim outputFormat As FormatEncoderParams = New JpegFormatEncoderParams()" + ControlChars.CrLf)
                Case ".GIF"
                    sb.Append("    Dim outputFormat As FormatEncoderParams = New GifFormatEncoderParams()" + ControlChars.CrLf)
                Case ".PNG"
                    sb.Append("    Dim outputFormat As FormatEncoderParams = New PngFormatEncoderParams()" + ControlChars.CrLf)
            End Select
        Else
            sb.Append("    ' Get the output image format (same format As the source image)" + ControlChars.CrLf)
            sb.Append("    Dim outputFormat As FormatEncoderParams = ImageArchiver.GetFormatEncoderParamsFromFilePath(sourceFilePath)" + ControlChars.CrLf)
        End If

        sb.Append(ControlChars.CrLf)
        sb.Append("    Dim outputFileName As String = sourceFileNameWithoutExtension + outputFormat.FileExtension" + ControlChars.CrLf)
        sb.Append("    Dim outputFilePath As String = System.IO.Path.Combine(outputImagesFolder, outputFileName)" + ControlChars.CrLf)

        sb.Append(ControlChars.CrLf)
        sb.Append("    ' Process the image" + ControlChars.CrLf)
        sb.Append("    job.SaveProcessedImageToFileSystem(sourceFilePath, outputFilePath)" + ControlChars.CrLf)
        sb.Append("Next" + ControlChars.CrLf)

        Me.litCode.Text = sb.ToString()
    End Sub

End Class
