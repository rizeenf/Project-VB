Imports System.Linq.Expressions
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim inputUser As String
    Dim inputPass As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.AutoSize = False
        TextBox1.Height = 30
        TextBox2.AutoSize = False
        TextBox2.Height = 30
        TextBox2.PasswordChar = "*"

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        inputUser = TextBox1.Text
        inputPass = TextBox2.Text

        'Cek kosong
        CekInputKosong(TextBox1, TextBox2)

        'Cek valid and go to next page
        CekValidateUser(inputUser, inputPass)

        'clear 
        ClearLogin(TextBox1, TextBox2)
    End Sub

    Sub CekValidateUser(inputUser As String, inputPass As String)

        da = New MySqlDataAdapter("select * from user where username='" & Me.inputUser & "' AND password='" & Me.inputPass & "' ", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "user")

        If ds.Tables(0).Rows.Count > 0 Then
            MessageBox.Show("Login berhasil!")
            MainMenu.Show()
            Me.Hide()
        Else
            MessageBox.Show("Username atau password salah!")
        End If


    End Sub

    Sub CekInputKosong(TextBox1 As TextBox, TextBox2 As TextBox)
        If TextBox1.Text = "" Then
            MsgBox("Silahkan masukkan username terlebih dahulu !")
            TextBox1.Focus()
        End If

        If TextBox2.Text = "" Then
            MsgBox("Silahkan masukkan password terlebih dahulu !")
            TextBox1.Focus()
        End If
    End Sub

    Sub ClearLogin(TextBox1 As TextBox, TextBox2 As TextBox)
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

End Class