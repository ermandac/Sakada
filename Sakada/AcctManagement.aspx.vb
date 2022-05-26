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

    Private Sub LoadAcctMain()
        Dim obj As New clsLogin
        Dim lData = obj.GetLoginDB()
        gvAcctMain.DataSource = lData
        gvAcctMain.DataBind()
    End Sub
End Class