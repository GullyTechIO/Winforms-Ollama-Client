''' <summary>
''' Represents a single message entry in the conversation.
''' </summary>
Public Class MessageEntry
    ''' <summary>
    ''' Gets or sets the source of the message (e.g., "user" or "assistant").
    ''' </summary>
    Public Property Source As String

    ''' <summary>
    ''' Gets or sets the content of the message.
    ''' </summary>
    Public Property Message As String

    ''' <summary>
    ''' Initializes a new instance of the MessageEntry class.
    ''' </summary>
    ''' <param name="source">The source of the message.</param>
    ''' <param name="message">The content of the message.</param>
    Public Sub New(source As String, message As String)
        Me.Source = source
        Me.Message = message
    End Sub
End Class