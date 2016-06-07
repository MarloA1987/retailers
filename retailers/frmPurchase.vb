Public Class frmPurchase
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SqlRefresh = "select walletid,telconame,amount,datepurchased from wallet limit 0,50"
        SqlReFill("wallet", ListView1)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim selected As String
        If RadioButton1.Checked = True Then
            selected = RadioButton1.Text
        ElseIf (RadioButton2.Checked = True) Then
            selected = RadioButton2.Text

        End If

        Me.Dispose()
    End Sub
End Class