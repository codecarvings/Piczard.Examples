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

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks

Partial Class examples_example_A_103_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
        Me.ddlGifMaxColors.Enabled = Me.cbGifQuantize.Checked
        Me.ddlPngMaxColors.Enabled = Me.cbPngConvertToIndex.Checked
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        ' Save the image and display the source code
        Me.SaveImage()
        Me.GenerateExampleCode()
    End Sub

    Protected Sub SaveImage()
        Dim savedImageFileName As String = ""
        Dim formatEncoderParams As FormatEncoderParams = Nothing

        Dim source As Object = Nothing
        Select Case Me.ddlSourceType.SelectedIndex
            Case 0
                ' File system
                source = Me.imgLoaded.ImageUrl
            Case 1
                ' Byte array
                source = System.IO.File.ReadAllBytes(Server.MapPath(Me.imgLoaded.ImageUrl))
            Case 2
                ' Stream
                source = System.IO.File.OpenRead(Server.MapPath(Me.imgLoaded.ImageUrl))
                ' Note: The stream will be automatically closed/disposed by the LoadedImage class
        End Select
        Using loadedImage As LoadedImage = ImageArchiver.LoadImage(source)
            Select Case Me.ddlImageFormat.SelectedValue
                Case "Auto"
                    formatEncoderParams = ImageArchiver.GetDefaultFormatEncoderParams()
                Case "JPEG"
                    formatEncoderParams = New JpegFormatEncoderParams(Integer.Parse(Me.txtJpegQuality.Text))
                Case "GIF"
                    Dim gifFormatEncoderParams As GifFormatEncoderParams = New GifFormatEncoderParams()
                    formatEncoderParams = gifFormatEncoderParams
                    gifFormatEncoderParams.QuantizeImage = Me.cbGifQuantize.Checked
                    If (Me.cbGifQuantize.Checked) Then
                        gifFormatEncoderParams.MaxColors = Integer.Parse(Me.ddlGifMaxColors.SelectedValue)
                    End If
                Case "PNG"
                    Dim pngformatEncoderParams As PngFormatEncoderParams = New PngFormatEncoderParams()
                    formatEncoderParams = pngformatEncoderParams
                    pngformatEncoderParams.ConvertToIndexed = Me.cbPngConvertToIndex.Checked
                    If (Me.cbPngConvertToIndex.Checked) Then
                        pngformatEncoderParams.MaxColors = Integer.Parse(Me.ddlPngMaxColors.SelectedValue)
                    End If
            End Select

            savedImageFileName = "~/repository/output/Ex_A_103" + formatEncoderParams.FileExtension

            ' IMPORTANT NOTE:  Apply a Noop (No Operation) filter to prevent a known GDI+ problem with transparent images
            ' Example: http://forums.asp.net/t/1235100.aspx
            Select Case Me.ddlOutputType.SelectedIndex
                Case 0
                    ' File system
                    Call New NoopFilter().SaveProcessedImageToFileSystem(loadedImage.Image, savedImageFileName, formatEncoderParams)
                Case 1
                    ' Byte array
                    Dim imageBytes As Byte() = New NoopFilter().SaveProcessedImageToByteArray(loadedImage.Image, formatEncoderParams)
                    System.IO.File.WriteAllBytes(Server.MapPath(savedImageFileName), imageBytes)
                Case 2
                    ' Stream
                    Using stream As System.IO.Stream = System.IO.File.OpenWrite(Server.MapPath(savedImageFileName))
                        Call New NoopFilter().SaveProcessedImageToStream(loadedImage.Image, stream, formatEncoderParams)
                    End Using
            End Select
        End Using

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgSaved.ImageUrl = savedImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()
        Me.phOutputPreview.Visible = True

        ' Display extension and size of both files
        Me.litLoadedImageFileDetails.Text = System.IO.Path.GetExtension(Me.imgLoaded.ImageUrl).ToUpper() + " file (" + (New System.IO.FileInfo(Server.MapPath(Me.imgLoaded.ImageUrl))).Length.ToString() + " bytes)"
        Me.litSavedImageFileDetails.Text = System.IO.Path.GetExtension(savedImageFileName).ToUpper() + " file (" + (New System.IO.FileInfo(Server.MapPath(savedImageFileName))).Length.ToString() + " bytes)"
    End Sub

    Protected Sub GenerateExampleCode()
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        sb.Append("' Load the image" + ControlChars.CrLf)

        Select Case Me.ddlSourceType.SelectedIndex
            Case 0
                ' File system
                sb.Append("Dim source As String = Server.MapPath(""~/repository/source/flowers2.png"")" + ControlChars.CrLf)
            Case 1
                ' Byte array
                sb.Append("Dim source As Byte() = System.IO.File.ReadAllBytes(Server.MapPath(""~/repository/source/flowers2.png""))" + ControlChars.CrLf)
            Case 2
                ' Stream
                sb.Append("Dim source As System.IO.Stream = System.IO.File.OpenRead(Server.MapPath(""~/repository/source/flowers2.png""))" + ControlChars.CrLf)
                sb.Append("' Note: The stream will be automatically closed/disposed by the LoadedImage class" + ControlChars.CrLf)
        End Select

        sb.Append("Using loadedImage As LoadedImage = ImageArchiver.LoadImage(source)" + ControlChars.CrLf)

        If (Me.ddlImageFormat.SelectedValue <> "Auto") Then
            sb.Append("    ' Setup the format params" + ControlChars.CrLf)
        End If

        Dim fileExtension As String = ""
        Select Case Me.ddlImageFormat.SelectedValue
            Case "Auto"
                fileExtension = ".jpg"
            Case "JPEG"
                sb.Append("    Dim formatEncoderParams As JpegFormatEncoderParams = New JpegFormatEncoderParams(" + Me.txtJpegQuality.Text + ")" + ControlChars.CrLf)
                fileExtension = ".jpg"
            Case "GIF"
                sb.Append("    Dim formatEncoderParams As GifFormatEncoderParams = New GifFormatEncoderParams()" + ControlChars.CrLf)
                sb.Append("    formatEncoderParams.QuantizeImage = " + If(Me.cbGifQuantize.Checked, "true", "false") + ControlChars.CrLf)
                If (Me.cbGifQuantize.Checked) Then
                    sb.Append("    formatEncoderParams.MaxColors = " + Me.ddlGifMaxColors.SelectedValue + ControlChars.CrLf)
                End If
                fileExtension = ".gif"
            Case "PNG"
                sb.Append("    Dim formatEncoderParams As PngFormatEncoderParams = New PngFormatEncoderParams()" + ControlChars.CrLf)
                sb.Append("    formatEncoderParams.ConvertToIndexed = " + If(Me.cbPngConvertToIndex.Checked, "true", "false") + ControlChars.CrLf)
                If (Me.cbPngConvertToIndex.Checked) Then
                    sb.Append("    formatEncoderParams.MaxColors = " + Me.ddlPngMaxColors.SelectedValue + ControlChars.CrLf)
                End If
                fileExtension = ".png"
        End Select

        If (Me.ddlImageFormat.SelectedValue <> "Auto") Then
            sb.Append(ControlChars.CrLf)
        End If

        sb.Append("    ' Save the image" + ControlChars.CrLf)
        sb.Append("    Dim outputFilePath As String = Server.MapPath(""~/repository/output/Ex_A_103" + fileExtension + """)" + ControlChars.CrLf)

        Select Case Me.ddlOutputType.SelectedIndex
            Case 0
                ' File system
                sb.Append("    ImageArchiver.SaveImageToFileSystem(loadedImage.Image, outputFilePath" + If(Me.ddlImageFormat.SelectedValue <> "Auto", ", formatEncoderParams", "") + ")" + ControlChars.CrLf)
            Case 1
                ' Byte array
                sb.Append("    Dim imageBytes As Byte() = ImageArchiver.SaveImageToByteArray(loadedImage.Image" + If(Me.ddlImageFormat.SelectedValue <> "Auto", ", formatEncoderParams", "") + ")" + ControlChars.CrLf)
                sb.Append("    System.IO.File.WriteAllBytes(outputFilePath, imageBytes)" + ControlChars.CrLf)
            Case 2
                ' Stream
                sb.Append("    Using stream As System.IO.Stream = System.IO.File.OpenWrite(outputFilePath)" + ControlChars.CrLf)
                sb.Append("        ImageArchiver.SaveImageToStream(loadedImage.Image, stream" + If(Me.ddlImageFormat.SelectedValue <> "Auto", ", formatEncoderParams", "") + ")" + ControlChars.CrLf)
                sb.Append("    End Using" + ControlChars.CrLf)
        End Select

        sb.Append("End Using" + ControlChars.CrLf)

        Me.litCode.Text = sb.ToString()
        Me.phCodeContainer.Visible = True
    End Sub

End Class
