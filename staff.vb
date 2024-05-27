Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class staff
    Dim connection As New SqlConnection("Data Source=DESKTOP-KTBHN7P\SQLEXPRESS;Initial Catalog=HC;Integrated Security=True;Encrypt=False")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As New SqlCommand("INSERT into staff (E_ID, E_Name, DoB, Gender, Phone_no, email_id,Address)" & "VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", connection)
        connection.Open()
        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Employee added")
            DisplaycdetailsData()
        Else
            MessageBox.Show("Employeee not added")
        End If
        connection.Close()
    End Sub
    Private Sub DisplaycdetailsData()
        Dim adapter As New SqlDataAdapter("SELECT * FROM staff", connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub
    Private Sub edit_Click(sender As Object, e As EventArgs) Handles edit.Click
        Try
            Dim query As String = "UPDATE staff SET E_ID = @E_ID, E_Name = @E_Name, DOB = @DOB, Gender = @Gender, Phone_no = @Phone_no, email_id =@email_id, Address =@Address WHERE E_ID= @E_ID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@E_ID", TextBox1.Text)
                command.Parameters.AddWithValue("@E_Name", TextBox2.Text)
                command.Parameters.AddWithValue("@DOB", TextBox3.Text)
                command.Parameters.AddWithValue("@Gender", TextBox4.Text)
                command.Parameters.AddWithValue("@Phone_no", TextBox5.Text)
                command.Parameters.AddWithValue("@email_id", TextBox6.Text)
                command.Parameters.AddWithValue("@Address", TextBox7.Text)
                connection.Open()
                If command.ExecuteNonQuery() = 1 Then
                    MessageBox.Show("Employye  detail updated")
                    DisplaycdetailsData()
                Else
                    MessageBox.Show("Failed to update Employee details")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = selectedRow.Cells("E_ID").Value.ToString()
            TextBox2.Text = selectedRow.Cells("E_Name").Value.ToString()
            TextBox3.Text = selectedRow.Cells("DOB").Value.ToString()
            TextBox4.Text = selectedRow.Cells("Gender").Value.ToString()
            TextBox5.Text = selectedRow.Cells("Phone_no").Value.ToString()
            TextBox5.Text = selectedRow.Cells("email_id").Value.ToString()
            TextBox5.Text = selectedRow.Cells("Address").Value.ToString()
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim command As New SqlCommand("DELETE FROM staff WHERE E_ID = @E_ID", connection)
        command.Parameters.AddWithValue("@E_ID", TextBox1.Text)
        connection.Open()
        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Employee details deleted")
            DisplaycdetailsData()
        Else
            MessageBox.Show("Failed to delete Employee details")
        End If
        connection.Close()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        dash5.Show()
    End Sub
End Class