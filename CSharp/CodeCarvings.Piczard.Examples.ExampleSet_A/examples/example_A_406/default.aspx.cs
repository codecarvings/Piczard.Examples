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

public partial class examples_example_A_406_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            // Load the image
            this.laodImage("~/repository/source/valencia2.jpg");
        }
    }

    protected void laodImage(string sourceImagePath)
    {
        FixedCropConstraint cropConstraint = new FixedCropConstraint(350, 250);
        cropConstraint.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy.DoNotResize;

        // Load the image in the PictureTrimmer control
        this.InlinePictureTrimmer1.LoadImageFromFileSystem(sourceImagePath, cropConstraint);

        // Store the source image file path in the viewstate
        this.SourceImageFilePath = Server.MapPath(sourceImagePath);        
    }

    protected string SourceImageFilePath
    {
        get
        {
            return (string) this.ViewState["SourceImageFilePath"];
        }
        set
        {
            this.ViewState["SourceImageFilePath"] = value;
        }
    }

}
