Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Public Class Employee
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim script As String = "$(document).ready(function(){$('#btnLoad').click();});"
            ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)
        End If
    End Sub

    Private Sub DefaultSettings()
        dvButtons.Visible = True
        dvMain.Visible = True
        dvNewEmployee.Visible = False
        'Buttons

        btnNew.Visible = True
        btnBack.Visible = False
        btnDelete.Visible = False
        btnEdit.Visible = False
        btnSave.Visible = False

        'Search
        txtSearch.Visible = True
        btnSearch.Visible = True
        tblSearch.Visible = True

        gvBirthDate.Visible = True
        txtReadBirthDate.Visible = False

        ClearFields()
    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs)
        DefaultSettings()
        LoadEmpMain()
    End Sub

    Private Sub LoadEmpMain()
        Dim obj As New clsConnectEmp
        Dim lData = obj.GetEmpDB(txtSearch.Text)
        gvEmpMain.DataSource = lData
        gvEmpMain.DataBind()

    End Sub

    Private Sub LoadCAHistory()
        Dim obj As New clsConnectCA
        Dim lData = obj.GetCAHistory(lblEmployeeID.Text)
        gvCAHistory.DataSource = lData
        gvCAHistory.DataBind()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        lblSavingControl.Text = 1

        'Buttons
        btnBack.Visible = True
        btnNew.Visible = False
        btnEdit.Visible = False
        btnSave.Visible = True

        'Content
        dvMain.Visible = False
        dvNewEmployee.Visible = True
        dvCashAdvanceHistory.Visible = False

        'Searc
        txtSearch.Visible = False
        btnSearch.Visible = False
        tblSearch.Visible = False

        LoadSupervisor()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        DefaultSettings()
        LoadEmpMain()
    End Sub

    Private Sub LoadSupervisor()
        Dim obj As New clsConnectSup
        Dim lData = obj.GetSupervisor()
        ddSupervisor.DataSource = lData
        ddSupervisor.DataValueField = "SupID"
        ddSupervisor.DataTextField = "SupName"
        ddSupervisor.DataBind()

        ddSupervisor.Items.Insert(0, "--Select--")
        ddSupervisor.Items(0).Value = 0
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim obj As New clsConnectEmp
            'obj.FullName = txtFullName.Text
            obj.FirstName = txtFirstName.Text
            obj.MiddleName = txtMiddleName.Text
            obj.LastName = txtLastName.Text
            obj.Address = txtAddress.Text
            obj.MobileNo = txtMobileNo.Text
            obj.EmailAdd = txtEmailAdd.Text
            obj.BirthDate = txtBirthDate.Text
            obj.Age = txtAge.Text
            obj.Supervisor = ddSupervisor.SelectedValue
            Dim oEmpDetails As New clsConnectEmp
            If lblSavingControl.Text = 1 Then
                oEmpDetails.SaveEmpDetails(obj)
            Else
                oEmpDetails.UpdateEmpRecord(lblEmployeeID.Text, obj)
            End If

        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnSave")
        Finally
            DefaultSettings()
            LoadEmpMain()
        End Try
    End Sub

    Private Sub ClearFields()
        'txtFullName.Text = String.Empty
        txtFirstName.Text = String.Empty
        txtMiddleName.Text = String.Empty
        txtLastName.Text = String.Empty
        txtAddress.Text = String.Empty
        txtMobileNo.Text = String.Empty
        txtEmailAdd.Text = String.Empty
        txtBirthDate.Text = String.Empty
        txtAge.Text = String.Empty
    End Sub

    Private Sub gvEmpMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEmpMain.SelectedIndexChanged
        Try
            SetReadOnlyFields()
            lblSavingControl.Text = 2
            'Buttons
            btnNew.Visible = False
            btnBack.Visible = True
            btnSave.Visible = False
            btnDelete.Visible = False
            btnEdit.Visible = True
            tblSearch.Visible = False
            'Textbox
            txtReadBirthDate.Visible = True
            'Content View
            gvBirthDate.Visible = False
            dvMain.Visible = False
            dvNewEmployee.Visible = True
            dvCashAdvanceHistory.Visible = True
            lblEmployeeID.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(0).Text.Replace("&nbsp;", ""))
            LoadCAHistory()
            'txtFullName.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(1).Text).ToString()
            txtFirstName.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(2).Text).ToString()
            txtMiddleName.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(3).Text).ToString()
            txtLastName.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(4).Text).ToString()
            txtAge.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(5).Text).ToString()
            txtAddress.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(6).Text).ToString()
            txtReadBirthDate.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(7).Text).ToString()
            txtBirthDate.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(7).Text).ToString()
            txtEmailAdd.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(8).Text).ToString()
            txtMobileNo.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(9).Text).ToString()
            LoadSupervisor()
            ddSupervisor.SelectedItem.Text = Server.HtmlDecode(gvEmpMain.SelectedRow.Cells(10).Text).ToString()
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -gvClick")
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim obj As New clsConnectEmp
            obj.DeleteEmpRecord(lblEmployeeID.Text)
            DefaultSettings()
            LoadEmpMain()
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnDelete")
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        btnSave.Visible = True
        btnDelete.Visible = True
        txtReadBirthDate.Visible = False
        gvBirthDate.Visible = True
        btnEdit.Visible = False
        SetEnableFields()
    End Sub

    Private Sub SetReadOnlyFields()
        txtFirstName.ReadOnly = True
        txtMiddleName.ReadOnly = True
        txtLastName.ReadOnly = True
        txtAddress.ReadOnly = True
        txtReadBirthDate.ReadOnly = True
        txtMobileNo.ReadOnly = True
        txtEmailAdd.ReadOnly = True
        txtAge.ReadOnly = True
        ddSupervisor.Enabled = False
    End Sub

    Private Sub SetEnableFields()
        txtFirstName.ReadOnly = False
        txtMiddleName.ReadOnly = False
        txtLastName.ReadOnly = False
        txtAddress.ReadOnly = False
        txtMobileNo.ReadOnly = False
        txtEmailAdd.ReadOnly = False
        txtAge.ReadOnly = False
        ddSupervisor.Enabled = True
    End Sub

    Private Sub gvEmpMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvEmpMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = True
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = True
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Me.Page.ClientScript.GetPostBackClientHyperlink(gvEmpMain, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadEmpMain()
    End Sub
End Class