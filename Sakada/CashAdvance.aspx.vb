Public Class CashAdvance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim script As String = "$(document).ready(function(){$('#btnLoad').click();});"
            ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)
        End If
    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs)
        LoadCAMain()
        DefaultSettings()
        LoadSupervisor()
        LoadInitialEmp()
    End Sub

    Private Sub LoadCAMain()
        Dim obj As New clsConnectCA
        Dim lData = obj.GetCADB(txtSearch.Text)
        gvCAMain.DataSource = lData
        gvCAMain.DataBind()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadCAMain()
    End Sub

    Private Sub DefaultSettings()
        dvButtons.Visible = True
        dvMain.Visible = True
        dvNewCA.Visible = False
        'Buttons

        If (Session.Item("accessLevel") = "Admin") Then
            btnNew.Visible = True
        Else
            btnNew.Visible = False
        End If

        btnBack.Visible = False
        btnDelete.Visible = False
        btnEdit.Visible = False
        btnSave.Visible = False
        btnApprove.Visible = False
        btnDisapprove.Visible = False

        'Search
        txtSearch.Visible = True
        btnSearch.Visible = True
        tblSearch.Visible = True

        gvBirthDate.Visible = True
        txtReadBirthDate.Visible = False
    End Sub

    Private Sub gvCAMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCAMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(0).Visible = False

        End If

        If e.Row.RowType = DataControlRowType.Header Then
            'e.Row.Cells(0).Visible = False

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Me.Page.ClientScript.GetPostBackClientHyperlink(gvCAMain, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim obj As New clsConnectCA
            obj.CASupervisor = ddSupervisor.SelectedValue
            obj.CAEmployee = ddEmployee.SelectedValue
            obj.CADate = txtCADate.Text
            obj.CAAmount = txtAmount.Text
            obj.CAEmpID = ddEmployee.SelectedValue
            Dim oCADetails As New clsConnectCA
            If lblSavingControl.Text = 1 Then
                oCADetails.SaveCADetails(obj)
            ElseIf lblSavingControl.Text = 2 Then
                oCADetails.UpdateCADetails(lblCashAdvanceID.Text, obj)
            End If

        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnSave")
        Finally
            DefaultSettings()
            LoadCAMain()
        End Try
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
        dvNewCA.Visible = True

        'Searc
        txtSearch.Visible = False
        btnSearch.Visible = False
        tblSearch.Visible = False
        tblStatus.Visible = False
        ClearFields()
        LoadSupervisor()
        LoadInitialEmp()
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim obj As New clsConnectCA
            obj.SetStatusApprove(lblCashAdvanceID.Text)
            LoadCAMain()
            DefaultSettings()
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnApprove_Click")
        End Try
    End Sub

    Private Sub btnDisapprove_Click(sender As Object, e As EventArgs) Handles btnDisapprove.Click
        Try
            Dim obj As New clsConnectCA
            obj.SetStatusDisapprove(lblCashAdvanceID.Text)
            LoadCAMain()
            DefaultSettings()
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnDisapprove_Click")
        End Try
    End Sub

    Private Sub LoadEmployee()
        Dim obj As New clsConnectEmp
        Dim lData = obj.GetEmployeeBySupName(ddSupervisor.SelectedValue)
        ddEmployee.DataSource = lData
        ddEmployee.DataValueField = "ID"
        ddEmployee.DataTextField = "FullName"
        ddEmployee.DataBind()

        If ddSupervisor.SelectedItem.Text = "--Select a Supervisor--" Then
            LoadInitialEmp()
        End If

        pnlUpdateEmp.Update()
    End Sub

    Private Sub LoadInitialEmp()
        ddEmployee.Items.Insert(0, "--Select a Supervisor--")
        ddEmployee.Items(0).Value = 0
    End Sub

    Private Sub ddSupervisor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddSupervisor.SelectedIndexChanged
        LoadEmployee()
    End Sub

    Private Sub gvCAMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCAMain.SelectedIndexChanged
        Try
            SetReadOnlyFields()
            LoadEmployee()
            LoadSupervisor()
            lblSavingControl.Text = 2
            txtSearch.Visible = False
            btnSearch.Visible = False
            tblSearch.Visible = False
            If (Session.Item("accessLevel") = "Admin") Then
                btnNew.Visible = False
                btnBack.Visible = True
                btnEdit.Visible = True
            Else
                btnBack.Visible = True
                btnApprove.Visible = True
                btnDisapprove.Visible = True
            End If
            tblStatus.Visible = True
            txtReadBirthDate.Visible = True
            gvBirthDate.Visible = False
            dvMain.Visible = False
            dvNewCA.Visible = True
            lblCashAdvanceID.Text = Server.HtmlDecode(gvCAMain.SelectedRow.Cells(0).Text.Replace("&nbsp;", ""))
            ddEmployee.SelectedItem.Text = Server.HtmlDecode(gvCAMain.SelectedRow.Cells(1).Text.Replace("&nbsp;", ""))
            ddSupervisor.SelectedItem.Text = Server.HtmlDecode(gvCAMain.SelectedRow.Cells(2).Text.Replace("&nbsp;", ""))
            txtStatus.Text = Server.HtmlDecode(gvCAMain.SelectedRow.Cells(3).Text.Replace("&nbsp;", ""))
            txtCADate.Text = Server.HtmlDecode(gvCAMain.SelectedRow.Cells(4).Text.Replace("&nbsp;", ""))
            txtReadBirthDate.Text = Server.HtmlDecode(gvCAMain.SelectedRow.Cells(4).Text.Replace("&nbsp;", ""))
            txtAmount.Text = Server.HtmlDecode(gvCAMain.SelectedRow.Cells(5).Text.Replace("&nbsp;", ""))
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -gvClick")
        End Try
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

    Private Sub ClearFields()
        txtAmount.Text = String.Empty
        txtCADate.Text = String.Empty
        ddEmployee.ClearSelection()
        ddSupervisor.ClearSelection()
    End Sub

    Private Sub SetReadOnlyFields()
        ddEmployee.Enabled = False
        ddSupervisor.Enabled = False
        txtReadBirthDate.ReadOnly = True
        txtAmount.ReadOnly = True
        txtStatus.ReadOnly = True
    End Sub

    Private Sub SetEnabledField()
        ddEmployee.Enabled = True
        ddSupervisor.Enabled = True
        txtReadBirthDate.Visible = False
        gvBirthDate.Visible = True
        txtAmount.ReadOnly = False
        txtStatus.ReadOnly = True
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        DefaultSettings()
        LoadCAMain()
        ClearFields()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        btnEdit.Visible = False
        btnSave.Visible = True
        btnDelete.Visible = True
        SetEnabledField()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim obj As New clsConnectCA
            obj.DeleteCARecord(lblCashAdvanceID.Text)
            LoadCAMain()
            DefaultSettings()
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnDelete_Click")
        End Try
    End Sub
End Class