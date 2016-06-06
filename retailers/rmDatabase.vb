Public Class rmDatabase

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        getData()
        ' Add any initialization after the InitializeComponent() call.
        txtServer.Text = ServerMySQL
        txtUsername.Text = UserNameMySQL
        txtPassword.Text = PwdMySQL
        txtPort.Text = PortMySQL
        txtDatabase.Text = DBNameMySQL
    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        DBNameMySQL = txtDatabase.Text
        ServerMySQL = txtServer.Text
        UserNameMySQL = txtUsername.Text
        PwdMySQL = txtPassword.Text
        PortMySQL = txtPort.Text
        SaveData()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        CheckConnection()
    End Sub
End Class
