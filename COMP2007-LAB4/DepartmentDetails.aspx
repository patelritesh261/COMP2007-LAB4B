<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentDetails.aspx.cs" Inherits="COMP2007_LAB4.DepartmentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Department Details</h1>
                <h5>All Fields are Required</h5>
                <br />
                <div class="form-group">
                    <label class="control-label" for="DepartmentTextBox">Department Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="DepartmentTextBox" placeholder="Department Name" required="true"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="alert-danger" ControlToValidate="DepartmentTextBox" ErrorMessage="Enter Department Name "></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label" for="BudgetTextBox">Budget</label>
                    <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="BudgetTextBox" placeholder="Budget" required="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="alert-danger" ControlToValidate="BudgetTextBox" ErrorMessage="Enter Budget "></asp:RequiredFieldValidator>
                </div>
            
                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server" 
                        UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                    <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server" OnClick="SaveButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
