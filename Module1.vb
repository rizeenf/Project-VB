'imports data mysql dari library VB.net
Imports MySql.Data.MySqlClient
Module Module1
    Public con As MySqlConnection
    Public cmd As MySqlCommand
    Public ds As DataSet
    Public da As MySqlDataAdapter
    Public rd As MySqlDataReader
    Public db As String
    'buat prosedur global dengan nama koneksi , yang akan dipanggil disetiap form.
    Public Sub koneksi()
        'untuk koneksi data , server localhost, user id buat aja root (default)
        ' paswword kosongin kalau default, database sesuaikan dengan yang kalian buat
        db = "Server=localhost;user id=root;password=;database=projectvbnet"
        con = New MySqlConnection(db)
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
                MsgBox("Koneksi ke database berhasil", MsgBoxStyle.Information, "Informasi")
            End If
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Public Function SQLTable(ByVal Source As String) As DataTable
        ' ---fungsi untuk membuat nomor otomatis dengan menghubungkan pada field yang ada di table--
        Try
            Dim adp As New MySqlDataAdapter(Source, con)
            Dim DT As New DataTable
            adp.Fill(DT)
            SQLTable = DT
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message)
            SQLTable = Nothing
        End Try
    End Function
End Module