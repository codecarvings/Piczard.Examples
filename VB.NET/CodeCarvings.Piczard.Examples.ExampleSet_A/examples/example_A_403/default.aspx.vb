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

Partial Class examples_example_A_403_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        End If

        If (Not Me.ScriptManager1.IsInAsyncPostBack) Then
            ' Load the custom CSS used by the PopupPictureTrimmer
            Dim lightBoxCustomCSS As System.Web.UI.HtmlControls.HtmlLink = New System.Web.UI.HtmlControls.HtmlLink()
            lightBoxCustomCSS.Href = Me.ResolveUrl("CodeCarvings.Piczard.LightBox.Custom.css")
            lightBoxCustomCSS.Attributes.Add("rel", "stylesheet")
            lightBoxCustomCSS.Attributes.Add("type", "text/css")
            Me.Header.FindControl("phHeader").Controls.Add(lightBoxCustomCSS)

            ' Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(Me)
        End If

        If (Not Me.IsPostBack) Then
            ' Load the image
            Dim cropConstraint As CropConstraint = New FixedCropConstraint(100, 100)
            cropConstraint.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy.DoNotResize

            Me.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers2.png", cropConstraint)
            Me.PopupPictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers2.png", cropConstraint)
        End If

        ' Configure the PictureTrimmer instances
        Me.SetPictureTrimmerConfiguration(Me.InlinePictureTrimmer1)
        Me.SetPictureTrimmerConfiguration(Me.PopupPictureTrimmer1)
        Me.PopupPictureTrimmer1.LightBoxCssClass = Me.ddlPopupLightBoxCssClass.SelectedValue

        Me.cbShowResizePanel.Enabled = Me.cbAllowResize.Checked

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
    End Sub

    Protected Sub SetPictureTrimmerConfiguration(ByVal pictureTrimmer As PictureTrimmer)
        pictureTrimmer.ShowDetailsPanel = Me.cbShowDetailsPanel.Checked
        pictureTrimmer.ShowZoomPanel = Me.cbShowZoomPanel.Checked
        pictureTrimmer.AllowResize = Me.cbAllowResize.Checked
        pictureTrimmer.ShowResizePanel = Me.cbShowResizePanel.Checked
        pictureTrimmer.ShowRotatePanel = Me.cbShowRotatePanel.Checked
        pictureTrimmer.ShowFlipPanel = Me.cbShowFlipPanel.Checked
        pictureTrimmer.ShowImageAdjustmentsPanel = Me.cbShowImageAdjustmentsPanel.Checked
        pictureTrimmer.ShowCropAlignmentLines = Me.cbShowCropAlignmentLines.Checked
        pictureTrimmer.EnableSnapping = Me.cbEnableSnapping.Checked
        pictureTrimmer.ShowRulers = Me.cbShowRulers.Checked
        pictureTrimmer.UIUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlUIUnit.SelectedValue), GfxUnit)
        pictureTrimmer.BackColor = System.Drawing.ColorTranslator.FromHtml(Me.txtBackColor.Text)
        pictureTrimmer.ForeColor = System.Drawing.ColorTranslator.FromHtml(Me.txtForeColor.Text)
        pictureTrimmer.CanvasColor.Value = System.Drawing.ColorTranslator.FromHtml(Me.txtCanvasColor.Text)
        pictureTrimmer.ImageBackColor.Value = System.Drawing.ColorTranslator.FromHtml(Me.txtImageBackColor.Text)
        pictureTrimmer.CropShadowMode = DirectCast([Enum].Parse(GetType(PictureTrimmerCropShadowMode), Me.ddlCropShadowMode.SelectedValue), PictureTrimmerCropShadowMode)
    End Sub

End Class
