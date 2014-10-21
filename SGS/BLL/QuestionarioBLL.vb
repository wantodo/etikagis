Imports System.Net.Mail

Public Class QuestionarioBLL

    Public Function ListaQuestionario(codEmpresa As Integer, codCategoria As Integer, codRepresentante As Integer) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.ListaQuestionario(codEmpresa, codCategoria, codRepresentante)
    End Function

    Public Sub InsereQuestionario(objQuestionario As MODEL.Questionario)
        Dim obj As New DAL.QuestionarioDAL

        obj.InsereQuestionario(objQuestionario)
    End Sub

    Public Sub ExcluiQuestionario(idRepresentante As Integer, idCategoria As Integer)
        Dim obj As New DAL.QuestionarioDAL

        obj.ExcluiQuestionario(idRepresentante, idCategoria)
    End Sub

    Public Function RetornaStatusQuestionario() As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaStatusQuestionario
    End Function


    Public Function EnviaEmailFinalizar(objQuestionario As MODEL.Questionario)
        Dim obj As New DAL.QuestionarioDAL
        Dim sSMTPeMail As String = ""
        Dim sNomeDestinatario As String = ""
        Dim seMailDestinatario As String = ""
        Dim seMailRemetente As String = ""
        Dim sNomeRemetente As String = ""
        Dim sAssuntoEmail As String = ""

        Try

            sSMTPeMail = "smtp.etikaconsultoria.com.br"
            'sSMTPeMail = "smtp.duratex.com.br"

            'seMailDestinatario = "diogo.bastos@duratex.com.br" 'objQuestionario.representante.dc_email
            seMailDestinatario = objQuestionario.representante.dc_email
            sNomeDestinatario = objQuestionario.representante.no_representante

            seMailRemetente = "mribeiro@etikaconsultoria.com.br​"
            'seMailRemetente = "cadastro.fornecedores@duratex.com.br​"
            sNomeRemetente = "Etika Consultoria"
            sAssuntoEmail = "Questionário Finalizado!"

        Catch ex As Exception
            Dim erro As New Exception("Falha ao obter configurações de email.")
        End Try

        Dim sEmailDest As String = seMailDestinatario

        Dim Mensagem As MailMessage = New MailMessage()
        Dim Mailmsg As New System.Net.Mail.MailMessage()
        Dim mSmtpCliente As New SmtpClient(sSMTPeMail)

        Mailmsg.From = New MailAddress(seMailRemetente, sNomeRemetente)

        Mailmsg.Subject = sAssuntoEmail
        Mailmsg.BodyEncoding = System.Text.Encoding.UTF8
        Mailmsg.IsBodyHtml = True

        Mailmsg.Body = "<HTML>"
        Mailmsg.Body += "Existe um questionário liberado para a área " & objQuestionario.representante.dc_area
        Mailmsg.Body += "</HTML>"

        Mailmsg.To.Add(New MailAddress(seMailDestinatario, sNomeDestinatario))

        Mailmsg.Attachments.Clear()

        mSmtpCliente.Send(Mailmsg)
        Mailmsg.Attachments.Dispose()
        Mailmsg.Dispose()
        EnviaEmailFinalizar = True

        'Try
        '    Try
        '        mSmtpCliente.Send(Mailmsg)
        '        EnviaEmailFinalizar = True
        '    Catch ex As Exception
        '        EnviaEmailFinalizar = False
        '        Dim erro As New Exception("Houve um problema ao enviar seu email. Tente Novamente.")
        '        Throw erro
        '    End Try
        '    Mailmsg.Attachments.Dispose()
        '    Mailmsg.Dispose()
        'Catch ex As Exception
        '    EnviaEmailFinalizar = False
        '    Dim erro As New Exception("Houve um problema ao enviar seu email. Tente Novamente.")
        '    Throw erro
        'End Try
    End Function


    Public Function RetornaPontoFocal(codEmpresa As Integer) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaPontoFocal(codEmpresa)
    End Function

    Public Sub AlteraQuestionario(codRepresentante As Integer, codStatus As Integer)
        Dim obj As New DAL.QuestionarioDAL

        obj.AlteraQuestionario(codRepresentante, codStatus)
    End Sub

    Function RetornaQuestionarioRepresentante(parametros As Array) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaQuestionarioRepresentante(parametros)
    End Function

End Class


