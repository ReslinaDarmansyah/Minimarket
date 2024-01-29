Public Class FormPenjualan
    Dim subTotal As Double
    Dim totSel, jmlUang, kembali As Double
    Dim i, nomor As Integer
    Dim idBarang, noFaktur As String

    Sub tampilData()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "SELECT temp2110024.idbarang2110024, namabarang2110024, harga2110024, qty2110024, harga2110024 * qty2110024 AS subtotal2110024 FROM temp2110024 JOIN barang2110024 ON temp2110024.idbarang2110024=barang2110024.idbarang2110024"
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "data")
        dgv.DataSource = ds.Tables("data")
        kon.Close()
    End Sub
    Sub cetakFaktur()
        Dim CrFaktur As New FakturPenjualan
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "SELECT detailjual2110024.idjual2110024, tgl2110024,detailjual2110024.idbarang2110024,namabarang2110024, satuan2110024, harga2110024, qty2110024, harga2110024*qty2110024 AS subtotal, nama2110024 AS username2110024 FROM detailjual2110024 JOIN jual2110024 ON detailjual2110024.idjual2110024=jual2110024.idjual2110024 JOIN barang2110024 ON detailjual2110024.idbarang2110024=barang2110024.idbarang2110024 JOIN user2110024 ON jual2110024.username2110024=user2110024.username2110024 WHERE detailjual2110024.idjual2110024='" & TextBox1.Text & "'"
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "faktur2110024")
        CrFaktur.SetDataSource(ds.Tables("faktur2110024"))
        FormCetak.CrystalReportViewer1.ReportSource = CrFaktur
        FormCetak.WindowState = FormWindowState.Maximized
        FormCetak.Show()
        kon.Close()
    End Sub

    Sub buatTombol()
        'Buat tombol hapus di dalam datagrid
        Dim btnHapus As New DataGridViewButtonColumn
        btnHapus.Name = "btnHapus"
        btnHapus.HeaderText = ""
        btnHapus.FlatStyle = FlatStyle.Popup
        btnHapus.DefaultCellStyle.ForeColor = Color.Red
        btnHapus.Text = "Hapus"
        btnHapus.Width = 50
        btnHapus.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnHapus)
    End Sub

    Sub setDgv()
        dgv.Columns(0).HeaderText = "Kode Barang"
        dgv.Columns(1).HeaderText = "Nama Barang"
        dgv.Columns(2).HeaderText = "Harga"
        dgv.Columns(3).HeaderText = "Qty"
        dgv.Columns(4).HeaderText = "Sub Total"

        dgv.Columns(0).Width = 150
        dgv.Columns(1).Width = 280
        dgv.Columns(2).Width = 150
        dgv.Columns(3).Width = 150
        dgv.Columns(4).Width = 150
    End Sub

    Sub bersih()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
    End Sub

    Sub hitungTotal()
        totSel = 0
        For x = 0 To dgv.Rows.Count - 1
            totSel = totSel + CDbl(dgv.Rows.Item(x).Cells(4).Value)
        Next x
        Label3.Text = Format(totSel, "Rp ###,###,##")
    End Sub

    Sub buatNoFaktur()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select ifnull(max(right(idjual2110024,4)),0) as nourut from jual2110024"
        cek = perintah.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            nomor = CInt(cek.Item("nourut"))
            nomor = nomor + 1
            noFaktur = "F" + Format(nomor, "0000")
            TextBox1.Text = noFaktur
        End If
        kon.Close()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub FormPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Clear()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "delete from temp2110024"
        perintah.ExecuteNonQuery()
        kon.Close()

        Call buatNoFaktur()
        dgv.Columns.Clear()
        Call tampilData()
        Call setDgv()
        Call buatTombol()
        Call hitungTotal()

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            kon.Open()
            perintah.Connection = kon
            perintah.CommandType = CommandType.Text
            perintah.CommandText = "select * from barang2110024 where idbarang2110024='" & TextBox2.Text & "'"
            cek = perintah.ExecuteReader
            cek.Read()
            If cek.HasRows Then
                idBarang = CStr(cek.Item("idbarang2110024"))
                TextBox3.Text = CStr(cek.Item("namabarang2110024"))
                TextBox4.Text = CStr(cek.Item("stok2110024"))
                TextBox5.Text = CStr(cek.Item("hargajual2110024"))
                TextBox6.Focus()
            Else
                MsgBox("Data barang tidak ditemukan", MsgBoxStyle.Exclamation, "Informasi")
                TextBox2.Clear()
                TextBox2.Focus()
            End If
            kon.Close()
        End If
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        subTotal = Val(TextBox5.Text) * Val(TextBox6.Text)
        TextBox7.Text = Format(subTotal, "Rp ###,###,##")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'simpan data ke dalam tabel temp
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "insert into temp2110024 values('" & TextBox2.Text & "','" & TextBox6.Text & "','" & TextBox5.Text & "')"
        perintah.ExecuteNonQuery()
        kon.Close()

        dgv.Columns.Clear()
        Call tampilData()
        Call setDgv()
        Call buatTombol()
        Call hitungTotal()
        Call bersih()

    End Sub

    Private Sub TextBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Button2_Click(e, CType(AcceptButton, EventArgs))
        End Select
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim i As Integer
        If e.ColumnIndex = 5 Then
            i = dgv.CurrentRow.Index
            idBarang = CStr(dgv.Rows.Item(i).Cells(0).Value)
            kon.Open()
            perintah.Connection = kon
            perintah.CommandType = CommandType.Text
            perintah.CommandText = "delete from temp2110024 where idbarang2110024='" & idBarang & "'"
            perintah.ExecuteNonQuery()
            kon.Close()

            dgv.Columns.Clear()
            Call tampilData()
            Call setDgv()
            Call buatTombol()
            Call hitungTotal()
            Call bersih()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        Try
            jmlUang = Val(CDbl(TextBox8.Text))
            kembali = jmlUang - totSel
            TextBox9.Text = Format(kembali, "Rp ###,###,##")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        FormDataPenjualan.Show()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        FormCariBarang.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "insert into jual2110024 values('" & TextBox1.Text & "',now(),'" & userLogin & "','" & totSel & "')"
        perintah.ExecuteNonQuery()
        kon.Close()

        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "insert into detailjual2110024(idjual2110024, idbarang2110024,qty2110024,harga2110024) select '" & TextBox1.Text & "', idbarang2110024,qty2110024,harga2110024 from temp2110024"
        perintah.ExecuteNonQuery()
        kon.Close()

        MsgBox("Data berhasil disimpan", MsgBoxStyle.Information, "Informasi")
        Call cetakFaktur()
        Call bersih()
        TextBox1.Clear()
        TextBox8.Text = "0"
        TextBox9.Clear()
        Label3.Text = "0"

        Button1_Click(e, CType(AcceptButton, EventArgs))
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
End Class