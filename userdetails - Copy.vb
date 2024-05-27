Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class userdetails
    Dim connection As New SqlConnection("Data Source=DESKTOP-KTBHN7P\SQLEXPRESS;Initial Catalog=HC;Integrated Security=True;Encrypt=False")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As New SqlCommand("INSERT into udetail (Full_name, DOB, Gender, Phone_no ,Email_id ,Address)" & "VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')", connection)
        connection.Open()
        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show(" Your Details saved")
            Me.Hide()
            userb7.Show()
        Else
            MessageBox.Show("Your details not saved")
        End If

        connection.Close()

    End Sub

    Private Sub userdetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class