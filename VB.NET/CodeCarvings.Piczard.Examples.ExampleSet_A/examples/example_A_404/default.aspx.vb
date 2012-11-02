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
Imports CodeCarvings.Piczard.Serialization
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks

Partial Class examples_example_A_404_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        Else
            ' Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(Me)

            ' Load the fancybox script
            ExamplesHelper.LoadLibrary_FancyBox(Me)
        End If

        If (Not Me.Page.IsPostBack) Then
            Me.PopulateDropdownLists()

            ' Load the image
            Me.LoadImageIntoPictureTrimmer(True)

            ' Initialize the zoom factor
            Me.InlinePictureTrimmer1.UserState.UIParams.ZoomFactor = Single.Parse(Me.ddlZoomFactor.SelectedValue, System.Globalization.CultureInfo.InvariantCulture)
        End If

        ' Update the canvas color and the image back color
        Me.InlinePictureTrimmer1.CanvasColor = System.Drawing.ColorTranslator.FromHtml(Me.txtCanvasColor.Text)
        Me.InlinePictureTrimmer1.ImageBackColor = Me.InlinePictureTrimmer1.CanvasColor.Clone()

        ' Update the UI Unit
        Me.InlinePictureTrimmer1.UIUnit = DirectCast([Enum].Parse(GetType(GfxUnit), Me.ddlGfxUnit.SelectedValue), GfxUnit)

        ' Reset the OnClientControlLoad event hanlder (used to display the preview when the btnPreview is clicked
        Me.InlinePictureTrimmer1.OnClientControlLoadFunction = ""

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
    End Sub

    Protected Const SourceImageFileName As String = "~/repository/source/donkey1.jpg"
    Protected Const OutputImageFileName As String = "~/repository/output/Ex_A_404.jpg"

    Protected Sub LoadImageIntoPictureTrimmer(ByVal resetSelection As Boolean)
        ' Pre-process the image
        Dim job As ImageProcessingJob = Me.GetImagePreProcessingJob()
        Using preProcessedImage As System.Drawing.Bitmap = job.GetProcessedImage(SourceImageFileName)
            ' Backup the previous userState
            Dim previousUserState As PictureTrimmerUserState = Nothing
            If (Me.InlinePictureTrimmer1.ImageLoaded) Then
                previousUserState = Me.InlinePictureTrimmer1.UserState
            End If

            ' Load the pre-processed image into the PictureTrimmer control
            Me.InlinePictureTrimmer1.LoadImage(preProcessedImage, New FreeCropConstraint(Nothing, 600, Nothing, 600))

            If (previousUserState IsNot Nothing) Then
                If (resetSelection) Then
                    ' Re-calculate the cropping rectangle
                    previousUserState.Value.ImageSelection.Crop.Rectangle = Nothing
                    ' Re-center the image
                    previousUserState.UIParams.PictureScrollH = Nothing
                    previousUserState.UIParams.PictureScrollV = Nothing
                End If

                ' Restore the previous user state
                Me.InlinePictureTrimmer1.UserState = previousUserState
            End If
        End Using
    End Sub

    Protected Function GetImagePreProcessingJob() As ImageProcessingJob
        Dim job As ImageProcessingJob = New ImageProcessingJob()

        ' Set the back color
        job.ImageBackColor.Value = Me.InlinePictureTrimmer1.ImageBackColor.Clone()

        ' Add the color filter
        Select Me.ddlDefaultColorFilters.SelectedIndex
            Case 1
                ' Grayscale
                job.Filters.Add(DefaultColorFilters.Grayscale)
            Case 2
                ' Sepia
                job.Filters.Add(DefaultColorFilters.Sepia)
            Case 3
                ' Invert
                job.Filters.Add(DefaultColorFilters.Invert)
        End Select

        ' Add the rotation filter
        Dim rotationAngle As Single = Single.Parse(Me.ddlRotationAngle.SelectedValue, System.Globalization.CultureInfo.InvariantCulture)
        job.Filters.Add(New MyRotationFilter(rotationAngle))

        Return job
    End Function

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        ' Process the image
        Me.InlinePictureTrimmer1.SaveProcessedImageToFileSystem(OutputImageFileName)

        ' Display the output image
        Me.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayPreview"
    End Sub

    Protected Sub ddlDefaultColorFilters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDefaultColorFilters.SelectedIndexChanged
        ' Update the image
        Me.LoadImageIntoPictureTrimmer(False)
    End Sub

    Protected Sub ddlRotationAngle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRotationAngle.SelectedIndexChanged
        ' Update the image
        Me.LoadImageIntoPictureTrimmer(True)
    End Sub

    Protected Sub PopulateDropdownLists()
        For i As Integer = 0 To 360 Step 5
            Dim item As ListItem = New ListItem(i.ToString() + "°", i.ToString())
            Me.ddlRotationAngle.Items.Add(item)

            If (i = 20) Then
                ' Default value for this example = 20°
                item.Selected = True
            End If
        Next 
    End Sub

End Class
