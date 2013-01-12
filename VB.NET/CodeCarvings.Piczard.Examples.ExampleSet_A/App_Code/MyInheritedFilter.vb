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

Imports Microsoft.VisualBasic
Imports System.Drawing

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Processing
Imports CodeCarvings.Piczard.Filters.Watermarks

' Custom ImageProcessingFilter (Used by Example A.401)
<Serializable()> Public Class MyInheritedFilter
    Inherits TextWatermark

#Region "Constructors"

    Public Sub New(ByVal text As String)
        MyBase.New(text)
    End Sub

    Public Sub New()
        MyBase.New("")
    End Sub

#End Region

#Region "Overriedes"

    Protected Overrides Sub ApplyTextWatermark(ByVal args As CodeCarvings.Piczard.Processing.ImageProcessingActionExecuteArgs, ByVal g As System.Drawing.Graphics)
        ' Draw a filled rectangle
        Dim rectangleWidth As Integer = 14
        Using brush As Brush = New SolidBrush(Color.FromArgb(220, Color.Red))
            g.FillRectangle(brush, New Rectangle(args.Image.Size.Width - rectangleWidth, 0, rectangleWidth, args.Image.Size.Height))
        End Using

        Using transform As System.Drawing.Drawing2D.Matrix = g.Transform
            Using stringFormat As StringFormat = New StringFormat()
                ' Vertical text (bottom -> top)
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical
                transform.RotateAt(180.0F, New PointF(args.Image.Size.Width / 2, args.Image.Size.Height / 2))
                g.Transform = transform

                ' Align: top left, +2px displacement 
                ' (because of the matrix transformation we have to use inverted values)
                MyBase.ContentAlignment = ContentAlignment.MiddleLeft
                MyBase.ContentDisplacement = New Point(-2, -2)

                MyBase.ForeColor = Color.White
                MyBase.Font.Size = 10

                ' Draw the string by invoking the base Apply method
                MyBase.StringFormat = stringFormat
                MyBase.ApplyTextWatermark(args, g)
                MyBase.StringFormat = Nothing
            End Using
        End Using
    End Sub

#End Region

End Class
