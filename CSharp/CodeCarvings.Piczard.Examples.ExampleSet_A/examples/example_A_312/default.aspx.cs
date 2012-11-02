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

public partial class examples_example_A_312_default : System.Web.UI.Page
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

        if (!this.IsPostBack)
        {
            this.UpdateUI();
        }
    }

    protected void InlinePictureTrimmer1_ValueChanged(object sender, PictureTrimmerValueChangedEventArgs e)
    {
        // NOTE: The ValueChanged event handler is raised only after a page postback!

        string logMessage;
        if (this.cbAutoUndoChanges.Checked)
        {
            // Undo the changes
            e.NewValue = e.PreviousValue;

            logMessage = "Value changed | Changes undone.";
        }
        else
        {
            logMessage = "Value changed ********************\r\n";
            logMessage += "**  Previous value: " + e.PreviousValue.ToString() + "\r\n";
            logMessage += "**  New value: " + e.NewValue.ToString() + "\r\n";
            logMessage += "********************************************************";
        }

        this.MyLogEvent(logMessage);
    }

    protected void MyLogEvent(string message)
    {
        string newEvent = DateTime.Now.ToString("s") + " - " + message + "\r\n";
        this.txtMyLog.Text = newEvent + this.txtMyLog.Text;
    }

    protected void btnLoadImage_Click(object sender, EventArgs e)
    {
        if (!this.InlinePictureTrimmer1.ImageLoaded)
        {
            this.InlinePictureTrimmer1.LoadImageFromFileSystem("~/repository/source/donkey1.jpg", new FreeCropConstraint());
            this.MyLogEvent("Image loaded.");
        }
        else
        {
            this.InlinePictureTrimmer1.UnloadImage();
            this.MyLogEvent("Image unloaded.");
        }

        this.UpdateUI();
    }

    protected void UpdateUI()
    {
        if (this.InlinePictureTrimmer1.ImageLoaded)
        {
            this.btnLoadImage.Text = "Unload image";
        }
        else
        {
            this.btnLoadImage.Text = "Load image";
        }
    }

}
