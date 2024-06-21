Imports Newtonsoft.Json
Imports System.Net

Public Class ViewSubmissions
    Private submissions As List(Of SubmissionData)

    Private Sub ViewSubmissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Back Button
        Dim btnBack As New Button()
        btnBack.Name = "btnBack"
        btnBack.Text = "Back"
        btnBack.Top = 10
        btnBack.Left = 10
        btnBack.Width = 100
        AddHandler btnBack.Click, AddressOf btnBack_Click
        Me.Controls.Add(btnBack)

        ' ListBox to display submissions
        Dim lstSubmissions As New ListBox()
        lstSubmissions.Name = "lstSubmissions"
        lstSubmissions.Top = 40
        lstSubmissions.Left = 10
        lstSubmissions.Width = 300
        lstSubmissions.Height = 200
        AddHandler lstSubmissions.DoubleClick, AddressOf lstSubmissions_DoubleClick
        Me.Controls.Add(lstSubmissions)

        ' Fetch and display submissions
        FetchSubmissions()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        Me.Hide()
        MainForm.Show()
    End Sub

    Private Sub FetchSubmissions()
        Using client As New WebClient()
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            Dim response As String = client.DownloadString("http://localhost:3000/submissions")
            submissions = JsonConvert.DeserializeObject(Of List(Of SubmissionData))(response)

            Dim lstSubmissions As ListBox = DirectCast(Me.Controls("lstSubmissions"), ListBox)
            lstSubmissions.Items.Clear()
            For Each submission As SubmissionData In submissions
                lstSubmissions.Items.Add("Submission ID: " & submission.submissionId & " - Form ID: " & submission.formId)
            Next
        End Using
    End Sub

    Private Sub lstSubmissions_DoubleClick(sender As Object, e As EventArgs)
        Dim lstSubmissions As ListBox = DirectCast(sender, ListBox)
        If lstSubmissions.SelectedIndex >= 0 Then
            Dim selectedSubmission As SubmissionData = submissions(lstSubmissions.SelectedIndex)
            Dim viewSubmission As New ViewSubmission(selectedSubmission, Me)
            Me.Hide()
            viewSubmission.Show()
        End If
    End Sub
End Class

Public Class SubmissionData
    Public Property formId As String
    Public Property submissionId As String
    Public Property responses As List(Of Response)
End Class

Public Class Response
    Public Property question As String
    Public Property answer As String
End Class