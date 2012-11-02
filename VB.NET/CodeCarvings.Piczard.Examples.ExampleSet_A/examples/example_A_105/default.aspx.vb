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

Partial Class examples_example_A_105_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
        Me.phOutputContainer.Visible = False
        Me.phCodeContainer.Visible = False
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.processImage()
    End Sub

    Protected Sub processImage()
        Using sourceImage As LoadedImage = ImageArchiver.LoadImage(Me.imgSource.ImageUrl)
            ' Generate the image #1
            Dim outputImageFileName As String = "~/repository/output/Ex_A_105___1.jpg"
            Call New ScaledResizeConstraint(150, 250).SaveProcessedImageToFileSystem(sourceImage, outputImageFileName)
            Me.imgOutput1.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

            ' Generate the image #2
            outputImageFileName = "~/repository/output/Ex_A_105___2.jpg"
            Call New FixedCropConstraint(150, 250).SaveProcessedImageToFileSystem(sourceImage, outputImageFileName)
            Me.imgOutput2.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

            ' Generate the image #3
            outputImageFileName = "~/repository/output/Ex_A_105___3.jpg"
            Call New ImageProcessingJob(New ImageProcessingFilter() { _
                New FixedResizeConstraint(150, 250), _
                DefaultColorFilters.Grayscale _
            }).SaveProcessedImageToFileSystem(sourceImage, outputImageFileName)
            Me.imgOutput3.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()
        End Using

        Me.phOutputContainer.Visible = True
        Me.phCodeContainer.Visible = True
    End Sub

End Class
