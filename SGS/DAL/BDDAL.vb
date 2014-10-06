Imports System.Data
Imports System.Data.SqlClient

Public Class BDDAL
    Protected m_connectionString As String
    Private m_manterConexao As Boolean
    Public conexao As SqlConnection
    Public ts As SqlTransaction = Nothing

    Public Property ConnectionString() As String
        Get
            Return Me.m_connectionString
        End Get
        Set(ByVal value As String)
            Me.m_connectionString = value
        End Set
    End Property

    Public Property ManterConexao() As Boolean
        Get
            Return Me.m_manterConexao
        End Get
        Set(ByVal value As Boolean)
            Me.m_manterConexao = value
        End Set
    End Property

    Public Sub New(ByVal connectionString As String)
        Me.m_connectionString = connectionString
    End Sub

    Public Sub New(ByVal connectionString As String, ByVal manterConexao As Boolean)
        Me.m_connectionString = connectionString
        Me.m_manterConexao = manterConexao
    End Sub

    Public Sub Fechar()
        If Not (conexao Is Nothing) Then
            conexao.Close()
            conexao = Nothing
        End If
    End Sub

    Public Overloads Function ExecuteNonQuery(ByVal cmdText As String, ByVal cmdType As CommandType) As Integer
        Return ExecuteNonQuery(cmdText, cmdType, Nothing)
    End Function

    Public Overloads Function ExecuteNonQuery(ByVal cmdText As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As SqlParameter) As Integer
        Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, parameters)
        Dim retVal As Integer
        cmd.Transaction = ts
        cmd.CommandTimeout = 2000
        Try
            If ManterConexao = False Then
                retVal = cmd.ExecuteNonQuery()
                Me.Fechar()
            Else
                retVal = cmd.ExecuteNonQuery()
            End If
            Return retVal
        Catch e As Exception
            Me.Fechar()
            Throw e
        End Try
    End Function

    Public Overloads Function ExecuteScalar(ByVal cmdText As String, ByVal cmdType As CommandType) As Object
        Return ExecuteScalar(cmdText, cmdType, Nothing)
    End Function

    Public Overloads Function ExecuteScalar(ByVal cmdText As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As SqlParameter) As Object
        Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, parameters)
        Dim retVal As Object
        Try
            If ManterConexao = False Then
                retVal = cmd.ExecuteScalar()
                Me.Fechar()
            Else
                retVal = cmd.ExecuteScalar()
            End If
            Return retVal

        Catch e As Exception
            Me.Fechar()
            Throw e
        End Try
    End Function

    Public Function GetDataReader(ByVal cmd As SqlCommand) As SqlDataReader
        Try
            If ManterConexao = False Then
                Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Else
                Return cmd.ExecuteReader()
            End If
        Catch e As Exception
            Me.Fechar()
            Throw e
        End Try
    End Function

    Public Function GetDataSet(ByVal cmdText As String, ByVal cmdType As CommandType) As DataSet
        Try
            Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, Nothing)
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            da.SelectCommand = cmd
            da.Fill(ds)
            Return ds
        Catch e As Exception
            Me.Fechar()
            Throw e
        End Try
    End Function

    Public Function GetDataTable(ByVal cmdText As String, ByVal cmdType As CommandType) As DataTable
        Try
            Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, Nothing)
            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch e As Exception
            Me.Fechar()
            Throw e
        End Try
    End Function

    Public Function GetDataSet(ByVal cmdText As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As SqlParameter) As DataSet
        Try
            Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, parameters)
            cmd.CommandTimeout = 0
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            da.SelectCommand = cmd
            da.Fill(ds)
            Return ds
        Catch e As Exception
            Me.Fechar()
            Throw e
        End Try
    End Function

    Public Function GetDataTable(ByVal cmdText As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Try
            Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, parameters)
            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch e As Exception
            Me.Fechar()
            Throw e
        End Try
    End Function

    Public Overloads Function ExecuteReader(ByVal cmdText As String, ByVal cmdType As CommandType) As SqlDataReader
        Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, Nothing)
        Return GetDataReader(cmd)
    End Function

    Public Overloads Function ExecuteReader(ByVal cmdText As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As SqlParameter) As SqlDataReader

        Dim cmd As SqlCommand = CriarComando(cmdText, cmdType, parameters)
        Return GetDataReader(cmd)
    End Function

    Public Function CriarComando(ByVal cmdText As String, ByVal cmdType As CommandType, ByVal ParamArray parameters() As SqlParameter) As SqlCommand
        Dim cmd As New SqlCommand(cmdText)
        cmd.CommandType = cmdType
        If Not (parameters Is Nothing) Then
            Dim param As SqlParameter
            For Each param In parameters
                cmd.Parameters.Add(param)
            Next param
        End If
        VerificarConexao(cmd)
        Return cmd
    End Function

    Public Sub VerificarConexao(ByRef cmd As SqlCommand)
        If conexao Is Nothing Then
            conexao = New SqlConnection(Me.ConnectionString)
            conexao.Open()
        Else
            If conexao.State <> ConnectionState.Open Then
                conexao.Open()
            End If
        End If
        cmd.Connection = conexao
        Return
    End Sub

    Public Function CriarParametro(ByVal name As String, ByVal type As SqlDbType, ByVal value As Object) As SqlParameter
        Dim param As New SqlParameter
        param.ParameterName = name
        param.SqlDbType = type
        If value Is Nothing Then
            param.Value = DBNull.Value
        Else
            If type = SqlDbType.VarChar And value.ToString().Length = 0 Then
                param.Value = DBNull.Value
            Else
                param.Value = value
            End If
        End If
        Return param
    End Function

    Public Shared Function GetString(ByRef dr As SqlDataReader, ByVal ordinal As Integer) As String
        If Not dr.IsDBNull(ordinal) Then
            Return dr.GetString(ordinal)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetInt32(ByRef dr As SqlDataReader, ByVal ordinal As Integer) As Integer
        If Not dr.IsDBNull(ordinal) Then
            Return dr.GetInt32(ordinal)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetDouble(ByRef dr As SqlDataReader, ByVal ordinal As Integer) As Double
        If Not dr.IsDBNull(ordinal) Then
            Return dr.GetDouble(ordinal)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetDateTime(ByRef dr As SqlDataReader, ByVal ordinal As Integer) As DateTime
        If Not dr.IsDBNull(ordinal) Then
            Return dr.GetDateTime(ordinal)
        Else
            Return Nothing
        End If
    End Function

    Public Sub Transacao()
        If conexao Is Nothing Then
            conexao = New SqlConnection(Me.ConnectionString)
            conexao.Open()
        End If
        ts = conexao.BeginTransaction
        Return
    End Sub

End Class
