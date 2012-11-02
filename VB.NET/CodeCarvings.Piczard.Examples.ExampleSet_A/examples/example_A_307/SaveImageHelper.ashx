<%@ WebHandler Language="VB" Class="SaveImageHelper" %>
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

Public Class SaveImageHelper : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        ' Load the picture trimmer core
        Dim pictureTrimmer As PictureTrimmerCore = PictureTrimmerCore.FromJSON(context.Request("coreJSONString"))
        Dim outputImageFileName As String
        Dim imageProcessed As Boolean = False
        
        If (TypeOf PictureTrimmer Is PopupPictureTrimmerCore) Then
            ' *** POPUP PICTURETRIMMER ***
            outputImageFileName = "~/repository/output/Ex_A_307_2.jpg"
            Dim saveChanges As Boolean = Boolean.Parse(context.Request("saveChanges"))

            If (saveChanges) Then
                ' Save the processed image
                pictureTrimmer.SaveProcessedImageToFileSystem(outputImageFileName)
                imageProcessed = True
            End If

            ' Unload the image
            pictureTrimmer.UnloadImage()
        Else
            ' *** INLINE PICTURETRIMMER ***
            outputImageFileName = "~/repository/output/Ex_A_307_1.jpg"
            
            ' Save the processed image
            pictureTrimmer.SaveProcessedImageToFileSystem(outputImageFileName)
            imageProcessed = True
        End If
        
        ' Return a JSON string containing the url of the image
        context.Response.ContentType = "application/json"
        Dim result As JSONObject = New JSONObject()
        If (imageProcessed) Then
            Dim imageUrl As String = VirtualPathUtility.ToAbsolute(outputImageFileName) + "?timestamp=" + DateTime.UtcNow.Ticks.ToString()
            result.SetStringValue("imageUrl", imageUrl)
        End If
        context.Response.Write(result.Encode())
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class