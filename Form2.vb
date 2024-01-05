Imports MySql.Data.MySqlClient

Public Class Form2
    'buat variabel tanpa type data
    Dim kodesd
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        Label1.Text = "kode barang"
        Label2.Text = "nama Barang"
        Label3.Text = "stock barang"
        Label4.Text = "pencarian Data (nama)"
        Button1.Text = "Simpan Data"
        Button2.Text = "Update/ Edit Data"
        Button3.Text = "Hapus Data"
        Button4.Text = "Refresh Data"
        ' supaya kode ga bisa diotak-atik
        TextBox1.ReadOnly = True
        ' buat prosedur membersihkan objek
        bersih()
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        '--------- Untuk mematikan tombol
        Me.Button1.Enabled = True
        Me.Button2.Enabled = False
        Me.Button3.Enabled = False
        tampil_data()
    End Sub
    Sub tampil_data()
        '------- Untuk menampilkan data di datagrid -----------
        da = New MySqlDataAdapter("select * from product order by productId", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "product")
        Me.DataGridView1.DataSource = (ds.Tables("product"))
        '-------------- diatas codingnya ---------------
        nomor()
    End Sub
    Sub nomor()
        Dim DR As DataRow
        Dim s As String
        'mengambil kode dari field ID, kemudian dicari nilai yg paling besar (max)
        'kemudian hasilnya d tampung d field buatan dgn nama Nomor
        DR = SQLTable("select max(right(productId,1)) as nomor from product").Rows(0)
        'jika berisi null atau tdk ditemukan
        If DR.IsNull("Nomor") Then
            s = "KS-1" 'member nilai awal
        Else
            s = "KS-" & Format(DR("Nomor") + 1, "0")
        End If
        TextBox1.Text = s
        TextBox1.Enabled = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '------- Coding Simpan --------
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or Me.TextBox3.Text =
        "" Then
            MsgBox(" Maaf datanya belum lengkap mas bro")
        Else
            Dim simpan As String
            MsgBox("data anda akan disimpan mas bro")
            simpan = "insert into product (productId,product_name,product_qty)values('" & Me.TextBox1.Text & "','" & Me.TextBox2.Text & "','" & Me.TextBox3.Text & "')"
            cmd = New MySqlCommand(simpan, con) ' untuk menghubungkan ke database dan table lalu simpan

            cmd.ExecuteNonQuery() ' mengeksekusi datanya
            bersih()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox(" Maaf datanya tidak ada yang di update")
        Else
            ' ------- Coding update data --------
            MsgBox("Anda akan mengupdate datanya ya")
            Dim edit As String
            edit = "update product set productId ='" & Me.TextBox1.Text & "' , product_name = '" & Me.TextBox2.Text & "', product_qty ='" & Me.TextBox3.Text & "' where productId = '" & Me.TextBox1.Text & "'"
            cmd = New MySqlCommand(edit, con) ' untuk menghubungkan ke database dan table lalu Update

            cmd.ExecuteNonQuery() ' mengeksekusi datanya
            bersih()
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Then
            MsgBox(" Maaf datanya tidak ada yang di hapus")
            bersih()
        Else
            ' ------- Coding update data --------
            If MessageBox.Show("Anda yakin akan menghapus datanya ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim hapus As String
                hapus = "delete from product where productId = '" & Me.TextBox1.Text & "'"
                cmd = New MySqlCommand(hapus, con)
                cmd.ExecuteNonQuery()
                bersih()
            Else
            End If
        End If
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' fungsi untuk menclik data yang akan dipilih
        kodesd = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox1.Text = kodesd
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Me.Button1.Enabled = False
        Me.Button2.Enabled = True
        Me.Button3.Enabled = True
    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        '----------- menyaring di datagrid dengan nama barang---------
        da = New MySqlDataAdapter("select * from product where product_name like '%" & Me.TextBox4.Text & "%'", con)
        ds = New DataSet
        ' ds.Clear()
        da.Fill(ds, "product")
        DataGridView1.DataSource = (ds.Tables("product"))
    End Sub
    Private Sub button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        bersih()
    End Sub
End Class