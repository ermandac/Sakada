Public Class MillingSupervisor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim script As String = "$(document).ready(function(){$('#btnLoad').click();});"
            ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)
        End If
    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs)
        LoadSupMain()
        DefaultSettings()
    End Sub

    Private Sub LoadSupMain()
        Dim obj As New clsConnectSup
        Dim lData = obj.GetSupDB(txtSearch.Text)
        gvSupMain.DataSource = lData
        gvSupMain.DataBind()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As Object) Handles btnSearch.Click
        LoadSupMain()
    End Sub

    Private Sub DefaultSettings()
        dvButtons.Visible = True
        dvMain.Visible = True
        dvNewSupervisor.Visible = False
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
    Private Sub gvSupMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSupMain.SelectedIndexChanged
        Try
            SetReadOnlyFields()
            lblSavingControl.Text = 2
            btnNew.Visible = False
            btnBack.Visible = True
            btnSave.Visible = False
            btnDelete.Visible = False
            btnEdit.Visible = True
            tblSearch.Visible = False
            txtReadBirthDate.Visible = True
            'Content View
            gvBirthDate.Visible = False
            dvMain.Visible = False
            dvNewSupervisor.Visible = True
            lblSupervisorID.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(0).Text).ToString()
            txtFirstName.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(2).Text).ToString()
            txtMiddleName.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(3).Text).ToString()
            txtLastName.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(4).Text).ToString()
            txtAge.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(5).Text).ToString()
            txtAddress.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(6).Text).ToString()
            txtReadBirthDate.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(7).Text).ToString()
            txtBirthDate.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(7).Text).ToString()
            txtEmailAdd.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(8).Text).ToString()
            txtMobileNo.Text = Server.HtmlDecode(gvSupMain.SelectedRow.Cells(9).Text).ToString()
        Catch ex As Exception

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

    Private Sub gvSupMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSupMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Me.Page.ClientScript.GetPostBackClientHyperlink(gvSupMain, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim obj As New clsConnectSup
            'obj.SupName = txtFullName.Text
            obj.FirstName = txtFirstName.Text
            obj.MiddleName = txtMiddleName.Text
            obj.LastName = txtLastName.Text
            obj.SupAddress = txtAddress.Text
            obj.SupMobileNo = txtMobileNo.Text
            obj.SupEmailAdd = txtEmailAdd.Text
            obj.SupBirthday = txtBirthDate.Text
            obj.SupAge = txtAge.Text
            Dim oSupDetails As New clsConnectSup
            If lblSavingControl.Text = 2 Then
                oSupDetails.UpdateSup(lblSupervisorID.Text, obj)
            Else
                oSupDetails.SaveSupDetails(obj)
            End If
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnSave")
        Finally
            DefaultSettings()
            LoadSupMain()
            ClearFields()
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
        dvNewSupervisor.Visible = True

        'Searc
        txtSearch.Visible = False
        btnSearch.Visible = False
        tblSearch.Visible = False
        ClearFields()
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        DefaultSettings()
        LoadSupMain()
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
    End Sub

    Private Sub SetEnableFields()
        txtFirstName.ReadOnly = False
        txtMiddleName.ReadOnly = False
        txtLastName.ReadOnly = False
        txtAddress.ReadOnly = False
        txtMobileNo.ReadOnly = False
        txtEmailAdd.ReadOnly = False
        txtAge.ReadOnly = False
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim obj As New clsConnectSup
            obj.DeleteSupRecord(lblSupervisorID.Text)
            DefaultSettings()
            LoadSupMain()
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -btnDelete")
        End Try
    End Sub
End Class