Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class suppliers8
    Dim connection As New SqlConnection("Data Source=DESKTOP-KTBHN7P\SQLEXPRESS;Initial Catalog=HC;Integrated Security=True;Encrypt=False")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As New SqlCommand("INSERT into sup (S_ID, Suplier_name, Company_name, Phone_no, email_id)" & "VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')", connection)
        connection.Open()
        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Suplier added")
            DisplaycdetailsData()
        Else
            MessageBox.Show("Suplier not added")
        End If
        connection.Close()
    End Sub
    Private Sub DisplaycdetailsData()
        Dim adapter As New SqlDataAdapter("SELECT * FROM sup", connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub
    Private Sub edit_Click(sender As Object, e As EventArgs) Handles edit.Click
        Try
            Dim query As String = "UPDATE sup SET S_ID = @S_ID, Suplier_name = @Suplier_name, Company_name = @Company_name, Phone_no = @Phone_no, email_id = @email_id WHERE P_ID= @P_ID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@S_ID", TextBox1.Text)
                command.Parameters.AddWithValue("@Suplier_name", TextBox2.Text)
                command.Parameters.AddWithValue("@Company_name", TextBox3.Text)
                command.Parameters.AddWithValue("@Phone_no", TextBox4.Text)
                command.Parameters.AddWithValue("@email_id", TextBox5.Text)
                connection.Open()
                If command.ExecuteNonQuery() = 1 Then
                    MessageBox.Show("Supplier detail updated")
                    DisplaycdetailsData()
                Else
                    MessageBox.Show("Failed to update Supplier")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = selectedRow.Cells("S_ID").Value.ToString()
            TextBox2.Text = selectedRow.Cells("Supplier_name").Value.ToString()
            TextBox3.Text = selectedRow.Cells("Company_name").Value.ToString()
            TextBox4.Text = selectedRow.Cells("Phone_no").Value.ToString()
            TextBox5.Text = selectedRow.Cells("email_id").Value.ToString()
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim command As New SqlCommand("DELETE FROM sup WHERE S_ID = @S_ID", connection)
        command.Parameters.AddWithValue("@S_ID", TextBox1.Text)
        connection.Open()
        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Supplier details deleted")
            DisplaycdetailsData()
        Else
            MessageBox.Show("Failed to delete Suplier details")
        End If
        connection.Close()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        dash5.Show()
    End Sub
End Class