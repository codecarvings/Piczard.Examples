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

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Web

Partial Class examples_example_A_322_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Me.ScriptManager1.IsInAsyncPostBack) Then
            ' After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "initializeUI", "initializeUI();", True)
        End If

        ' Reset some settings after every postback...
        Me.MyUpdateProgress1.AssociatedUpdatePanelID = Me.UpdatePanel1.UniqueID

        If (Not Me.IsPostBack) Then
            ' Load the image
            Me.PopupPictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers1.jpg", New FreeCropConstraint())
        End If

        ' Update the AutoPostBackOnPopupCloseMode
        ' NOTE: The "AutoPostBackOnPopupCloseMode.Never" option is not available here because the 
        ' PopupClose event handler cannot be managed with that option        
        Me.PopupPictureTrimmer1.AutoPostBackOnPopupClose = DirectCast([Enum].Parse(GetType(PictureTrimmerAutoPostBackOnPopupCloseMode), Me.ddlAutoPostBackMode.SelectedValue), PictureTrimmerAutoPostBackOnPopupCloseMode)

    End Sub

    Protected Sub btnServerSideOpenPopup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnServerSideOpenPopup.Click
        Select Me.ddlServerSidePopupSizeMode.SelectedIndex
            Case 0
                ' Default:
                Me.PopupPictureTrimmer1.OpenPopup()
            Case 1
                ' Custom size:
                Me.PopupPictureTrimmer1.OpenPopup(900, 540)
        End Select
    End Sub

    Protected Sub PopupPictureTrimmer1_PopupClose(ByVal sender As Object, ByVal e As CodeCarvings.Piczard.Web.PictureTrimmerPopupCloseEventArgs) Handles PopupPictureTrimmer1.PopupClose
        Me.MyLogEvent("Popup closed (SaveChanges=" + e.SaveChanges.ToString(System.Globalization.CultureInfo.InvariantCulture) + ").")
    End Sub

    Protected Sub PopupPictureTrimmer1_ValueChanged(ByVal sender As Object, ByVal e As CodeCarvings.Piczard.Web.PictureTrimmerValueChangedEventArgs) Handles PopupPictureTrimmer1.ValueChanged
        Dim logMessage As String = "Value changed ********************" + ControlChars.CrLf
        logMessage += "**  Previous value: " + e.PreviousValue.ToString() + ControlChars.CrLf
        logMessage += "**  New value: " + e.NewValue.ToString() + ControlChars.CrLf
        logMessage += "********************************************************"

        Me.MyLogEvent(logMessage)
    End Sub

    Protected Sub MyLogEvent(ByVal message As String)
        Dim newEvent As String = DateTime.Now.ToString("s") + " - " + message + ControlChars.CrLf
        Me.txtMyLog.Text = newEvent + Me.txtMyLog.Text
    End Sub

End Class
