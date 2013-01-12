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
Imports CodeCarvings.Piczard.Helpers
Imports CodeCarvings.Piczard.Serialization

' Custom Rotation ImageProcessingFilter (Used by Example A.404)
' NOTE: This filter implements JSON serialziation
<Serializable()> Public Class MyRotationFilter
    Inherits ImageProcessingFilter
    Implements IJSONSerializable

#Region "Constructors"

    Public Sub New(ByVal rotationAngle As Single)
        Me.RotationAngle = rotationAngle
    End Sub

    Public Sub New()
        Me.New(0.0F)

    End Sub

#End Region

#Region "Overriedes"

    Protected Overrides Sub Apply(ByVal args As CodeCarvings.Piczard.Processing.ImageProcessingActionExecuteArgs)
        Dim normalizedRotationAngle As Single = Me._RotationAngle Mod 360.0F
        If (normalizedRotationAngle = 0.0F) Then
            ' No need to rotate the image
            Return
        End If

        Dim result As Bitmap = Nothing
        Try
            ' Intial calculations
            Dim t As Double
            Dim t1 As Double
            Dim b1 As Double
            Dim t2 As Double
            Dim b2 As Double

            Me.CalculateValues(args.Image.Size, t, t1, b1, t2, b2)

            ' Calculate the new image size
            Dim outputImageSize As Size = Me.GetOutputImageSize(t1, b1, t2, b2)

            ' Create the result image
            result = New Bitmap(outputImageSize.Width, outputImageSize.Height, CodeCarvings.Piczard.CommonData.DefaultPixelFormat)

            ' Set the right image resolution (DPI)
            ImageHelper.SetImageResolution(result, args.ImageProcessingJob.OutputResolution)

            Using g As Graphics = Graphics.FromImage(result)
                ' Use the max quality
                ImageHelper.SetGraphicsMaxQuality(g)

                If ((args.IsLastAction) And (Not args.AppliedImageBackColorValue.HasValue)) Then
                    ' Optimization (not mandatory)
                    ' This is the last filter action and the ImageBackColor has not been yet applied...
                    ' Apply the ImageBackColor now to save RAM & CPU
                    args.ApplyImageBackColor(g)
                End If

                ' Calculate the points for the DrawImage method
                Dim shapePoints(2) As Point

                If ((t >= 0.0R) And (t < (Math.PI / 2.0R))) Then
                    shapePoints(0) = New Point(Convert.ToInt32(Math.Round(b2)), 0)
                    shapePoints(1) = New Point(outputImageSize.Width, Convert.ToInt32(Math.Round(t2)))
                    shapePoints(2) = New Point(0, Convert.ToInt32(Math.Round(b1)))
                ElseIf ((t >= (Math.PI / 2.0R)) And (t < Math.PI)) Then
                    shapePoints(0) = New Point(outputImageSize.Width, Convert.ToInt32(Math.Round(t2)))
                    shapePoints(1) = New Point(Convert.ToInt32(Math.Round(t1)), outputImageSize.Height)
                    shapePoints(2) = New Point(Convert.ToInt32(Math.Round(b2)), 0)
                ElseIf ((t >= Math.PI) And (t < (Math.PI + (Math.PI / 2.0R)))) Then
                    shapePoints(0) = New Point(Convert.ToInt32(Math.Round(t1)), outputImageSize.Height)
                    shapePoints(1) = New Point(0, Convert.ToInt32(Math.Round(b1)))
                    shapePoints(2) = New Point(outputImageSize.Width, Convert.ToInt32(Math.Round(t2)))
                Else
                    shapePoints(0) = New Point(0, Convert.ToInt32(Math.Round(b1)))
                    shapePoints(1) = New Point(Convert.ToInt32(Math.Round(b2)), 0)
                    shapePoints(2) = New Point(Convert.ToInt32(Math.Round(t1)), outputImageSize.Height)
                End If

                ' Draw the image
                g.DrawImage(args.Image, shapePoints)
            End Using

            ' Return the image
            args.Image = result
        Catch
            ' An error has occurred...

            ' Release the resources
            If (result IsNot Nothing) Then
                result.Dispose()
                result = Nothing
            End If

            ' Re-throw the exception
            Throw
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub CalculateValues(ByVal imageSize As Size, ByRef t As Double, ByRef t1 As Double, ByRef b1 As Double, ByRef t2 As Double, ByRef b2 As Double)
        ' Convert image size to double
        Dim imageWidth As Double = Convert.ToDouble(imageSize.Width)
        Dim imageHeight As Double = Convert.ToDouble(imageSize.Height)

        ' Convert the rotation angle
        t = Convert.ToDouble(Me._RotationAngle) * Math.PI / 180.0R
        While (t < 0.0R)
            t += (Math.PI * 2.0R)
        End While

        If ( _
            ((t >= 0.0R) And (t < (Math.PI / 2.0R))) _
            Or _
            ((t >= Math.PI) And (t < (Math.PI + (Math.PI / 2.0R))))) Then
            t1 = Math.Abs(Math.Cos(t)) * imageWidth
            b1 = Math.Abs(Math.Cos(t)) * imageHeight

            t2 = Math.Abs(Math.Sin(t)) * imageWidth
            b2 = Math.Abs(Math.Sin(t)) * imageHeight
        Else
            t1 = Math.Abs(Math.Sin(t)) * imageHeight
            b1 = Math.Abs(Math.Sin(t)) * imageWidth

            t2 = Math.Abs(Math.Cos(t)) * imageHeight
            b2 = Math.Abs(Math.Cos(t)) * imageWidth
        End If
    End Sub

    Private Function GetOutputImageSize(ByVal t1 As Double, ByVal b1 As Double, ByVal t2 As Double, ByVal b2 As Double) As Size
        Dim width As Integer = Convert.ToInt32(Math.Ceiling(t1 + b2))
        Dim height As Integer = Convert.ToInt32(Math.Ceiling(b1 + t2))
        Return New Size(width, height)
    End Function

    Public Function GetOutputImageSize(ByVal imageSize As Size) As Size
        Dim t As Double
        Dim t1 As Double
        Dim b1 As Double
        Dim t2 As Double
        Dim b2 As Double
        Me.CalculateValues(imageSize, t, t1, b1, t2, b2)

        Return Me.GetOutputImageSize(t1, b1, t2, b2)
    End Function

#End Region

#Region "Properties"
    Private _RotationAngle As Single
    Public Property RotationAngle() As Single
        Get
            Return Me._RotationAngle
        End Get
        Set(ByVal value As Single)
            Me._RotationAngle = value
        End Set
    End Property
#End Region

#Region "Serialization"

    Protected ReadOnly Property SerializationVersion() As Integer Implements IJSONSerializable.SerializationVersion
        Get
            ' Default version = 1
            Return 1
        End Get
    End Property

    Protected Function GetSerializationException() As JSONSerializationException Implements IJSONSerializable.GetSerializationException
        ' No error
        Return Nothing
    End Function

    Protected Function ToJSONObject(ByVal options As JSONSerializationOptions) As JSONObject Implements IJSONSerializable.ToJSONObject
        ' Options cannot be Nothing
        If (options Is Nothing) Then
            options = JSONSerializationOptions.Default
        End If

        Dim result As JSONObject = options.GetNewJSONObject(Me)
        result.SetNumberValue("rotationAngle", Me._RotationAngle)

        Return result
    End Function

    Private Shared Function FromJSON_1(ByVal jsonObject As JSONObject) As MyRotationFilter
        If (jsonObject Is Nothing) Then
            Return Nothing
        End If

        Dim result As MyRotationFilter = New MyRotationFilter()
        result.RotationAngle = jsonObject.GetNumberSingleValue("rotationAngle").Value

        Return result
    End Function

    Public Shared Function FromJSON(ByVal jsonObject As JSONObject) As MyRotationFilter
        If (jsonObject Is Nothing) Then
            Return Nothing
        End If

        Dim version As Integer = jsonObject.GetSerializationVersion()
        Select Case version
            Case 1
                Return FromJSON_1(jsonObject)
        End Select

        Throw New JSONInvalidObjectException(GetType(MyRotationFilter))
    End Function

    Public Shared Function FromJSON(ByVal jsonString As String) As MyRotationFilter
        Return FromJSON(JSONObject.Decode(jsonString))
    End Function

#End Region

End Class
