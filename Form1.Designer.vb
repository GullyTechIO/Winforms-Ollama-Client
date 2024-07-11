<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        rtbConversation = New RichTextBox()
        txtNewInput = New TextBox()
        btnSendRequest = New Button()
        ToolStrip1 = New ToolStrip()
        tsbClearConversation = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        ToolStripLabel1 = New ToolStripLabel()
        cmbModels = New ToolStripComboBox()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' rtbConversation
        ' 
        rtbConversation.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        rtbConversation.Font = New Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point)
        rtbConversation.Location = New Point(12, 12)
        rtbConversation.Name = "rtbConversation"
        rtbConversation.ScrollBars = RichTextBoxScrollBars.ForcedVertical
        rtbConversation.Size = New Size(1075, 610)
        rtbConversation.TabIndex = 0
        rtbConversation.Text = ""
        ' 
        ' txtNewInput
        ' 
        txtNewInput.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtNewInput.Enabled = False
        txtNewInput.Location = New Point(12, 628)
        txtNewInput.Multiline = True
        txtNewInput.Name = "txtNewInput"
        txtNewInput.Size = New Size(976, 115)
        txtNewInput.TabIndex = 1
        txtNewInput.Text = "Why is the sky blue?"
        ' 
        ' btnSendRequest
        ' 
        btnSendRequest.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSendRequest.Enabled = False
        btnSendRequest.FlatStyle = FlatStyle.Flat
        btnSendRequest.Location = New Point(1010, 650)
        btnSendRequest.Name = "btnSendRequest"
        btnSendRequest.Size = New Size(77, 76)
        btnSendRequest.TabIndex = 3
        btnSendRequest.Text = "Send"
        btnSendRequest.UseVisualStyleBackColor = True
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Dock = DockStyle.Bottom
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {tsbClearConversation, ToolStripSeparator1, ToolStripLabel1, cmbModels})
        ToolStrip1.Location = New Point(0, 754)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1099, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' tsbClearConversation
        ' 
        tsbClearConversation.DisplayStyle = ToolStripItemDisplayStyle.Text
        tsbClearConversation.Image = CType(resources.GetObject("tsbClearConversation.Image"), Image)
        tsbClearConversation.ImageTransparentColor = Color.Magenta
        tsbClearConversation.Name = "tsbClearConversation"
        tsbClearConversation.Size = New Size(55, 29)
        tsbClearConversation.Text = "Clear"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' ToolStripLabel1
        ' 
        ToolStripLabel1.Name = "ToolStripLabel1"
        ToolStripLabel1.Size = New Size(75, 29)
        ToolStripLabel1.Text = "Models:"
        ' 
        ' cmbModels
        ' 
        cmbModels.DropDownStyle = ComboBoxStyle.DropDownList
        cmbModels.Name = "cmbModels"
        cmbModels.Size = New Size(250, 34)
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1099, 788)
        Controls.Add(ToolStrip1)
        Controls.Add(btnSendRequest)
        Controls.Add(txtNewInput)
        Controls.Add(rtbConversation)
        MinimumSize = New Size(677, 435)
        Name = "Form1"
        Text = "Winforms Ollama Client"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents rtbConversation As RichTextBox
    Friend WithEvents txtNewInput As TextBox
    Friend WithEvents btnSendRequest As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbClearConversation As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents cmbModels As ToolStripComboBox

End Class
