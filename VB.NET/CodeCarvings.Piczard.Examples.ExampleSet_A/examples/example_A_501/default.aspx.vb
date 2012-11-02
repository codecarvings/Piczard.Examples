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

Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks

Partial Class examples_example_A_501_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID

        If (Not Me.IsPostBack) Then
            ' First initialization
            Me.SetPreset_1()
        End If

        If (Me.cbValidateImageSize.Checked) Then
            ' Register the event handler after every post back !
            AddHandler Me.ImageUpload1.ImageUpload, AddressOf Me.ImageUpload1_ImageUpload
        End If
    End Sub

    Protected Sub SetPreset_1()
        ' Preset: Interactive crop
        Me.cbEnableInteractiveImageProcessing.Checked = True
        Me.ddlCropConstraintMode.SelectedIndex = 0
        Me.cbAutoOpenImageEditPopupAfterUpload.Checked = True

        Me.cbPostProcessing_Resize.Checked = False
        Me.cbPostProcessing_Grayscale.Checked = False
        Me.cbPostProcessing_Watermark.Checked = False

        Me.cbPreviewConstriant.Checked = True

        Me.cbValidateImageSize.Checked = False

        Me.UpdateDemoParameters()
    End Sub

    Protected Sub SetPreset_2()
        ' Preset: Automatic resize
        Me.cbEnableInteractiveImageProcessing.Checked = False
        Me.ddlCropConstraintMode.SelectedIndex = 0
        Me.cbAutoOpenImageEditPopupAfterUpload.Checked = False

        Me.cbPostProcessing_Resize.Checked = True
        Me.cbPostProcessing_Grayscale.Checked = False
        Me.cbPostProcessing_Watermark.Checked = False

        Me.cbPreviewConstriant.Checked = True

        Me.cbValidateImageSize.Checked = False

        Me.UpdateDemoParameters()
    End Sub

    Protected Sub UpdateDemoParameters()
        ' Unload the current image if necessary
        If (Me.ImageUpload1.HasImage) Then
            Me.ImageUpload1.UnloadImage()
        End If

        Me.ddlCropConstraintMode.Enabled = Me.cbEnableInteractiveImageProcessing.Checked
        Me.cbAutoOpenImageEditPopupAfterUpload.Enabled = Me.cbEnableInteractiveImageProcessing.Checked

        Me.ImageUpload1.EnableEdit = Me.cbEnableInteractiveImageProcessing.Checked
        Me.txtInteractiveImageProcessing.Text = If(Me.ImageUpload1.EnableEdit, "", "' Disable interactive image processing features" + ControlChars.CrLf + "ImageUpload1.EnableEdit = False" + ControlChars.CrLf)
        If (Me.ImageUpload1.EnableEdit) Then
            Select Case Me.ddlCropConstraintMode.SelectedIndex
                Case 0
                    ' Crop enabled: Fixed size
                    Me.ImageUpload1.CropConstraint = New FixedCropConstraint(300, 200)
                    Me.txtInteractiveImageProcessing.Text += "' Add a fixed size crop constraint (300 x 200 pixels)" + ControlChars.CrLf
                    Me.txtInteractiveImageProcessing.Text += "ImageUpload1.CropConstraint = New FixedCropConstraint(300, 200)" + ControlChars.CrLf
                Case 1
                    ' Crop enabled: Fixed aspect ratio
                    Me.ImageUpload1.CropConstraint = New FixedAspectRatioCropConstraint(16.0F / 9.0F)
                    Me.txtInteractiveImageProcessing.Text += "' Add a fixed aspect ratio crop constraint (asperct ratio = 16:9)" + ControlChars.CrLf
                    Me.txtInteractiveImageProcessing.Text += "ImageUpload1.CropConstraint = New FixedAspectRatioCropConstraint(16F / 9F)" + ControlChars.CrLf
                Case 2
                    ' Crop enabled: Free size
                    Me.ImageUpload1.CropConstraint = New FreeCropConstraint()
                    Me.txtInteractiveImageProcessing.Text += "' Add a free crop constraint" + ControlChars.CrLf
                    Me.txtInteractiveImageProcessing.Text += "ImageUpload1.CropConstraint = New FreeCropConstraint()" + ControlChars.CrLf
                Case 3
                    ' Crop disabled
                    Me.ImageUpload1.CropConstraint = Nothing
            End Select
        Else
            ' Reset the CropConstraint
            Me.ImageUpload1.CropConstraint = Nothing
        End If

        Me.ImageUpload1.AutoOpenImageEditPopupAfterUpload = If(Me.ddlCropConstraintMode.Enabled, Me.cbAutoOpenImageEditPopupAfterUpload.Checked, False)

        Me.txtInteractiveImageProcessing.Text += If(Me.ImageUpload1.AutoOpenImageEditPopupAfterUpload, "' Automatically open image edit window after upload" + ControlChars.CrLf + "ImageUpload1.AutoOpenImageEditPopupAfterUpload = True" + ControlChars.CrLf, "")
        Me.txtInteractiveImageProcessing.Rows = Me.txtInteractiveImageProcessing.Text.Split(ControlChars.Lf).Length - 1
        Me.txtInteractiveImageProcessing.Visible = Not String.IsNullOrEmpty(Me.txtInteractiveImageProcessing.Text)


        ' Create a filter collection to handle multiple filters
        Dim postProcessing As ImageProcessingFilterCollection = New ImageProcessingFilterCollection()
        Dim postProcessingCode As String = ""
        If (Me.cbPostProcessing_Resize.Checked) Then
            ' Add the resize filter
            postProcessing.Add(New ScaledResizeConstraint(200, 200))
            postProcessingCode += "New ScaledResizeConstraint(200, 200)"
        End If
        If (Me.cbPostProcessing_Grayscale.Checked) Then
            ' Add the graysacale filter
            postProcessing.Add(CodeCarvings.Piczard.Filters.Colors.DefaultColorFilters.Grayscale)
            If (Not String.IsNullOrEmpty(postProcessingCode)) Then
                postProcessingCode += " + "
            End If
            postProcessingCode += "DefaultColorFilters.Grayscale"
        End If
        If (Me.cbPostProcessing_Watermark.Checked) Then
            ' Add the watermark filter
            postProcessing.Add(New CodeCarvings.Piczard.Filters.Watermarks.ImageWatermark("~/repository/watermark/piczardWatermark1.png", System.Drawing.ContentAlignment.BottomRight))
            If (Not String.IsNullOrEmpty(postProcessingCode)) Then
                postProcessingCode += " + "
            End If
            postProcessingCode += "new ImageWatermark(""~/watermark1.png"", ContentAlignment.BottomRight)"
        End If
        Me.ImageUpload1.PostProcessingFilter = postProcessing

        If (Not String.IsNullOrEmpty(postProcessingCode)) Then
            Me.txtPostProcessing.Text = "' Add the post-processing filters" + ControlChars.CrLf + "ImageUpload1.PostProcessingFilter = " + postProcessingCode + ControlChars.CrLf
        Else
            Me.txtPostProcessing.Text = ""
        End If
        Me.txtPostProcessing.Visible = Not String.IsNullOrEmpty(Me.txtPostProcessing.Text)
        Me.txtPostProcessing.Rows = postProcessing.Count + 2

        If (Me.cbPreviewConstriant.Checked) Then
            Me.ImageUpload1.PreviewFilter = New FixedResizeConstraint(200, 200, Color.Black)
        Else
            Me.ImageUpload1.PreviewFilter = Nothing
        End If

        Me.txtPreviewConstriant.Text = If(Me.cbPreviewConstriant.Checked, "' Set a square resize constraint (200 x 200 pixels, bg:black) for the preview" + ControlChars.CrLf + "ImageUpload1.PreviewFilter = new FixedResizeConstraint(200, 200, Color.Black)" + ControlChars.CrLf, "")
        Me.txtPreviewConstriant.Visible = Not String.IsNullOrEmpty(Me.txtPreviewConstriant.Text)

        Me.txtValidateImageSize.Visible = Me.cbValidateImageSize.Checked
    End Sub

#Region "Event Handlers"

    Protected Sub ImageUpload1_ImageUpload(ByVal sender As Object, ByVal args As SimpleImageUpload.ImageUploadEventArgs)
        If (Me.ImageUpload1.SourceImageSize.Width < 250) Then
            ' The uploaded image is too small
            Me.ImageUpload1.UnloadImage()
            Me.ImageUpload1.SetCurrentStatusMessage("<span style=""color:#cc0000;"">Image width must be at least 250 px.</span>")
            Return
        End If
    End Sub

    Protected Sub btnPreset_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreset_1.Click
        Me.SetPreset_1()
    End Sub

    Protected Sub btnPreset_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreset_2.Click
        Me.SetPreset_2()
    End Sub

    Protected Sub cbEnableInteractiveImageProcessing_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEnableInteractiveImageProcessing.CheckedChanged
        Me.UpdateDemoParameters()
    End Sub

    Protected Sub ddlCropConstraintMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCropConstraintMode.SelectedIndexChanged
        Me.UpdateDemoParameters()
    End Sub

    Protected Sub cbAutoOpenImageEditPopupAfterUpload_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAutoOpenImageEditPopupAfterUpload.CheckedChanged
        Me.UpdateDemoParameters()
    End Sub

    Protected Sub cbPostProcessing_Resize_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPostProcessing_Resize.CheckedChanged
        Me.UpdateDemoParameters()
    End Sub

    Protected Sub cbPostProcessing_Grayscale_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPostProcessing_Grayscale.CheckedChanged
        Me.UpdateDemoParameters()
    End Sub

    Protected Sub cbPostProcessing_Watermark_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPostProcessing_Watermark.CheckedChanged
        Me.UpdateDemoParameters()
    End Sub

    Protected Sub cbPreviewConstriant_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPreviewConstriant.CheckedChanged
        Me.UpdateDemoParameters()
    End Sub

    Protected Sub cbValidateImageSize_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbValidateImageSize.CheckedChanged
        Me.UpdateDemoParameters()
    End Sub

#End Region

End Class
