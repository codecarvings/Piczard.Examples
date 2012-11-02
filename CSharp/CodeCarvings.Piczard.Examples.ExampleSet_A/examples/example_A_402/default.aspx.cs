/*
 * Piczard Examples | ExampleSet -A- C#
 * Copyright 2011-2012 Sergio Turolla - All Rights Reserved.
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
using System.Web.UI;
using System.Web.UI.WebControls;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_402_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            // Load the image
            this.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers1.jpg", new FreeCropConstraint());
            this.PopupPictureTrimmer1.LoadImageFromFileSystem("~/repository/source/trevi1.jpg", new FreeCropConstraint());
        }

        // Set the culture
        this.InlinePictureTrimmer1.Culture = this.ddlLanguage.SelectedValue;
        this.PopupPictureTrimmer1.Culture = this.ddlLanguage.SelectedValue;

        // ======== NOTE =========
        // Please see:
        // - The MyStaticLocalizationPlugin class contained in the ~/App_Code folder
        // - The MyDynamicLocalizationPlugin class contained in the ~/App_Code folder
        // - The web.config file - section: configuration/codeCarvings.piczard/coreSettings/plugins
    }
}
