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

Partial Class design_controls_PageTitle
    Inherits System.Web.UI.UserControl

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        Dim macroAreaTitle As String = ""
        Select Case Me.MacroAreaID
            Case 1
                macroAreaTitle = "Piczard: The Basics"
            Case 2
                macroAreaTitle = "Image manipulation"
            Case 3
                macroAreaTitle = "Web - PictureTrimmer"
            Case 4
                macroAreaTitle = "Customize Piczard"
            Case 5
                macroAreaTitle = "Web - Image Upload Demos"
            Case 6
                macroAreaTitle = "Third Party Plugins"
        End Select

        Me.litMacroAreaTitle.Text = HttpUtility.HtmlEncode(macroAreaTitle.ToUpper())
        Me.litMainTitle.Text = HttpUtility.HtmlEncode(Me.Title.ToUpper())

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

    Public Property Title() As String
        Get
            Dim result As Object = Me.ViewState("Title")
            If (result IsNot Nothing) Then
                Return DirectCast(result, String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Me.ViewState("Title") = value
        End Set
    End Property

End Class
