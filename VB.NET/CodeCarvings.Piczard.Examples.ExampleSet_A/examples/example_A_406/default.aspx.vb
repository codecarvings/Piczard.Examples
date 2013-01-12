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

Partial Class examples_example_A_406_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me.IsPostBack) Then
            ' Load the image
            Me.laodImage("~/repository/source/valencia2.jpg")
        End If
    End Sub

    Protected Sub laodImage(ByVal sourceImagePath As String)
        Dim cropConstraint As FixedCropConstraint = New FixedCropConstraint(350, 250)
        cropConstraint.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy.DoNotResize

        ' Load the image in the PictureTrimmer control
        Me.InlinePictureTrimmer1.LoadImageFromFileSystem(sourceImagePath, cropConstraint)

        ' Store the source image file path in the viewstate
        Me.SourceImageFilePath = Server.MapPath(sourceImagePath)
    End Sub

    Protected Property SourceImageFilePath() As String
        Get
            Return DirectCast(Me.ViewState("SourceImageFilePath"), String)
        End Get
        Set(ByVal value As String)
            Me.ViewState("SourceImageFilePath") = value
        End Set
    End Property
End Class
