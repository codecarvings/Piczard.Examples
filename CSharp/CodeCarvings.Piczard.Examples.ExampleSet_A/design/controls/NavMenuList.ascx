<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavMenuList.ascx.cs" Inherits="design_controls_NavMenuList" %>

<asp:PlaceHolder runat="server" ID="phList1">
    <ul class="NavMenu_UL1"> 
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_101/default.aspx"><% =this.RenderExampleID("A.101") %>Use a single image filter</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_102/default.aspx"><% =this.RenderExampleID("A.102") %>Use multiple image filters</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_103/default.aspx"><% =this.RenderExampleID("A.103") %>Load &amp; save image files</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_104/default.aspx"><% =this.RenderExampleID("A.104") %>Batch process multiple images</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_105/default.aspx"><% =this.RenderExampleID("A.105") %>Generate different images from one picture</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_106/default.aspx"><% =this.RenderExampleID("A.106") %>Working with different image resolutions</asp:HyperLink>
        </li>
    </ul>  
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="phList2">
    <ul class="NavMenu_UL1"> 
        <li>
            Resize an image<br />
            <ul class="NavMenu_UL2"> 
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_201/default.aspx"><% =this.RenderExampleID("A.201") %>Manual</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_202/default.aspx"><% =this.RenderExampleID("A.202") %>Automatic</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li>
            Crop an image<br />
            <ul class="NavMenu_UL2"> 
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_211/default.aspx"><% =this.RenderExampleID("A.211") %>Manual</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_212/default.aspx"><% =this.RenderExampleID("A.212") %>Automatic</asp:HyperLink>
                </li>
            </ul>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_221/default.aspx"><% =this.RenderExampleID("A.221") %>Rotate &amp; flip an image</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_222/default.aspx"><% =this.RenderExampleID("A.222") %>Adjust and change image colors</asp:HyperLink> 
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_223/default.aspx"><% =this.RenderExampleID("A.223") %>Apply a text watermark</asp:HyperLink> 
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_224/default.aspx"><% =this.RenderExampleID("A.224") %>Apply an image watermark</asp:HyperLink> 
        </li>
    </ul>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="phList3">
    <ul class="NavMenu_UL1"> 
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_301/default.aspx"><% =this.RenderExampleID("A.301") %>Overview</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_302/default.aspx"><% =this.RenderExampleID("A.302") %>Crop enabled</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_303/default.aspx"><% =this.RenderExampleID("A.303") %>Crop disabled</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_304/default.aspx"><% =this.RenderExampleID("A.304") %>UserState &amp; PictureTrimmer Value</asp:HyperLink>
        </li>        
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_305/default.aspx"><% =this.RenderExampleID("A.305") %>Load &amp; save images and values</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_306/default.aspx"><% =this.RenderExampleID("A.306") %>Load &amp; save images and values (SQL Server)</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_307/default.aspx"><% =this.RenderExampleID("A.307") %>Client side load, unload and save images</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_308/default.aspx"><% =this.RenderExampleID("A.308") %>Apply filters to the selected image</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_309/default.aspx"><% =this.RenderExampleID("A.309") %>Cropping Landscape and Portrait images</asp:HyperLink>
        </li>
        <li>
            InlinePictureTrimmer<br />
            <ul class="NavMenu_UL2"> 
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_311/default.aspx"><% =this.RenderExampleID("A.311") %>Overview</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_312/default.aspx"><% =this.RenderExampleID("A.312") %>Server side events</asp:HyperLink>
                </li>  
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_313/default.aspx"><% =this.RenderExampleID("A.313") %>Client side events</asp:HyperLink>
                </li>  
            </ul>
        </li>
        <li>
            PopupPictureTrimmer<br />
            <ul class="NavMenu_UL2"> 
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_321/default.aspx"><% =this.RenderExampleID("A.321") %>Overview</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_322/default.aspx"><% =this.RenderExampleID("A.322") %>Server side Events</asp:HyperLink>
                </li>  
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_323/default.aspx"><% =this.RenderExampleID("A.323") %>Client side Events</asp:HyperLink>
                </li>                 
            </ul>
        </li>
    </ul>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="phList4">
    <ul class="NavMenu_UL1">
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_401/default.aspx"><% =this.RenderExampleID("A.401") %>Custom filters</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_402/default.aspx"><% =this.RenderExampleID("A.402")%>Localize &amp; customize texts</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_403/default.aspx"><% =this.RenderExampleID("A.403") %>Customize layout</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_404/default.aspx"><% =this.RenderExampleID("A.404") %>Dynamic content image &amp; GUI expansion</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_405/default.aspx"><% =this.RenderExampleID("A.405") %>Enrich the PopupPictureTrimmer window</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_406/default.aspx"><% =this.RenderExampleID("A.406") %>Real time crop preview</asp:HyperLink>
        </li>
    </ul>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="phList5">
    <ul class="NavMenu_UL1"> 
        <li>
            SimpleImageUpload Control<br />
            <ul class="NavMenu_UL2">
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_501/default.aspx"><% =this.RenderExampleID("A.501") %>Overview</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_502/default.aspx"><% =this.RenderExampleID("A.502") %>Usage example #1 (image crop)</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_503/default.aspx"><% =this.RenderExampleID("A.503") %>Usage example #2</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_504/default.aspx"><% =this.RenderExampleID("A.504") %>Usage example #3</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_505/default.aspx"><% =this.RenderExampleID("A.505") %>Usage example #4 (SQL Server)</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_506/default.aspx"><% =this.RenderExampleID("A.506") %>Usage example #5</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_507/default.aspx"><% =this.RenderExampleID("A.507") %>Usage example #6 (image resize + watermark)</asp:HyperLink>
                </li>                
            </ul>
        </li>
        <li>
            Programmatic Image Processing<br />
            <ul class="NavMenu_UL2">
                <li>
                    <asp:HyperLink runat="server" NavigateUrl="~/examples/example_A_511/default.aspx"><% =this.RenderExampleID("A.511") %>Default file upload #1</asp:HyperLink>
                </li>
            </ul>
        </li>        
    </ul>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="phList6">
    <ul class="NavMenu_UL1"> 
        <li>
            <a href="http://digivogue.com/products/Piczard-DeBrand/comparison/" onclick="window.open(this.href, '_blank', 'width=1400,height=800,scrollbars=yes'); return false;"><strong>A.601</strong> - DeBrand Plugin By Digivogue</a>
        </li>
    </ul>  
</asp:PlaceHolder>