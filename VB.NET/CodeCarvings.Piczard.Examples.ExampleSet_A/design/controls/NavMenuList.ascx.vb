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

Partial Class design_controls_NavMenuList
    Inherits System.Web.UI.UserControl

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)

        Me.phList1.Visible = False
        Me.phList2.Visible = False
        Me.phList3.Visible = False
        Me.phList4.Visible = False
        Me.phList5.Visible = False
        Me.phList6.Visible = False

        Dim phActiveList As PlaceHolder = DirectCast(Me.FindControl("phList" + Me.MacroAreaID.ToString(System.Globalization.CultureInfo.InvariantCulture)), PlaceHolder)
        If (phActiveList IsNot Nothing) Then
            phActiveList.Visible = True
        End If

        MyBase.OnPreRender(e)
    End Sub

    Public Property MacroAreaID() As Integer
        Get
            Dim result As Object = Me.ViewState("MacroAreaID")
            If (result IsNot Nothing) Then
                Return DirectCast(result, Integer)
            Else
                Return 1
            End If
        End Get
        Set(ByVal value As Integer)
            Me.ViewState("MacroAreaID") = value
        End Set
    End Property

    Public Property ShowExampleID() As Boolean
        Get
            Dim result As Object = Me.ViewState("ShowExampleID")
            If (result IsNot Nothing) Then
                Return DirectCast(result, Boolean)
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            Me.ViewState("ShowExampleID") = value
        End Set
    End Property

    Protected Function RenderExampleID(ByVal id As String) As String
        If (Me.ShowExampleID) Then
            Return "<strong>" + HttpUtility.HtmlEncode(id) + "</strong> - "
        Else
            Return ""
        End If
    End Function

End Class
