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
Imports CodeCarvings.Piczard.Web

Partial Class examples_example_A_301_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        End If

        If (Not Me.ScriptManager1.IsInAsyncPostBack) Then
            ' Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(Me)

            ' Load the fancybox script
            ExamplesHelper.LoadLibrary_FancyBox(Me)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID

        Select Me.ddlInterfaceMode.SelectedValue
            Case "Full"
                Me.InlinePictureTrimmer1.ShowRulers = True
                Me.InlinePictureTrimmer1.ShowCropAlignmentLines = True
                Me.InlinePictureTrimmer1.ShowDetailsPanel = True
                Me.InlinePictureTrimmer1.ShowZoomPanel = True
                Me.InlinePictureTrimmer1.ShowResizePanel = True
                Me.InlinePictureTrimmer1.ShowRotatePanel = True
                Me.InlinePictureTrimmer1.ShowFlipPanel = True
                Me.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = True
            Case "Standard"
                Me.InlinePictureTrimmer1.ShowRulers = True
                Me.InlinePictureTrimmer1.ShowCropAlignmentLines = True
                Me.InlinePictureTrimmer1.ShowDetailsPanel = True
                Me.InlinePictureTrimmer1.ShowZoomPanel = True
                Me.InlinePictureTrimmer1.ShowResizePanel = True
                Me.InlinePictureTrimmer1.ShowRotatePanel = True
                Me.InlinePictureTrimmer1.ShowFlipPanel = True
                Me.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = False
            Case "Easy"
                Me.InlinePictureTrimmer1.ShowRulers = True
                Me.InlinePictureTrimmer1.ShowCropAlignmentLines = True
                Me.InlinePictureTrimmer1.ShowDetailsPanel = False
                Me.InlinePictureTrimmer1.ShowZoomPanel = False
                Me.InlinePictureTrimmer1.ShowResizePanel = True
                Me.InlinePictureTrimmer1.ShowRotatePanel = True
                Me.InlinePictureTrimmer1.ShowFlipPanel = True
                Me.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = False
            Case "Minimal"
                Me.InlinePictureTrimmer1.ShowRulers = True
                Me.InlinePictureTrimmer1.ShowCropAlignmentLines = True
                Me.InlinePictureTrimmer1.ShowDetailsPanel = False
                Me.InlinePictureTrimmer1.ShowZoomPanel = False
                Me.InlinePictureTrimmer1.ShowResizePanel = False
                Me.InlinePictureTrimmer1.ShowRotatePanel = False
                Me.InlinePictureTrimmer1.ShowFlipPanel = False
                Me.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = False
            Case "Poor"
                Me.InlinePictureTrimmer1.ShowRulers = False
                Me.InlinePictureTrimmer1.ShowCropAlignmentLines = False
                Me.InlinePictureTrimmer1.ShowDetailsPanel = False
                Me.InlinePictureTrimmer1.ShowZoomPanel = False
                Me.InlinePictureTrimmer1.ShowResizePanel = False
                Me.InlinePictureTrimmer1.ShowRotatePanel = False
                Me.InlinePictureTrimmer1.ShowFlipPanel = False
                Me.InlinePictureTrimmer1.ShowImageAdjustmentsPanel = False
        End Select

        Me.InlinePictureTrimmer1.UIUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlGfxUnit.SelectedValue), GfxUnit)
        Me.InlinePictureTrimmer1.CanvasColor = System.Drawing.ColorTranslator.FromHtml(Me.txtCanvasColor.Text)
        ' Reset the OnClientControlLoad event hanlder (used to display the output image when the btnProcessImage is clicked
        Me.InlinePictureTrimmer1.OnClientControlLoadFunction = ""
    End Sub

    Protected Const SourceImageFileName As String = "~/repository/source/donkey1.jpg"
    Protected Const OutputImageFileName As String = "~/repository/output/Ex_A_301.jpg"

    Protected Sub btnProcessImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcessImage.Click
        If (Not Me.InlinePictureTrimmer1.ImageLoaded) Then
            ' Image not loaded !!!
            Return
        End If

        ' Process the image
        Me.InlinePictureTrimmer1.SaveProcessedImageToFileSystem(OutputImageFileName)

        ' Display the output image
        Me.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayOutputImage"
    End Sub

    Protected Sub btnLoadImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadImage.Click
        If (Me.InlinePictureTrimmer1.ImageLoaded) Then
            ' Unload the image
            Me.InlinePictureTrimmer1.UnloadImage()
            Me.btnProcessImage.Enabled = False
            Me.ddlImageSelectionStrategy.Enabled = True
            Me.ddlOutputResolution.Enabled = True
            Me.btnLoadImage.Text = "Load image"
        Else
            ' Load the image
            Dim outputResolution As Single = Single.Parse(Me.ddlOutputResolution.SelectedValue, System.Globalization.CultureInfo.InvariantCulture)

            Dim cropConstrant As CropConstraint = New FixedCropConstraint(GfxUnit.Pixel, 350, 250)
            cropConstrant.DefaultImageSelectionStrategy = DirectCast([Enum].Parse(GetType(CropConstraintImageSelectionStrategy), Me.ddlImageSelectionStrategy.SelectedValue), CropConstraintImageSelectionStrategy)
            If (cropConstrant.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy.Slice) Then
                cropConstrant.Margins.SetZero()
            End If

            Me.InlinePictureTrimmer1.AllowResize = cropConstrant.DefaultImageSelectionStrategy <> CropConstraintImageSelectionStrategy.DoNotResize

            Me.InlinePictureTrimmer1.LoadImageFromFileSystem(SourceImageFileName, outputResolution, cropConstrant)

            Me.btnProcessImage.Enabled = True
            Me.ddlImageSelectionStrategy.Enabled = False
            Me.ddlOutputResolution.Enabled = False
            Me.btnLoadImage.Text = "Unload image"
        End If
    End Sub

    Protected Sub ddlInterfaceMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInterfaceMode.SelectedIndexChanged
        If (Me.InlinePictureTrimmer1.ImageLoaded) Then
            ' Reset the zoom factor to force the interface to center the image
            Me.InlinePictureTrimmer1.UserState.UIParams.ZoomFactor = Nothing
            Me.InlinePictureTrimmer1.UserState.UIParams.PictureScrollH = Nothing
            Me.InlinePictureTrimmer1.UserState.UIParams.PictureScrollV = Nothing
        End If
    End Sub
End Class
