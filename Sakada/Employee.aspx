<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Employee.aspx.vb" Inherits="Sakada.Employee" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/w3.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
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
    <asp:UpdatePanel ID="pnlShowLoader" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <%--<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="pnlShowLoader">
                    <ProgressTemplate>
                        <div class="modalLoader">
                            <div class="centerLoader" style="text-align: center;">
                                <img alt="" src="../../../Images/wsi_logo.gif" /><br />
                                <label style="font-size:20px;color:dodgerblue" >Loading please wait...</label>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            <asp:Button runat="server" Text="Load" ID="btnLoad" style="display:none" ClientIDMode="Static" OnClick="btnLoad_Click"/>
            <asp:Label ID="lblEmployeeID" runat="server" Text="0" style="display:none;" />
            <asp:Label ID="lblID" runat="server" Text="0" style="display:none;" />
            <asp:Label ID="lblSavingControl" runat="server" Text="0" style="display:none;" />
            <div class="panel panel-default card" style="border-radius: 0px; background-color: white; margin-top:15px;">
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="myEmployee" runat="server" Style="border: 0px; background-color: #FFB973; color: #293955; font-size: 16px; height: 40px; width: 150px; font-weight: 600; text-align: center; vertical-align: middle;" Text="Employee"></asp:Button>
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
                        <asp:GridView ID="gvEmpMain" AlternatingRowStyle-BackColor="#efefef" RowStyle-Height="50px" RowStyle-VerticalAlign="Middle" BorderStyle="None" GridLines="Horizontal" AllowPaging="true" PageSize="10" CssClass="table table-hover" AutoGenerateColumns="false" runat="server" AutoPostBack="true" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" EmptyDataRowStyle-HorizontalAlign="Center">
                            <Columns>
                                <%--0--%><asp:BoundField DataField="EmpID" HeaderText="Employee ID" />
                                <%--1--%><asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <%--2--%><asp:BoundField DataField="FirstName" HeaderText="Full Name" />
                                <%--3--%><asp:BoundField DataField="MiddleName" HeaderText="Full Name" />
                                <%--4--%><asp:BoundField DataField="LastName" HeaderText="Full Name" />
                                <%--5--%><asp:BoundField DataField="Age" HeaderText="Age" />
                                <%--6--%><asp:BoundField DataField="Address" HeaderText="Address" />
                                <%--7--%><asp:BoundField DataField="BirthDate" HeaderText="Birthday" />
                                <%--8--%><asp:BoundField DataField="EmailAdd" HeaderText="Email Address" />
                                <%--9--%><asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                <%--10--%><asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                <%--11--%><asp:BoundField DataField="ID" HeaderText="ID" />
                            </Columns>

                            <HeaderStyle BorderColor="#0099FF" BorderStyle="None" />
                        </asp:GridView>
                    </div>
                    <div id="dvNewEmployee" runat="server" visible="false">
                        <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                            <asp:TableRow>
                                <asp:TableCell Width="70%" CssClass="borderRightOfTable" VerticalAlign="Top">
                                    <div id="dvEmployeeContent" runat="server" style="margin-top: 10px;">
                                        <div>
                                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="3" CssClass="paddingRow">
                                                        <div class="borderBottom">
                                                            <label style="color:#293955; font-size:18px;">Employee Details</label>
                                                        </div>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                <asp:TableCell Width="33%" CssClass="paddingRow">
                                                    <div>
                                                        <label class="labelStyle">First Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox BackColor="White" Width="100%" ID="txtFirstName" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell Width="33%" CssClass="paddingRow">
                                                    <div>
                                                        <label class="labelStyle">Middle Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox BackColor="White" Width="100%" ID="txtMiddleName" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell Width="33%" CssClass="paddingRow">
                                                    <div>
                                                        <label class="labelStyle">Last Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox BackColor="White" Width="100%" ID="txtLastName" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                    </div>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                                <%--<asp:TableRow>
                                                    <asp:TableCell Width="90%" CssClass="paddingRow">
                                                        <div>
                                                            <label class="labelStyle">Full Name&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="txtFullName" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="paddingRow" Width="10%" HorizontalAlign="Left">
                                                        <div>
                                                            <label class="labelStyle">Age&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="txtAge" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                        </div>
                                                    </asp:TableCell>
                                                </asp:TableRow>--%>
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="paddingRow" ColumnSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Address&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="txtAddress" CssClass="form-control textBoxBorderRadius" runat="server"/>
                                                        </div>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="paddingRow" Width="25%" RowSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Birthdate&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox Visible="false" BackColor="White" Width="100%" ID="txtReadBirthDate" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                            <div id="gvBirthDate" runat="server">
                                                                <div class="input-group date" id="grpDate" style="width: 100%">
                                                                    <asp:TextBox BackColor="White" ID="txtBirthDate" ClientIDMode="Static" Width="100%" Style="padding-left:8px;" CssClass="form-control textBoxBorderRadius" runat="server"/>
                                                                    <span class="input-group-addon" style="border-radius: 0px">
                                                                        <span class="glyphicon glyphicon-calendar" style="background-color: transparent;" />
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="paddingRow" Width="25%" RowSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Mobile No.&nbsp;</label>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <span>+63</span>
                                                                </div>
                                                               <asp:TextBox runat="server" ID="txtMobileNo" MaxLength="10" CssClass="form-control default-width" />
                                                            </div>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="paddingRow" Width="40%" RowSpan="3">
                                                        <div>
                                                            <label class="labelStyle">Email Address&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            
                                                            <asp:TextBox BackColor="White" Width="100%" ID="txtEmailAdd" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                        </div>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="paddingRow" Width="5%" HorizontalAlign="Left">
                                                        <div>
                                                            <label class="labelStyle">Age&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox BackColor="White" Width="100%" ID="txtAge" CssClass="form-control textBoxBorderRadius" runat="server" />
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell CssClass="paddingRow" Width="30%" RowSpan="3">
                                                        <asp:UpdatePanel runat="server" ID="pnlUpdateSup" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div>
                                                                <label class="labelStyle">Milling Supervisor&nbsp;</label>
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList BackColor="White" ID="ddSupervisor" Width="50%" CssClass="form-control textBoxBorderRadius" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
                        &nbsp;
                        <div id="dvCashAdvanceHistory" runat="server" visible="true">
                        <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                            <asp:TableRow>
                                <asp:TableCell Width="70%" CssClass="borderRightOfTable" VerticalAlign="Top">
                                    <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="3" CssClass="paddingRow">
                                                <div class="borderBottom">
                                                    <label style="color:#293955; font-size:18px;">Cash Advance History</label>
                                                </div>
                                                <div id="gvCashAdvance" runat="server">
                                                    <asp:UpdatePanel runat="server" ID="pnlUpdateCAH" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvCAHistory" AlternatingRowStyle-BackColor="#efefef" RowStyle-Height="50px" RowStyle-VerticalAlign="Middle" BorderStyle="None" GridLines="Horizontal" AllowPaging="true" PageSize="10" CssClass="table table-hover" AutoGenerateColumns="false" runat="server" AutoPostBack="true">
                                                                <Columns>
                                                                    <%--0--%><asp:BoundField DataField="CADate" HeaderText="Date" />
                                                                    <%--1--%><asp:BoundField DataField="CASupervisor" HeaderText="Supervisor" />
                                                                    <%--2--%><asp:BoundField DataField="CAAmount" HeaderText="Amount" />
                                                                    <%--3--%><asp:BoundField DataField="CAStatus" HeaderText="Status" />
                                                                </Columns>
                                                                <HeaderStyle BorderColor="#0099FF" BorderStyle="None" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        </div>
                    </div>
                    &nbsp;
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function pageLoad(sender, args) {
            $(function() {
                $("#grpDate").datepicker({
                    autoclose: true,
                    todayBtn: "linked",
                    todayHighlight: true
                });
            });
        }
    </script>
</asp:Content>
