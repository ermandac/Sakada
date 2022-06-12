Public Class AcctManagement
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
        dvNewLogin.Visible = False
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
    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs)
        DefaultSettings()
        LoadAcctMain()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        DefaultSettings()
        LoadAcctMain()
        SetEnableFields()
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
        dvNewLogin.Visible = True

        'Search
        txtSearch.Visible = False
        btnSearch.Visible = False
        tblSearch.Visible = False
        LoadSupervisor()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ValidateFields() Then
                Dim obj As New clsLogin
                obj.UserName = ddSupervisor.SelectedItem.Text
                obj.LoginName = txtLoginName.Text
                obj.PassWord = txtPassword.Text
                obj.AccessLevel = ddAccessLevel.SelectedValue
                obj.Reference = ddSupervisor.SelectedValue
                Dim oLoginDetails As New clsLogin
                If lblSavingControl.Text = 1 Then
                    oLoginDetails.SaveLoginDetails(obj)
                ElseIf lblSavingControl.Text = 2 Then
                    oLoginDetails.UpdateLogin(lblAccessID.Text, obj)
                End If
            End If
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnSave")
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        btnEdit.Visible = False
        btnSave.Visible = True
        btnDelete.Visible = True
        SetEnableFields()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim obj As New clsLogin
            obj.DeleteLoginRecord(lblAccessID.Text)
            LoadAcctMain()
            DefaultSettings()
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnDelete_Click")
        End Try
    End Sub


    Private Sub gvAcctMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAcctMain.SelectedIndexChanged
        lblSavingControl.Text = 2
        Try
            SetReadOnlyFields()
            btnNew.Visible = False
            btnBack.Visible = True
            btnSave.Visible = False
            btnEdit.Visible = True
            txtSearch.Visible = False
            btnSearch.Visible = False
            tblSearch.Visible = False
            dvMain.Visible = False
            dvNewLogin.Visible = True

            lblAccessID.Text = Server.HtmlDecode(gvAcctMain.SelectedRow.Cells(0).Text.Replace("&nbsp;", ""))
            lblReference.Text = Server.HtmlDecode(gvAcctMain.SelectedRow.Cells(5).Text.Replace("&nbsp;", ""))
            LoadSupervisorWithSupID(lblReference.Text)
            ddSupervisor.SelectedItem.Text = Server.HtmlDecode(gvAcctMain.SelectedRow.Cells(1).Text.Replace("&nbsp;", ""))
            txtLoginName.Text = Server.HtmlDecode(gvAcctMain.SelectedRow.Cells(2).Text.Replace("&nbsp;", ""))
            txtPassword.Text = Server.HtmlDecode(gvAcctMain.SelectedRow.Cells(3).Text.Replace("&nbsp;", ""))
            ddAccessLevel.SelectedValue = Server.HtmlDecode(gvAcctMain.SelectedRow.Cells(4).Text.Replace("&nbsp;", ""))
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & "-gvClick")
        End Try
    End Sub

    Private Sub gvAcctMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAcctMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(5).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(5).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Me.Page.ClientScript.GetPostBackClientHyperlink(gvAcctMain, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Private Sub LoadSupervisorWithSupID(SupID As String)
        Dim obj As New clsConnectSup
        Dim lData = obj.GetSupervisorWithSupID(SupID)
        ddSupervisor.DataSource = lData
        ddSupervisor.DataValueField = "SupID"
        ddSupervisor.DataTextField = "SupName"
        ddSupervisor.DataBind()

        ddSupervisor.Items.Insert(0, "--Select--")
        ddSupervisor.Items(0).Value = 0
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

    Private Sub LoadAcctMain()
        Dim obj As New clsLogin
        Dim lData = obj.GetLoginDB()
        gvAcctMain.DataSource = lData
        gvAcctMain.DataBind()
    End Sub

    Private Sub SetReadOnlyFields()
        ddSupervisor.Enabled = False
        ddAccessLevel.Enabled = False
        txtLoginName.ReadOnly = True
        txtPassword.ReadOnly = True
        txtEmail.ReadOnly = True
        txtConfirmPass.ReadOnly = True
    End Sub

    Private Sub SetEnableFields()
        ddSupervisor.Enabled = True
        ddAccessLevel.Enabled = True
        txtLoginName.ReadOnly = False
        txtPassword.ReadOnly = False
        txtEmail.ReadOnly = False
        txtConfirmPass.ReadOnly = False
    End Sub

    Private Function ValidateFields() As Boolean
        Dim boolVal As Boolean = True
        If txtPassword.Text <> txtConfirmPass.Text Then
            boolVal = False
            txtPassword.Focus()
            txtConfirmPass.Focus()
        ElseIf txtPassword.Text = String.Empty Then
            boolVal = False
            txtPassword.Focus()
        ElseIf txtConfirmPass.Text = String.Empty Then
            boolVal = False
            txtConfirmPass.Focus()
        End If
        Return boolVal
    End Function
End Class