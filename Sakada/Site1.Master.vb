Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserName = CType(Session.Item("userName"), String)
        Dim UserLogin = CType(Session.Item("userLogin"), String)
        If UserName = String.Empty Then
            Response.Redirect("Login.aspx")
        Else
            lblUserLogin.Text = "Hi, " & UserName
        End If
    End Sub
End Class