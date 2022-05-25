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
        Dim lData = obj.GetCADB()
        gvCAMain.DataSource = lData
        gvCAMain.DataBind()
    End Sub

    Private Sub DefaultSettings()
        dvButtons.Visible = True
        dvMain.Visible = True
        dvNewCA.Visible = False
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
            Else
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
        ClearFields()
    End Sub
    Private Sub LoadEmployee()
        Dim obj As New clsConnectEmp
        Dim lData = obj.GetEmployeeBySupName(ddSupervisor.SelectedValue)
        ddEmployee.DataSource = lData
        ddEmployee.DataValueField = "EmpID"
        ddEmployee.DataTextField = "FullName"
        ddEmployee.DataBind()

        If ddSupervisor.SelectedItem.Text = "--Select a Supervisor--" Then
            LoadInitialEmp()
        End If

        pnlUpdateEmp.Update()
        pnlUpdateSup.Update()
    End Sub

    Private Sub LoadInitialEmp()
        ddEmployee.Items.Insert(0, "--Select a Supervisor--")
        ddEmployee.Items(0).Value = 0
    End Sub

    Private Sub ddSupervisor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddSupervisor.SelectedIndexChanged
        LoadEmployee()
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

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        DefaultSettings()
        LoadCAMain()
    End Sub
End Class