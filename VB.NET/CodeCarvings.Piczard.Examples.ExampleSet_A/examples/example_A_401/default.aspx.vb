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

Partial Class examples_example_A_401_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Reset some settings after every postback...
        Me.phOutputContainer.Visible = False
        Me.phCodeContainer.Visible = False
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Me.ProcessImage()

        ' Display the source code
        Me.phCodeContainer.Visible = True
        For i As Integer = 0 To 2
            Dim phCode As PlaceHolder = DirectCast(Me.phCodeContainer.FindControl("phCode_" + i.ToString()), PlaceHolder)
            phCode.Visible = (i = Me.ddlFilter.SelectedIndex)
        Next
    End Sub

    Protected Sub ProcessImage()
        ' ======== NOTE =========
        ' Please see:
        ' - MyAggretatedFilters class contained in the ~/App_Code folder
        ' - MyInheritedFilter class contained in the ~/App_Code folder
        ' - MyCustomFilter1 class contained in the ~/App_Code folder

        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_401.png"

        Select Me.ddlFilter.SelectedIndex
            Case 0
                ' Aggretated filters
                Call New MyAggretatedFilters("Crop + TextWatermark + Sepia (" + DateTime.Now.ToShortDateString() + ")").SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
            Case 1
                ' Inherited filter
                Call New MyInheritedFilter("Extended TextWatermark " + DateTime.Now.ToString("s")).SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
            Case 2
                ' Totally custom filter
                Call New MyCustomFilter1().SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)
        End Select

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

        ' Display the generated image
        Me.phOutputContainer.Visible = True
    End Sub

End Class
