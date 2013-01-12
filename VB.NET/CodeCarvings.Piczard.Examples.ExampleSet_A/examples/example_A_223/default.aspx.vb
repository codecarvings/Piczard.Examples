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

Partial Class examples_example_A_223_default
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

        If (Not Me.IsPostBack) Then
            Me.PopulateDropdownLists()
            Me.txtText.Text = "Piczard - " + DateTime.Now.ToString()
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
        Dim outputImageFileName As String = "~/repository/output/Ex_A_223.jpg"

        Dim textWatermark As TextWatermark = New TextWatermark()
        textWatermark.Text = Me.txtText.Text
        textWatermark.ContentAlignment = DirectCast([Enum].Parse(GetType(ContentAlignment), Me.ddlContentAlignment.SelectedValue), ContentAlignment)
        textWatermark.Unit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlMainUnit.SelectedValue), GfxUnit)
        textWatermark.ContentDisplacement = New Point(Integer.Parse(Me.ddlContentDisplacementX.SelectedValue, CultureInfo.InvariantCulture), Integer.Parse(Me.ddlContentDisplacementY.SelectedValue, CultureInfo.InvariantCulture))
        textWatermark.ForeColor = Color.FromArgb(Integer.Parse(Me.ddlForeColorAlpha.SelectedValue, CultureInfo.InvariantCulture), ColorTranslator.FromHtml(Me.txtForeColor.Text))
        textWatermark.Font.Name = Me.ddlFontName.SelectedValue
        textWatermark.Font.Size = Single.Parse(Me.ddlFontSize.SelectedValue, CultureInfo.InvariantCulture)
        textWatermark.Font.Bold = Me.cbFontBold.Checked
        textWatermark.Font.Italic = Me.cbFontItalic.Checked
        textWatermark.Font.Underline = Me.cbFontUnderline.Checked
        textWatermark.Font.Strikeout = Me.cbFontStrikeout.Checked
        textWatermark.TextRenderingHint = DirectCast([Enum].Parse(GetType(System.Drawing.Text.TextRenderingHint), Me.ddlTextRenderingHint.SelectedValue), System.Drawing.Text.TextRenderingHint)
        textWatermark.TextContrast = Integer.Parse(Me.ddlTextContrast.SelectedValue, CultureInfo.InvariantCulture)

        Using stringFormat As StringFormat = New StringFormat()
            ' Setup the string format parameters
            If (Me.cbStringFormatFlags_DirectionVertical.Checked) Then
                stringFormat.FormatFlags = stringFormat.FormatFlags Or StringFormatFlags.DirectionVertical
            End If

            textWatermark.StringFormat = stringFormat

            ' Process the image
            textWatermark.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
        End Using

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
        sbCode.Append("Dim outputImage As String = ""~/repository/output/Ex_A_223.jpg""" + crlf)
        sbCode.Append(crlf)

        sbCode.Append("' Setup the TextWatermark" + crlf)
        sbCode.Append("Dim textWatermark As TextWatermark = New TextWatermark()" + crlf)
        sbCode.Append("textWatermark.Text = """ + Me.txtText.Text + """" + crlf)
        If (Me.ddlContentAlignment.SelectedValue <> "MiddleCenter") Then
            ' Default value = MiddleCenter
            sbCode.Append("textWatermark.ContentAlignment = ContentAlignment." + Me.ddlContentAlignment.SelectedValue + crlf)
        End If
        If (Me.ddlMainUnit.SelectedValue <> "Pixel") Then
            ' Default value = Pixel
            sbCode.Append("textWatermark.Unit = GfxUnit." + Me.ddlMainUnit.SelectedValue + crlf)
        End If
        If ((Me.ddlContentDisplacementX.SelectedValue <> "0") Or (Me.ddlContentDisplacementY.SelectedValue <> "0")) Then
            ' Default value = 0:0
            sbCode.Append("textWatermark.ContentDisplacement = New Point(" + Me.ddlContentDisplacementX.SelectedValue + ", " + Me.ddlContentDisplacementY.SelectedValue + ")" + crlf)
        End If
        If ((Me.txtForeColor.Text <> "#606060") Or (Me.ddlForeColorAlpha.SelectedValue <> "255")) Then
            ' Default value = #606060 - Alpha:255
            sbCode.Append("textWatermark.ForeColor = Color.FromArgb(" + Me.ddlForeColorAlpha.SelectedValue + ", ColorTranslator.FromHtml(""" + Me.txtForeColor.Text + """))" + crlf)
        End If
        If ((Me.ddlFontName.SelectedValue <> "GenericSansSerif")) Then
            ' Default value = GenericSansSerif
            sbCode.Append("textWatermark.Font.Name = """ + Me.ddlFontName.SelectedValue + """" + crlf)
        End If
        If (Me.ddlFontSize.SelectedValue <> "12") Then
            ' Default value = 12
            sbCode.Append("textWatermark.Font.Size = " + Me.ddlFontSize.SelectedValue + ".0F" + crlf)
        End If
        If (Me.cbFontBold.Checked) Then
            ' Default value = false
            sbCode.Append("textWatermark.Font.Bold = true" + crlf)
        End If
        If (Me.cbFontItalic.Checked) Then
            ' Default value = false
            sbCode.Append("textWatermark.Font.Italic = true" + crlf)
        End If
        If (Me.cbFontUnderline.Checked) Then
            ' Default value = false
            sbCode.Append("textWatermark.Font.Underline = true" + crlf)
        End If
        If (Me.cbFontStrikeout.Checked) Then
            ' Default value = false
            sbCode.Append("textWatermark.Font.Strikeout = true" + crlf)
        End If
        If (Me.ddlTextRenderingHint.SelectedValue <> "ClearTypeGridFit") Then
            ' Default value = ClearTypeGridFit
            sbCode.Append("textWatermark.TextRenderingHint = System.Drawing.Text.TextRenderingHint." + Me.ddlTextRenderingHint.SelectedValue + crlf)
        End If
        If (Me.ddlTextContrast.SelectedValue <> "4") Then
            ' Default value = 4
            sbCode.Append("textWatermark.TextContrast = " + Me.ddlTextContrast.SelectedValue + crlf)
        End If

        If (Me.cbStringFormatFlags_DirectionVertical.Checked) Then
            ' The StringFormat must be disposed
            sbCode.Append("Using stringFormat As StringFormat = New StringFormat()" + crlf)
            sbCode.Append("   ' Vertical text" + crlf)
            sbCode.Append("   stringFormat.FormatFlags = stringFormat.FormatFlags Or StringFormatFlags.DirectionVertical" + crlf)
            sbCode.Append("   textWatermark.StringFormat = stringFormat" + crlf)
            sbCode.Append(crlf)
            sbCode.Append("   ' Process the image" + crlf)
            sbCode.Append("   textWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage)" + crlf)
            sbCode.Append("End Using" + crlf)
        Else
            sbCode.Append(crlf)
            sbCode.Append("' Process the image" + crlf)
            sbCode.Append("textWatermark.SaveProcessedImageToFileSystem(sourceImage, outputImage)" + crlf)
        End If

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

        For i As Integer = 0 To 255
            Dim item As ListItem = New ListItem(i.ToString(), i.ToString())
            Me.ddlForeColorAlpha.Items.Add(item)

            If (i = 255) Then
                ' Default value = 255
                item.Selected = True
            End If
        Next

        For i As Integer = 6 To 32
            Dim item As ListItem = New ListItem(i.ToString(), i.ToString())
            Me.ddlFontSize.Items.Add(item)

            If (i = 12) Then
                ' Default value = 12
                item.Selected = True
            End If
        Next

        For i As Integer = 0 To 12
            Dim item As ListItem = New ListItem(i.ToString(), i.ToString())
            Me.ddlTextContrast.Items.Add(item)

            If (i = 4) Then
                ' Default value = 4
                item.Selected = True
            End If
        Next
    End Sub

End Class
