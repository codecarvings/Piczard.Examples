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

Partial Class examples_example_A_201_default
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
        Me.ProcessImage()
        Me.DisplayCode()
    End Sub

    Protected Sub ProcessImage()
        ' Setup the source file name and the output file name
        Dim sourceImageFileName As String = Me.imgSource.ImageUrl
        Dim outputImageFileName As String = "~/repository/output/Ex_A_201.jpg"

        ' Get the image transformation class
        Dim imageTransformation As ImageTransformation = New ImageTransformation()
        imageTransformation.ResizeFactor = Single.Parse(Me.ddlResizeFactor.SelectedValue, System.Globalization.CultureInfo.InvariantCulture)

        ' Process the image
        imageTransformation.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)

        ' Update the displayed image (add a timestamp parameter to the query URL to ensure that the image is reloaded by the browser)
        Me.imgOutput.ImageUrl = outputImageFileName + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()

        ' Display the generated image
        Me.phOutputContainer.Visible = True
    End Sub

    Protected Sub DisplayCode()
        Dim sbCode As System.Text.StringBuilder = New System.Text.StringBuilder()

        Dim crlf As String = ControlChars.CrLf
        sbCode.Append("' Prepare the parameters" + crlf)
        sbCode.Append("Dim sourceImage As String = ""~/repository/source/trevi1.jpg""" + crlf)
        sbCode.Append("Dim outputImage As String = ""~/repository/output/Ex_A_201.jpg""" + crlf)
        sbCode.Append(crlf)


        sbCode.Append("' Setup the image transformation filter" + crlf)
        sbCode.Append("Dim imageTransformation As ImageTransformation = New ImageTransformation()" + crlf)
        sbCode.Append("imageTransformation.ResizeFactor = " + Me.ddlResizeFactor.SelectedValue + "F" + crlf)

        sbCode.Append("' Process the image" + crlf)
        sbCode.Append("imageTransformation.SaveProcessedImageToFileSystem(sourceImage, outputImage)" + crlf)

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub

End Class
