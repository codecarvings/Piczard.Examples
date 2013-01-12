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

Imports CodeCarvings.Piczard

Partial Class examples_example_A_311_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID
    End Sub

    Protected Sub btnLoadImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadImage.Click
        If (Not Me.InlinePictureTrimmer1.ImageLoaded) Then
            Me.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", New FreeCropConstraint())
        Else
            Me.InlinePictureTrimmer1.UnloadImage()
        End If
    End Sub

    Protected Sub btnEnable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnable.Click
        Me.InlinePictureTrimmer1.Enabled = Not Me.InlinePictureTrimmer1.Enabled
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.InlinePictureTrimmer1.Visible = Not Me.InlinePictureTrimmer1.Visible
    End Sub
End Class
