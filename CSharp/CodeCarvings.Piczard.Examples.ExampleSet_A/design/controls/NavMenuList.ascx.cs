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

public partial class design_controls_NavMenuList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        this.phList1.Visible = false;
        this.phList2.Visible = false;
        this.phList3.Visible = false;
        this.phList4.Visible = false;
        this.phList5.Visible = false;
        this.phList6.Visible = false;

        PlaceHolder phActiveList = (PlaceHolder) this.FindControl("phList" + this.MacroAreaID.ToString(System.Globalization.CultureInfo.InvariantCulture));
        if (phActiveList != null)
        {
            phActiveList.Visible = true;
        }

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

    public bool ShowExampleID
    {
        get
        {
            object result = this.ViewState["ShowExampleID"];
            if (result != null)
            {
                return (bool)result;
            }
            else
            {
                return false;
            }
        }
        set
        {
            this.ViewState["ShowExampleID"] = value;
        }
    }

    protected string RenderExampleID(string id)
    {
        if (this.ShowExampleID)
        {
            return "<strong>" + HttpUtility.HtmlEncode(id) + "</strong> - ";
        }
        else
        {
            return "";
        }
    }

}
