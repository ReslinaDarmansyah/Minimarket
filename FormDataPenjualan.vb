Public Class FormDataPenjualan
    Dim totSel As Double
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
        dgv.Columns(0).HeaderText = "No Faktur"
        dgv.Columns(1).HeaderText = "Tanggal Faktur"
        dgv.Columns(2).HeaderText = "Jumlah Item"
        dgv.Columns(3).HeaderText = "Total Transaksi"
        dgv.Columns(0).Width = 150
        dgv.Columns(1).Width = 200
        dgv.Columns(2).Width = 150
        dgv.Columns(3).Width = 150
    End Sub
    Sub hitungTotal()
        totSel = 0
        For x = 0 To dgv.Rows.Count - 1
            totSel = totSel + CDbl(dgv.Rows.Item(x).Cells(3).Value)
        Next x
        Label3.Text = Format(totSel, "Rp ###,###,##")
    End Sub

    Private Sub FormDataPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        dgv.Columns.Clear()
        Call tampilData("SELECT jual2110024.idjual2110024, tgl2110024, COUNT(idbarang2110024) AS jmlitem2110024, total2110024 FROM jual2110024 JOIN detailjual2110024 ON jual2110024.idjual2110024=detailjual2110024.idjual2110024 where tgl2110024='" & Format(dtpTanggal.Value, "yyyy-MM-dd") & "' GROUP BY detailjual2110024.idjual2110024")
        Call setDgv()
        Call hitungTotal()
    End Sub

    Private Sub dtpTanggal_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpTanggal.ValueChanged
        dgv.Columns.Clear()
        Call tampilData("SELECT jual2110024.idjual2110024, tgl2110024, COUNT(idbarang2110024) AS jmlitem2110024, total2110024 FROM jual2110024 JOIN detailjual2110024 ON jual2110024.idjual2110024=detailjual2110024.idjual2110024 where tgl2110024='" & Format(dtpTanggal.Value, "yyyy-MM-dd") & "' GROUP BY detailjual2110024.idjual2110024")
        Call setDgv()
        Call hitungTotal()
    End Sub
End Class