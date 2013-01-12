<%@ WebHandler Language="VB" Class="ImageFromDB_Content" %>
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

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web

Public Class ImageFromDB_Content : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        ' Get the parameters
        Dim id As Integer = Integer.Parse(context.Request("id"))
        Dim imageBackColorValue As Color = CodeCarvings.Piczard.Helpers.StringConversionHelper.StringToColor(context.Request("imageBackColorValue"))
        Dim imageBackColorApplyMode As PictureTrimmerImageBackColorApplyMode = DirectCast(Integer.Parse(context.Request("imageBackColorApplyMode")), PictureTrimmerImageBackColorApplyMode)
        
        Using connection As OleDbConnection = ExamplesHelper.GetNewOpenDbConnection()
            Using command As OleDbCommand = connection.CreateCommand()
                ' Load the image bytes
                command.CommandText = "SELECT [SourceImage] FROM [Ex_A_305] WHERE [Id]=@Id"
                
                command.Parameters.AddWithValue("@Id", id)
                Dim buffer As Byte() = DirectCast(command.ExecuteScalar(), Byte())

                ' Generate and transmit the content image
                Dim ipj As ImageProcessingJob = PictureTrimmerCore.GetContentImageJob(imageBackColorValue, imageBackColorApplyMode)
                ipj.TransmitProcessedImageToWebResponse(buffer, context.Response, PictureTrimmerCore.GetContentImageFormatEncoderParams(imageBackColorApplyMode))
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class