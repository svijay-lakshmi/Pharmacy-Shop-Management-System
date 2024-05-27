Public Class userlogin3
    Private Sub exitbtn_Click(sender As Object, e As EventArgs) Handles exitbtn.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        registration4.Show()
    End Sub

    Private Sub inbtn_Click(sender As Object, e As EventArgs) Handles inbtn.Click
        Me.Hide()
        userb7.Show()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class