<%@ Page Title="Piczard Examples | Web - Image Upload Demos | SimpleImageUpload Control | Usage example #1 (image crop)" Language="VB" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="examples_example_A_502_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="5" Title="A.502 - SimpleImageUpload Control - Usage example #1 (image crop)" />

        <div style="border:solid 1px #54691d; padding:10px;">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False"
                DataSourceID="AccessDataSource1" EmptyDataText="No record found."
                DataKeyNames="Id" 
                GridLines="None"
                HeaderStyle-HorizontalAlign="left"
                Width="100%"
                >
                <Columns>
	                <asp:BoundField HeaderText="Id" DataField="Id" ReadOnly="True" SortExpression="Id">
		                <ItemStyle Width="30px" Height="40px" HorizontalAlign="Left" VerticalAlign="Middle" />
	                </asp:BoundField>
	                <asp:BoundField HeaderText="Title" DataField="Title" SortExpression="Title">
		                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
	                </asp:BoundField>
    	            
	                <asp:ImageField HeaderText="Preview" DataImageUrlField="Id" DataImageUrlFormatString="~/repository/store/ex_A_502/picture1/thumbnail/{0}.jpg"
	                    DataAlternateTextField="Id" DataAlternateTextFormatString="Record #{0}">
	                    <ItemStyle Width="60px" HorizontalAlign="Left" VerticalAlign="Middle" />
	                </asp:ImageField>
    	            
	                <asp:CommandField ShowCancelButton="False" ShowDeleteButton="True" ShowEditButton="True">
		                <ItemStyle Width="120px" HorizontalAlign="Left" VerticalAlign="Middle" />
	                </asp:CommandField>
                </Columns>
                <HeaderStyle BackColor="#54691d" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#e0eebc" />
            </asp:GridView>

            <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="<%$ ConnectionStrings:Db1ConnectionString %>"
                SelectCommand="SELECT [Id], [Title] FROM [Ex_A_502]"
                DeleteCommand="DELETE FROM [Ex_A_502] WHERE [Id]=@Id">
                <DeleteParameters>
	                <asp:ControlParameter ControlID="GridView1" Name="Id" PropertyName="SelectedValue" Type="Int32" />
                </DeleteParameters>
            </asp:AccessDataSource>

            <br />
            <asp:Button runat="server" ID="btnAddNew" Text="Add new record" CausesValidation="false" />
        </div>

        <div class="InlinePanel2" style="margin-top:50px;">
            Example A.502 highlights:<br />
            <ul>
                <li>Image files are stored in the file system (some temporary file is being created during the image upload/edit process)</li>
                <li>The record ID is used as image filename on the server.</li>
                <li>"Picture1" is a required field (client & server validation demo).</li>
                <li>If file upload is in progress then the user cannot save the record.</li>
            </ul>
        </div>

    </div>
</asp:Content>

