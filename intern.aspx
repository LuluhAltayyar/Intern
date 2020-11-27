<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="intern.aspx.cs" 
    Inherits="guide.intern.intern" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <br />
</p>
<table class="nav-justified">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblOutput" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 160px">Intern Id</td>
        <td>
            <asp:TextBox ID="txtInternId" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 160px">F Name English</td>
        <td>
            <asp:TextBox ID="txtFnameEn" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 160px">F Name Arabic </td>
        <td>
            <asp:TextBox ID="txtFnameAr" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 160px">Country</td>
        <td>
            <asp:DropDownList ID="ddlCountry" runat="server" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 160px; height: 24px;">Salary</td>
        <td style="height: 24px">
            <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox>

           
        </td>
    </tr>
    <tr>
        <td style="width: 160px">Active</td>
        <td>
                <asp:RadioButtonList ID="rblActive" runat="server">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td style="width: 160px">Hobbies</td>
        <td>
            <asp:CheckBoxList ID="cblHobbies" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td style="width: 160px">&nbsp;</td>
        <td>
            <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            <asp:Button ID="btnShowData" runat="server" Text="Show Data" OnClick="btnShowData_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
            <asp:Button ID="btnShowIntern" runat="server" OnClick="btnShowIntern_Click" Text="Show Intern" />
            <br />
            <br />

        </td>
    </tr>
</table>
    <div>
             <asp:GridView ID="gvIntern" runat="server">
            </asp:GridView>
        <br />
            <asp:GridView ID="gvInternHobby" runat="server">
            </asp:GridView>

             <br />

    </div>
</asp:Content>
