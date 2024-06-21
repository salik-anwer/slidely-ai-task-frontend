Imports Newtonsoft.Json
Imports System.Net

Public Class ViewForm
    Private listForms As ViewForms
    Private formData As FormData
    Private questionControls As New List(Of Tuple(Of Label, TextBox))()

    ' Constructor to accept the selected form data and the list forms instance
    Public Sub New(selectedFormData As FormData, listFormsInstance As ViewForms)
        InitializeComponent()
        formData = selectedFormData
        listForms = listFormsInstance

        ' Display Form Details
        LoadFormDetails()
    End Sub

    Private Sub ViewForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Back Button
        Dim btnBack As New Button()
        btnBack.Name = "btnBack"
        btnBack.Text = "Back"
        btnBack.Top = 10
        btnBack.Left = 10
        btnBack.Width = 100
        AddHandler btnBack.Click, AddressOf btnBack_Click
        Me.Controls.Add(btnBack)
    End Sub

    Private Sub LoadFormDetails()
        ' Form Name Label
        Dim lblFormName As New Label()
        lblFormName.Text = "Form Name: " & formData.title
        lblFormName.Top = 40
        lblFormName.Left = 10
        Me.Controls.Add(lblFormName)

        ' Form Description Label
        Dim lblFormDesc As New Label()
        lblFormDesc.Text = "Form Description: " & formData.description
        lblFormDesc.Top = 70
        lblFormDesc.Left = 10
        lblFormDesc.Width = 300
        Me.Controls.Add(lblFormDesc)

        ' Display Questions with TextBoxes for answers
        Dim questionTop As Integer = 100
        For Each question As String In formData.questions
            Dim lblQuestion As New Label()
            lblQuestion.Text = question
            lblQuestion.Top = questionTop
            lblQuestion.Left = 10
            lblQuestion.Width = 300
            Me.Controls.Add(lblQuestion)

            Dim txtAnswer As New TextBox()
            txtAnswer.Top = questionTop + 20
            txtAnswer.Left = 10
            txtAnswer.Width = 300
            Me.Controls.Add(txtAnswer)

            questionControls.Add(New Tuple(Of Label, TextBox)(lblQuestion, txtAnswer))
            questionTop += 50
        Next

        ' Submit Button
        Dim btnSubmit As New Button()
        btnSubmit.Name = "btnSubmit"
        btnSubmit.Text = "Submit"
        btnSubmit.Top = questionTop + 20
        btnSubmit.Left = 10
        btnSubmit.Width = 100
        AddHandler btnSubmit.Click, AddressOf btnSubmit_Click
        Me.Controls.Add(btnSubmit)
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim responses As New List(Of Response)()
        Dim subId As String = Guid.NewGuid().ToString()

        For Each questionControl In questionControls
            Dim response As New Response With {
            .question = questionControl.Item1.Text,
            .answer = questionControl.Item2.Text
        }
            responses.Add(response)
        Next

        Dim submissionData As New With {
        .formId = formData.id,
        .submissionId = subId,
        .responses = responses
    }

        Dim json As String = JsonConvert.SerializeObject(submissionData)

        Using client As New WebClient()
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            client.UploadString("http://localhost:3000/submit", "POST", json)
        End Using

        MsgBox("Form submitted successfully!")
        Me.Hide()
        ViewForms.Show()
    End Sub

    Public Class Response
        Public Property question As String
        Public Property answer As String
    End Class


    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        Me.Hide()
        ViewForms.Show()
    End Sub
End Class
