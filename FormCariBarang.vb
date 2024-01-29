Public Class FormCariBarang

    Sub tampilData(ByVal sql As String)
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = sql
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "data")
        dgv.DataSource = ds.Tables("data")
        kon.Close()
    End Sub
    Sub setDgv()
        dgv.Columns(0).HeaderText = "Kode Barang"
        dgv.Columns(1).HeaderText = "Nama Barang"
        dgv.Columns(2).HeaderText = "Satuan"
        dgv.Columns(3).HeaderText = "Harga Beli"
        dgv.Columns(4).HeaderText = "Harga Jual"
        dgv.Columns(5).HeaderText = "Stok"

        dgv.Columns(0).Width = 60
        dgv.Columns(1).Width = 110
        dgv.Columns(2).Width = 60
        dgv.Columns(3).Width = 70
        dgv.Columns(4).Width = 70
        dgv.Columns(5).Width = 50
        dgv.Columns(5).Width = 80
    End Sub
    Sub buatTombol()
        'Buat tombol pilih di dalam datagrid
        Dim btnPilih As New DataGridViewButtonColumn
        btnPilih.Name = "btnPilih"
        btnPilih.HeaderText = ""
        btnPilih.FlatStyle = FlatStyle.Popup
        btnPilih.DefaultCellStyle.ForeColor = Color.DarkViolet
        btnPilih.Text = "Pilih"
        btnPilih.Width = 50
        btnPilih.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnPilih)
    End Sub

    
    Private Sub FormCariBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        dgv.Columns.Clear()
        Call tampilData("select * from barang2110024")
        Call setDgv()
        Call buatTombol()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        dgv.Columns.Clear()
        Call tampilData("select * from barang2110024 where idbarang2110024='" & TextBox1.Text & "' or namabarang2110024 like '%" & TextBox1.Text & "%'")
        Call setDgv()
        Call buatTombol()
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim i As Integer
        If e.ColumnIndex = 6 Then
            i = dgv.CurrentRow.Index
            FormPenjualan.TextBox2.Text = CStr(dgv.Rows.Item(i).Cells(0).Value)
            FormPenjualan.TextBox3.Text = CStr(dgv.Rows.Item(i).Cells(1).Value)
            FormPenjualan.TextBox4.Text = CStr(dgv.Rows.Item(i).Cells(5).Value)
            FormPenjualan.TextBox5.Text = CStr(dgv.Rows.Item(i).Cells(4).Value)
            Me.Dispose()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class