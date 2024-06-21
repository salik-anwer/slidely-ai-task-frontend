Public Class MainForm
    Private Sub createBtn_Click(sender As Object, e As EventArgs) Handles createBtn.Click
        Me.Hide()
        CreateForm.Show()
    End Sub

    Private Sub viewFormsBtn_Click(sender As Object, e As EventArgs) Handles viewFormsBtn.Click
        Me.Hide()
        ViewForms.show()
    End Sub

    Private Sub viewSubsBtn_Click(sender As Object, e As EventArgs) Handles viewSubsBtn.Click
        Me.Hide()
        ViewSubmissions.Show()
    End Sub
End Class