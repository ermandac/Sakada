Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserName = CType(Session.Item("userName"), String)
        Dim UserLogin = CType(Session.Item("userLogin"), String)
        Dim AccessLevel = CType(Session.Item("accessLevel"), String)
        If (AccessLevel = "Admin") Then
            lblUserLogin.Text = "Hi, " & UserName
        ElseIf (AccessLevel = "Supervisor") Then
            lblUserLogin.Text = "Hi, " & UserName
            sideNaveAcctMgt.Visible = False
            sideNavEmployee.Visible = False
            sideNavSupervisor.Visible = False
        Else
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Session.RemoveAll()
        Session.Clear()
        Response.Redirect("Login.aspx")
    End Sub
End Class