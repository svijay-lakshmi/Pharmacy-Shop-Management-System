Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class sales
    Dim connection As New SqlConnection("Data Source=DESKTOP-KTBHN7P\SQLEXPRESS;Initial Catalog=HC;Integrated Security=True;Encrypt=False")
    Private Sub DisplayfdetailsData()
        Try
            connection.Open()
            Dim query As String = "SELECT * FROM Pay"
            Dim adapter As New SqlDataAdapter(query, connection)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        dash5.Show()
    End Sub
End Class


