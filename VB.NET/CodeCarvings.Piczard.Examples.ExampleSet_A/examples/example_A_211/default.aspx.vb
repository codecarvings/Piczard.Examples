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

Imports CodeCarvings.Piczard

Partial Class examples_example_A_211_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI(); initializeExampleUI();", True)
        End If

        If (Not Me.ScriptManager1.IsInAsyncPostBack) Then
            ' Load the colorpicker script
            ExamplesHelper.LoadLibrary_ColorPicker(Me)
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
        Dim outputImageFileName As String = "~/repository/output/Ex_A_211.jpg"

        ' Setup the image crop filter
        Dim imageCrop As ImageCrop = Nothing

        Select Case Me.ddlRectangle.SelectedIndex
            Case 0
                ' Select whole image
                imageCrop = New ImageCrop()
            Case 1
                ' x=80, y=80, width=340, height=190 (Pixel)
                imageCrop = New ImageCrop(New Rectangle(80, 80, 340, 190))
            Case 2
                ' x=-20, y=-20, width=540, height=390 (Pixel)
                imageCrop = New ImageCrop(New Rectangle(-20, -20, 540, 390))
            Case 3
                ' x=-0.5, y=--1, width=5, height=5 (Inch / 96 DPI)
                imageCrop = ImageCrop.Calculate(GfxUnit.Inch, New RectangleF(-0.5F, -1.0F, 5.0F, 5.0F))
            Case 4
                ' x=-1.5, y=--5, width=10.5, height=10.5 (Cm / 96 DPI)
                imageCrop = ImageCrop.Calculate(GfxUnit.Cm, New RectangleF(-1.5F, -5.0F, 10.5F, 10.5F), 96.0F)
        End Select

        ' Set the canvas color
        imageCrop.CanvasColor = System.Drawing.ColorTranslator.FromHtml(Me.txtCanvasColor.Text)

        ' Process the image
        imageCrop.SaveProcessedImageToFileSystem(sourceImageFileName, outputImageFileName)

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
        sbCode.Append("Dim outputImage As String = ""~/repository/output/Ex_A_211.jpg""" + crlf)
        sbCode.Append(crlf)

        sbCode.Append("' Setup the image crop filter" + crlf)
        Select Case Me.ddlRectangle.SelectedIndex
            Case 0
                ' Select whole image
                sbCode.Append("Dim imageCrop As ImageCrop = New ImageCrop()" + crlf)
            Case 1
                ' x=80, y=80, width=340, height=190 (Pixel)
                sbCode.Append("Dim imageCrop As ImageCrop = New ImageCrop(New Rectangle(80, 80, 340, 190))" + crlf)
            Case 2
                ' x=-20, y=-20, width=540, height=390 (Pixel)
                sbCode.Append("Dim imageCrop As ImageCrop = New ImageCrop(New Rectangle(-20, -20, 540, 390))" + crlf)
            Case 3
                ' x=-0.5, y=--1, width=5, height=5 (Inch / 96 DPI)
                sbCode.Append("Dim imageCrop As ImageCrop = ImageCrop.Calculate(GfxUnit.Inch, New RectangleF(-0.5F, -1.0F, 5.0F, 5.0F))" + crlf)
            Case 4
                ' x=-1.5, y=--5, width=10.5, height=10.5 (Cm / 96 DPI)
                sbCode.Append("Dim imageCrop As ImageCrop = ImageCrop.Calculate(GfxUnit.Cm, New RectangleF(-1.5F, -5.0F, 10.5F, 10.5F), 96.0F)" + crlf)
        End Select
        sbCode.Append("imageCrop.CanvasColor = System.Drawing.ColorTranslator.FromHtml(""" + Me.txtCanvasColor.Text + """)" + crlf)

        sbCode.Append(crlf)
        sbCode.Append("' Process the image" + crlf)
        sbCode.Append("imageCrop.SaveProcessedImageToFileSystem(sourceImage, outputImage)" + crlf)

        Me.phCodeContainer.Visible = True
        Me.litCode.Text = sbCode.ToString()
    End Sub

End Class
