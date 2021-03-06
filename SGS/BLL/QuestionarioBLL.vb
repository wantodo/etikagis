﻿Imports System.Net.Mail

Public Class QuestionarioBLL

    Public Function ListaQuestionario(codEmpresa As Integer, codCategoria As Integer, codRepresentante As Integer, dtCompetencia As String) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.ListaQuestionario(codEmpresa, codCategoria, codRepresentante, dtCompetencia)
    End Function

    Public Sub InsereQuestionario(objQuestionario As MODEL.Questionario)
        Dim obj As New DAL.QuestionarioDAL

        obj.InsereQuestionario(objQuestionario)
    End Sub

    Public Sub ExcluiQuestionario(codQuestionario As Integer)
        Dim obj As New DAL.QuestionarioDAL

        obj.ExcluiQuestionario(codQuestionario)
    End Sub

    Public Function RetornaStatusQuestionario() As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaStatusQuestionario
    End Function

    Public Function RetornaPontoFocal(codEmpresa As Integer) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaPontoFocal(codEmpresa)
    End Function

    Public Sub AlteraQuestionario(codQuestionario As Integer, codStatus As Integer, dt_competencia As String, Optional ByVal codRepresentante As Integer = 0)
        Dim obj As New DAL.QuestionarioDAL

        obj.AlteraQuestionario(codQuestionario, codStatus, codRepresentante, dt_competencia)
    End Sub

    Function RetornaStatusQuestionario(parametros As Array) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaStatusQuestionario(parametros)
    End Function

    Function RetornaQuestionarioRepresentante(parametros As Array) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaQuestionarioRepresentante(parametros)
    End Function

    Public Function EnviaEmailQuestionadioLiberado(objQuestionario As MODEL.Questionario)
        Dim obj As New DAL.QuestionarioDAL
        Dim sSMTPeMail As String = ""
        Dim sNomeDestinatario As String = ""
        Dim seMailDestinatario As String = ""
        Dim seMailRemetente As String = ""
        Dim sNomeRemetente As String = ""
        Dim sAssuntoEmail As String = ""

        Try

            sSMTPeMail = "smtp.etikaconsultoria.com.br"
           
            seMailDestinatario = objQuestionario.representante.dc_email
            sNomeDestinatario = objQuestionario.representante.no_representante

            seMailRemetente = "etikaconsultoria@etikaconsultoria.com.br"
            sNomeRemetente = "Etika Consultoria"
            sAssuntoEmail = "Questionário Liberado!"

        Catch ex As Exception
            Dim erro As New Exception("Falha ao obter configurações de email.")

            EnviaEmailQuestionadioLiberado = False
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

        Mailmsg.CC.Add(New MailAddress("etikaconsultoria@etikaconsultoria.com.br", "Etika Consultoria"))

        Mailmsg.Attachments.Clear()

        mSmtpCliente.Credentials = New System.Net.NetworkCredential(seMailRemetente, "consult@1357")
        mSmtpCliente.Send(Mailmsg)
        Mailmsg.Attachments.Dispose()
        Mailmsg.Dispose()
        EnviaEmailQuestionadioLiberado = True

    End Function

    Function EnviaEmailQuestionarioRespondido(objQuestionario As MODEL.Questionario) As Boolean
        Dim obj As New DAL.QuestionarioDAL
        Dim sSMTPeMail As String = ""
        Dim sNomeDestinatario As String = ""
        Dim seMailDestinatario As String = ""
        Dim seMailRemetente As String = ""
        Dim sNomeRemetente As String = ""
        Dim sAssuntoEmail As String = ""

        Try

            sSMTPeMail = "smtp.etikaconsultoria.com.br"

            'seMailRemetente = objQuestionario.representante.dc_email
            seMailRemetente = "etikaconsultoria@etikaconsultoria.com.br"
            sNomeRemetente = objQuestionario.representante.no_representante

            seMailDestinatario = "etikaconsultoria@etikaconsultoria.com.br"
            sNomeDestinatario = "Etika Consultoria"
            sAssuntoEmail = "Questionário Respondido!"

        Catch ex As Exception
            Dim erro As New Exception("Falha ao obter configurações de email.")

            EnviaEmailQuestionarioRespondido = False
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
        Mailmsg.Body += "O representante " & objQuestionario.representante.dc_area & " respondeu seu questionário!"
        Mailmsg.Body += "</HTML>"

        Mailmsg.To.Add(New MailAddress(seMailDestinatario, sNomeDestinatario))

        Mailmsg.Attachments.Clear()

        mSmtpCliente.Credentials = New System.Net.NetworkCredential(seMailRemetente, "consult@1357")
        mSmtpCliente.Send(Mailmsg)
        Mailmsg.Attachments.Dispose()
        Mailmsg.Dispose()
        EnviaEmailQuestionarioRespondido = True
    End Function

    Public Function EnviaEmailAnaliseQuestao(objQuestionario As MODEL.Questionario)
        Dim obj As New DAL.QuestionarioDAL
        Dim sSMTPeMail As String = ""
        Dim sNomeDestinatario As String = ""
        Dim seMailDestinatario As String = ""
        Dim seMailRemetente As String = ""
        Dim sNomeRemetente As String = ""
        Dim sAssuntoEmail As String = ""

        Try

            sSMTPeMail = "smtp.etikaconsultoria.com.br"

            seMailDestinatario = objQuestionario.representante.dc_email
            sNomeDestinatario = objQuestionario.representante.no_representante

            seMailRemetente = "etikaconsultoria@etikaconsultoria.com.br"
            sNomeRemetente = "Etika Consultoria"
            sAssuntoEmail = "Questão devolvida!"

        Catch ex As Exception
            Dim erro As New Exception("Falha ao obter configurações de email.")

            EnviaEmailAnaliseQuestao = False
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
        Mailmsg.Body += "Questão devolvida, verificar as questões marcadas em amarelo.  " & objQuestionario.representante.dc_area
        Mailmsg.Body += "</HTML>"

        Mailmsg.To.Add(New MailAddress(seMailDestinatario, sNomeDestinatario))

        Mailmsg.CC.Add(New MailAddress("etikaconsultoria@etikaconsultoria.com.br", "Etika Consultoria"))

        Mailmsg.Attachments.Clear()

        mSmtpCliente.Credentials = New System.Net.NetworkCredential(seMailRemetente, "consult@1357")
        mSmtpCliente.Send(Mailmsg)
        Mailmsg.Attachments.Dispose()
        Mailmsg.Dispose()
        EnviaEmailAnaliseQuestao = True

    End Function

    Public Sub AlteraPrazo(cd_representante As Integer, Optional dt_prazo As Date = Nothing)
        Dim obj As New DAL.QuestionarioDAL

        obj.AlteraPrazo(cd_representante, dt_prazo)
    End Sub

    Function RetornaUltimaCompetencia(cd_representante As Integer) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaUltimaCompetencia(cd_representante)
    End Function
End Class


