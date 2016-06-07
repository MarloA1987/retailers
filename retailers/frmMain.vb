Public Class frmMain
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        getData()
        showOrders()
    End Sub
    Public errorDisplay As String
    Private Sub showOrders()
        msgShow = False
        SqlRefresh = "SELECT orderid,customerid,number,loadrefid,orderdate FROM `orders` order by orderid desc"
        SqlReFill("orders", ListView1)
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        rmDatabase.Show()
    End Sub
    Function numbers(ByVal n As String)

        Dim c As Integer = n.Length - 1
        Dim result As String = ""
        Dim errorMessage As Boolean = False
        For i As Integer = 0 To c

            If IsNumeric(n(c)) = True Then
                result = n
            Else
                errorDisplay = "Detected letters, only numeric allowed"
                result = ""
                errorMessage = True
            End If
            If i = 3 Then
                txtPreNum.Text = n(0) & n(1) & n(2)
                'WE WILL NOW IDENTIFY TELCO PREFIX NUMBER
                SqlRefresh = "select numid,telconame from prenumtelco where prenum=@prenum"
                SqlReFill("prenumtelco", Nothing, "ShowValueInTextbox", {"prenum"}, {txtPreNum}, {txtPreNumID, txtTelco})
            End If
        Next
        If errorMessage = True Then
            MessageBox.Show(errorDisplay)
        End If
        Return result

    End Function
    Private Sub btnPurchase_Click(sender As Object, e As EventArgs) Handles btnPurchase.Click
        frmPurchase.ShowDialog()
    End Sub

    Private Sub btnUPDATEDB_Click(sender As Object, e As EventArgs) Handles btnUPDATEDB.Click
        Dim sqls As String()
        Dim sqlList As New List(Of String)()
        Dim sqlInTrack As Integer = 0
        sqlList.Add("CREATE TABLE IF NOT EXISTS `wallet` (
                      `id` int(11) NOT NULL AUTO_INCREMENT,
                      `amount` double(6,2) NOT NULL,
                      `telco` enum('SMART','GLOBE') NOT NULL,
                      `purchaseDate` date NOT NULL,
                      PRIMARY KEY (`id`)
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;")

        sqlList.Add("CREATE TABLE IF NOT EXISTS `orders` (
                        `OrderID` int(31) NOT NULL AUTO_INCREMENT,
                        `CustomerID` int(31) NOT NULL,
                        `LoadRefID` int(31) NOT NULL,
                        `TelcoName` varchar(10) NOT NULL,
                        `Number` int(10) NOT NULL,
                        `OrderDate` date,
                       PRIMARY KEY (`OrderID`)
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;")
        sqlList.Add("CREATE TABLE IF NOT EXISTS `loadref` (
                      `LoadRefID` int(31) NOT NULL AUTO_INCREMENT,
                      `TelcoName` varchar(10) NOT NULL,
                      `Cost` float NOT NULL,
                      `RetailAmount` float(5,4) NOT NULL,
                      PRIMARY KEY (`LoadRefID`)
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;")
        sqlList.Add("CREATE TABLE IF NOT EXISTS `customers` (
                  `customerid` int(31) NOT NULL AUTO_INCREMENT,
                  `customername` varchar(100) NOT NULL,
                  `Credit` double(12,2) NOT NULL,
                  `Payments` double(12,2) NOT NULL,
                  PRIMARY KEY (`customerid`),
                  UNIQUE KEY `customername` (`customername`)
                ) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;")
        sqlList.Add("create table if not exists NumberRef(
                    NumID int(31) not null auto_increment,
                    TelcoName ENUM('SMART','GLOBE') not null,
                    PreNum int(3) not null unique,
                    primary key(NumID)
                    );")
        sqls = sqlList.ToArray()
        Dim isQueryErr As Boolean = False
        For Each sqL In sqls
            sqlInTrack += 1
            If IsError(sqL) = False Then
                Try
                    ConnDB()
                    cmd = New MySql.Data.MySqlClient.MySqlCommand(sqL, conn)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show("Warning! Sql query seems not to work on " & sqlInTrack, "Message error", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    isQueryErr = True
                Finally
                    DisconnDB()
                End Try
            End If
        Next
        'CHECK IF ERROR OCCURS
        If isQueryErr = True Then
            MessageBox.Show("Error occurs on creating tables")
        Else
            MessageBox.Show("Successfully created tables on database.")
        End If
    End Sub
    Private sqls As String()
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sqlList As New List(Of String)()
        sqlList.Add("SELECT orderid,customerid,number,loadrefid,orderdate FROM `orders` order by orderid desc") '0
        sqls = sqlList.ToArray()


        SqlRefresh = sqls(0)
        StatusSet = "New"
        SqlUpdateNew("orders", ListView1, {"customerid", "loadrefid", "telconame", "number", "orderdate"}, {TxtCustoID, txtPreNum, txtTelco, txtNumber, txtDate})

    End Sub

    Private Sub txtNumber_TextChanged(sender As Object, e As EventArgs) Handles txtNumber.KeyUp


    End Sub
End Class
