Imports Newtonsoft.Json
Imports System.Net

Public Class CreateForm
    Private questionCount As Integer = 0
    Private questionControls As New List(Of Tuple(Of Label, TextBox, Button))()

    Private Sub CreateFormForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Save Form Button
        Dim btnSaveForm As New Button()
        btnSaveForm.Name = "btnSaveForm"
        btnSaveForm.Text = "Save Form"
        btnSaveForm.Top = 10
        btnSaveForm.Left = 10
        btnSaveForm.Width = 100
        AddHandler btnSaveForm.Click, AddressOf btnSaveForm_Click
        Me.Controls.Add(btnSaveForm)

        ' Back Button
        Dim btnBack As New Button()
        btnBack.Name = "btnBack"
        btnBack.Text = "Back"
        btnBack.Top = 10
        btnBack.Left = 300
        btnBack.Width = 100
        AddHandler btnBack.Click, AddressOf btnBack_Click
        Me.Controls.Add(btnBack)

        ' Form Name
        Dim lblFormName As New Label()
        lblFormName.Text = "Form Name:"
        lblFormName.Top = 40
        lblFormName.Left = 10
        Me.Controls.Add(lblFormName)

        Dim txtFormName As New TextBox()
        txtFormName.Name = "txtFormName"
        txtFormName.Top = 40
        txtFormName.Left = 200
        txtFormName.Width = 200
        Me.Controls.Add(txtFormName)

        ' Form Description
        Dim lblFormDesc As New Label()
        lblFormDesc.Text = "Form Description:"
        lblFormDesc.Top = 70
        lblFormDesc.Left = 10
        Me.Controls.Add(lblFormDesc)

        Dim txtFormDesc As New TextBox()
        txtFormDesc.Name = "txtFormDesc"
        txtFormDesc.Top = 70
        txtFormDesc.Left = 200
        txtFormDesc.Width = 200
        Me.Controls.Add(txtFormDesc)

        ' Add Question Button
        Dim btnAddQuestion As New Button()
        btnAddQuestion.Name = "btnAddQuestion"
        btnAddQuestion.Text = "Add Question"
        btnAddQuestion.Top = 100
        btnAddQuestion.Left = 10
        btnAddQuestion.Width = 100
        AddHandler btnAddQuestion.Click, AddressOf btnAddQuestion_Click
        Me.Controls.Add(btnAddQuestion)
    End Sub
    Private Sub btnAddQuestion_Click(sender As Object, e As EventArgs)
        AddQuestionControl()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        Me.Hide()
        MainForm.Show()
    End Sub

    Private Sub AddQuestionControl()
        questionCount += 1

        ' Create new label for the question
        Dim lblQuestion As New Label()
        lblQuestion.Text = "Question " & questionCount & ":"
        lblQuestion.Top = 130 + (30 * questionCount)
        lblQuestion.Left = 10
        lblQuestion.Name = "lblQuestion" & questionCount
        Me.Controls.Add(lblQuestion)

        ' Create new textbox for the question
        Dim txtQuestion As New TextBox()
        txtQuestion.Name = "txtQuestion" & questionCount
        txtQuestion.Top = lblQuestion.Top
        txtQuestion.Left = 200
        txtQuestion.Width = 200
        Me.Controls.Add(txtQuestion)

        ' Create new button to delete the question
        Dim btnDelete As New Button()
        btnDelete.Name = "btnDelete" & questionCount
        btnDelete.Text = "Delete"
        btnDelete.Top = lblQuestion.Top
        btnDelete.Left = 400
        btnDelete.Width = 50
        AddHandler btnDelete.Click, AddressOf btnDelete_Click
        Me.Controls.Add(btnDelete)

        ' Store the question label, textbox, and delete button in a list
        questionControls.Add(New Tuple(Of Label, TextBox, Button)(lblQuestion, txtQuestion, btnDelete))
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        ' Find the button that was clicked
        Dim btnDelete As Button = DirectCast(sender, Button)
        Dim questionControl = questionControls.FirstOrDefault(Function(q) q.Item3 Is btnDelete)

        If questionControl IsNot Nothing Then
            ' Remove the controls from the form
            Me.Controls.Remove(questionControl.Item1) ' Label
            Me.Controls.Remove(questionControl.Item2) ' TextBox
            Me.Controls.Remove(questionControl.Item3) ' Button

            ' Remove the control from the list
            questionControls.Remove(questionControl)
        End If

        ' Update question labels
        For i As Integer = 0 To questionControls.Count - 1
            questionControls(i).Item1.Text = "Question " & (i + 1) & ":"
            questionControls(i).Item1.Top = 130 + (30 * (i + 1))
            questionControls(i).Item2.Top = questionControls(i).Item1.Top
            questionControls(i).Item3.Top = questionControls(i).Item1.Top
        Next

        questionCount -= 1
    End Sub

    Private Sub btnSaveForm_Click(sender As Object, e As EventArgs)
        Dim formId As String = Guid.NewGuid().ToString()
        Dim formName As String = DirectCast(Me.Controls("txtFormName"), TextBox).Text
        Dim formDesc As String = DirectCast(Me.Controls("txtFormDesc"), TextBox).Text
        Dim questions As New List(Of String)()

        For Each questionControl In questionControls
            questions.Add(questionControl.Item2.Text) ' TextBox
        Next

        Dim formData As New With {
            .id = formId,
            .title = formName,
            .description = formDesc,
            .questions = questions
        }

        Dim json As String = JsonConvert.SerializeObject(formData)
        Using client As New WebClient()
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            client.UploadString("http://localhost:3000/create", "POST", json)
        End Using

        MsgBox("Form saved successfully!")
        Me.Hide()
        MainForm.Show()
    End Sub
End Class
