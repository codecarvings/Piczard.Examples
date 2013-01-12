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
Imports CodeCarvings.Piczard.Web

Partial Class examples_example_A_312_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID

        If (Not Me.IsPostBack) Then
            Me.UpdateUI()
        End If
    End Sub

    Protected Sub InlinePictureTrimmer1_ValueChanged(ByVal sender As Object, ByVal e As CodeCarvings.Piczard.Web.PictureTrimmerValueChangedEventArgs) Handles InlinePictureTrimmer1.ValueChanged
        ' NOTE: The ValueChanged event handler is raised only after a page postback!

        Dim logMessage As String
        If (Me.cbAutoUndoChanges.Checked) Then
            ' Undo the changes
            e.NewValue = e.PreviousValue

            logMessage = "Value changed | Changes undone."
        Else
            logMessage = "Value changed ********************" + ControlChars.CrLf
            logMessage += "**  Previous value: " + e.PreviousValue.ToString() + ControlChars.CrLf
            logMessage += "**  New value: " + e.NewValue.ToString() + ControlChars.CrLf
            logMessage += "********************************************************"
        End If

        Me.MyLogEvent(logMessage)
    End Sub

    Protected Sub MyLogEvent(ByVal message As String)
        Dim newEvent As String = DateTime.Now.ToString("s") + " - " + message + ControlChars.CrLf
        Me.txtMyLog.Text = newEvent + Me.txtMyLog.Text
    End Sub

    Protected Sub btnLoadImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadImage.Click
        If (Not Me.InlinePictureTrimmer1.ImageLoaded) Then
            Me.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", New FreeCropConstraint())
            Me.MyLogEvent("Image loaded.")
        Else
            Me.InlinePictureTrimmer1.UnloadImage()
            Me.MyLogEvent("Image unloaded.")
        End If

        Me.UpdateUI()
    End Sub

    Protected Sub UpdateUI()
        If (Me.InlinePictureTrimmer1.ImageLoaded) Then
            Me.btnLoadImage.Text = "Unload image"
        Else
            Me.btnLoadImage.Text = "Load image"
        End If
    End Sub

End Class
