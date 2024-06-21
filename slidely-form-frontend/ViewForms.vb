Imports Newtonsoft.Json
Imports System.Net

Public Class ViewForms
    Private forms As List(Of FormData)

    Private Sub ViewForms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Back Button
        Dim btnBack As New Button()
        btnBack.Name = "btnBack"
        btnBack.Text = "Back"
        btnBack.Top = 10
        btnBack.Left = 10
        btnBack.Width = 100
        AddHandler btnBack.Click, AddressOf btnBack_Click
        Me.Controls.Add(btnBack)

        ' ListBox to display forms
        Dim lstForms As New ListBox()
        lstForms.Name = "lstForms"
        lstForms.Top = 40
        lstForms.Left = 10
        lstForms.Width = 300
        lstForms.Height = 200
        AddHandler lstForms.DoubleClick, AddressOf lstForms_DoubleClick
        Me.Controls.Add(lstForms)

        ' Fetch and display forms
        FetchForms()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        Me.Hide()
        MainForm.Show()
    End Sub

    Private Sub FetchForms()
        Using client As New WebClient()
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            Dim response As String = client.DownloadString("http://localhost:3000/forms")
            forms = JsonConvert.DeserializeObject(Of List(Of FormData))(response)

            Dim lstForms As ListBox = DirectCast(Me.Controls("lstForms"), ListBox)
            lstForms.Items.Clear()
            For Each form As FormData In forms
                lstForms.Items.Add(form.title & " - " & form.description)
            Next
        End Using
    End Sub

    Private Sub lstForms_DoubleClick(sender As Object, e As EventArgs)
        Dim lstForms As ListBox = DirectCast(sender, ListBox)
        If lstForms.SelectedIndex >= 0 Then
            Dim selectedForm As FormData = forms(lstForms.SelectedIndex)
            Dim viewForm As New ViewForm(selectedForm, Me)
            Me.Hide()
            viewForm.Show()
        End If
    End Sub
End Class

Public Class FormData
    Public Property id As String
    Public Property title As String
    Public Property description As String
    Public Property questions As List(Of String)
End Class
