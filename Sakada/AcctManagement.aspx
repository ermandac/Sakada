<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="AcctManagement.aspx.vb" Inherits="Sakada.AcctManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/w3.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <link href="datepicker/datepicker1.css" rel="stylesheet" />
    <script src="datepicker/datepicker1.js"></script>
    <style>
        .paddingRow {
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 4px;
        }

        .textBoxBorderRadius {
            border-radius: 0px;
            padding: 0px;
            padding-left: 5px;
            padding-right: 5px;
            /*-moz-box-sizing: content-box;
            box-sizing: content-box;*/
            height: 32px;
        }

        .labelProp {
            font-size: 11px;
            margin: 0px;
            font-family: 'Segoe UI';
        }

        .borderRightOfTable {
            border-right: 1px solid #CCCCCC;
            /*padding-right:10px;*/
        }
        .borderBottom {
            border-bottom: 1px solid #FFB973;
        }
        .card {
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel runat="server" ID="pnlShowLoader">
         <ContentTemplate>
             <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
             <asp:Button runat="server" Text="Load" ID="btnLoad" style="display:none" ClientIDMode="Static" OnClick="btnLoad_Click"/>
             <asp:Label ID="lblEmployeeID" runat="server" Text="0" style="display:none;" />
             <asp:Label ID="lblSavingControl" runat="server" Text="0" style="display:none;" />
             <div class="panel panel-default card" style="border-radius: 0px; background-color: white; margin-top:15px;">
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="myAcctMngt" runat="server" Style="border: 0px; background-color: #FFB973; color: #293955; font-size: 16px; height: 40px; width: 250px; font-weight: 600; text-align: center; vertical-align: middle;" Text="Account Management" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <div id="dvButtons" visible="false" runat="server" style="padding: 10px 10px 10px 10px; background-color: #EEEEEE">
                        <asp:Table Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                            <asp:TableRow>
                                <asp:TableCell Width="75%">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-default buttonStyle" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default buttonStyle" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default buttonStyle" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-default buttonStyle" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger buttonStyle w3-right-align" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:TableCell>
                                <asp:TableCell Width="25%" HorizontalAlign="Right">
                                    <asp:Table Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                        <asp:TableRow ID="tblSearch" BorderColor="#0A90CF" BorderStyle="Solid" BorderWidth="1px">
                                            <asp:TableCell Width="90%">
                                                <asp:Textbox ID="txtSearch" CssClass="form-control" Style="border: 0px solid #0A90CF; border-radius: 0px;" runat="server" placeholder="Search" Width="100%" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="10%">
                                                <div>
                                                    <asp:LinkButton ID="btnSearch" Style="background-color: #0A90CF; border-radius: 0px; border: 0px;" runat="server" CssClass="btn btn-info"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                                                </div>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <hr style="margin: 0px 0px 0px 0px;" />
                    <div id="dvMain" runat="server" visible="false">
                        <asp:GridView ID="gvAcctMain" AlternatingRowStyle-BackColor="#efefef" RowStyle-Height="50px" RowStyle-VerticalAlign="Middle" BorderStyle="None" GridLines="Horizontal" AllowPaging="true" PageSize="10" CssClass="table table-hover" AutoGenerateColumns="false" runat="server" AutoPostBack="true" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" EmptyDataRowStyle-HorizontalAlign="Center">
                            <Columns>
                                <%--0--%><asp:BoundField DataField="LoginID" HeaderText="ID" />
                                <%--1--%><asp:BoundField DataField="UserName" HeaderText="Name" />
                                <%--2--%><asp:BoundField DataField="LoginName" HeaderText="Login Name" />
                                <%--3--%><asp:BoundField DataField="AccessLevel" HeaderText="Access Level" />
                            </Columns>

                            <HeaderStyle BorderColor="#0099FF" BorderStyle="None" />
                        </asp:GridView>
                    </div>
                    <div id="dvNewCA" runat="server" visible="true">
                        <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                            <asp:TableRow>
                                <asp:TableCell Width="70%" CssClass="borderRightOfTable" VerticalAlign="Top">
                                    <div id="dvCAContent" runat="server" style="margin-top: 10px;">
                                        <div>
                                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="paddingRow" Width="50%" RowSpan="2">
                                                        <asp:UpdatePanel runat="server" ID="pnlUpdateSup" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div>
                                                                <label class="labelStyle">Milling Supervisor&nbsp;</label>
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList BackColor="White" ID="ddSupervisor" Width="100%" CssClass="form-control textBoxBorderRadius" runat="server" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="paddingRow" Width="50%" RowSpan="2">
                                                        <asp:UpdatePanel runat="server" ID="pnlUpdateEmp" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div>
                                                                <label class="labelStyle">Access Level&nbsp;</label>
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList BackColor="White" ID="ddAccessLevel" Width="100%" CssClass="form-control textBoxBorderRadius" runat="server" AutoPostBack="true">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem>Supervisor</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="paddingRow" Width="50%" RowSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Login Name&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="txtLoginName" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="paddingRow" Width="50%" RowSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Password&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="txtPassword" CssClass="form-control textBoxBorderRadius" runat="server" TextMode="Password" />
                                                        </div>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="paddingRow" Width="50%" RowSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Email Address&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="TextBox1" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="paddingRow" Width="50%" RowSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Confirm Password&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="TextBox2" CssClass="form-control textBoxBorderRadius" runat="server" TextMode="Password" />
                                                        </div>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>
                                    </div>
                                </asp:TableCell>
                                <asp:TableCell Width="30%" VerticalAlign="Top">
                                    <div style="margin-top:10px;">

                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    &nbsp;
                </div>
             </div>
         </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
