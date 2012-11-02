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

Partial Class examples_example_A_202_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        End If

        If (Not Me.ScriptManager1.IsInAsyncPostBack) Then
            ' Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(Me)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
        Me.phOutputContainer.Visible = False
        Me.phCodeContainer.Visible = False

        Me.phResizeMode_Fixed.Visible = False
        Me.phResizeMode_Scaled.Visible = False
        Me.UpdatePanel1.FindControl("phResizeMode_" + Me.ddlResizeMode.SelectedValue).Visible = True
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.ProcessImage()
        Me.DisplayCode()
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_202.jpg"

        ' Setup the resize filter
        Dim resizeFilter As ResizeConstraint = Nothing
        Select Case Me.ddlResizeMode.SelectedValue
            Case "Fixed"
                Dim fixedResizeFilter As FixedResizeConstraint = Nothing
                Select Case Me.ddlConstraints_Fixed.SelectedIndex
                    Case 0
                        ' 100 x 100 Pixel
                        fixedResizeFilter = New FixedResizeConstraint(100, 100)
                    Case 1
                        ' 150 x 250 Pixel
                        fixedResizeFilter = New FixedResizeConstraint(150, 250)
                    Case 2
                        ' 580 x 500 Pixel
                        fixedResizeFilter = New FixedResizeConstraint(580, 500)
                    Case 3
                        ' 2 x 2 Inch
                        fixedResizeFilter = New FixedResizeConstraint(GfxUnit.Inch, 2.0F, 2.0F)
                End Select

                ' Default value = Fit
                If (Me.ddlFixedImagePosition.SelectedValue <> "Fit") Then
                    fixedResizeFilter.ImagePosition = DirectCast([Enum].Parse(GetType(FixedResizeConstraintImagePosition), Me.ddlFixedImagePosition.SelectedValue), FixedResizeConstraintImagePosition)
                End If

                ' Default value = White
                Dim canvasColor As Color = System.Drawing.ColorTranslator.FromHtml(Me.txtFixedCanvasColor.Text)
                If (canvasColor <> Color.FromArgb(255, 255, 255, 255)) Then
                    fixedResizeFilter.CanvasColor = canvasColor
                End If

                ' Default value = EnlargeSmallImages:true
                If (Not Me.cbFixedEnlargeSmallImages.Checked) Then
                    fixedResizeFilter.EnlargeSmallImages = Me.cbFixedEnlargeSmallImages.Checked
                End If
                resizeFilter = fixedResizeFilter
            Case "Scaled"
                Dim scaledResizeFilter As ScaledResizeConstraint = Nothing
                Select Case Me.ddlConstraints_Scaled.SelectedIndex
                    Case 0
                        ' 100 x 100 Pixel
                        scaledResizeFilter = New ScaledResizeConstraint(100, 100)
                    Case 1
                        ' 150 x 250 Pixel
                        scaledResizeFilter = New ScaledResizeConstraint(150, 250)
                    Case 2
                        ' 580 x 500 Pixel
                        scaledResizeFilter = New ScaledResizeConstraint(580, 500)
                    Case 3
                        ' 2 x 2 Inch
                        scaledResizeFilter = New ScaledResizeConstraint(GfxUnit.Inch, 2.0F, 2.0F)
                End Select

                ' Default value = EnlargeSmallImages:true
                If (Not Me.cbScaledEnlargeSmallImages.Checked) Then
                    scaledResizeFilter.EnlargeSmallImages = Me.cbScaledEnlargeSmallImages.Checked
                End If

                resizeFilter = scaledResizeFilter
        End Select

        ' Process the image
        resizeFilter.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

        ' Display the generated image
        Me.phOutputContainer.Visible = True
    End Sub

    Protected Sub DisplayCode()
        Dim sbCode As System.Text.StringBuilder = New System.Text.StringBuilder()

        Dim crlf As String = ControlChars.CrLf
        sbCode.Append("' Prepare the parameters" + crlf)
        sbCode.Append("Dim sourceImage As String = ""~/repository/source/trevi1.jpg""" + crlf)
        sbCode.Append("Dim outputImage As String = ""~/repository/output/Ex_A_202.jpg""" + crlf)
        sbCode.Append(crlf)

        sbCode.Append("' Setup the resize filter" + crlf)

        Select Case Me.ddlResizeMode.SelectedValue
            Case "Fixed"
                Select Case Me.ddlConstraints_Fixed.SelectedIndex
                    Case 0
                        ' 100 x 100 Pixel
                        sbCode.Append("Dim resizeFilter As FixedResizeConstraint = New FixedResizeConstraint(100, 100)" + crlf)
                    Case 1
                        ' 150 x 250 Pixel
                        sbCode.Append("Dim resizeFilter As FixedResizeConstraint = New FixedResizeConstraint(150, 250)" + crlf)
                    Case 2
                        ' 580 x 500 Pixel
                        sbCode.Append("Dim resizeFilter As FixedResizeConstraint = New FixedResizeConstraint(580, 500)" + crlf)
                    Case 3
                        ' 2 x 2 Inch
                        sbCode.Append("Dim resizeFilter As FixedResizeConstraint = New FixedResizeConstraint(GfxUnit.Inch, 2.0F, 2.0F)" + crlf)
                End Select

                ' Default value = Fit
                If (Me.ddlFixedImagePosition.SelectedValue <> "Fit") Then
                    sbCode.Append("resizeFilter.ImagePosition = FixedResizeConstraintImagePosition." + Me.ddlFixedImagePosition.SelectedValue + crlf)
                End If

                ' Default color = White
                Dim canvasColor As Color = System.Drawing.ColorTranslator.FromHtml(Me.txtFixedCanvasColor.Text)
                If (canvasColor <> Color.FromArgb(255, 255, 255, 255)) Then
                    sbCode.Append("resizeFilter.CanvasColor = System.Drawing.ColorTranslator.FromHtml(""" + Me.txtFixedCanvasColor.Text + """)" + crlf)
                End If

                ' Default value = EnlargeSmallImages:true
                If (Not Me.cbFixedEnlargeSmallImages.Checked) Then
                    sbCode.Append("resizeFilter.EnlargeSmallImages = " + Me.cbFixedEnlargeSmallImages.Checked.ToString(System.Globalization.CultureInfo.InvariantCulture).ToLower() + crlf)
                End If
            Case "Scaled"
                Select Case Me.ddlConstraints_Scaled.SelectedIndex
                    Case 0
                        ' 100 x 100 Pixel
                        sbCode.Append("Dim resizeFilter As ScaledResizeConstraint = New ScaledResizeConstraint(100, 100)" + crlf)
                    Case 1
                        ' 150 x 250 Pixel
                        sbCode.Append("Dim resizeFilter As ScaledResizeConstraint = New ScaledResizeConstraint(150, 250)" + crlf)
                    Case 2
                        ' 580 x 500 Pixel
                        sbCode.Append("Dim resizeFilter As ScaledResizeConstraint = New ScaledResizeConstraint(580, 500)" + crlf)
                    Case 3
                        ' 2 x 2 Inch
                        sbCode.Append("Dim resizeFilter As ScaledResizeConstraint = New ScaledResizeConstraint(GfxUnit.Inch, 2.0F, 2.0F)" + crlf)
                End Select

                ' Default value = EnlargeSmallImages:true
                If (Not Me.cbScaledEnlargeSmallImages.Checked) Then
                    sbCode.Append("resizeFilter.EnlargeSmallImages = " + Me.cbScaledEnlargeSmallImages.Checked.ToString(System.Globalization.CultureInfo.InvariantCulture).ToLower() + crlf)
                End If
        End Select

        sbCode.Append(crlf)
        sbCode.Append("' Process the image" + crlf)
        sbCode.Append("resizeFilter.SaveProcessedImageToFileSystem(sourceImage, outputImage)                   " + crlf)

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub

End Class
