<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CloseButton = New System.Windows.Forms.Label()
        Me.MinButton = New System.Windows.Forms.Label()
        Me.CreateJobListButton = New System.Windows.Forms.Label()
        Me.LogoLabel = New System.Windows.Forms.Label()
        Me.CommandPanel = New System.Windows.Forms.Label()
        Me.MemoryManagerPanel = New System.Windows.Forms.Label()
        Me.ExecuteButton = New System.Windows.Forms.Label()
        Me.SettingButton = New System.Windows.Forms.Label()
        Me.MemoryBackUpPanel = New System.Windows.Forms.Label()
        Me.LogLabel = New 存储管理系统可视化.LeonTextBox()
        Me.SuspendLayout()
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.Color.Transparent
        Me.CloseButton.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CloseButton.Image = Global.存储管理系统可视化.My.Resources.UnityResource.Close_0
        Me.CloseButton.Location = New System.Drawing.Point(758, 0)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(39, 21)
        Me.CloseButton.TabIndex = 1
        Me.CloseButton.Tag = "Close"
        '
        'MinButton
        '
        Me.MinButton.BackColor = System.Drawing.Color.Transparent
        Me.MinButton.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MinButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MinButton.Image = Global.存储管理系统可视化.My.Resources.UnityResource.Min_0
        Me.MinButton.Location = New System.Drawing.Point(725, 0)
        Me.MinButton.Name = "MinButton"
        Me.MinButton.Size = New System.Drawing.Size(32, 21)
        Me.MinButton.TabIndex = 0
        Me.MinButton.Tag = "Min"
        '
        'CreateJobListButton
        '
        Me.CreateJobListButton.BackColor = System.Drawing.Color.Transparent
        Me.CreateJobListButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CreateJobListButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CreateJobListButton.ForeColor = System.Drawing.Color.White
        Me.CreateJobListButton.Image = Global.存储管理系统可视化.My.Resources.UnityResource.CreateJobList
        Me.CreateJobListButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateJobListButton.Location = New System.Drawing.Point(15, 35)
        Me.CreateJobListButton.Name = "CreateJobListButton"
        Me.CreateJobListButton.Size = New System.Drawing.Size(100, 37)
        Me.CreateJobListButton.TabIndex = 2
        Me.CreateJobListButton.Tag = ""
        Me.CreateJobListButton.Text = "重置    "
        Me.CreateJobListButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LogoLabel
        '
        Me.LogoLabel.AutoSize = True
        Me.LogoLabel.BackColor = System.Drawing.Color.Transparent
        Me.LogoLabel.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LogoLabel.ForeColor = System.Drawing.Color.Yellow
        Me.LogoLabel.Location = New System.Drawing.Point(15, 5)
        Me.LogoLabel.Name = "LogoLabel"
        Me.LogoLabel.Size = New System.Drawing.Size(154, 21)
        Me.LogoLabel.TabIndex = 5
        Me.LogoLabel.Text = "存储管理系统可视化"
        '
        'CommandPanel
        '
        Me.CommandPanel.BackColor = System.Drawing.Color.Transparent
        Me.CommandPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CommandPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CommandPanel.ForeColor = System.Drawing.Color.Transparent
        Me.CommandPanel.Location = New System.Drawing.Point(30, 84)
        Me.CommandPanel.Name = "CommandPanel"
        Me.CommandPanel.Size = New System.Drawing.Size(176, 302)
        Me.CommandPanel.TabIndex = 6
        Me.CommandPanel.Text = "命令区："
        Me.CommandPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'MemoryManagerPanel
        '
        Me.MemoryManagerPanel.BackColor = System.Drawing.Color.Transparent
        Me.MemoryManagerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MemoryManagerPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MemoryManagerPanel.ForeColor = System.Drawing.Color.Transparent
        Me.MemoryManagerPanel.Location = New System.Drawing.Point(406, 84)
        Me.MemoryManagerPanel.Name = "MemoryManagerPanel"
        Me.MemoryManagerPanel.Size = New System.Drawing.Size(188, 302)
        Me.MemoryManagerPanel.TabIndex = 7
        Me.MemoryManagerPanel.Text = "图形化存储管理区："
        Me.MemoryManagerPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ExecuteButton
        '
        Me.ExecuteButton.BackColor = System.Drawing.Color.Transparent
        Me.ExecuteButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ExecuteButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ExecuteButton.ForeColor = System.Drawing.Color.White
        Me.ExecuteButton.Image = Global.存储管理系统可视化.My.Resources.UnityResource.Execute
        Me.ExecuteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExecuteButton.Location = New System.Drawing.Point(121, 35)
        Me.ExecuteButton.Name = "ExecuteButton"
        Me.ExecuteButton.Size = New System.Drawing.Size(100, 37)
        Me.ExecuteButton.TabIndex = 8
        Me.ExecuteButton.Tag = ""
        Me.ExecuteButton.Text = "执行   "
        Me.ExecuteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SettingButton
        '
        Me.SettingButton.BackColor = System.Drawing.Color.Transparent
        Me.SettingButton.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SettingButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.SettingButton.Image = Global.存储管理系统可视化.My.Resources.UnityResource.Setting_0
        Me.SettingButton.Location = New System.Drawing.Point(695, 0)
        Me.SettingButton.Name = "SettingButton"
        Me.SettingButton.Size = New System.Drawing.Size(32, 21)
        Me.SettingButton.TabIndex = 20
        Me.SettingButton.Tag = "Setting"
        '
        'MemoryBackUpPanel
        '
        Me.MemoryBackUpPanel.BackColor = System.Drawing.Color.Transparent
        Me.MemoryBackUpPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MemoryBackUpPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MemoryBackUpPanel.ForeColor = System.Drawing.Color.Transparent
        Me.MemoryBackUpPanel.Location = New System.Drawing.Point(212, 84)
        Me.MemoryBackUpPanel.Name = "MemoryBackUpPanel"
        Me.MemoryBackUpPanel.Size = New System.Drawing.Size(188, 302)
        Me.MemoryBackUpPanel.TabIndex = 21
        Me.MemoryBackUpPanel.Text = "备份对比区："
        Me.MemoryBackUpPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LogLabel
        '
        Me.LogLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LogLabel.Location = New System.Drawing.Point(600, 84)
        Me.LogLabel.Name = "LogLabel"
        Me.LogLabel.Size = New System.Drawing.Size(176, 302)
        Me.LogLabel.TabIndex = 22
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.存储管理系统可视化.My.Resources.UnityResource.BGI
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(799, 399)
        Me.Controls.Add(Me.LogLabel)
        Me.Controls.Add(Me.MemoryBackUpPanel)
        Me.Controls.Add(Me.SettingButton)
        Me.Controls.Add(Me.ExecuteButton)
        Me.Controls.Add(Me.MemoryManagerPanel)
        Me.Controls.Add(Me.LogoLabel)
        Me.Controls.Add(Me.CreateJobListButton)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.MinButton)
        Me.Controls.Add(Me.CommandPanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "操作系统调度算法可视化"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MinButton As Label
    Friend WithEvents CloseButton As Label
    Friend WithEvents CreateJobListButton As Label
    Friend WithEvents LogoLabel As Label
    Friend WithEvents CommandPanel As Label
    Friend WithEvents MemoryManagerPanel As Label
    Friend WithEvents ExecuteButton As Label
    Friend WithEvents SettingButton As Label
    Friend WithEvents MemoryBackUpPanel As Label
    Friend WithEvents LogLabel As LeonTextBox
End Class
