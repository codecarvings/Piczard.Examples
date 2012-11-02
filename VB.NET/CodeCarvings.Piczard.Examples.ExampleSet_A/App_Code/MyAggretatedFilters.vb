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

Imports Microsoft.VisualBasic
Imports System.Drawing

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Processing
Imports CodeCarvings.Piczard.Filters.Colors
Imports CodeCarvings.Piczard.Filters.Watermarks

' Custom ImageProcessingFilter (Used by Example A.401)
' NOTE: This filter does not implement JSON serialziation
' (Please see the the "MyRotationFilter" class for a JSON serialization example)
<Serializable()> Public Class MyAggretatedFilters
    Inherits ImageProcessingFilter

#Region "Constructors"

    Public Sub New(ByVal text As String)
        Me.Text = text
    End Sub

    Public Sub New()
        Me.New(String.Empty)

    End Sub

#End Region

#Region "Overriedes"

    Protected Overrides Sub LoadImageProcessingActions(ByVal args As ImageProcessingActionLoadArgs)
        ' Filter #1
        args.LoadImageProcessingActions(New FixedCropConstraint(300, 200))

        ' Filter #2
        Dim watermark As TextWatermark = New TextWatermark(Me.Text, ContentAlignment.TopCenter)
        watermark.Font.Size = 12
        watermark.ForeColor = Color.Black
        args.LoadImageProcessingActions(watermark)

        ' Filter #3
        args.LoadImageProcessingActions(DefaultColorFilters.Sepia)
    End Sub

    Protected Overrides Sub Apply(ByVal args As CodeCarvings.Piczard.Processing.ImageProcessingActionExecuteArgs)
        ' This is only a container for multiple filters
        Throw New Exception("Cannot invoke the Apply method.")
    End Sub

#End Region

#Region "Propertites"

    Private _Text As String
    Public Property Text() As String
        Get
            Return Me._Text
        End Get
        Set(ByVal value As String)
            Me._Text = value
        End Set
    End Property

#End Region

End Class
