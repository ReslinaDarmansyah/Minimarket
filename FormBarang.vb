Public Class FormBarang

    Sub tampilData(ByVal Sql As String)
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = Sql
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "data")
        dgv.DataSource = ds.Tables("data")
        kon.Close()
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""

    End Sub
    Sub setdgv()
        dgv.Columns(0).HeaderText = "Id Barang"
        dgv.Columns(1).HeaderText = "Nama Barang"
        dgv.Columns(2).HeaderText = "Satuan"
        dgv.Columns(3).HeaderText = "Harga Beli"
        dgv.Columns(4).HeaderText = "Harga Jual"
        dgv.Columns(5).HeaderText = "Stok"

        dgv.Columns(0).Width = 70
        dgv.Columns(1).Width = 110
        dgv.Columns(2).Width = 80
        dgv.Columns(3).Width = 100
        dgv.Columns(4).Width = 100
        dgv.Columns(5).Width = 80


    End Sub
    Sub buatTombol()
        'Buat tombol edit di dalam datagrid
        Dim btnEdit As New DataGridViewButtonColumn
        btnEdit.Name = "btnEdit"
        btnEdit.HeaderText = ""
        btnEdit.FlatStyle = FlatStyle.Popup
        btnEdit.DefaultCellStyle.ForeColor = Color.DarkViolet
        btnEdit.Text = "Edit"
        btnEdit.Width = 50
        btnEdit.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnEdit)

        'Buat tombol hapus di dalam datagrid
        Dim btnHapus As New DataGridViewButtonColumn
        btnHapus.Name = "btnHapus"
        btnHapus.HeaderText = ""
        btnHapus.FlatStyle = FlatStyle.Popup
        btnHapus.DefaultCellStyle.ForeColor = Color.DarkViolet
        btnHapus.Text = "Hapus"
        btnHapus.Width = 50
        btnHapus.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnHapus)
    End Sub

    Private Sub FormBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnSimpan.Enabled = False
        btnUpdate.Enabled = False

        dgv.Columns.Clear()
        Call tampilData("select idbarang2110024, namabarang2110024, satuan2110024, hargabeli2110024, hargajual2110024, stok2110024 from barang2110024")
        Call setdgv()
        Call buatTombol()

    End Sub

    Private Sub btnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        btnSimpan.Enabled = True
        TextBox1.Focus()
        Call bersih()

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "update barang2110024 set namabarang2110024='" & TextBox2.Text & "', satuan2110024='" & TextBox3.Text & "', hargabeli2110024 = '" & TextBox4.Text & "', hargajual2110024='" & TextBox5.Text & "' , stok2110024='" & TextBox6.Text & "' where idbarang2110024='" & TextBox1.Text & "'"
        perintah.ExecuteNonQuery()
        kon.Close()
        Call bersih()
        FormBarang_Load(e, CType(AcceptButton, EventArgs))
        MsgBox("Data berhasil diupdate", MsgBoxStyle.Information, "Informasi")
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        dgv.Columns.Clear()
        Call tampilData("select idbarang2110024, namabarang2110024, satuan2110024, hargabeli2110024, hargajual2110024, stok2110024 from barang2110024 where idbarang2110024 like '%" & TextBox7.Text & "%' or namabarang2110024 like '%" & TextBox7.Text & "%' ")
        Call setdgv()
        Call buatTombol()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "insert into barang2110024(idbarang2110024, namabarang2110024, satuan2110024, hargabeli2110024, hargajual2110024, stok2110024) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')"
        perintah.ExecuteNonQuery()
        kon.Close()
        Call bersih()
        FormBarang_Load(e, CType(AcceptButton, EventArgs))
        MsgBox("Data berhasil disimpan", MsgBoxStyle.Information, "Informasi")
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "select * from barang2110024 where idbarang2110024 ='" & TextBox1.Text & "'"
                cek = perintah.ExecuteReader
                cek.Read()
                If cek.HasRows Then
                    MsgBox("Username sudah ada", MsgBoxStyle.Information,
                    "Informasi")
                    TextBox1.Clear()
                    TextBox1.Focus()
                Else
                    TextBox2.Focus()
                End If
                kon.Close()
        End Select
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim i As Integer
        Dim id As String
        i = dgv.CurrentRow.Index
        id = CStr(dgv.Rows.Item(i).Cells(0).Value)
        'Jika diklik tombol edit
        If e.ColumnIndex = 6 Then
            TextBox1.Text = id
            TextBox2.Text = CStr(dgv.Rows.Item(i).Cells(1).Value)
            TextBox3.Text = CStr(dgv.Rows.Item(i).Cells(2).Value)
            TextBox4.Text = CStr(dgv.Rows.Item(i).Cells(3).Value)
            TextBox5.Text = CStr(dgv.Rows.Item(i).Cells(4).Value)
            TextBox6.Text = CStr(dgv.Rows.Item(i).Cells(5).Value)
            btnUpdate.Enabled = True
            btnSimpan.Enabled = False
        End If
        'Jika diklik tombol hapus
        If e.ColumnIndex = 7 Then
            Dim x As Byte
            x = CByte(MsgBox("Hapus data dengan kode " + id,
            CType(MsgBoxStyle.Critical + vbYesNo, MsgBoxStyle), "Konfirmasi"))
            If x = vbYes Then
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "delete from barang2110024 where idbarang2110024 = '" & id & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                'panggil even form load
                FormBarang_Load(e, CType(AcceptButton, EventArgs))
            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub
End Class