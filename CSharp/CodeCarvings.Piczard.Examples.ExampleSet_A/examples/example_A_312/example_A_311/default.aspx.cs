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
using System.Web.UI;
using System.Web.UI.WebControls;

using CodeCarvings.Piczard;

public partial class examples_example_A_311_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ScriptManager1.IsInAsyncPostBack)
        {
            // After every Ajax postback re-initialize the JQuery UI elements
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "initializeUI", "initializeUI();", true);
        }

        // Reset some settings after every postback...
        this.MyUpdateProgress1.AssociatedUpdatePanelID = this.UpdatePanel1.UniqueID;
    }

    protected void btnLoadImage_Click(object sender, EventArgs e)
    {
        if (!this.InlinePictureTrimmer1.ImageLoaded)
        {
            this.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", new FreeCropConstraint());
        }
        else
        {
            this.InlinePictureTrimmer1.UnloadImage();
        }
    }

    protected void btnEnable_Click(object sender, EventArgs e)
    {
        this.InlinePictureTrimmer1.Enabled = !this.InlinePictureTrimmer1.Enabled;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.InlinePictureTrimmer1.Visible = !this.InlinePictureTrimmer1.Visible;
    }

}
