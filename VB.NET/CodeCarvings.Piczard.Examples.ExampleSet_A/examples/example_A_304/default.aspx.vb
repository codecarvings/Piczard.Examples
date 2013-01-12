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
Imports CodeCarvings.Piczard.Web
Imports CodeCarvings.Piczard.Filters.Colors

Partial Class examples_example_A_304_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
        If (Me.cbAutoUpdateUserState.Checked) Then
            ' Auto update the user state
            Me.InlinePictureTrimmer1.OnClientUserStateChangedFunction = "InlinePictureTrimmer1_onUserStateChanged"
        Else
            Me.InlinePictureTrimmer1.OnClientUserStateChangedFunction = ""
        End If

        If (Not Me.IsPostBack) Then
            Me.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", New FreeCropConstraint())
        End If
    End Sub

    Protected Sub btnServerSideGetUserState_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnServerSideGetUserState.Click
        Me.DisplayUserState()
    End Sub

    Protected Sub btnServerSideSetUserState_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnServerSideSetUserState.Click
        ' Set some random values
        Dim rnd As Random = New Random()

        Dim userState As PictureTrimmerUserState = Me.InlinePictureTrimmer1.UserState

        ' Set the resize factor
        userState.Value.ImageSelection.Transformation.ResizeFactor = rnd.Next(50, 150)

        ' Horizontal flip
        userState.Value.ImageSelection.Transformation.FlipH = rnd.Next(0, 4) < 2

        ' Set the rectangle
        Dim x As Integer = rnd.Next(-100, 300)
        Dim y As Integer = rnd.Next(-100, 300)
        Dim w As Integer = rnd.Next(50, 400)
        Dim h As Integer = rnd.Next(50, 400)
        userState.Value.ImageSelection.Crop.Rectangle = New Rectangle(x, y, w, h)

        ' Set the Saturation
        userState.Value.ImageAdjustments.Saturation = rnd.Next(-100, 100)

        ' Auto-zoom and auto-center the view
        userState.UIParams.ZoomFactor = Nothing
        userState.UIParams.PictureScrollH = Nothing
        userState.UIParams.PictureScrollV = Nothing

        Me.DisplayUserState()
    End Sub

    Protected Sub DisplayUserState()
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim crlf As String = ControlChars.CrLf

        ' *** UserState ******
        Dim userState As PictureTrimmerUserState = Me.InlinePictureTrimmer1.UserState

        ' *** Value ******
        Dim value As PictureTrimmerValue = userState.Value

        ' *** ImageSelection ******
        Dim imageSelection As ImageSelection = value.ImageSelection

        ' *** ImageTransformation ******
        Dim imageTransformation As ImageTransformation = imageSelection.Transformation

        sb.Length = 0
        sb.Append("Resize factor:" + imageTransformation.ResizeFactor.ToString(CultureInfo.InvariantCulture) + "%" + crlf)
        sb.Append("Rotation angle:" + imageTransformation.RotationAngle.ToString(CultureInfo.InvariantCulture) + "°" + crlf)
        sb.Append("Flip horizontal:" + If(imageTransformation.FlipH, "yes", "no") + crlf)
        sb.Append("Flip vertical:" + If(imageTransformation.FlipV, "yes", "no") + crlf)
        Me.txtUserState_Value_ImageSelection_Transformation.Text = sb.ToString()

        ' *** ImageCrop ******
        Dim imageCrop As ImageCrop = imageSelection.Crop

        sb.Length = 0
        sb.Append("Rectangle:" + If(imageCrop.Rectangle.HasValue, imageCrop.Rectangle.Value.ToString(), "Auto") + crlf)
        ' Note: in this example imageCrop.CanvasColor is not displayed.
        Me.txtUserState_Value_ImageSelection_Crop.Text = sb.ToString()

        ' *** ImageAdjustments ******
        Dim imageAdjustments As ImageAdjustmentsFilter = value.ImageAdjustments

        sb.Length = 0
        sb.Append("Brightness:" + imageAdjustments.Brightness.ToString(CultureInfo.InvariantCulture) + crlf)
        sb.Append("Contrast:" + imageAdjustments.Contrast.ToString(CultureInfo.InvariantCulture) + crlf)
        sb.Append("Hue:" + imageAdjustments.Hue.ToString(CultureInfo.InvariantCulture) + "°" + crlf)
        sb.Append("Saturation:" + imageAdjustments.Saturation.ToString(CultureInfo.InvariantCulture) + crlf)
        Me.txtUserState_Value_ImageAdjustments.Text = sb.ToString()

        ' Note: In this example value.ImageBackColorApplyMode is not displayed.

        ' *** UIParams ******
        Dim uiParams As PictureTrimmerUIParams = userState.UIParams

        sb.Length = 0
        sb.Append("Zoom factor:" + If(uiParams.ZoomFactor.HasValue, uiParams.ZoomFactor.Value.ToString(CultureInfo.InvariantCulture) + "%", "auto") + crlf)
        sb.Append("Picture scroll horizontal:" + If(uiParams.PictureScrollH.HasValue, uiParams.PictureScrollH.Value.ToString(CultureInfo.InvariantCulture), "auto") + crlf)
        sb.Append("Picture scroll vertical:" + If(uiParams.PictureScrollH.HasValue, uiParams.PictureScrollV.Value.ToString(CultureInfo.InvariantCulture), "auto") + crlf)
        sb.Append("Command panel scroll vertical:" + uiParams.CommandPanelScrollV.ToString(CultureInfo.InvariantCulture) + crlf)
        Me.txtUserState_UIParams.Text = sb.ToString()

    End Sub

End Class
