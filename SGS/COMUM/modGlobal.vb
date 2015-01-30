Public Module modGlobal
    Public strConexao As String

    Sub New()
        ConectaBanco()
    End Sub

    Public Sub ConectaBanco()
        'Prod
        strConexao = "Data Source=mssql01-farm51.kinghost.net;Initial Catalog=etikaconsultoria;Persist Security Info=True; User ID=etikaconsultoria;Password=suporte2014"

        'Dev
        'strConexao = "Data Source=NESC-EC0146529;Initial Catalog=etikaconsultoria;Persist Security Info=True; User ID=etikaconsultoria;Password=P@ssw0rdM0del0"


    End Sub
End Module
