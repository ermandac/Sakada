Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim obj As New clsLogin
        Dim lData = obj.GetLogin(txtUserName.Text, txtPassword.Text)
        lblUserName.Text = obj.GetUser(txtUserName.Text)
        lblAccessLevel.Text = obj.GetAccessLevel(txtUserName.Text)
        Session.RemoveAll()
        Session.Clear()
        If lData.Count > 0 Then
            pnlWarningMessage.Visible = False
            Session.Add("userLogin", txtUserName.Text)
            Session.Add("userName", lblUserName.Text)
            Session.Add("accessLevel", lblAccessLevel.Text)
            Response.Redirect("CashAdvance.aspx")
        Else
            pnlWarningMessage.Visible = True
        End If
    End Sub

End Class