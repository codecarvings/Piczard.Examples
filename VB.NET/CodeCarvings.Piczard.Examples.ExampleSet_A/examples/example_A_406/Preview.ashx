<%@ WebHandler Language="VB" Class="Preview" %>
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

Imports System
Imports System.Web

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web
Imports CodeCarvings.Piczard.Serialization
Imports CodeCarvings.Piczard.Helpers
Imports CodeCarvings.Piczard.Filters.Colors

Public Class Preview : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim data As String = context.Request("data")
        If (String.IsNullOrEmpty(Data)) Then
            ' Invalid data
            context.Response.ContentType = "text/plain"
            context.Response.Write("Invalid data")
            context.Response.End()
            Return
        End If
        
        ' Parse the JSON data
        Dim oData As JSONObject = JSONObject.Decode(data)

        ' Get an image processing job to apply the required filters
        Dim ipj As ImageProcessingJob = New ImageProcessingJob()
        
        ' Apply the transformations
        Dim transformation As ImageTransformation = New ImageTransformation()
        transformation.RotationAngle = oData.GetNumberSingleValue("rotationAngle").Value
        transformation.FlipH = oData.GetBoolValue("flipH").Value
        transformation.FlipV = oData.GetBoolValue("flipV").Value
        ipj.Filters.Add(transformation)
        
        ' Apply the image adjustments
        Dim adjustments As ImageAdjustmentsFilter = New ImageAdjustmentsFilter()
        adjustments.Brightness = oData.GetNumberSingleValue("brightness").Value
        adjustments.Contrast = oData.GetNumberSingleValue("contrast").Value
        adjustments.Hue = oData.GetNumberSingleValue("hue").Value
        adjustments.Saturation = oData.GetNumberSingleValue("saturation").Value
        ipj.Filters.Add(adjustments)

        ' Process the image and trasmit it to the browser
        Dim sourceImageFilePath As String = SecurityHelper.DecryptString(oData.GetStringValue("image"))
        ipj.TransmitProcessedImageToWebResponse(sourceImageFilePath, context.Response, New JpegFormatEncoderParams(92))
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class