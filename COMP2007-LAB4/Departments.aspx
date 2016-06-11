<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="COMP2007_LAB4.Departments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Departments List</h1>
                <a href="DepartmentDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Department</a>
               <label for="PageSizeDropdownlist">Records per page:</label>
                 <asp:DropDownList ID="PageSizeDropdownlist" runat="server" AutoPostBack="true"
                     CssClass="btn btn-default btn-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropdownlist_SelectedIndexChanged">
                     <asp:ListItem Text="3" Value="3"></asp:ListItem>
                      <asp:ListItem Text="5" Value="5"></asp:ListItem>
                      <asp:ListItem Text="All" Value="10000"></asp:ListItem>
                </asp:DropDownList>
                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" 
                    ID="DepartmentsGridView" AutoGenerateColumns="false" DataKeyNames="DepartmentID" OnRowDeleting="DepartmentsGridView_RowDeleting" 
                    AllowPaging="true" PageSize="3" OnPageIndexChanging="DepartmentsGridView_PageIndexChanging"
                    AllowSorting="true" OnSorting="DepartmentsGridView_Sorting"
                    OnRowDataBound="DepartmentsGridView_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" Visible="true" SortExpression="DepartmentID" />
                        <asp:BoundField DataField="Name" HeaderText="Department Name" Visible="true" SortExpression="Name" />
                        <asp:BoundField DataField="Budget" HeaderText="Budget" Visible="true" SortExpression="Budget" DataFormatString="{0:C}"/>
                       
                        <asp:HyperLinkField HeaderText="Edit"  Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit"  DataNavigateUrlFormatString="DepartmentDetails.aspx?DepartmentID={0}"  DataNavigateUrlFields="DepartmentID"   ControlStyle-CssClass="btn btn-primary btn-sm" />
                        <asp:CommandField HeaderText="Delete"  DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>



</asp:Content>
