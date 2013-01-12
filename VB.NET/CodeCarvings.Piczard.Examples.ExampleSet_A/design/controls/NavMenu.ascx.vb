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

Partial Class design_controls_NavMenu
    Inherits System.Web.UI.UserControl

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

    Protected Function RenderAccordianActive() As String
        Dim n As Integer = Me.MacroAreaID - 1
        Return n.ToString(System.Globalization.CultureInfo.InvariantCulture)
    End Function

End Class
