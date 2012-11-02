<%@ WebHandler Language="VB" Class="LoadImageHelper" %>
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

Public Class LoadImageHelper : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        ' Load the picture trimmer core
        Dim pictureTrimmer As PictureTrimmerCore = PictureTrimmerCore.FromJSON(context.Request("coreJSONString"))

        Dim imagePath As String = Nothing
        Dim imageToLoad As Integer = Integer.Parse(context.Request("imageToLoad"))
        Select imageToLoad
            Case 0
                imagePath = "~/repository/source/temple1.jpg"
            Case 1
                imagePath = "~/repository/source/flowers1.jpg"
            Case 2
                imagePath = "~/repository/source/donkey1.jpg"
        End Select

        ' Load the image
        pictureTrimmer.LoadImageFromFileSystem(imagePath, New FreeCropConstraint(Nothing, 500, Nothing, 500))

        If (TypeOf pictureTrimmer Is PopupPictureTrimmerCore) Then
            ' Open the popup
            Dim popupPictureTrimmer As PopupPictureTrimmerCore = DirectCast(pictureTrimmer, PopupPictureTrimmerCore)
            popupPictureTrimmer.OpenPopup()
        End If
        
        ' Return the JSON
        context.Response.ContentType = "application/json"
        context.Response.Write(pictureTrimmer.GetAttachDataJSON())
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class