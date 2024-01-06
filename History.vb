Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class History
    Private Sub History_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ContextMenuStrip = ContextMenuStrip1

        Bersih()
        Resize_Grid()

    End Sub

    Sub Resize_Grid()
        With DataGridView2
            .Columns(0).Width = 100
            .Columns(1).Width = 200
            .Columns(2).Width = 400
            .Columns(3).Width = 200
            .Columns(4).Width = 200
            .Columns(5).Width = 200

        End With
    End Sub
    Sub Bersih()
        Tampil_data_history()
    End Sub


    Sub Tampil_data_history()
        '------- Untuk menampilkan data di datagrid -----------
        da = New MySqlDataAdapter("select historyId as ID, productId as Kode_Buku,history_name as Nama_Buku, history_qty as Updated_Qty,history_change as Changes , history_date as Last_Update from history order by historyId desc", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "history")
        Me.DataGridView2.DataSource = (ds.Tables("history"))
        '-------------- Diatas codingnya ---------------
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        da = New MySqlDataAdapter("select historyId as ID, productId as Kode_Buku, history_name as Nama_Buku, history_qty as Updated_Qty,history_change as Changes, history_date as Last_Update from history where history_name like '%" & Me.TextBox4.Text & "%' OR history_date like '%" & Me.TextBox4.Text & "%' OR productId like '%" & Me.TextBox4.Text & "%' OR history_qty like '%" & Me.TextBox4.Text & "%' OR history_change like '%" & Me.TextBox4.Text & "%'", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "history")
        DataGridView2.DataSource = (ds.Tables("history"))
        Resize_Grid()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
        MainMenu.Show()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub LoginPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginPageToolStripMenuItem.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub AdminPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminPageToolStripMenuItem.Click
        Me.Close()
        MainMenu.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub LoginFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginFormToolStripMenuItem.Click
        Me.Close()
        Form1.Show()

    End Sub



    Private Sub AdminDashboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminDashboardToolStripMenuItem.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub RefreshToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem1.Click
        Bersih()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        MainMenu.Show()
    End Sub
End Class