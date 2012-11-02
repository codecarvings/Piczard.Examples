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
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks
Imports CodeCarvings.Piczard.Web

Partial Class examples_example_A_309_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        If (Not Me.ScriptManager1.IsInAsyncPostBack) Then
            ' Load the fancybox script
            ExamplesHelper.LoadLibrary_FancyBox(Me)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID

        ' Reset the OnClientControlLoad event hanlder (used to display the output image when the btnProcessImage is clicked
        Me.InlinePictureTrimmer1.OnClientControlLoadFunction = ""
    End Sub

    Protected Const SourceImageFileName_Landscape As String = "~/repository/source/trevi1.jpg"
    Protected Const SourceImageFileName_Portrait As String = "~/repository/source/valencia2.jpg"
    Protected Const OutputImageFileName As String = "~/repository/output/Ex_A_309.jpg"

    Protected Sub btnLoadLandscape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadLandscape.Click
        ' Load the landscape image
        Me.loadImage(SourceImageFileName_Landscape)
    End Sub

    Protected Sub btnLoadPortrait_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadPortrait.Click
        ' Load the landscape image
        Me.loadImage(SourceImageFileName_Portrait)
    End Sub

    Private Property lastLoadImagePath() As String
        Get
            Return DirectCast(Me.ViewState("lastLoadImagePath"), String)
        End Get
        Set(ByVal value As String)
            Me.ViewState("lastLoadImagePath") = value
        End Set
    End Property

    Protected Sub loadImage(ByVal sourceImagePath As String)
        ' Store the source image path (it's used in the ddlCropOrientation_SelectedIndexChanged event handler)
        Me.lastLoadImagePath = sourceImagePath

        ' Get the source iamge size
        Dim sourceImageSize As System.Drawing.Size
        Using image As LoadedImage = ImageArchiver.LoadImage(sourceImagePath)
            sourceImageSize = image.Size
        End Using

        ' Auto detect the image orientation
        If (sourceImageSize.Width > sourceImageSize.Height) Then
            ' Is a landscape image
            Me.ddlCropOrientation.SelectedIndex = 0
        Else
            ' Is a portrait image
            Me.ddlCropOrientation.SelectedIndex = 1
        End If

        ' Load the image
        Dim cropConstraint As CropConstraint = Me.getCropConstraint()
        Me.InlinePictureTrimmer1.LoadImageFromFileSystem(sourceImagePath, cropConstraint)

        If (Not Me.ddlCropOrientation.Enabled) Then
            ' This is the first time an image is loaded...
            ' Refresh some UI elements

            ' Enable the drop down  list control
            Me.ddlCropOrientation.Enabled = True
            ' Enable the "Preview" button
            Me.btnProcessImage.Enabled = True
        End If
    End Sub

    Protected Function getCropConstraint() As CropConstraint
        ' Get the crop constraint associated with the image orientation currently selected

        Dim result As FixedCropConstraint
        If (Me.ddlCropOrientation.SelectedIndex = 0) Then
            ' Return the landscape crop constraint
            result = New FixedCropConstraint(250, 150)
            result.Margins.SetZero()
        Else
            ' Return the portrait crop constraint
            result = New FixedCropConstraint(150, 250)
            result.Margins.SetZero()
        End If

        Return result
    End Function

    Protected Sub ddlCropOrientation_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCropOrientation.SelectedIndexChanged
        ' Image orientation changed...

        ' Re-Load the image wit the new Crop constraint
        Dim cropConstraint As CropConstraint = Me.getCropConstraint()
        Me.InlinePictureTrimmer1.LoadImageFromFileSystem(Me.lastLoadImagePath, cropConstraint)
    End Sub

    Protected Sub btnProcessImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcessImage.Click
        ' Process the image
        Me.InlinePictureTrimmer1.SaveProcessedImageToFileSystem(OutputImageFileName)

        ' Display the output image
        Me.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayOutputImage"
    End Sub

End Class
