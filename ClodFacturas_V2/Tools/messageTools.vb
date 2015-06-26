Imports Microsoft.VisualBasic

Public Class messageTools
    Private cont As Control
    Private outString As String

    Sub New(cont As Control)
        Me.cont = cont
    End Sub

    Sub setMessage(message As String, sticky As Boolean)
        Dim mess = "$.jGrowl('" & message & "', { sticky: " & sticky.ToString.ToLower & " });"
        ScriptManager.RegisterClientScriptBlock(cont, cont.GetType(), "alertScript", "$(function() {" & mess & "});", True)
    End Sub

    Sub setError(message As String, sticky As Boolean)
        Dim mess = "$.jGrowl('" & message & "', { sticky: " & sticky.ToString.ToLower & ", theme: 'manilla',header: 'Error'});"
        ScriptManager.RegisterClientScriptBlock(cont, cont.GetType(), "alertScript", "$(function() {" & mess & "});", True)
    End Sub

    Sub setError(message As String, life As Integer)
        Dim mess = "$.jGrowl('" & message & "', { life: " & life & ", theme: 'manilla',header: 'Error'});"
        ScriptManager.RegisterClientScriptBlock(cont, cont.GetType(), "alertScript", "$(function() {" & mess & "});", True)
    End Sub

    Sub setMessage(message As String, life As Integer)
        Dim mess = "$.jGrowl('" & message & "', { life: " & life & " });"
        ScriptManager.RegisterClientScriptBlock(cont, cont.GetType(), "alertScript", "$(function() {" & mess & "});", True)
    End Sub

    Sub setMultipleMessage(message As String, life As Integer)
        outString &= "$.jGrowl('" & message & "', { life: " & life & " });"
    End Sub

    Sub sendMultipleMessage()
        ScriptManager.RegisterClientScriptBlock(cont, cont.GetType(), "alertScript", outString, True)
    End Sub

End Class

