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

Imports Microsoft.VisualBasic

Imports CodeCarvings.Piczard
Imports CodeCarvings.Piczard.Plugins

' DynamicLocalizationPlugin (Used by Example A.402)
' Please see also the web.config file - section: configuration/codeCarvings.piczard/coreSettings/plugins
Public Class MyDynamicLocalizationPlugin 
    Inherits Plugin
    Implements IDynamicLocalizationPlugin

    Public Sub New(ByVal constructorArgs As PluginConstructorArgs)
        MyBase.New(constructorArgs)
    End Sub

    Protected Overrides Sub Initialize(ByVal initializationArgs As CodeCarvings.Piczard.Plugins.PluginInitializationArgs)

    End Sub

#Region "IDynamicLocalizationPlugin Members"

    Protected Function GetString(ByVal key As String, ByVal culture As String, ByVal tag As String) As PluginOperationResult(Of String) Implements IDynamicLocalizationPlugin.GetString
        ' ======== NOTE =========
        ' Parameters:
        ' - key = Resource key (e.g. "PT_MUI_Text_Details")
        '   Please see Piczard documentation for a list of all the resource keys.
        ' - culture = PictureTrimmer.Culture property (e.g. "en", "en-US", "fr", etc...)
        '   Please see: http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo.aspx
        ' - tag = PictureTrimmer.Tag property (optional value that can be used to pass parameters from the PictureTrimmer instance to the plugins)

        Dim result As String = Nothing

        If (culture = "en-JM") Then
            ' Manage the "en-JM" culture
            Select Case key
                Case "PT_MUI_Text_Details"
                    result = "Details [en-JM]"
                    If (tag = "ExA402_PopupPictureTrimmer") Then
                        result += " - Popup"
                    Else
                        result += " - Inline"
                    End If
                Case "PT_MUI_Text_Original"
                    result = "Source:"
            End Select
        End If

        If (result IsNot Nothing) Then
            ' Return the custom text
            Return MyBase.GetHandledResult(Of String)(result)
        Else
            ' Use the default text
            Return MyBase.GetNotHandledResult(Of String)()
        End If
    End Function

#End Region

End Class
