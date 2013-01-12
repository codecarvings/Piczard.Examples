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

Partial Class examples_example_A_212_default
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

        Me.phCropMode_Fixed.Visible = False
        Me.phCropMode_FixedAspectRatio.Visible = False
        Me.phCropMode_Free.Visible = False
        Me.UpdatePanel1.FindControl("phCropMode_" + Me.ddlCropMode.SelectedValue).Visible = True
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.ProcessImage()
        Me.DisplayCode()
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_212.jpg"

        ' Setup the crop filter
        Dim cropFilter As CropConstraint = Nothing
        Select Case Me.ddlCropMode.SelectedValue
            Case "Fixed"
                Dim fixedCropConstraint As FixedCropConstraint = Nothing
                Select Case Me.ddlConstraints_Fixed.SelectedIndex
                    Case 0
                        ' 180 x 100 Pixel
                        fixedCropConstraint = New FixedCropConstraint(180, 100)
                    Case 1
                        ' 50 x 100 Mm (96 DPI)
                        fixedCropConstraint = New FixedCropConstraint(GfxUnit.Mm, 50.0F, 100.0F)
                End Select
                cropFilter = fixedCropConstraint
            Case "FixedAspectRatio"
                Dim fixedAspectRatioCropFilter As FixedAspectRatioCropConstraint = Nothing
                Select Case Me.ddlAspectRatio.SelectedIndex
                    Case 0
                        fixedAspectRatioCropFilter = New FixedAspectRatioCropConstraint(1.0F / 1.0F)
                    Case 1
                        fixedAspectRatioCropFilter = New FixedAspectRatioCropConstraint(2.0F / 1.0F)
                    Case 2
                        fixedAspectRatioCropFilter = New FixedAspectRatioCropConstraint(1.0F / 2.0F)
                    Case 3
                        fixedAspectRatioCropFilter = New FixedAspectRatioCropConstraint(16.0F / 9.0F)
                End Select
                Select Case Me.ddlConstraints_FixedAspectRatio.SelectedIndex
                    Case 0
                        ' No constraint
                    Case 1
                        ' Min width: 500px
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Width
                        fixedAspectRatioCropFilter.Min = 500
                    Case 2
                        ' Min height: 5.2 inch (96 DPI)
                        fixedAspectRatioCropFilter.Unit = GfxUnit.Inch
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Height
                        fixedAspectRatioCropFilter.Min = 5.2F
                    Case 3
                        ' Max width: 200px
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Width
                        fixedAspectRatioCropFilter.Max = 200
                    Case 4
                        ' Max height: 5 cm (96 DPI)
                        fixedAspectRatioCropFilter.Unit = GfxUnit.Cm
                        fixedAspectRatioCropFilter.LimitedDimension = SizeDimension.Height
                        fixedAspectRatioCropFilter.Max = 5.0F
                End Select
                cropFilter = fixedAspectRatioCropFilter
            Case "Free"
                Dim freeCropFilter As FreeCropConstraint = Nothing
                Select Case Me.ddlConstraints_Free.SelectedIndex
                    Case 0
                        ' No constraints
                        freeCropFilter = New FreeCropConstraint()
                    Case 1
                        ' Max width: 200px
                        freeCropFilter = New FreeCropConstraint(Nothing, 200, Nothing, Nothing)
                    Case 2
                        ' Max height: 5 cm (96 DPI)
                        freeCropFilter = New FreeCropConstraint(GfxUnit.Cm, Nothing, Nothing, Nothing, 5.0F)
                    Case 3
                        ' Fixed width: 4.1 inch (96 DPI)
                        freeCropFilter = New FreeCropConstraint(GfxUnit.Inch, 4.1F, 4.1F, Nothing, Nothing)
                    Case 4
                        ' Fixed height: 400px
                        freeCropFilter = New FreeCropConstraint(Nothing, Nothing, 400, 400)
                End Select
                cropFilter = freeCropFilter
        End Select
        cropFilter.DefaultImageSelectionStrategy = DirectCast([Enum].Parse(GetType(CropConstraintImageSelectionStrategy), Me.ddlImageSelectionStrategy.SelectedValue), CropConstraintImageSelectionStrategy)
        If (Me.ddlMargins.SelectedValue <> "Auto") Then
            ' Disabled both the margins (horizontal and vertical).
            ' Note: By default the margins are automatically calculated
            cropFilter.Margins.SetZero()
        End If

        ' Set the canvas color
        cropFilter.CanvasColor = System.Drawing.ColorTranslator.FromHtml(Me.txtCanvasColor.Text)

        ' Process the image
        cropFilter.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)

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
        sbCode.Append("Dim outputImage As String = ""~/repository/output/Ex_A_212.jpg""" + crlf)
        sbCode.Append(crlf)

        sbCode.Append("' Create the cropping filter" + crlf)
        Select Case Me.ddlCropMode.SelectedValue
            Case "Fixed"
                Select Case Me.ddlConstraints_Fixed.SelectedIndex
                    Case 0
                        sbCode.Append("Dim cropFilter As FixedCropConstraint = New FixedCropConstraint(180, 100)" + crlf)
                    Case 1
                        sbCode.Append("Dim cropFilter As FixedCropConstraint = New FixedCropConstraint(GfxUnit.Mm, 50.0F, 100.0F)" + crlf)
                End Select
            Case "FixedAspectRatio"
                Select Case Me.ddlAspectRatio.SelectedIndex
                    Case 0
                        sbCode.Append("Dim cropFilter As FixedAspectRatioCropConstraint = New FixedAspectRatioCropConstraint(1.0F / 1.0F)" + crlf)
                    Case 1
                        sbCode.Append("Dim cropFilter As FixedAspectRatioCropConstraint = New FixedAspectRatioCropConstraint(2.0F / 1.0F)" + crlf)
                    Case 2
                        sbCode.Append("Dim cropFilter As FixedAspectRatioCropConstraint = New FixedAspectRatioCropConstraint(1.0F / 2.0F)" + crlf)
                    Case 3
                        sbCode.Append("Dim cropFilter As FixedAspectRatioCropConstraint = New FixedAspectRatioCropConstraint(16.0F / 9.0F)" + crlf)
                End Select
                Select Case Me.ddlConstraints_FixedAspectRatio.SelectedIndex
                    Case 1
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Width" + crlf)
                        sbCode.Append("cropFilter.Min = 500" + crlf)
                    Case 2
                        sbCode.Append("cropFilter.Unit = GfxUnit.Inch" + crlf)
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Height" + crlf)
                        sbCode.Append("cropFilter.Min = 5.2F" + crlf)
                    Case 3
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Width" + crlf)
                        sbCode.Append("cropFilter.Max = 200" + crlf)
                    Case 4
                        sbCode.Append("cropFilter.Unit = GfxUnit.Cm" + crlf)
                        sbCode.Append("cropFilter.LimitedDimension = SizeDimension.Height" + crlf)
                        sbCode.Append("cropFilter.Max = 5.0F" + crlf)
                End Select
            Case "Free"
                Select Case Me.ddlConstraints_Free.SelectedIndex
                    Case 0
                        sbCode.Append("Dim cropFilter As FreeCropConstraint = New FreeCropConstraint()" + crlf)
                    Case 1
                        sbCode.Append("Dim cropFilter As FreeCropConstraint = New FreeCropConstraint(Nothing, 200, Nothing, Nothing)" + crlf)
                    Case 2
                        sbCode.Append("Dim cropFilter As FreeCropConstraint = New FreeCropConstraint(GfxUnit.Cm, Nothing, Nothing, Nothing, 5.0F)" + crlf)
                    Case 3
                        sbCode.Append("Dim cropFilter As FreeCropConstraint = New FreeCropConstraint(GfxUnit.Inch, 4.1F, 4.1F, Nothing, Nothing)" + crlf)
                    Case 4
                        sbCode.Append("Dim cropFilter As FreeCropConstraint = New FreeCropConstraint(Nothing, Nothing, 400, 400)" + crlf)
                End Select
        End Select

        If (Me.ddlImageSelectionStrategy.SelectedValue <> "Slice") Then
            ' Default value: Slice
            sbCode.Append("cropFilter.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy." + Me.ddlImageSelectionStrategy.SelectedValue + crlf)
        End If
        If (Me.ddlMargins.SelectedValue <> "Auto") Then
            ' Default value: Auto margins
            sbCode.Append("cropFilter.Margins.SetZero()" + crlf)
        End If
        sbCode.Append("cropFilter.CanvasColor = System.Drawing.ColorTranslator.FromHtml(""" + Me.txtCanvasColor.Text + """)")

        sbCode.Append(crlf)
        sbCode.Append("' Process the image" + crlf)
        sbCode.Append("cropFilter.SaveProcessedImageToFileSystem(sourceImage, outputImage)")

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub

End Class
