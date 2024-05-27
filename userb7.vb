Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class userb7
    Dim connection As New SqlConnection("Data Source=DESKTOP-KTBHN7P\SQLEXPRESS;Initial Catalog=HC;Integrated Security=True;Encrypt=False")
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        feedback.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim result = MessageBox.Show("are you sure you would like to exit?", "logging out",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result = DialogResult.Yes) Then
            Me.Hide()

            Form1.Show()
        End If

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        delivery.Show()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        know.Show()
    End Sub
    Private Sub userb7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Medicines")
        ComboBox1.Items.Add("Skin Care")
        ComboBox1.Items.Add("Baby Essentials")
        ComboBox1.Items.Add("Fit Care")
        ComboBox1.Items.Add("Medical Equipments")
        DataGridView1.AutoGenerateColumns = True
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        DataGridView1.DataSource = Nothing
        Dim selectedCategory As String = ComboBox1.SelectedItem.ToString()
        Try
            Using connection As New SqlConnection("Data Source=DESKTOP-KTBHN7P\SQLEXPRESS;Initial Catalog=HC;Integrated Security=True;Encrypt=False")
                connection.Open()
                Dim query As String = "SELECT P_ID, product_name, product_price, mfg_date, exp_date FROM product WHERE category = @category"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@category", selectedCategory)
                    Using adapter As New SqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)
                        DataGridView1.DataSource = dataTable
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving products: " & ex.Message)
        End Try
    End Sub
    Private Sub cmbCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        DataGridView1.DataSource = Nothing
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim command As New SqlCommand("INSERT INTO [order] (Product_id, Product_name, Price, Quantity) VALUES (@Product_id, @Product_name, @Price, @Quantity)", connection)
        command.Parameters.AddWithValue("@Product_id", TextBox1.Text)
        command.Parameters.AddWithValue("@Product_name", TextBox2.Text)
        command.Parameters.AddWithValue("@Price", TextBox3.Text)
        command.Parameters.AddWithValue("@Quantity", TextBox4.Text)
        connection.Open()
        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Product added to cart")
            DisplaycdetailsData()
        Else
            MessageBox.Show("Product not added to cart")
        End If
        connection.Close()
    End Sub
    Private Sub DisplaycdetailsData()
        Dim adapter As New SqlDataAdapter("SELECT * FROM [order]", connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView2.DataSource = table
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            Dim command As New SqlCommand("DELETE FROM [order] WHERE Product_id = @Product_id", connection)
            command.Parameters.AddWithValue("@Product_id", TextBox1.Text)
            connection.Open()
            If command.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Product Removed from Cart")
                DisplaycdetailsData()
            Else
                MessageBox.Show("Failed to remove product from cart")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            TextBox1.Text = selectedRow.Cells("Product_id").Value.ToString()
            TextBox2.Text = selectedRow.Cells("Product_name").Value.ToString()
            TextBox3.Text = selectedRow.Cells("Price").Value.ToString()
            TextBox4.Text = selectedRow.Cells("Quantity").Value.ToString()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView2.DataSource = Nothing
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim timestamp As DateTime = DateTime.Now
        Dim command As New SqlCommand("INSERT INTO Pay (Mode_of_payment, Username, Net_amount, timestamp) VALUES (@Mode_of_payment, @Username, @Net_amount, @timestamp)", connection)
        command.Parameters.AddWithValue("@Mode_of_payment", RadioButton1.Checked)
        command.Parameters.AddWithValue("@Username", TextBox1.Text)
        command.Parameters.AddWithValue("@Net_amount", TextBox2.Text)
        Try
            connection.Open()
            If command.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Order Placed")
            Else
                MessageBox.Show("Order not Placed.")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim totalNetAmount As Double = 0
        Dim Quantity As Integer
        For Each row As DataGridViewRow In DataGridView2.Rows
            If Not row.IsNewRow Then
                Dim QuantityCell As DataGridViewCell = row.Cells("Quantity")
                If QuantityCell.Value IsNot Nothing AndAlso Integer.TryParse(QuantityCell.Value.ToString(), Quantity) Then
                    Dim Price As Double
                    If Not Double.TryParse(row.Cells("Price").Value.ToString(), Price) Then
                        MessageBox.Show("Invalid unit price in order. Please enter a valid number.")
                        Return
                    End If
                    Dim netAmount As Double = Quantity * Price
                    totalNetAmount += netAmount
                Else
                    MessageBox.Show("Invalid quantity in order. Please enter a valid number.")
                    Return
                End If
            End If
        Next
        MessageBox.Show($"Total net amount for all orders: {totalNetAmount}")
        TextBox8.Text = totalNetAmount.ToString()
        TextBox5.Text = totalNetAmount.ToString()
    End Sub
End Class


