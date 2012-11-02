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

Imports System.Globalization

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web
Imports CodeCarvings.Piczard.Helpers

Partial Class examples_example_A_302_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID

        Me.phCropMode_Fixed.Visible = False
        Me.phCropMode_FixedAspectRatio.Visible = False
        Me.phCropMode_Free.Visible = False
        Me.UpdatePanel1.FindControl("phCropMode_" + Me.ddlCropMode.SelectedValue).Visible = True

        Me.ddlConstraint_FixedAspectRatio_Min.Enabled = Me.cb_FixedAspectRatio_Min.Checked
        Me.ddlConstraint_FixedAspectRatio_Max.Enabled = Me.cb_FixedAspectRatio_Max.Checked

        Me.ddlConstraint_Free_Width_Min.Enabled = Me.cb_Free_Width_Min.Checked
        Me.ddlConstraint_Free_Width_Max.Enabled = Me.cb_Free_Width_Max.Checked
        Me.ddlConstraint_Free_Height_Min.Enabled = Me.cb_Free_Height_Min.Checked
        Me.ddlConstraint_Free_Height_Max.Enabled = Me.cb_Free_Height_Max.Checked

        If (Not Me.IsPostBack) Then
            Me.PopulateDropdownLists()
        End If
    End Sub

    Protected Sub ddlUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnit.SelectedIndexChanged
        Me.PopulateDropdownLists()
    End Sub

    Protected Sub btnLoadImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadImage.Click
        ' Update the Output resolution and the UI Unit
        Dim unit As GfxUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlUnit.SelectedValue), GfxUnit)
        Me.InlinePictureTrimmer1.UIUnit = unit

        Dim outputResolution As Single = Single.Parse(Me.ddlDPI.SelectedValue, CultureInfo.InvariantCulture)

        Dim cropConstrant As CropConstraint = Nothing
        Select Case Me.ddlCropMode.SelectedValue
            Case "Fixed"
                Dim fixedWidth As Single = Single.Parse(Me.ddlConstraint_Fixed_Width.SelectedValue, CultureInfo.InvariantCulture)
                Dim fixedHeight As Single = Single.Parse(Me.ddlConstraint_Fixed_Height.SelectedValue, CultureInfo.InvariantCulture)
                Dim fixedCropConstraint As FixedCropConstraint = New FixedCropConstraint(unit, fixedWidth, fixedHeight)
                cropConstrant = fixedCropConstraint
            Case "FixedAspectRatio"
                Dim aspectRatioX As Single = Single.Parse(Me.ddlConstraint_FixedAspectRatio_X.SelectedValue, CultureInfo.InvariantCulture)
                Dim aspectRatioY As Single = Single.Parse(Me.ddlConstraint_FixedAspectRatio_Y.SelectedValue, CultureInfo.InvariantCulture)
                Dim aspectRatio As Single = aspectRatioX / aspectRatioY
                Dim fixedAspectRatioCropConstraint As FixedAspectRatioCropConstraint = New FixedAspectRatioCropConstraint(aspectRatio)
                fixedAspectRatioCropConstraint.Unit = unit

                If ((Me.cb_FixedAspectRatio_Min.Checked) Or (Me.cb_FixedAspectRatio_Max.Checked)) Then
                    Dim minValue As Single = Single.Parse(Me.ddlConstraint_FixedAspectRatio_Min.SelectedValue, CultureInfo.InvariantCulture)
                    Dim maxValue As Single = Single.Parse(Me.ddlConstraint_FixedAspectRatio_Max.SelectedValue, CultureInfo.InvariantCulture)

                    ' Ensure that Min value is not greater than Max value
                    If ((Me.cb_FixedAspectRatio_Min.Checked) And (Me.cb_FixedAspectRatio_Max.Checked)) Then
                        If (maxValue < minValue) Then
                            ' ERROR
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "FixedAspectRatioCropConstraintError1", "alert(""Error: Min value cannot be greater than Max value."");", True)
                            Return
                        End If
                    End If

                    fixedAspectRatioCropConstraint.LimitedDimension = DirectCast([Enum].Parse(GetType(SizeDimension), Me.ddlConstraint_FixedAspectRatio_LimitedDimension.SelectedValue), SizeDimension)
                    If (Me.cb_FixedAspectRatio_Min.Checked) Then
                        fixedAspectRatioCropConstraint.Min = minValue
                    End If
                    If (Me.cb_FixedAspectRatio_Max.Checked) Then
                        fixedAspectRatioCropConstraint.Max = maxValue
                    End If
                End If

                cropConstrant = fixedAspectRatioCropConstraint
            Case "Free"
                Dim freeCropConstraint As FreeCropConstraint = New FreeCropConstraint()
                freeCropConstraint.Unit = unit

                If ((Me.cb_Free_Width_Min.Checked) Or (Me.cb_Free_Width_Max.Checked)) Then
                    Dim minWidth As Single = Single.Parse(Me.ddlConstraint_Free_Width_Min.SelectedValue, CultureInfo.InvariantCulture)
                    Dim maxWidth As Single = Single.Parse(Me.ddlConstraint_Free_Width_Max.SelectedValue, CultureInfo.InvariantCulture)

                    ' Ensure that Min width is not greater than Max width
                    If ((Me.cb_Free_Width_Min.Checked) And (Me.cb_Free_Width_Max.Checked)) Then
                        If (maxWidth < minWidth) Then
                            ' ERROR
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "FreeCropConstraintError1", "alert(""Error: Min width cannot be greater than Max width."");", True)
                            Return
                        End If
                    End If

                    If (Me.cb_Free_Width_Min.Checked) Then
                        freeCropConstraint.MinWidth = minWidth
                    End If
                    If (Me.cb_Free_Width_Max.Checked) Then
                        freeCropConstraint.MaxWidth = maxWidth
                    End If
                End If

                If ((Me.cb_Free_Height_Min.Checked) Or (Me.cb_Free_Height_Max.Checked)) Then
                    Dim minHeight As Single = Single.Parse(Me.ddlConstraint_Free_Height_Min.SelectedValue, CultureInfo.InvariantCulture)
                    Dim maxHeight As Single = Single.Parse(Me.ddlConstraint_Free_Height_Max.SelectedValue, CultureInfo.InvariantCulture)

                    ' Ensure that Min height is not greater than Max height
                    If ((Me.cb_Free_Height_Min.Checked) And (Me.cb_Free_Height_Max.Checked)) Then
                        If (maxHeight < minHeight) Then
                            ' ERROR
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "FreeCropConstraintError2", "alert(""Error: Min height cannot be greater than Max height."");", True)
                            Return
                        End If
                    End If

                    If (Me.cb_Free_Height_Min.Checked) Then
                        freeCropConstraint.MinHeight = minHeight
                    End If
                    If (Me.cb_Free_Height_Max.Checked) Then
                        freeCropConstraint.MaxHeight = maxHeight
                    End If
                End If

                cropConstrant = freeCropConstraint
        End Select

        ' Setup the margins
        If (Me.ddlMarginsH.SelectedValue = "") Then
            ' Horizontal margin = automatic
            cropConstrant.Margins.Horizontal = Nothing
        Else
            ' Hortizontal margin - custom value
            cropConstrant.Margins.Horizontal = Single.Parse(Me.ddlMarginsH.SelectedValue, CultureInfo.InvariantCulture)
        End If
        If (Me.ddlMarginsV.SelectedValue = "") Then
            ' Horizontal margin = automatic
            cropConstrant.Margins.Vertical = Nothing
        Else
            ' Hortizontal margin - custom value
            cropConstrant.Margins.Vertical = Single.Parse(Me.ddlMarginsV.SelectedValue, CultureInfo.InvariantCulture)
        End If

        ' Setup the DefaultImageSelectionStrategy
        cropConstrant.DefaultImageSelectionStrategy = DirectCast([Enum].Parse(GetType(CropConstraintImageSelectionStrategy), Me.ddlImageSelectionStrategy.SelectedValue), CropConstraintImageSelectionStrategy)

        ' Load the image
        Me.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", outputResolution, cropConstrant)

        Me.InlinePictureTrimmer1.Visible = True
        Me.phBeforeLoad.Visible = False
        Me.phAfterLoad.Visible = True

        Me.DisplayCode()
    End Sub

    Protected Sub btnUnloadImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnloadImage.Click
        Me.InlinePictureTrimmer1.UnloadImage()
        Me.InlinePictureTrimmer1.Visible = False
        Me.phCodeContainer.Visible = False
        Me.phBeforeLoad.Visible = True
        Me.phAfterLoad.Visible = False
    End Sub

#Region "Display the code"
    Protected Sub DisplayCode()
        Dim sbCode As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim crlf As String = ControlChars.CrLf

        Dim unit As GfxUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlUnit.SelectedValue), GfxUnit)
        If (unit <> GfxUnit.Pixel) Then
            ' Default value: Pixel
            sbCode.Append("InlinePictureTrimmer1.UIUnit = GfxUnit." + unit.ToString() + crlf)
        End If

        Select Me.ddlCropMode.SelectedValue
            Case "Fixed"
                If (unit = GfxUnit.Pixel) Then
                    sbCode.Append("Dim cropConstrant As FixedCropConstraint = new FixedCropConstraint(" + Me.ddlConstraint_Fixed_Width.SelectedValue + ", " + Me.ddlConstraint_Fixed_Height.SelectedValue + ")" + crlf)
                Else
                    sbCode.Append("Dim cropConstrant As FixedCropConstraint = new FixedCropConstraint(GfxUnit." + unit.ToString() + ", " + Me.ddlConstraint_Fixed_Width.SelectedValue + "F, " + Me.ddlConstraint_Fixed_Height.SelectedValue + "F)" + crlf)
                End If
            Case "FixedAspectRatio"
                sbCode.Append("Dim cropConstrant As FixedAspectRatioCropConstraint = new FixedAspectRatioCropConstraint(" + Me.ddlConstraint_FixedAspectRatio_X.SelectedValue + "F/" + Me.ddlConstraint_FixedAspectRatio_Y.SelectedValue + "F)" + crlf)
                If ((Me.cb_FixedAspectRatio_Min.Checked) Or (Me.cb_FixedAspectRatio_Max.Checked)) Then
                    If (unit <> GfxUnit.Pixel) Then
                        sbCode.Append("cropConstrant.Unit = GfxUnit." + unit.ToString() + crlf)
                    End If

                    Dim limitedDimension As SizeDimension = DirectCast([Enum].Parse(GetType(SizeDimension), Me.ddlConstraint_FixedAspectRatio_LimitedDimension.SelectedValue), SizeDimension)
                    If (limitedDimension <> SizeDimension.Width) Then
                        ' Default value = Width
                        sbCode.Append("cropConstrant.LimitedDimension = SizeDimension." + limitedDimension.ToString() + crlf)
                    End If

                    If (Me.cb_FixedAspectRatio_Min.Checked) Then
                        sbCode.Append("cropConstrant.Min = " + Me.ddlConstraint_FixedAspectRatio_Min.SelectedValue + "F" + crlf)
                    End If
                    If (Me.cb_FixedAspectRatio_Max.Checked) Then
                        sbCode.Append("cropConstrant.Max = " + Me.ddlConstraint_FixedAspectRatio_Max.SelectedValue + "F" + crlf)
                    End If
                End If

            case "Free":
                sbCode.Append("Dim cropConstrant As FreeCropConstraint = new FreeCropConstraint()" + crlf)

                If ((Me.cb_Free_Width_Min.Checked) Or (Me.cb_Free_Width_Max.Checked) Or (Me.cb_Free_Height_Min.Checked) Or (Me.cb_Free_Height_Max.Checked)) Then
                    If (unit <> GfxUnit.Pixel) Then
                        sbCode.Append("cropConstrant.Unit = GfxUnit." + unit.ToString() + crlf)
                    End If

                    If (Me.cb_Free_Width_Min.Checked) Then
                        sbCode.Append("cropConstrant.MinWidth = " + Me.ddlConstraint_Free_Width_Min.SelectedValue + "F" + crlf)
                    End If
                    If (Me.cb_Free_Width_Max.Checked) Then
                        sbCode.Append("cropConstrant.MaxWidth = " + Me.ddlConstraint_Free_Width_Max.SelectedValue + "F" + crlf)
                    End If
                    If (Me.cb_Free_Height_Min.Checked) Then
                        sbCode.Append("cropConstrant.MinHeight = " + Me.ddlConstraint_Free_Height_Min.SelectedValue + "F" + crlf)
                    End If
                    If (Me.cb_Free_Height_Max.Checked) Then
                        sbCode.Append("cropConstrant.MaxHeight = " + Me.ddlConstraint_Free_Height_Max.SelectedValue + "F" + crlf)
                    End If
                End If
        End Select

        If (Me.ddlMarginsH.SelectedValue <> "") Then
            ' Default value = Auto
            sbCode.Append("cropConstrant.Margins.Horizontal = " + Me.ddlMarginsH.SelectedValue + "F" + crlf)
        End If
        If (Me.ddlMarginsV.SelectedValue <> "") Then
            ' Default value = Auto
            sbCode.Append("cropConstrant.Margins.Vertical = " + Me.ddlMarginsV.SelectedValue + "F" + crlf)
        End If

        Dim defaultImageSelectionStrategy As CropConstraintImageSelectionStrategy = DirectCast([Enum].Parse(GetType(CropConstraintImageSelectionStrategy), Me.ddlImageSelectionStrategy.SelectedValue), CropConstraintImageSelectionStrategy)
        If (defaultImageSelectionStrategy <> CropConstraintImageSelectionStrategy.Slice) Then
            ' Default value: Slice
            sbCode.Append("cropConstrant.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy." + defaultImageSelectionStrategy.ToString() + crlf)
        End If

        If (Me.ddlDPI.SelectedValue = "96") Then
            ' Default value = 96
            sbCode.Append("InlinePictureTrimmer1.LoadImageFromFileSystem(""~/repository/source/donkey1.jpg"", cropConstrant)" + crlf)
        Else
            sbCode.Append("InlinePictureTrimmer1.LoadImageFromFileSystem(""~/repository/source/donkey1.jpg"", " + Me.ddlDPI.SelectedValue + "F, cropConstrant)" + crlf)
        End If

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub
#End Region

#Region "Populate the drop down lists"
    Protected Sub PopulateDropdownLists()
        Dim minValue As Single = 1.0F
        Dim stepValue As Single = 1.0F
        Dim totSteps As Integer = 25
        Dim item As ListItem

        Dim unit As GfxUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlUnit.SelectedValue), GfxUnit)
        Select unit
            Case GfxUnit.Pixel
                stepValue = 20.0F
            Case GfxUnit.Point
                stepValue = 15.0F
            Case GfxUnit.Pica
                stepValue = 1.0F
            Case GfxUnit.Inch
                stepValue = 0.2F
            Case GfxUnit.Mm
                stepValue = 5.0F
            Case GfxUnit.Cm
                stepValue = 0.5F
        End Select
        minValue = stepValue

        Me.ddlConstraint_Fixed_Width.Items.Clear()
        Me.ddlConstraint_Fixed_Height.Items.Clear()
        For i As Integer = 0 To totSteps - 1
            Dim value As Single = minValue + Convert.ToSingle(i) * stepValue
            Dim valueString As String = StringConversionHelper.SingleToString(value)

            item = New ListItem(valueString, valueString)
            If (i = 14) Then
                item.Selected = True
            End If
            Me.ddlConstraint_Fixed_Width.Items.Add(item)

            item = New ListItem(valueString, valueString)
            If (i = 9) Then
                item.Selected = True
            End If
            Me.ddlConstraint_Fixed_Height.Items.Add(item)
        Next

        Me.ddlConstraint_FixedAspectRatio_Min.Items.Clear()
        Me.ddlConstraint_FixedAspectRatio_Max.Items.Clear()
        For i As Integer = 0 To totSteps - 1
            Dim value As Single = minValue + Convert.ToSingle(i) * stepValue
            Dim valueString As String = StringConversionHelper.SingleToString(value)

            item = New ListItem(valueString, valueString)
            If (i = 0) Then
                item.Selected = True
            End If
            Me.ddlConstraint_FixedAspectRatio_Min.Items.Add(item)

            item = New ListItem(valueString, valueString)
            If (i = (totSteps - 1)) Then
                item.Selected = True
            End If
            Me.ddlConstraint_FixedAspectRatio_Max.Items.Add(item)
        Next

        Me.ddlConstraint_Free_Width_Min.Items.Clear()
        Me.ddlConstraint_Free_Width_Max.Items.Clear()
        Me.ddlConstraint_Free_Height_Min.Items.Clear()
        Me.ddlConstraint_Free_Height_Max.Items.Clear()
        For i As Integer = 0 To totSteps - 1
            Dim value As Single = minValue + Convert.ToSingle(i) * stepValue
            Dim valueString As String = StringConversionHelper.SingleToString(value)

            item = New ListItem(valueString, valueString)
            If (i = 0) Then
                item.Selected = True
            End If
            Me.ddlConstraint_Free_Width_Min.Items.Add(item)

            item = New ListItem(valueString, valueString)
            If (i = (totSteps - 1)) Then
                item.Selected = True
            End If
            Me.ddlConstraint_Free_Width_Max.Items.Add(item)

            item = New ListItem(valueString, valueString)
            If (i = 0) Then
                item.Selected = True
            End If
            Me.ddlConstraint_Free_Height_Min.Items.Add(item)

            item = New ListItem(valueString, valueString)
            If (i = (totSteps - 1)) Then
                item.Selected = True
            End If
            Me.ddlConstraint_Free_Height_Max.Items.Add(item)
        Next

        minValue = 0.0F
        totSteps = 101
        Me.ddlMarginsH.Items.Clear()
        item = New ListItem("Auto", "")
        item.Selected = True
        Me.ddlMarginsH.Items.Add(item)
        Me.ddlMarginsV.Items.Clear()
        item = New ListItem("Auto", "")
        item.Selected = True
        Me.ddlMarginsV.Items.Add(item)
        For i As Integer = 0 To totSteps - 1
            Dim value As Single = minValue + Convert.ToSingle(i) * stepValue
            Dim valueString As String = StringConversionHelper.SingleToString(value)

            item = New ListItem(valueString, valueString)
            Me.ddlMarginsH.Items.Add(item)
            item = New ListItem(valueString, valueString)
            Me.ddlMarginsV.Items.Add(item)
        Next

        If (Me.ddlConstraint_FixedAspectRatio_X.Items.Count = 0) Then
            totSteps = 100
            For i As Integer = 1 To totSteps - 1
                Dim valueString As String = StringConversionHelper.Int32ToString(i)

                item = New ListItem(valueString, valueString)
                If (i = 16) Then
                    item.Selected = True
                End If
                Me.ddlConstraint_FixedAspectRatio_X.Items.Add(item)

                item = New ListItem(valueString, valueString)
                If (i = 9) Then
                    item.Selected = True
                End If
                Me.ddlConstraint_FixedAspectRatio_Y.Items.Add(item)
            Next
        End If

    End Sub
#End Region

End Class
