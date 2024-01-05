Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Text = "Koneksi Vb-Net Dan MYSQL"
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'cek koneksi ke mysql
        koneksi()
        'masuk ke form 2 dan menghilangkan form 1
        Form2.Show()
        Me.Hide()
    End Sub
End Class
