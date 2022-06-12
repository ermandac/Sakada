Imports Twilio
Imports Twilio.Rest.Api.V2010.Account
Imports Twilio.Types
Module mdlSMS
    Public Sub SendSMS(bodyMessage As String, Number As String)
        Const accountSid = "ACaa19abd331f22ef1eacb043d074064d4"
        Const authToken = "fd62063439ddcba773b8f8c27c190717"
        TwilioClient.Init(accountSid, authToken)
        Dim toNumber As New PhoneNumber(Number)
        Dim fromNumber As New PhoneNumber("+12163694666")
        Dim message = MessageResource.Create(toNumber, from:=fromNumber, body:=bodyMessage)
        Console.WriteLine(message.Sid)
    End Sub
End Module
