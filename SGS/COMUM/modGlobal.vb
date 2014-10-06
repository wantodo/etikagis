Public Module modGlobal
    Public strConexao As String

    Sub New()
        ConectaBanco()
    End Sub

    Public Sub ConectaBanco()
        strConexao = "Data Source=mssql01-farm51.kinghost.net;Initial Catalog=etikaconsultoria;Persist Security Info=True; User ID=etikaconsultoria;Password=suporte2014"
    End Sub
End Module
