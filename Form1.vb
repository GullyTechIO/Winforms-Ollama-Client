Imports System.Text
Imports System.Net.Http
Imports System.Threading
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.Security.Policy

Public Class Form1
    ''' <summary>
    ''' Dictionary to store conversation messages with timestamps.
    ''' </summary>
    Dim messages As Dictionary(Of DateTime, MessageEntry) = New Dictionary(Of DateTime, MessageEntry)

    ''' <summary>
    ''' Initializes the form and starts the Ollama connection check.
    ''' </summary>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Allow threaded subroutines to interact with UI objects
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

        ' Enable smooth automatic scrolling when the assistant is streaming a response
        rtbConversation.HideSelection = False

        ' Start a new thread to check if Ollama is online
        Dim evaluator As New Thread(Sub() Me.CheckOllamaOnline())
        evaluator.Start()

    End Sub

    ''' <summary>
    ''' Checks if Ollama is running and retrieves available models.
    ''' </summary>
    Public Sub CheckOllamaOnline()
        Try
            rtbConversation.AppendText("> Checking for Ollama at http://localhost:11434/ ..." & vbCrLf)

            ' Connect to Ollama server
            Dim url As String = "http://localhost:11434/"
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = client.GetAsync(url).Result
            rtbConversation.AppendText("      " & response.Content.ReadAsStringAsync().Result & vbCrLf)

            ' Retrieve list of models into cmbModels
            rtbConversation.AppendText("> Retrieving Models ..." & vbCrLf)
            If FillModelList() Then
                ' That succeeded, so enable the form elements
                rtbConversation.AppendText("> Ready to begin conversation." & vbCrLf & vbCrLf)
                btnSendRequest.Enabled = True
                txtNewInput.Enabled = True
            End If
        Catch ex As Exception
            rtbConversation.SelectionColor = Color.Red
            rtbConversation.AppendText("# Failed to detect Ollama at http://localhost:11434/: " & ex.Message & vbCrLf & vbCrLf)
            txtNewInput.Enabled = False
        End Try
    End Sub

    ''' <summary>
    ''' Retrieves available models from Ollama and populates the model list.
    ''' </summary>
    ''' <returns>True if models are successfully retrieved, False otherwise.</returns>
    Private Function FillModelList()
        Dim url As String = "http://localhost:11434/api/tags"
        Dim client As New HttpClient()
        Dim response As HttpResponseMessage = client.GetAsync(url).Result


        If response.IsSuccessStatusCode Then
            Dim json As String = response.Content.ReadAsStringAsync().Result

            Dim responseObject As JObject = JObject.Parse(json)
            Dim models = responseObject("models")

            For Each modelName In models
                cmbModels.Items.Add(modelName("name"))
            Next

            If models.Count > 0 Then
                rtbConversation.AppendText("      Retrieved " & models.Count & vbCrLf)
                cmbModels.SelectedIndex = 0
                Return True
            Else
                rtbConversation.AppendText("      No models are available" & vbCrLf)
                Return False
            End If

        Else
                rtbConversation.SelectionColor = Color.Red
            rtbConversation.AppendText("# Failed to retrieve models. Status code: " & response.StatusCode & vbCrLf)
            Return False
        End If
    End Function

    ''' <summary>
    ''' Adds a new message to the conversation history.
    ''' </summary>
    ''' <param name="source">The source of the message (e.g., "user" or "assistant").</param>
    ''' <param name="message">The content of the message.</param>
    Private Sub AddMessage(source As String, message As String)
        Dim currentTime As DateTime = DateTime.Now
        Dim newMessage As New MessageEntry(source, message)

        messages.Add(currentTime, newMessage)
    End Sub

    ''' <summary>
    ''' Sends the conversation history to the AI and processes the response.
    ''' </summary>
    Public Async Sub SendToAI()
        Dim client As New HttpClient()
        Dim tosendmessages = New List(Of Object)
        Dim entiremessage As String = ""

        btnSendRequest.Enabled = False

        ' Set the richtextbox alignment and color for assistant's response
        rtbConversation.SelectionColor = Color.Blue
        rtbConversation.SelectionAlignment = HorizontalAlignment.Left
        rtbConversation.SelectedText = "Assistant [" & DateTime.Now.ToShortTimeString & "]: " & vbCrLf

        ' Construct the request by adding each entry from the 'messages' list
        ' Optionally uncomment this line to send a System message:
        'tosendmessages.Add(New Dictionary(Of String, Object) From {{"role", "system"}, {"content", "You are a friendly and helpful AI assistant"}})

        For Each kvp As KeyValuePair(Of DateTime, MessageEntry) In messages
            tosendmessages.Add(New Dictionary(Of String, Object) From {
                {"role", kvp.Value.Source},
                {"content", kvp.Value.Message}
            })
        Next

        ' Set options for the API request
        Dim data As New Dictionary(Of String, Object) From {
            {"model", cmbModels.Text},
            {"stream", True},
            {"messages", tosendmessages}
        }

        ' Convert request data to JSON
        Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented)
        Dim content As HttpContent = New StringContent(json, Encoding.UTF8, "application/json")

        ' Send request to Ollama API
        Dim url = "http://localhost:11434/api/chat"
        Dim request As New HttpRequestMessage(HttpMethod.Post, url) With {
            .Content = content
        }

        ' Process the streaming response
        Dim response = Await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
        Dim stream = Await response.Content.ReadAsStreamAsync()

        Using reader As New IO.StreamReader(stream)
            Dim line As String = Await reader.ReadLineAsync()

            ' Process streaming response
            While line IsNot Nothing
                Try
                    Dim jsonResponse As JObject = JObject.Parse(line)
                    If jsonResponse.SelectToken("message") IsNot Nothing Then
                        Dim newtoken As String = jsonResponse.SelectToken("message").SelectToken("content").ToString()
                        rtbConversation.SelectionColor = Color.Blue
                        rtbConversation.AppendText(newtoken) ' Add to the richtextbox
                        entiremessage.Append(newtoken)
                    End If
                Catch ex As Exception
                    ' Handle JSON parsing error if occurs
                    rtbConversation.SelectionColor = Color.Red
                    rtbConversation.AppendText("JSON Parsing Error")
                End Try

                line = Await reader.ReadLineAsync()
            End While
        End Using

        rtbConversation.AppendText(vbCrLf & vbCrLf)
        AddMessage("assistant", entiremessage) ' add the final incoming message to the global message list

        btnSendRequest.Enabled = True
    End Sub

    ''' <summary>
    ''' Handles the click event of the send button.
    ''' </summary>
    Private Sub btnSendRequest_Click(sender As Object, e As EventArgs) Handles btnSendRequest.Click
        SendUserMessage()
    End Sub

    ''' <summary>
    ''' Handles the key up event of the input text box.
    ''' Sends the message if Enter key is pressed.
    ''' </summary>
    Private Sub txtNewInput_KeyUp(sender As Object, e As KeyEventArgs) Handles txtNewInput.KeyUp
        If e.KeyData = Keys.Enter Then
            e.Handled = True
            SendUserMessage()
            txtNewInput.Clear()
        End If
    End Sub

    ''' <summary>
    ''' Sends the user's message to the AI and updates the conversation display.
    ''' </summary>
    Public Sub SendUserMessage()
        If btnSendRequest.Enabled = True And txtNewInput.Text.Length > 0 Then
            rtbConversation.SelectionColor = Color.Green
            rtbConversation.SelectionAlignment = HorizontalAlignment.Right
            rtbConversation.SelectedText = "User [" & DateTime.Now.ToShortTimeString & "]: " & vbCrLf & txtNewInput.Text & vbCrLf & vbCrLf

            ' Add message to conversation history
            AddMessage("user", txtNewInput.Text)

            txtNewInput.Clear()

            ' Start a new thread to send the message to AI
            Dim evaluator As New Thread(Sub() SendToAI())
            evaluator.Start()
        End If
    End Sub

    ''' <summary>
    ''' Clears the conversation history and display.
    ''' </summary>
    Private Sub tsbClearConversation_Click(sender As Object, e As EventArgs) Handles tsbClearConversation.Click
        messages = New Dictionary(Of DateTime, MessageEntry)
        rtbConversation.Clear()
    End Sub

End Class
