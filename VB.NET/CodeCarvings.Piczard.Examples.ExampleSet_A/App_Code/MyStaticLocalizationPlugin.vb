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

' StaticLocalizationPlugin (Used by Example A.402)
' Please see also the web.config file - section: configuration/codeCarvings.piczard/coreSettings/plugins
Public Class MyStaticLocalizationPlugin
    Inherits Plugin
    Implements IStaticLocalizationPlugin

    Public Sub New(ByVal constructorArgs As PluginConstructorArgs)
        MyBase.New(constructorArgs)
    End Sub

    Protected Overrides Sub Initialize(ByVal initializationArgs As CodeCarvings.Piczard.Plugins.PluginInitializationArgs)

    End Sub

#Region "IStaticLocalizationPlugin Members"

    Protected Function GetCultures() As CodeCarvings.Piczard.Plugins.PluginOperationResult(Of String()) Implements CodeCarvings.Piczard.Plugins.IStaticLocalizationPlugin.GetCultures
        ' Get a list of the handled cultures
        Return MyBase.GetHandledResult(Of String())(New String() {"en-TT", "en-ZW"})
    End Function

    Protected Function GetDictionary(ByVal culture As String) As CodeCarvings.Piczard.Plugins.PluginOperationResult(Of System.Collections.Generic.Dictionary(Of String, String)) Implements CodeCarvings.Piczard.Plugins.IStaticLocalizationPlugin.GetDictionary
        If (culture = "en-TT") Then
            ' Manage the "en-TT" culture

            Dim result As Dictionary(Of String, String) = New Dictionary(Of String, String)()

            ' Customize only 2 texts (other texts are not customized)
            result("PT_MUI_Text_Details") = "Details [en-TT]"
            result("PT_MUI_Text_Original") = "Source:"

            Return MyBase.GetHandledResult(Of Dictionary(Of String, String))(result)
        End If

        If (culture = "en-ZW") Then
            ' Manage the "en-ZW" culture

            ' Load an xml file containing the localized texts
            Dim filePath As String = HttpContext.Current.Server.MapPath("~/App_Data/CodeCarvings.Piczard.en-ZW.resx.xml")
            Dim result As Dictionary(Of String, String) = CodeCarvings.Piczard.Globalization.GlobalizationManager.LoadDictionaryFromXMLFile(filePath)

            Return MyBase.GetHandledResult(Of Dictionary(Of String, String))(result)
        End If

        ' Culture not handled...
        Return MyBase.GetNotHandledResult(Of Dictionary(Of String, String))()
    End Function

#End Region

End Class
