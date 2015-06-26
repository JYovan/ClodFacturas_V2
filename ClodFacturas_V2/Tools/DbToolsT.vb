Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO


'SERGIO IRIGOYEN
Namespace ToolsT
    Public Class DbToolsT
        Private connection As SqlConnection
        Private trans As SqlTransaction

        Sub New()
            Connect()
        End Sub

        Private Function obtenerbasedatos() As String
            'Dim bdString As String = ""
            'Try
            '    Dim objReader As New StreamReader(Application.StartupPath & "\bdlocal.txt")
            '    Dim sLine As String = ""
            '    Dim arrText As New ArrayList()
            '    Do
            '        sLine = objReader.ReadLine()
            '        If Not sLine Is Nothing Then
            '            bdString = "Data Source=" & sLine & ";Initial Catalog=rezza_proyecto;user id=sa;password=;"
            '            'bdString = "Data Source=.\SQLEXPRESS2008;Initial Catalog=rezza_proyecto;user id=sa;password=12345;"
            '        End If
            '    Loop Until sLine Is Nothing
            '    objReader.Close()
            'Catch ex As Exception
            '    bdString = "Data Source=.\SQLEXPRESS2008;Initial Catalog=rezza_proyecto;user id=sa;password=12345;"
            'End Try
            'Return bdString
            Dim cadena As String = ""

            Try

                'If My.Settings.Item("autenticacion") = True Then
                'autenticion windows 
                'cadena = "Data Source=" & My.Settings.Item("Servidor").ToString.Trim & ";Initial Catalog=" & My.Settings.Item("BasedeDatos").ToString.Trim & ";Integrated Security=True"
                'Else
                'autenticacion SQL 
                cadena = "currentConn"
                'End If

            Catch
                cadena = ""
            End Try

            Return cadena
        End Function

        Public Sub Connect()
            Try
                Dim strConnString As String = ConfigurationManager.ConnectionStrings("currentConn").ConnectionString

                connection = New SqlConnection(strConnString)
                Dim command As New SqlCommand("set dateformat dmy", connection)
                connection.Open()
                command.ExecuteNonQuery()
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Sub Disconnect()
            Try
                connection.Close()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Rollback()
            trans.Rollback()
        End Sub

        Public Sub Commit()
            trans.Commit()
        End Sub

        Public Sub BeginTransaction()
            trans = connection.BeginTransaction()
        End Sub

        ''' <summary>
        ''' Ejecuta la consulta que se manda como parámetro dentro de la trasacción actual. Lanza una excepción cuando ocurre un error.
        ''' </summary>
        ''' <param name="query">La consulta</param>
        ''' <remarks></remarks>
        Public Sub NonQueryWithTransaction(query As [String])
            Dim command As New SqlCommand(query, connection)
            command.Transaction = trans
            Try
                command.ExecuteNonQuery()
            Catch exc As Exception
                Throw exc
            End Try
        End Sub
        Public Sub NonQueryWithTransaction(ByVal query As [String], ByVal ParamArray parametros As Object())
            Dim command As New SqlCommand(query, connection)
            command.Transaction = trans
            Try
                For i As Integer = 0 To parametros.Length - 1
                    command.Parameters.AddWithValue(i.ToString(), parametros(i))
                Next
                command.ExecuteNonQuery()
            Catch exc As Exception
                Throw exc
            End Try
        End Sub

        Public Function GetDataAdapterRpt(sqlQuery As String, strtabla As String) As SqlDataAdapter
            Dim dataadap As New SqlDataAdapter()
            Dim mycomand As New SqlCommand()
            mycomand.Connection = connection
            mycomand.CommandText = sqlQuery
            mycomand.CommandType = CommandType.Text
            dataadap.SelectCommand = mycomand
            Return dataadap
        End Function

        ''' <summary>
        ''' Obtiene el conjunto de resultados que devuelve la consulta especificada.
        ''' </summary>
        ''' <param name="sqlQuery">La consulta</param>
        ''' <returns>Un <see cref="Dataset">dataset</see> con los resultados.</returns>
        ''' <remarks></remarks>
        Public Function GetDataSet(sqlQuery As String) As DataSet
            Dim Adapter As SqlDataAdapter = Nothing
            Dim ds As New DataSet()
            Try
                Adapter = New SqlDataAdapter(sqlQuery, connection)
                Adapter.Fill(ds, "data")
                Return ds
            Catch exc As Exception
                Throw exc
            End Try
        End Function
        Public Function GetDataSet(ByVal sqlQuery As String, ByVal ParamArray parametros As Object()) As DataSet
            ''Dim Adapter As SqlDataAdapter = Nothing
            Dim ds As New DataSet()
            Dim selectCmd As New SqlCommand(sqlQuery, connection)
            Try
                Dim Adapter As New SqlDataAdapter(selectCmd.CommandText, connection)
                Adapter.SelectCommand = selectCmd
                For i As Integer = 0 To parametros.Length - 1
                    selectCmd.Parameters.AddWithValue(i.ToString(), parametros(i))
                Next
                Adapter.Fill(ds, "data")
                Return ds
            Catch exc As Exception
                Throw exc
            End Try
        End Function

        ''' <summary>
        ''' Obtiene el conjunto de resultados que devuelve la consulta especificada, dentro de una transacción. 
        ''' </summary>
        ''' <param name="sqlQuery">La consulta</param>
        ''' <returns>Un <see cref="Dataset">dataset</see> con los resultados.</returns>
        ''' <remarks></remarks>
        Public Function GetDataSetWithTransaction(sqlQuery As String) As DataSet
            Dim Adapter As SqlDataAdapter = Nothing
            Dim ds As New DataSet()
            Try
                Adapter = New SqlDataAdapter(sqlQuery, connection)
                Adapter.SelectCommand.Transaction = trans
                Adapter.Fill(ds, "data")
                Return ds
            Catch exc As Exception
                Throw exc
            End Try
        End Function
        Public Function GetDataSetWithTransaction(ByVal sqlQuery As String, ByVal ParamArray parametros As Object()) As DataSet
            ''Dim Adapter As SqlDataAdapter = Nothing
            Dim ds As New DataSet()
            Dim selectCmd As New SqlCommand(sqlQuery, connection)
            Try
                Dim Adapter As New SqlDataAdapter(selectCmd.CommandText, connection)
                Adapter.SelectCommand = selectCmd
                Adapter.SelectCommand.Transaction = trans

                For i As Integer = 0 To parametros.Length - 1
                    selectCmd.Parameters.AddWithValue(i.ToString(), parametros(i))
                Next
                Adapter.Fill(ds, "data")
                Return ds
            Catch exc As Exception
                Throw exc
            End Try
        End Function

        ''' <summary>
        ''' Obtiene el valor de la última columna de identidad insertada en la transacción actual.
        ''' </summary>
        ''' <returns>El valor de la identidad</returns>
        ''' <remarks></remarks>
        Public Function ScopeIdentityWithTransaction() As String
            Dim Adapter As SqlDataAdapter = Nothing
            Dim ds As New DataSet()
            Dim result As Long = 0
            Try
                Adapter = New SqlDataAdapter("select SCOPE_IDENTITY() id", connection)
                Adapter.SelectCommand.Transaction = trans
                Adapter.Fill(ds, "data")
                result = ds.Tables(0).Rows(0)("id")
            Catch exc As Exception
                Throw exc
            End Try
            Return result
        End Function

        'Public Function GetDataSet(sqlQuery As String, whatever As String) As DataSet
        '    Return GetDataSet(sqlQuery)
        'End Function

        ''' <summary>
        ''' Ejecuta la consulta que se manda como parámetro. Lanza una excepción cuando ocurre un error.
        ''' </summary>
        ''' <param name="sqlquery">La consulta</param>
        ''' <remarks></remarks>
        Public Sub ExecuteNonQuery(sqlquery As String)
            Dim command As New SqlCommand(sqlquery, connection)
            Try
                command.ExecuteNonQuery()
            Catch exc As Exception


                Throw exc
            End Try
        End Sub
        Public Sub ExecuteNonQuery(ByVal sqlquery As String, ByVal ParamArray parametros As Object())
            Dim command As New SqlCommand(sqlquery, connection)
            Try
                For i As Integer = 0 To parametros.Length - 1
                    command.Parameters.AddWithValue(i.ToString(), parametros(i))
                Next
                command.ExecuteNonQuery()
            Catch exc As Exception
                Throw exc
            End Try
        End Sub


       

        Function NumeroEnLetras(pnumero) As String
            Dim xnumero As String
            Dim numero_ent As String
            Dim entero As String
            Dim temp As String
            Dim xnum As String
            Dim digito As String
            Dim letras As String = ""
            Dim num As String
            Dim i As Long
            Dim j As Long
            Dim c As Long
            Dim xcen(9) 'centenas
            Dim xdec(9) 'decenas
            Dim xuni(9) 'unidades
            Dim xexc(6) 'except
            Dim ceros(9)
            Try

                xcen(2) = "dosc"
                xcen(3) = "tresc"
                xcen(4) = "cuatroc"
                xcen(5) = "quin"
                xcen(6) = "seisc"
                xcen(7) = "setec"
                xcen(8) = "ochoc"
                xcen(9) = "novec"
                xdec(2) = "veinti"
                xdec(3) = "trei"
                xdec(4) = "cuare"
                xdec(5) = "cincue"
                xdec(6) = "sese"
                xdec(7) = "sete"
                xdec(8) = "oche"
                xdec(9) = "nove"
                xuni(1) = "un"
                xuni(2) = "dos"
                xuni(3) = "tres"
                xuni(4) = "cuatro"
                xuni(5) = "cinco"
                xuni(6) = "seis"
                xuni(7) = "siete"
                xuni(8) = "ocho"
                xuni(9) = "nueve"
                xexc(1) = "diez"
                xexc(2) = "once"
                xexc(3) = "doce"
                xexc(4) = "trece"
                xexc(5) = "catorce"
                xexc(6) = "quince"
                ceros(1) = "0"
                ceros(2) = "00"
                ceros(3) = "000"
                ceros(4) = "0000"
                ceros(5) = "00000"
                ceros(6) = "000000"
                ceros(7) = "0000000"
                ceros(8) = "00000000"

                c = 1
                i = 1
                j = 0

                xnumero = CStr(pnumero)
                If CDbl(LTrim(RTrim(pnumero))) < 999999999.99000001 Then
                    numero_ent = CDbl(Int(pnumero))
                    If Len(numero_ent) < 9 Then
                        numero_ent = ceros(9 - Len(numero_ent)) & numero_ent
                    End If
                    entero = CDbl(Int(numero_ent))

                    Do While i < 8
                        temp = 0
                        num = CDbl(Mid(numero_ent, i, 3))
                        xnum = Mid(numero_ent, i, 3)
                        digito = CDbl(Mid(xnum, 1, 1))

                        '/* analizo el numero entero de a 3 */
                        If xnum = "000" Then
                            j = 0
                        Else
                            j = 1
                            If digito > 1 Then
                                letras = letras & xcen(digito) & "ientos "
                            End If
                            If Mid(xnum, 1, 1) = "1" And Mid(xnum, 2, 2) <> "00" Then
                                letras = letras & "ciento "
                            ElseIf Mid(xnum, 1, 1) = "1" Then
                                letras = letras & "cien "
                            End If

                            '/* analisis de las decenas */
                            digito = CDbl(Mid(xnum, 2, 1))
                            If digito > 2 And Mid(xnum, 3, 1) = "0" Then
                                letras = letras & xdec(digito) & "nta "
                                temp = 1
                            End If

                            If digito > 2 And Mid(xnum, 3, 1) <> "0" Then
                                letras = letras & xdec(digito) & "nta y "
                            End If

                            If digito = 2 And Mid(xnum, 3, 1) = "0" Then
                                letras = letras & "veinte "
                                temp = 1
                            ElseIf digito = 2 And Mid(xnum, 3, 1) <> "0" Then
                                letras = letras & "veinti"
                            End If

                            If digito = 1 And Mid(xnum, 3, 1) >= "6" Then
                                letras = letras & "dieci"
                            ElseIf digito = 1 And Mid(xnum, 3, 1) < "6" Then
                                letras = letras & xexc(CDbl(Mid(xnum, 3, 1) + 1))
                                temp = 1
                            End If
                        End If

                        If temp = 0 Then
                            '/* analisis del ultimo digito */
                            digito = CDbl(Mid(xnum, 3, 1))
                            If ((c = 1) Or (c = 2)) And xnum = "001" Then
                                letras = letras & "un"
                            Else
                                If ((c = 1) Or (c = 2)) And xnum >= "020" And Mid(xnum, 3, 1) = "1" Then
                                    letras = letras & "un"
                                Else
                                    If digito <> 0 Then
                                        letras = letras & xuni(digito)
                                    End If
                                End If
                            End If
                        End If

                        If j = 1 And i = 1 And xnum = "001" And c = 1 Then
                            letras = letras & " millon "
                        ElseIf j = 1 And i = 1 And xnum <> "001" And c = 1 Then
                            letras = letras & " millones "
                        ElseIf j = 1 And i = 4 And c = 2 Then
                            letras = letras & " mil "
                        End If
                        i = i + 3
                        c = c + 1
                    Loop
                    If letras = "" Then
                        letras = "cero "
                    End If
                    letras = (letras.Insert(0, "(") & " pesos " & Format(pnumero, "###0.00").Split(".")(1) & "/100 M.N.").ToUpper() & ")"
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return letras
        End Function
    End Class
End Namespace

