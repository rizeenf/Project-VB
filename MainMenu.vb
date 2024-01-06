Imports MySql.Data.MySqlClient

Public Class MainMenu
    Dim kodesd
    Dim DateNow As String = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        Dim rak(4) As String
        rak(0) = "A-1"
        rak(1) = "A-2"
        rak(2) = "B-1"
        rak(3) = "C-1"
        rak(4) = "C-2"


        Label5.Text = DateNow
        DataGridView1.AllowUserToAddRows = False
        DataGridView2.AllowUserToAddRows = False
        TextBox1.ReadOnly = True
        Me.ContextMenuStrip = ContextMenuStrip1

        Bersih()
        Resize_Grid()


        ListBarang(rak)
        TahunInput()
    End Sub

    Sub ListBarang(barang)
        For Each item As String In barang
            ComboBox1.Items.Add(item)
        Next
    End Sub
    Sub TahunInput()
        Dim i As Integer
        For i = 2000 To 2025
            ComboBox2.Items.Add(i)
        Next
    End Sub


    Sub Resize_Grid()
        With DataGridView1
            .Columns(0).Width = 130
            .Columns(1).Width = 400
            .Columns(2).Width = 150
        End With

        With DataGridView2
            .Columns(0).Width = 30
            .Columns(1).Width = 170
            .Columns(2).Width = 100
            .Columns(3).Width = 140
        End With

    End Sub
    Sub Bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ComboBox1.ResetText()
        ComboBox2.ResetText()

        Me.Button1.Enabled = True
        Me.Button2.Enabled = False
        Me.Button3.Enabled = False
        Tampil_data_buku()
        Tampil_data_history()
    End Sub
    Sub Tampil_data_buku()
        da = New MySqlDataAdapter("select productId as Kode_Buku, product_name as Nama_Buku, product_qty as Stok, product_price as Tahun from product order by productId", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "product")
        Me.DataGridView1.DataSource = (ds.Tables("product"))
        '-------------- Diatas codingnya ---------------
        Nomor()
    End Sub
    Sub Tampil_data_history()
        da = New MySqlDataAdapter("select historyId as ID, productId as Kode_Buku, history_qty as Updated_Qty, history_date as Last_Update from history order by historyId desc", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "history")
        Me.DataGridView2.DataSource = (ds.Tables("history"))
    End Sub
    Sub Nomor()
        Dim DR As DataRow
        Dim s As String
        DR = SQLTable("select max(right(productId,1)) as nomor from product").Rows(0)
        If DR.IsNull("Nomor") Then
            s = "Book-1" 'member nilai awal
        Else
            s = "Book-" & Format(DR("Nomor") + 1, "0")
        End If
        TextBox1.Text = s
        TextBox1.Enabled = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or Me.TextBox3.Text = "" Then
            MsgBox("Silahkan lengkapi data bukunya!")
        Else
            Dim simpan As String
            Dim tambahHistory As String

            MsgBox("Data berhasil ditambahkan")
            simpan = "insert into product (productId,product_name,product_qty, product_price)values('" & Me.TextBox1.Text & "','" & Me.TextBox2.Text & "','" & Me.TextBox3.Text & "','" & Me.ComboBox2.SelectedItem() & "')"
            cmd = New MySqlCommand(simpan, con) ' untuk menghubungkan ke database dan table lalu simpan
            cmd.ExecuteNonQuery() ' mengeksekusi datanya

            tambahHistory = "insert into history (historyId,productId ,history_name,history_qty, history_change, history_date)values('" & Me.TextBox1.Text & "', '" & Me.TextBox1.Text & "' ,'" & Me.TextBox2.Text & "','" & Me.TextBox3.Text & "', 'Tambah buku' ,'" & DateNow & "')"
            cmd = New MySqlCommand(tambahHistory, con) ' untuk menghubungkan ke database dan table lalu simpan
            cmd.ExecuteNonQuery() ' mengeksekusi datanya

            Bersih()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Silahkan pilih data terlebih dahulu!")
        Else
            MsgBox("Hapus data berhasil!")
            Dim edit As String
            Dim tambahHistory As String

            edit = "update product set productId ='" & Me.TextBox1.Text & "' , product_name = '" & Me.TextBox2.Text & "', product_qty ='" & Me.TextBox3.Text & "' where productId = '" & Me.TextBox1.Text & "'"
            cmd = New MySqlCommand(edit, con) ' untuk menghubungkan ke database dan table lalu Update
            cmd.ExecuteNonQuery() ' mengeksekusi datanya

            tambahHistory = "insert into history (historyId,productId ,history_name,history_qty, history_change, history_date)values('" & Me.TextBox1.Text & "', '" & Me.TextBox1.Text & "' ,'" & Me.TextBox2.Text & "','" & Me.TextBox3.Text & "', 'Update' ,'" & DateNow & "')"
            cmd = New MySqlCommand(tambahHistory, con)
            cmd.ExecuteNonQuery() ' mengeksekusi datanya

            Bersih()
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Then
            MsgBox("Silahkan pilih data terlebih dahulu!")
            Bersih()
        Else
            If MessageBox.Show("Anda yakin akan menghapus buku ini ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim hapus As String
                hapus = "delete from product where productId = '" & Me.TextBox1.Text & "'"
                cmd = New MySqlCommand(hapus, con)
                cmd.ExecuteNonQuery()

                Dim tambahHistory As String
                tambahHistory = "insert into history (historyId, productId, history_name,history_qty, history_change, history_date)values('" & Me.TextBox1.Text & "', '" & Me.TextBox1.Text & "' ,'" & Me.TextBox2.Text & "','" & 0 & "', 'Hapus' , '" & DateNow & "')"
                cmd = New MySqlCommand(tambahHistory, con)
                cmd.ExecuteNonQuery() ' mengeksekusi datanya

                Bersih()
            Else
            End If
        End If
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 AndAlso e.RowIndex < DataGridView1.Rows.Count AndAlso e.ColumnIndex >= 0 AndAlso e.ColumnIndex < DataGridView1.Columns.Count Then
            kodesd = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox1.Text = kodesd
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            Me.Button1.Enabled = False
            Me.Button2.Enabled = True
            Me.Button3.Enabled = True
        End If

    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        da = New MySqlDataAdapter("select productId as Kode_Buku, product_name as Nama_Buku, product_qty as Stok, product_price as Tahun from product where product_name like '%" & Me.TextBox4.Text & "%' OR productId like '%" & Me.TextBox4.Text & "%' OR product_qty like '%" & Me.TextBox4.Text & "%' OR product_price like '%" & Me.TextBox4.Text & "%' ", con)
        ds = New DataSet
        da.Fill(ds, "product")
        DataGridView1.DataSource = (ds.Tables("product"))
        Resize_Grid()

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Bersih()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        Bersih()
    End Sub

    Private Sub HistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HistoryToolStripMenuItem.Click
        History.Show()
        Me.Close()
    End Sub

    Private Sub LoginPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginPageToolStripMenuItem.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub HistoryPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HistoryPageToolStripMenuItem.Click
        History.Show()
        Me.Close()
    End Sub

    Private Sub RefreshToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem1.Click
        Bersih()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Me.Close()

    End Sub

    Private Sub LoginFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginFormToolStripMenuItem.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        History.Show()
    End Sub
End Class