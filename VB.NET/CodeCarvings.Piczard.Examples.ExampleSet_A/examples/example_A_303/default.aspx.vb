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

Partial Class examples_example_A_303_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
    End Sub

    Protected Sub btnLoadImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadImage.Click
        ' Update the Output resolution and the UI Unit
        Dim unit As GfxUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlUnit.SelectedValue), GfxUnit)
        Me.InlinePictureTrimmer1.UIUnit = unit

        ' By default rotation is enabled
        Me.InlinePictureTrimmer1.ShowRotatePanel = True

        ' Load the image
        Me.InlinePictureTrimmer1.LoadImageFromFileSystem(Server.MapPath("~/repository/source/flowers1.jpg"))

        Me.InlinePictureTrimmer1.ShowResizePanel = Me.cbEnableResize.Checked
        Me.InlinePictureTrimmer1.ShowDetailsPanel = Me.cbEnableResize.Checked

        If (Me.cbEnableResize.Checked) Then
            ' Resize enabled -> Disable the AutoZoom feature
            Me.InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.Disabled
        Else
            ' Resize disabled -> Force the AutoZoom
            Me.InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.EnabledOnlyForLargeImages
        End If

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

    Protected Sub DisplayCode()
        Dim sbCode As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim crlf As String = ControlChars.CrLf

        sbCode.Append("InlinePictureTrimmer1.ShowZoomPanel = False" + crlf)

        Dim unit As GfxUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlUnit.SelectedValue), GfxUnit)
        If (unit <> GfxUnit.Pixel) Then
            ' Default value: Pixel
            sbCode.Append("InlinePictureTrimmer1.UIUnit = GfxUnit." + unit.ToString() + crlf)
        End If

        If (Me.cbEnableResize.Checked) Then
            ' Default = AutoZoomMode.Standard
            sbCode.Append("InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.Disabled" + crlf)
        Else
            ' Default = true
            sbCode.Append("InlinePictureTrimmer1.ShowResizePanel = False" + crlf)
            sbCode.Append("InlinePictureTrimmer1.ShowDetailsPanel = False" + crlf)

            sbCode.Append("InlinePictureTrimmer1.AutoZoomMode = PictureTrimmerAutoZoomMode.EnabledOnlyForLargeImages" + crlf)
        End If

        sbCode.Append("InlinePictureTrimmer1.LoadImageFromFileSystem(Server.MapPath(""~/repository/source/flowers1.jpg""))" + crlf)

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub

End Class
