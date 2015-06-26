Imports System.Net
Imports System.Web.Http


<Serializable()> _
Public Class Email
    Public Sub New()
        Me.Attachments = New List(Of Attachment)()
    End Sub
    Public Property MessageNumb() As Integer
        Get
            Return m_MessageNumber
        End Get
        Set(value As Integer)
            m_MessageNumber = value
        End Set
    End Property
    Private m_MessageNumber As Integer
    Public Property From() As String
        Get
            Return m_From
        End Get
        Set(value As String)
            m_From = value
        End Set
    End Property
    Private m_From As String
    Public Property Subject() As String
        Get
            Return m_Subject
        End Get
        Set(value As String)
            m_Subject = value
        End Set
    End Property
    Private m_Subject As String
    Public Property Body() As String
        Get
            Return m_Body
        End Get
        Set(value As String)
            m_Body = value
        End Set
    End Property
    Private m_Body As String
    Public Property DateSent() As DateTime
        Get
            Return m_DateSent
        End Get
        Set(value As DateTime)
            m_DateSent = value
        End Set
    End Property
    Private m_DateSent As DateTime
    Public Property Attachments() As List(Of Attachment)
        Get
            Return m_Attachments
        End Get
        Set(value As List(Of Attachment))
            m_Attachments = value
        End Set
    End Property
    Private m_Attachments As List(Of Attachment)
End Class

<Serializable()> _
Public Class Attachment
    Public Property FileName() As String
        Get
            Return m_FileName
        End Get
        Set(value As String)
            m_FileName = value
        End Set
    End Property
    Private m_FileName As String
    Public Property ContentType() As String
        Get
            Return m_ContentType
        End Get
        Set(value As String)
            m_ContentType = value
        End Set
    End Property
    Private m_ContentType As String
    Public Property Content() As Byte()
        Get
            Return m_Content
        End Get
        Set(value As Byte())
            m_Content = value
        End Set
    End Property
    Private m_Content As Byte()
End Class

