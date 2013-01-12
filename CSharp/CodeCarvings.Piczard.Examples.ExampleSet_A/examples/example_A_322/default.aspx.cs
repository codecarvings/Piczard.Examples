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
using CodeCarvings.Piczard.Web;

public partial class examples_example_A_322_default : System.Web.UI.Page
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
            // Load the image
            this.PopupPictureTrimmer1.LoadImageFromFileSystem("~/repository/source/flowers1.jpg", new FreeCropConstraint());
        }

        // Update the AutoPostBackOnPopupCloseMode
        // NOTE: The "AutoPostBackOnPopupCloseMode.Never" option is not available here because the 
        // PopupClose event handler cannot be managed with that option        
        this.PopupPictureTrimmer1.AutoPostBackOnPopupClose = (PictureTrimmerAutoPostBackOnPopupCloseMode)Enum.Parse(typeof(PictureTrimmerAutoPostBackOnPopupCloseMode), this.ddlAutoPostBackMode.SelectedValue);
    }

    protected void btnServerSideOpenPopup_Click(object sender, EventArgs e)
    {
        switch (this.ddlServerSidePopupSizeMode.SelectedIndex)
        {
            case 0:
                // Default:
                this.PopupPictureTrimmer1.OpenPopup();
                break;
            case 1:
                // Custom size:
                this.PopupPictureTrimmer1.OpenPopup(900, 540);
                break;
        }
    }

    protected void PopupPictureTrimmer1_PopupClose(object sender, PictureTrimmerPopupCloseEventArgs e)
    {
        this.MyLogEvent("Popup closed (SaveChanges=" + e.SaveChanges.ToString(System.Globalization.CultureInfo.InvariantCulture) + ").");
    }

    protected void PopupPictureTrimmer1_ValueChanged(object sender, PictureTrimmerValueChangedEventArgs e)
    {
        string logMessage = "Value changed ********************\r\n";
        logMessage += "**  Previous value: " + e.PreviousValue.ToString() + "\r\n";
        logMessage += "**  New value: " + e.NewValue.ToString() + "\r\n";
        logMessage += "********************************************************";

        this.MyLogEvent(logMessage);
    }

    protected void MyLogEvent(string message)
    {
        string newEvent = DateTime.Now.ToString("s") + " - " + message + "\r\n";
        this.txtMyLog.Text = newEvent + this.txtMyLog.Text;
    }

}
