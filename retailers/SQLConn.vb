Imports MySql.Data.MySqlClient

Module SQLConn
    Public ServerMySQL As String
    Public PortMySQL As String
    Public UserNameMySQL As String
    Public PwdMySQL As String
    Public DBNameMySQL As String
    Public Remember As String
    Public sqL As String
    'Public dt As New DataSet
    Public ds As DataSet
    Public cmd As MySqlCommand
    Public dr As MySqlDataReader
    Public da As MySqlDataAdapter
    ' Public conn As New MySqlConnection("server=localhost;database=posisdb;uid=root;pwd=")
    Public conn As New MySqlConnection
    Public StatusSet As String
    Public SqlRefresh As String

    Sub getData()
        Dim AppName As String = Application.ProductName
        Try
            DBNameMySQL = GetSetting(AppName, "DBSection", "DB_Name", "agribusiness")
            ServerMySQL = GetSetting(AppName, "DBSection", "DB_IP", "localhost")
            PortMySQL = GetSetting(AppName, "DBSection", "DB_Port", "3306")
            UserNameMySQL = GetSetting(AppName, "DBSection", "DB_User", "root")
            PwdMySQL = GetSetting(AppName, "DBSection", "DB_Password", "admin456")
            Remember = GetSetting(AppName, "DBSection", "LogUser", "temp")
            conn.ConnectionString = "server=" & ServerMySQL & ";port=" & PortMySQL & ";database=" & DBNameMySQL & ";uid=" & UserNameMySQL & ";pwd=" & PwdMySQL
        Catch ex As Exception
            MsgBox("System registry was not established, you can set/save " &
            "these settings by pressing F1", MsgBoxStyle.Information)
            SaveData()
        End Try
    End Sub
    Public Sub ConnDB()
        Try
            conn.Open()
        Catch ex As Exception
            MsgBox("The system failed to establish a connection", MsgBoxStyle.Information, "Database Settings")
        End Try
    End Sub
    Public Sub CheckConnection()
        Try
            conn.Open()
            conn.Dispose()
            MessageBox.Show("Connected")
        Catch ex As Exception
            MessageBox.Show("Unable to retrieve database please contact system administrator")
        End Try
    End Sub
    Public Sub DisconnDB()
        conn.Close()
        conn.Dispose()
    End Sub
    Sub SaveData()
        Dim AppName As String = Application.ProductName
        SaveSetting(AppName, "DBSection", "DB_Name", DBNameMySQL)
        SaveSetting(AppName, "DBSection", "DB_IP", ServerMySQL)
        SaveSetting(AppName, "DBSection", "DB_Port", PortMySQL)
        SaveSetting(AppName, "DBSection", "DB_User", UserNameMySQL)
        SaveSetting(AppName, "DBSection", "DB_Password", PwdMySQL)
        'MsgBox("Database connection settings are saved.", MsgBoxStyle.Information)
    End Sub

End Module

