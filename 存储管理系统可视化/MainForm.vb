Imports System.ComponentModel

Public Class MainForm
    Private Const Max_JobCount As Integer = 12 '必须与 JobColors 个数匹配
    Private Const ButtonAlphaDefault As Integer = 50
    Private Const Max_MemorySize As Integer = 1024
    Private Const ButtonAlphaActive As Integer = 100
    Private Const ButtonAlphaExecute As Integer = 150
    Dim ButtonColorDefault As Color = Color.Orange
    Dim ButtonColorActive As Color = Color.Aqua
    Dim ButtonColorExecute As Color = Color.Red
    Dim Buttons() As Label
    Dim SystemRandom As Random = New Random

#Region "窗体事件"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Buttons = New Label() {CreateJobListButton, ExecuteButton}
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)

        Me.Icon = My.Resources.UnityResource.SystemICON
        CloseButton.Left = Me.Width - CloseButton.Width
        MinButton.Left = CloseButton.Left - MinButton.Width
        SettingButton.Left = MinButton.Left - SettingButton.Width

        For Each InsButton As Label In Buttons
            InsButton.BackColor = Color.FromArgb(ButtonAlphaDefault, ButtonColorDefault)
            AddHandler InsButton.MouseEnter, AddressOf ColorButton_MouseEnter
            AddHandler InsButton.MouseDown, AddressOf ColorButton_MouseDown
            AddHandler InsButton.MouseUp, AddressOf ColorButton_MouseUp
            AddHandler InsButton.MouseLeave, AddressOf ColorButton_MouseLeave
        Next

        '设置容器控件的背景颜色
        CommandPanel.BackColor = Color.FromArgb(50, Color.White)
        MemoryBackUpPanel.BackColor = Color.FromArgb(50, Color.Gray)
        MemoryManagerPanel.BackColor = Color.FromArgb(50, Color.Gray)
        LogLabel.BackColor = Color.FromArgb(50, Color.White)

        '调整容器控件的位置和尺寸
        CommandPanel.Location = New Point(15, Buttons(0).Bottom + 10)
        CommandPanel.Size = New Size((Me.Width - 50) * 0.2, Me.Height - CommandPanel.Top - 15)
        MemoryBackUpPanel.Location = New Point(CommandPanel.Right + 10, CommandPanel.Top)
        MemoryBackUpPanel.Size = New Size((Me.Width - 50) * 0.3, CommandPanel.Height)
        MemoryManagerPanel.Location = New Point(MemoryBackUpPanel.Right + 10, CommandPanel.Top)
        MemoryManagerPanel.Size = New Size(MemoryBackUpPanel.Width, CommandPanel.Height)
        LogLabel.Location = New Point(MemoryManagerPanel.Right + 10, CommandPanel.Top)
        LogLabel.Size = New Size(CommandPanel.Width, CommandPanel.Height)
    End Sub

    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Me.Tag = True Then Exit Sub
        e.Cancel = True
        If MsgBox("就这样退出吗？真的不爱了？", MsgBoxStyle.OkCancel Or MsgBoxStyle.Question, "确定不留下么？") = MsgBoxResult.Ok Then Me.Tag = True : Application.Exit()
    End Sub

#End Region

#Region "标题栏按钮事件"

    Private Sub MinButton_Click(sender As Object, e As EventArgs) Handles MinButton.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        If MsgBox("就这样退出吗？真的不爱了？", MsgBoxStyle.OkCancel Or MsgBoxStyle.Question, "确定不留下么？") = MsgBoxResult.Ok Then Me.Tag = True : Application.Exit()
    End Sub

    Private Sub SettingButton_Click(sender As Object, e As EventArgs) Handles SettingButton.Click
        If Not AboutMe.Visible Then AboutMe.ShowDialog(Me)
    End Sub

#End Region

#Region "图像按钮动态效果"

    Private Sub ControlButton_MouseDown(sender As Label, e As MouseEventArgs) Handles MinButton.MouseDown, CloseButton.MouseDown, SettingButton.MouseDown
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_2")
    End Sub

    Private Sub ControlButton_MouseEnter(sender As Label, e As EventArgs) Handles MinButton.MouseEnter, CloseButton.MouseEnter, SettingButton.MouseEnter
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_1")
    End Sub

    Private Sub ControlButton_MouseLeave(sender As Label, e As EventArgs) Handles MinButton.MouseLeave, CloseButton.MouseLeave, SettingButton.MouseLeave
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_0")
    End Sub

    Private Sub ControlButton_MouseUp(sender As Label, e As MouseEventArgs) Handles MinButton.MouseUp, CloseButton.MouseUp, SettingButton.MouseUp
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_1")
    End Sub

#End Region

#Region "纯色按钮动态效果"

    Private Sub ColorButton_MouseDown(sender As Label, e As MouseEventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaExecute, ButtonColorExecute)
    End Sub

    Private Sub ColorButton_MouseEnter(sender As Label, e As EventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaActive, ButtonColorActive)
    End Sub

    Private Sub ColorButton_MouseLeave(sender As Label, e As EventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaDefault, ButtonColorDefault)
    End Sub

    Private Sub ColorButton_MouseUp(sender As Label, e As MouseEventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaActive, ButtonColorActive)
    End Sub

#End Region

#Region "纯色按钮事件"

    Private Sub CreateJobListButton_Click(sender As Object, e As EventArgs) Handles CreateJobListButton.Click
        GC.Collect()
    End Sub

    Private Sub PlayPauseButton_Click(sender As Object, e As EventArgs) Handles ExecuteButton.Click

    End Sub

#End Region

End Class
