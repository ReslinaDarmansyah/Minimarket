Public Class FormCetakLapPenjualan

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim tglAwal As String
        Dim tglAkhir As String

        tglAwal = Format(DateTimePicker1.Value, "yyyy-MM-dd")
        tglAkhir = Format(DateTimePicker2.Value, "yyyy-MM-dd")

        Dim CrLapPenjualan As New LapPenjualan
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "SELECT jual2110024.idjual2110024, tgl2110024, COUNT(idbarang2110024) AS jmlitem2110024, total2110024 FROM jual2110024 JOIN detailjual2110024 ON jual2110024.idjual2110024=detailjual2110024.idjual2110024 where tgl2110024 between '" & tglAwal & "' and '" & tglAkhir & "' GROUP BY detailjual2110024.idjual2110024"
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "penjualan2110024")
        CrLapPenjualan.SetDataSource(ds.Tables("penjualan2110024"))
        FormCetakPenjualan.CrystalReportViewer1.ReportSource = CrLapPenjualan
        FormCetakPenjualan.WindowState = FormWindowState.Maximized
        FormCetakPenjualan.Show()
        kon.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class