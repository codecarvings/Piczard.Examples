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
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks
Imports CodeCarvings.Piczard.Web

Partial Class examples_example_A_308_default
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

        If (Not Me.IsPostBack) Then
            ' Load the image
            Dim cropConstraint As FixedCropConstraint = New FixedCropConstraint(250, 150)
            cropConstraint.Margins.SetZero()
            Me.InlinePictureTrimmer1.LoadImageFromFileSystem(SourceImageFileName, cropConstraint)
        End If
    End Sub

    Protected Const SourceImageFileName As String = "~/repository/source/donkey1.jpg"
    Protected Const OutputImageFileName As String = "~/repository/output/Ex_A_308.jpg"
    Protected Const WatermarkImageFileName As String = "~/repository/watermark/piczardWatermark1.png"

    Protected Sub btnProcessImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcessImage.Click
        ' Process the image

        ' Get the image processing job
        Dim job As ImageProcessingJob = Me.InlinePictureTrimmer1.GetImageProcessingJob()

        ' Add the filters
        job.Filters.Add(DefaultColorFilters.Grayscale) ' Grayscale
        job.Filters.Add(New ImageWatermark(WatermarkImageFileName, System.Drawing.ContentAlignment.BottomRight)) ' Watermark

        ' Save the image
        job.SaveProcessedImageToFileSystem(SourceImageFileName, OutputImageFileName)

        ' Display the output image
        Me.InlinePictureTrimmer1.OnClientControlLoadFunction = "displayOutputImage"
    End Sub

End Class
