Public Class FormCetakLapBarang

    Private Sub FormCetakLapBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from barang2110024 order by idbarang2110024"
        cek = perintah.ExecuteReader
        While cek.Read
            ComboBox1.Items.Add(cek.Item("idbarang2110024"))
            ComboBox2.Items.Add(cek.Item("idbarang2110024"))
        End While
        kon.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim CrLapBarang As New LapBarang
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from barang2110024 where idbarang2110024 between '" & ComboBox1.Text & "' and '" & ComboBox2.Text & "'"
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "barang2110024")
        CrLapBarang.SetDataSource(ds.Tables("barang2110024"))
        FormCetak.CrystalReportViewer1.ReportSource = CrLapBarang
        FormCetak.WindowState = FormWindowState.Maximized
        FormCetak.Show()
        kon.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class