<%@ Page Title="Piczard Examples | Web - Image Upload Demos | SimpleImageUpload Control | Usage example #4 (SQL Server)" Language="C#" MasterPageFile="~/design/masters/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="examples_example_A_505_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageMenu" Runat="Server">
    <CommonUC:NavMenu runat="server" ID="NavMenu" MacroAreaID="5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="pageContainer">
        <CommonUC:PageTitle runat="server" ID="PageTitle" MacroAreaID="5" Title="A.505 - SimpleImageUpload Control - Usage example #4 (SQL Server)" />

        <div style="border:solid 1px #54691d; padding:10px;">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False"
                DataSourceID="SqlDataSource1" EmptyDataText="No record found."
                DataKeyNames="Id" 
                OnRowEditing="GridView1_RowEditing" 
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
	                
	                <asp:ImageField HeaderText="Preview" DataImageUrlField="Id" DataImageUrlFormatString="ImageFromDB.ashx?id={0}" 
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

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlServerConnectionString %>"
                SelectCommand="SELECT [Id], [Title] FROM [Ex_A_505]"
                DeleteCommand="DELETE FROM [Ex_A_505] WHERE [Id]=@Id" >
                <DeleteParameters>
	                <asp:ControlParameter ControlID="GridView1" Name="Id" PropertyName="SelectedValue" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>

            <br />
            <asp:Button runat="server" ID="btnAddNew" Text="Add new record" CausesValidation="false" OnClick="btnAddNew_Click" />
        </div>

        <div class="InlinePanel2" style="margin-top:50px;">
            Example A.505 highlights:<br />
            <ul>
                <li>Image files are stored in a MS SQL Server DB (some temporary file is being created during the image upload/edit process)</li>
                <li>The original image uploaded by the user is stored in the DB (thus further image processing is possible).</li>
            </ul>
        </div>

    </div>
</asp:Content>

