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

Imports System.Data
Imports System.Data.OleDb

Partial Class examples_example_A_502_default
    Inherits System.Web.UI.Page

    Protected Sub EditRecord(ByVal id As Integer)
        Response.Redirect("editRecord.aspx?Id=" + id.ToString(), True)
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        ' Go to the editRecord page
        Me.EditRecord(0)
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        ' Edit the record

        ' Get the record id
        Dim id As Integer = DirectCast(Me.GridView1.DataKeys(e.NewEditIndex).Value, Integer)

        Me.EditRecord(id)
    End Sub

    Protected Sub AccessDataSource1_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles AccessDataSource1.Deleted
        ' Delete the image files

        ' Get the record id
        Dim id As Integer = DirectCast(e.Command.Parameters("Id").Value, Integer)

        ' Get the picture file name
        Dim picture1FileName As String = String.Format("{0}.jpg", id)

        ' Delete the main image
        Dim picture1FilePath_main As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/main/"), picture1FileName)
        If (System.IO.File.Exists(picture1FilePath_main)) Then
            System.IO.File.Delete(picture1FilePath_main)
        End If

        ' Delete the thumbnail
        Dim picture1FilePath_thumbnail As String = System.IO.Path.Combine(Server.MapPath("~/repository/store/ex_A_502/picture1/thumbnail/"), picture1FileName)
        If (System.IO.File.Exists(picture1FilePath_thumbnail)) Then
            System.IO.File.Delete(picture1FilePath_thumbnail)
        End If
    End Sub
End Class
