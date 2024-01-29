Imports MySql.Data.MySqlClient
Module Module1
    Dim strKon As String = "server=192.168.0.101; uid=admin; password=123; database=dbminimarket2110024"
    Public kon As New MySqlConnection(strKon)
    Public perintah As New MySqlCommand
    Public mda As New MySqlDataAdapter
    Public ds As New DataSet
    Public cek As MySqlDataReader
    Public userLogin As String
End Module
