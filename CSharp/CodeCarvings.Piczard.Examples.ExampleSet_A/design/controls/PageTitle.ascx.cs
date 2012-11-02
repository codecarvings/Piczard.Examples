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

public partial class design_controls_PageTitle : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        string macroAreaTitle = "";
        switch (this.MacroAreaID)
        {
            case 1:
                macroAreaTitle = "Piczard: The Basics";
                break;
            case 2:
                macroAreaTitle = "Image manipulation";
                break;
            case 3:
                macroAreaTitle = "Web - PictureTrimmer";
                break;
            case 4:
                macroAreaTitle = "Customize Piczard";
                break;
            case 5:
                macroAreaTitle = "Web - Image Upload Demos";
                break;
            case 6:
                macroAreaTitle = "Third Party Plugins";
                break;
        }

        this.litMacroAreaTitle.Text = HttpUtility.HtmlEncode(macroAreaTitle.ToUpper());
        this.litMainTitle.Text = HttpUtility.HtmlEncode(this.Title.ToUpper());

        base.OnPreRender(e);
    }

    public int MacroAreaID
    {
        get
        {
            object result = this.ViewState["MacroAreaID"];
            if (result != null)
            {
                return (int)result;
            }
            else
            {
                return 1;
            }
        }
        set
        {
            this.ViewState["MacroAreaID"] = value;
        }
    }

    public string Title
    {
        get
        {
            object result = this.ViewState["Title"];
            if (result != null)
            {
                return (string)result;
            }
            else
            {
                return "";
            }
        }
        set
        {
            this.ViewState["Title"] = value;
        }
    }
}
