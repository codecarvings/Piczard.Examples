/*
 * Piczard Examples | ExampleSet -A- C#
 * Copyright 2011-2013 Sergio Turolla - All Rights Reserved.
 * Author: Sergio Turolla
 * <codecarvings.com>
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
 * ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT 
 * SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR 
 * ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN 
 * ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
 * OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Web;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Plugins;

// DynamicLocalizationPlugin (Used by Example A.402)
// Please see also the web.config file - section: configuration/codeCarvings.piczard/coreSettings/plugins
public class MyDynamicLocalizationPlugin
    : Plugin, IDynamicLocalizationPlugin
{

    public MyDynamicLocalizationPlugin(PluginConstructorArgs constructorArgs)
        : base(constructorArgs)
    {
    }

    protected override void Initialize(PluginInitializationArgs initializationArgs)
    {
    }

    #region IDynamicLocalizationPlugin Members

    public PluginOperationResult<string> GetString(string key, string culture, string tag)
    {
        // ======== NOTE =========
        // Parameters:
        // - key = Resource key (e.g. "PT_MUI_Text_Details")
        //   Please see Piczard documentation for a list of all the resource keys.
        // - culture = PictureTrimmer.Culture property (e.g. "en", "en-US", "fr", etc...)
        //   Please see: http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo.aspx
        // - tag = PictureTrimmer.Tag property (optional value that can be used to pass parameters from the PictureTrimmer instance to the plugins)

        string result = null;

        if (culture == "en-JM")
        {
            // Manage the "en-JM" culture
            switch (key)
            {
                case "PT_MUI_Text_Details":
                    result = "Details [en-JM]";
                    if (tag == "ExA402_PopupPictureTrimmer")
                    {
                        result += " - Popup";
                    }
                    else
                    {
                        result += " - Inline";
                    }
                    break;
                case "PT_MUI_Text_Original":
                    result = "Source:";
                    break;
            }
        }

        if (result != null)
        {
            // Return the custom text
            return base.GetHandledResult<string>(result);
        }
        else
        {
            // Use the default text
            return base.GetNotHandledResult<string>();
        }
    }

    #endregion

}
