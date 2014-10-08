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

    Public Sub ExcluiQuestionario(idRepresentante As Integer)
        Dim obj As New DAL.QuestionarioDAL

        obj.ExcluiQuestionario(idRepresentante)
    End Sub

    Public Function RetornaStatusQuestionario() As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaStatusQuestionario
    End Function


    Function EnviaEmailFinalizar(objQuestionario As MODEL.Questionario)
        Dim obj As New DAL.QuestionarioDAL
        Dim sSMTPeMail As String = ""
        Dim sNomeDestinatario As String = ""
        Dim seMailDestinatario As String = ""
        Dim seMailRemetente As String = ""
        Dim sNomeRemetente As String = ""
        Dim sAssuntoEmail As String = ""

        Dim Mailmsg As New System.Net.Mail.MailMessage()
        Dim mSmtpCliente As New SmtpClient(sSMTPeMail)
        Dim sEmailDest As String = seMailDestinatario
        Dim Mensagem As MailMessage = New MailMessage()

        Try

            sSMTPeMail = "smtp.etikaconsultoria.com.br"

            seMailDestinatario = objQuestionario.representante.dc_email
            sNomeDestinatario = objQuestionario.representante.no_representante

            seMailRemetente = "etika@etikaconsultoria.com.br​"
            sNomeRemetente = "Etika Consultoria"
            sAssuntoEmail = "Questionario Finalizado!"

        Catch ex As Exception
            Dim erro As New Exception("Falha ao obter configurações de email.")
        End Try


        Mailmsg.From = New MailAddress(seMailRemetente, sNomeRemetente)

        Mailmsg.Subject = sAssuntoEmail
        Mailmsg.BodyEncoding = System.Text.Encoding.UTF8
        Mailmsg.IsBodyHtml = True

        Mailmsg.Body = "<HTML>"
        Mailmsg.Body += "</HTML>"

        Mailmsg.To.Add(New MailAddress(seMailDestinatario, sNomeDestinatario))

        Mailmsg.Attachments.Clear()

        Try
            Try
                mSmtpCliente.Send(Mailmsg)
            Catch ex As Exception
                EnviaEmailFinalizar = False
                Dim erro As New Exception("Houve um problema ao enviar seu email. Tente Novamente.")
                Throw erro
            End Try
            Mailmsg.Attachments.Dispose()
            Mailmsg.Dispose()
        Catch ex As Exception
            EnviaEmailFinalizar = False
            Dim erro As New Exception("Houve um problema ao enviar seu email. Tente Novamente.")
            Throw erro
        End Try
    End Function

End Class
