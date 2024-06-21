<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.createBtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.viewFormsBtn = New System.Windows.Forms.Button()
        Me.viewSubsBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'createBtn
        '
        Me.createBtn.Location = New System.Drawing.Point(58, 162)
        Me.createBtn.Name = "createBtn"
        Me.createBtn.Size = New System.Drawing.Size(218, 117)
        Me.createBtn.TabIndex = 0
        Me.createBtn.Text = "Create New Form"
        Me.createBtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!)
        Me.Label1.Location = New System.Drawing.Point(51, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(314, 39)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Form Windows App"
        '
        'viewFormsBtn
        '
        Me.viewFormsBtn.Location = New System.Drawing.Point(294, 162)
        Me.viewFormsBtn.Name = "viewFormsBtn"
        Me.viewFormsBtn.Size = New System.Drawing.Size(218, 117)
        Me.viewFormsBtn.TabIndex = 2
        Me.viewFormsBtn.Text = "View All Forms"
        Me.viewFormsBtn.UseVisualStyleBackColor = True
        '
        'viewSubsBtn
        '
        Me.viewSubsBtn.Location = New System.Drawing.Point(532, 162)
        Me.viewSubsBtn.Name = "viewSubsBtn"
        Me.viewSubsBtn.Size = New System.Drawing.Size(218, 117)
        Me.viewSubsBtn.TabIndex = 3
        Me.viewSubsBtn.Text = "View Submissions"
        Me.viewSubsBtn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.viewSubsBtn)
        Me.Controls.Add(Me.viewFormsBtn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.createBtn)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents createBtn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents viewFormsBtn As Button
    Friend WithEvents viewSubsBtn As Button
End Class
