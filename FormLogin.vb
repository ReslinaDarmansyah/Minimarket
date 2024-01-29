Public Class FormLogin

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from user2110024 where username2110024='" & txtUser.Text & "' and password2110024=MD5('" & txtPassword.Text & "')"
        cek = perintah.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            userLogin = txtUser.Text
            FormMenu.lblUser.Text = cek.Item("nama2110024")
            cek.Close()
            'Update lastlogin pada tabel user berdasarkan user yang login
            perintah.Connection = kon
            perintah.CommandType = CommandType.Text
            perintah.CommandText = "update user2110024 set lastlogin2110024=now() where username2110024='" & txtUser.Text & "'"
            perintah.ExecuteNonQuery()
            FormMenu.Show()
            Me.Hide()
        Else
            MsgBox("Username atau password salah", MsgBoxStyle.Critical, "Informasi")
        End If
        kon.Close()
    End Sub

    Private Sub txtUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUser.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                txtPassword.Focus()
        End Select
    End Sub
    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                btnLogin.Focus()
        End Select
    End Sub

   
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub FormLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class