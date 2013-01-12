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

Partial Class examples_example_A_402_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me.IsPostBack) Then
            ' Load the image
            Me.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers1.jpg", New FreeCropConstraint())
            Me.PopupPictureTrimmer1.LoadImageFromFileSystem("~/repository/source/trevi1.jpg", New FreeCropConstraint())
        End If

        ' Set the culture
        Me.InlinePictureTrimmer1.Culture = Me.ddlLanguage.SelectedValue
        Me.PopupPictureTrimmer1.Culture = Me.ddlLanguage.SelectedValue

        ' ======== NOTE =========
        ' Please see:
        ' - The MyStaticLocalizationPlugin class contained in the ~/App_Code folder
        ' - The MyDynamicLocalizationPlugin class contained in the ~/App_Code folder
        ' - The web.config file - section: configuration/codeCarvings.piczard/coreSettings/plugins
    End Sub
End Class
