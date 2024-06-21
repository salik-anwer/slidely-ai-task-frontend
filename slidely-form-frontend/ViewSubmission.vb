' Define the ViewSubmission form
Imports Newtonsoft.Json
Imports System.Net

Public Class ViewSubmission
    Private submission As SubmissionData
    Private formData As FormData

    Public Sub New(submissionData As SubmissionData, parent As Form)
        InitializeComponent()
        submission = submissionData
    End Sub

    Private Sub ViewSubmission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Back Button
        Dim btnBack As New Button()
        btnBack.Name = "btnBack"
        btnBack.Text = "Back"
        btnBack.Top = 10
        btnBack.Left = 10
        btnBack.Width = 100
        AddHandler btnBack.Click, AddressOf btnBack_Click
        Me.Controls.Add(btnBack)

        ' Fetch form details
        FetchFormDetails()
    End Sub

    Private Sub FetchFormDetails()
        Using client As New WebClient()
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            Dim response As String = client.DownloadString("http://localhost:3000/form/" & submission.formId)
            formData = JsonConvert.DeserializeObject(Of FormData)(response)

            ' Display form details
            DisplayFormDetails()
        End Using
    End Sub

    Private Sub DisplayFormDetails()
        ' Form Name
        Dim lblFormName As New Label()
        lblFormName.Text = "Form Name: " & formData.title
        lblFormName.Top = 40
        lblFormName.Left = 10
        Me.Controls.Add(lblFormName)

        ' Form Description
        Dim lblFormDesc As New Label()
        lblFormDesc.Text = "Form Description: " & formData.description
        lblFormDesc.Top = 70
        lblFormDesc.Left = 10
        lblFormDesc.Width = 300
        Me.Controls.Add(lblFormDesc)

        ' Display questions and answers
        Dim topOffset As Integer = 100
        For Each response As Response In submission.responses
            Dim lblQuestion As New Label()
            lblQuestion.Text = response.question
            lblQuestion.Top = topOffset
            lblQuestion.Left = 10
            Me.Controls.Add(lblQuestion)

            Dim lblAnswer As New Label()
            lblAnswer.Text = response.answer
            lblAnswer.Top = topOffset
            lblAnswer.Left = 200
            Me.Controls.Add(lblAnswer)

            topOffset += 30
        Next
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        Me.Hide()
        ViewSubmissions.Show()
    End Sub
End Class


