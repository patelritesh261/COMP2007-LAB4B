﻿<%@ Page Title="Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="COMP2007_LAB4.Students" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Student List</h1>
                <a href="StudentDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Student</a>
               <label for="PageSizeDropdownlist">Records per page:</label>
                 <asp:DropDownList ID="PageSizeDropdownlist" runat="server" AutoPostBack="true"
                     CssClass="btn btn-default btn-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropdownlist_SelectedIndexChanged">
                     <asp:ListItem Text="3" Value="3"></asp:ListItem>
                      <asp:ListItem Text="5" Value="5"></asp:ListItem>
                      <asp:ListItem Text="All" Value="10000"></asp:ListItem>
                </asp:DropDownList>
                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" 
                    ID="StudentsGridView" AutoGenerateColumns="false" DataKeyNames="StudentID" OnRowDeleting="StudentsGridView_RowDeleting" AllowPaging="true" PageSize="3" OnPageIndexChanging="StudentsGridView_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="StudentID" HeaderText="Student ID" Visible="true" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" Visible="true" />
                        <asp:BoundField DataField="FirstMidName" HeaderText="First Name" Visible="true" />
                        <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" Visible="true"
                            DataFormatString="{0:MMM dd, yyyy}" />
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>




</asp:Content>
