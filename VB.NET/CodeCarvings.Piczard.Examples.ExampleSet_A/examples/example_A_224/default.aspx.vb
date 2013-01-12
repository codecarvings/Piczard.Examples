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

Imports System.Drawing
Imports System.Globalization

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Filters.Watermarks

Partial Class examples_example_A_224_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        If (Not Me.IsPostBack) Then
            Me.PopulateDropdownLists()
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
        Me.phOutputContainer.Visible = False
        Me.phCodeContainer.Visible = False
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.ProcessImage()
        Me.DisplayCode()
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_224.jpg"

        Dim imageWatermark As ImageWatermark = Nothing
        Select Me.ddlImageSource.SelectedIndex
            Case 0
                ' piczardWatermark1.png
                ' In this demo the image is automatically loaded/disposed by the ImageWatermark class
                imageWatermark = New ImageWatermark("~/repository/watermark/piczardWatermark1.png")

                imageWatermark.ContentAlignment = DirectCast([Enum].Parse(GetType(ContentAlignment), Me.ddlContentAlignment.SelectedValue), ContentAlignment)
                imageWatermark.Unit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlMainUnit.SelectedValue), GfxUnit)
                imageWatermark.ContentDisplacement = New Point(Integer.Parse(Me.ddlContentDisplacementX.SelectedValue, CultureInfo.InvariantCulture), Integer.Parse(Me.ddlContentDisplacementY.SelectedValue, CultureInfo.InvariantCulture))
                imageWatermark.Alpha = Integer.Parse(Me.ddlAlpha.SelectedValue, CultureInfo.InvariantCulture)

                ' Process the image
                imageWatermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
            Case 1
                ' codeCarvingsWatermark1.gif
                ' In this demo the image is manually loaded/disposed (useful when you need to apply the same
                ' watermark to multiple images)
                Using image As LoadedImage = ImageArchiver.LoadImage("~/repository/watermark/codeCarvingsWatermark1.gif")
                    imageWatermark = New ImageWatermark(image.Image)

                    imageWatermark.ContentAlignment = DirectCast([Enum].Parse(GetType(ContentAlignment), Me.ddlContentAlignment.SelectedValue), ContentAlignment)
                    imageWatermark.Unit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlMainUnit.SelectedValue), GfxUnit)
                    imageWatermark.ContentDisplacement = New Point(Integer.Parse(Me.ddlContentDisplacementX.SelectedValue, CultureInfo.InvariantCulture), Integer.Parse(Me.ddlContentDisplacementY.SelectedValue, CultureInfo.InvariantCulture))
                    imageWatermark.Alpha = Integer.Parse(Me.ddlAlpha.SelectedValue, CultureInfo.InvariantCulture)

                    ' Process the image
                    imageWatermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
                End Using
        End Select

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

        ' Display the generated image
        Me.phOutputContainer.Visible = True
    End Sub

    Protected Sub DisplayCode()
        Dim sbCode As System.Text.StringBuilder = New System.Text.StringBuilder()

        Dim crlf As String = ControlChars.CrLf
        sbCode.Append("' Prepare the parameters" + crlf)
        sbCode.Append("Dim sourceImage As String = ""~/repository/source/temple1.jpg""" + crlf)
        sbCode.Append("Dim outputImage As String = ""~/repository/output/Ex_A_224.jpg""" + crlf)

        Select Me.ddlImageSource.SelectedIndex
            Case 0
                ' piczardWatermark1.png
                sbCode.Append("Dim wmImage As String = ""~/repository/watermark/piczardWatermark1.png""" + crlf)
                sbCode.Append(crlf)

                sbCode.Append("' Setup the ImageWatermark" + crlf)
                sbCode.Append("Dim imageWatermark As ImageWatermark = New ImageWatermark(wmImage)" + crlf)

                If (Me.ddlContentAlignment.SelectedValue <> "MiddleCenter") Then
                    ' Default value = MiddleCenter
                    sbCode.Append("imageWatermark.ContentAlignment = ContentAlignment." + Me.ddlContentAlignment.SelectedValue + crlf)
                End If
                If (Me.ddlMainUnit.SelectedValue <> "Pixel") Then
                    ' Default value = Pixel
                    sbCode.Append("imageWatermark.Unit = GfxUnit." + Me.ddlMainUnit.SelectedValue + crlf)
                End If
                If ((Me.ddlContentDisplacementX.SelectedValue <> "0") Or (Me.ddlContentDisplacementY.SelectedValue <> "0")) Then
                    ' Default value = 0:0
                    sbCode.Append("imageWatermark.ContentDisplacement = new Point(" + Me.ddlContentDisplacementX.SelectedValue + ", " + Me.ddlContentDisplacementY.SelectedValue + ")" + crlf)
                End If
                If (Me.ddlAlpha.SelectedValue <> "100") Then
                    ' Default value = 100%
                    sbCode.Append("imageWatermark.Alpha = " + Me.ddlAlpha.SelectedValue + crlf)
                End If

                sbCode.Append(crlf)
                sbCode.Append("' Process the image" + crlf)
                sbCode.Append("imageWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage)" + crlf)
            Case 1
                ' codeCarvingsWatermark1.gif
                sbCode.Append("Dim wmImage As String = ""~/repository/watermark/codeCarvingsWatermark1.gif""" + crlf)
                sbCode.Append(crlf)

                sbCode.Append("Using image As LoadedImage = ImageArchiver.LoadImage(wmImage)" + crlf)
                sbCode.Append("   ' Setup the ImageWatermark" + crlf)
                sbCode.Append("   Dim imageWatermark As ImageWatermark = New ImageWatermark(image.Image)" + crlf)

                If (Me.ddlContentAlignment.SelectedValue <> "MiddleCenter") Then
                    ' Default value = MiddleCenter
                    sbCode.Append("   imageWatermark.ContentAlignment = ContentAlignment." + Me.ddlContentAlignment.SelectedValue + crlf)
                End If
                If (Me.ddlMainUnit.SelectedValue <> "Pixel") Then
                    ' Default value = Pixel
                    sbCode.Append("   imageWatermark.Unit = GfxUnit." + Me.ddlMainUnit.SelectedValue + crlf)
                End If
                If ((Me.ddlContentDisplacementX.SelectedValue <> "0") Or (Me.ddlContentDisplacementY.SelectedValue <> "0")) Then
                    ' Default value = 0:0
                    sbCode.Append("   imageWatermark.ContentDisplacement = new Point(" + Me.ddlContentDisplacementX.SelectedValue + ", " + Me.ddlContentDisplacementY.SelectedValue + ")" + crlf)
                End If
                If (Me.ddlAlpha.SelectedValue <> "100") Then
                    ' Default value = 100%
                    sbCode.Append("   imageWatermark.Alpha = " + Me.ddlAlpha.SelectedValue + crlf)
                End If

                sbCode.Append(crlf)
                sbCode.Append("   ' Process the image" + crlf)
                sbCode.Append("   imageWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage)" + crlf)

                sbCode.Append("End Using" + crlf)
        End Select

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub

    Protected Sub PopulateDropdownLists()
        For i As Integer = -30 To 30
            Dim item As ListItem = New ListItem("X: " + i.ToString(), i.ToString())
            Me.ddlContentDisplacementX.Items.Add(item)

            If (i = 0) Then
                ' Default value = 0
                item.Selected = True
            End If
        Next
        For i As Integer = -30 To 30
            Dim item As ListItem = New ListItem("Y: " + i.ToString(), i.ToString())
            Me.ddlContentDisplacementY.Items.Add(item)

            If (i = 0) Then
                ' Default value = 0
                item.Selected = True
            End If
        Next

        For i As Integer = 0 To 100
            Dim item As ListItem = New ListItem(i.ToString() + "%", i.ToString())
            Me.ddlAlpha.Items.Add(item)

            If (i = 100) Then
                ' Default value = 100
                item.Selected = True
            End If
        Next
    End Sub

End Class
