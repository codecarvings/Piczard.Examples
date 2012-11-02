<%@ WebHandler Language="VB" Class="ImageFromDB" %>
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
Imports System.Data
Imports System.Data.SqlClient

Imports CodeCarvings.Piczard

Public Class ImageFromDB : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        ' Get the record id
        Dim id As Integer = Integer.Parse(context.Request("Id"))

        Using connection As SqlConnection = ExamplesHelper.GetNewOpenDbConnection_SqlServer()
            Using command As SqlCommand = connection.CreateCommand()
                ' Load the image bytes
                command.CommandText = "SELECT [Picture1_file_thumbnail] FROM [Ex_A_505] WHERE [Id]=@Id"
                command.Parameters.AddWithValue("@Id", id)
                Dim buffer As Byte() = DirectCast(command.ExecuteScalar(), Byte())

                If (Buffer.Length > 0) Then
                    ' Write the image bytes
                    context.Response.ContentType = ImageArchiver.GetMimeTypeFromImageFormatId(System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length)
                Else
                    ' No image -> Transmit a default image
                    ImageArchiver.TransmitImageFileToWebResponse(context.Server.MapPath("NoImage.gif"), context.Response)
                End If
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class