Public Class Firmas
    Inherits System.Web.UI.Page
    Dim dbt As New ToolsT.DbToolsT
    Dim mensajes As New messageTools(Me)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MySecurity.checkValidSession(Me) = False Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType, "sendCont", "Cargar()", True)
        End If
        If Not IsPostBack Then
            consultarNombresF()
        End If
    End Sub

    Protected Sub btn_inicio_Click(sender As Object, e As ImageClickEventArgs) Handles btn_inicio.Click
        GuardarFirmas()
    End Sub

    Sub GuardarFirmas()
        Try
            dbt.ExecuteNonQuery("update firmas set nombre='" & txt_nombre_1.Text & "' where firm_id =1 ")
            dbt.ExecuteNonQuery("update firmas set nombre='" & txt_nombre_2.Text & "' where firm_id =2 ")
            dbt.ExecuteNonQuery("update firmas set nombre='" & txt_nombre_3.Text & "' where firm_id =3 ")
            dbt.ExecuteNonQuery("update firmas set app='" & txt_app_1.Text & "' where firm_id =1 ")
            dbt.ExecuteNonQuery("update firmas set app='" & txt_app_2.Text & "' where firm_id =2 ")
            dbt.ExecuteNonQuery("update firmas set app='" & txt_app_3.Text & "' where firm_id =3 ")
            dbt.ExecuteNonQuery("update firmas set apm='" & txt_apm_1.Text & "' where firm_id =1 ")
            dbt.ExecuteNonQuery("update firmas set apm='" & txt_apm_2.Text & "' where firm_id =2 ")
            dbt.ExecuteNonQuery("update firmas set apm='" & txt_apm_3.Text & "' where firm_id =3 ")
            dbt.ExecuteNonQuery("update firmas set puesto='" & txt_departamento_1.Text & "' where firm_id =1 ")
            dbt.ExecuteNonQuery("update firmas set puesto='" & txt_departamento_2.Text & "' where firm_id =2 ")
            dbt.ExecuteNonQuery("update firmas set puesto='" & txt_departamento_3.Text & "' where firm_id =3 ")
            mensajes.setMessage("Nombres de firmas modificados con exito.", 5000)
        Catch ex As Exception
        End Try
    End Sub

    Sub consultarNombresF()
        Try
            Dim ds As DataSet
            ds = dbt.GetDataSet("select * from  firmas ")
            txt_nombre_1.Text = ds.Tables(0).Rows(0).Item("Nombre")
            txt_nombre_2.Text = ds.Tables(0).Rows(1).Item("Nombre")
            txt_nombre_3.Text = ds.Tables(0).Rows(2).Item("Nombre")
            txt_app_1.Text = ds.Tables(0).Rows(0).Item("app")
            txt_app_2.Text = ds.Tables(0).Rows(1).Item("app")
            txt_app_3.Text = ds.Tables(0).Rows(2).Item("app")
            txt_apm_1.Text = ds.Tables(0).Rows(0).Item("apm")
            txt_apm_2.Text = ds.Tables(0).Rows(1).Item("apm")
            txt_apm_3.Text = ds.Tables(0).Rows(2).Item("apm")
            txt_departamento_1.Text = ds.Tables(0).Rows(0).Item("puesto")
            txt_departamento_2.Text = ds.Tables(0).Rows(1).Item("puesto")
            txt_departamento_3.Text = ds.Tables(0).Rows(2).Item("puesto")
        Catch ex As Exception
        End Try
    End Sub

End Class