Imports System.ComponentModel

Public Class MainForm
    ''' <summary>
    ''' 内存区域大小
    ''' </summary>
    Private Const Max_MemorySize As Integer = 1024
    ''' <summary>
    ''' 纯色按钮默认透明度
    ''' </summary>
    Private Const ButtonAlphaDefault As Integer = 25
    ''' <summary>
    ''' 纯色按钮鼠标进入透明度
    ''' </summary>
    Private Const ButtonAlphaActive As Integer = 50
    ''' <summary>
    ''' 纯色按钮鼠标按下透明度
    ''' </summary>
    Private Const ButtonAlphaExecute As Integer = 75
    ''' <summary>
    ''' 纯色按钮默认颜色
    ''' </summary>
    Dim ButtonColorDefault As Color = Color.Orange
    ''' <summary>
    ''' 纯色按钮鼠标进入颜色
    ''' </summary>
    Dim ButtonColorActive As Color = Color.Aqua
    ''' <summary>
    ''' 纯色按钮鼠标按下颜色
    ''' </summary>
    Dim ButtonColorExecute As Color = Color.Red
    ''' <summary>
    ''' 纯色按钮组
    ''' </summary>
    Dim Buttons() As Label
    ''' <summary>
    ''' 内存区域首节点
    ''' </summary>
    Dim FirstMemoryNode As MemoryNodeClass
    ''' <summary>
    ''' 进程列表
    ''' </summary>
    Dim ProcessList As List(Of ProcessClass) = New List(Of ProcessClass)
    ''' <summary>
    ''' 图形化内存区域
    ''' </summary>
    Dim MemoryRectangle As Rectangle
    ''' <summary>
    ''' 图形化内存区域单位高度
    ''' </summary>
    Dim MemoryCellHeight As Double
    ''' <summary>
    ''' 空白内存区域
    ''' </summary>
    Dim FreeMemoryRectangle As Rectangle
    ''' <summary>
    ''' 空白内存区域单位内存对应的图像宽度
    ''' </summary>
    Dim FreeMemoryCellWidth As Double
    ''' <summary>
    ''' 下次创建的进程ID
    ''' </summary>
    Dim NextPID As Integer = 0
    ''' <summary>
    ''' 全局随机数发生器
    ''' </summary>
    Dim UnityRandom As Random = New Random
    ''' <summary>
    ''' 总空闲内存大小
    ''' </summary>
    Dim FreeMemorySize As Integer = Max_MemorySize
    ''' <summary>
    ''' 不分页下次空闲内存节点（依次对应：首次适应、循环首次适应、最佳适应、最坏适应）
    ''' </summary>
    Dim NextFreeMemoryNodes(3) As MemoryNodeClass

#Region "窗体事件"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Buttons = New Label() {ResetSystemButton, AddProcessButton, DisposeProcessButton, RelocateButton, SegmentPageButton}
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)

        Me.Icon = My.Resources.UnityResource.SystemICON
        CloseButton.Left = Me.Width - CloseButton.Width
        MinButton.Left = CloseButton.Left - MinButton.Width
        SettingButton.Left = MinButton.Left - SettingButton.Width

        '设置容器控件的背景颜色
        ControlPanel.BackColor = Color.FromArgb(50, Color.Gray)
        MemoryBackUpPanel.BackColor = Color.FromArgb(50, Color.Gray)
        MemoryManagerPanel.BackColor = Color.FromArgb(50, Color.Gray)
        LogLabel.BackColor = Color.FromArgb(50, Color.Gray)
        FreeMemorySortByAddressPanel.BackColor = Color.FromArgb(50, Color.Gray)
        FreeMemorySortBySizePanel.BackColor = Color.FromArgb(50, Color.Gray)
        LogLabel.BackColor = Color.FromArgb(50, Color.Gray)

        '调整容器控件的位置和尺寸
        ControlPanel.Location = New Point(15, LogoLabel.Bottom + 10)
        ControlPanel.Size = New Size((Me.Width - 50) * 0.2, (Me.Height - ControlPanel.Top - 15) * 0.8)
        MemoryBackUpPanel.Location = New Point(ControlPanel.Right + 10, ControlPanel.Top)
        MemoryBackUpPanel.Size = New Size((Me.Width - 50) * 0.3, ControlPanel.Height)
        MemoryManagerPanel.Location = New Point(MemoryBackUpPanel.Right + 10, ControlPanel.Top)
        MemoryManagerPanel.Size = New Size(MemoryBackUpPanel.Width, ControlPanel.Height)
        LogLabel.Location = New Point(MemoryManagerPanel.Right + 10, ControlPanel.Top)
        LogLabel.Size = New Size(ControlPanel.Width, ControlPanel.Height)
        FreeMemorySortByAddressPanel.Location = New Point(15, ControlPanel.Bottom + 10)
        FreeMemorySortByAddressPanel.Size = New Size(ControlPanel.Width + MemoryManagerPanel.Width + 10, (Me.Height - ControlPanel.Top - 25) * 0.2)
        FreeMemorySortBySizePanel.Location = New Point(FreeMemorySortByAddressPanel.Right + 10, FreeMemorySortByAddressPanel.Top)
        FreeMemorySortBySizePanel.Size = FreeMemorySortByAddressPanel.Size

        '初始化纯色按钮
        For Each InsButton As Label In Buttons
            InsButton.BackColor = Color.FromArgb(ButtonAlphaDefault, ButtonColorDefault)
            InsButton.Parent = ControlPanel
            InsButton.Width = ControlPanel.Width - 30
            AddHandler InsButton.MouseEnter, AddressOf ColorButton_MouseEnter
            AddHandler InsButton.MouseDown, AddressOf ColorButton_MouseDown
            AddHandler InsButton.MouseUp, AddressOf ColorButton_MouseUp
            AddHandler InsButton.MouseLeave, AddressOf ColorButton_MouseLeave
        Next
        AddProcessLabel.Parent = ControlPanel
        ProcessMemorySizeNumeric.Parent = ControlPanel
        ProcessMemorySizeNumeric.Width = ControlPanel.Width - 30

        DisposeProcessLabel.Parent = ControlPanel
        ProcessListComboBox.Parent = ControlPanel
        ProcessListComboBox.Width = ControlPanel.Width - 30

        SegmentPageLabel.Parent = ControlPanel
        SegmentPageNumeric.Parent = ControlPanel
        DispathComboBox.Parent = ControlPanel
        SegmentPageNumeric.Width = ControlPanel.Width - 30
        DispathComboBox.Width = ControlPanel.Width - 30

        DispathComboBox.SelectedIndex = 0
        SegmentPageNumeric.SelectedIndex = 2

        MemoryRectangle = New Rectangle(15, 35, MemoryManagerPanel.Width - 30, MemoryManagerPanel.Height - 50)
        MemoryCellHeight = MemoryRectangle.Height / Max_MemorySize
        FreeMemoryRectangle = New Rectangle(15, 35, FreeMemorySortByAddressPanel.Width - 30, FreeMemorySortByAddressPanel.Height - 50)

        CreateProcessListButton_Click(New Object, New EventArgs)
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

    Private Sub CreateProcessListButton_Click(sender As Object, e As EventArgs) Handles ResetSystemButton.Click
        ProcessListComboBox.Items.Clear()
        ProcessList.Clear()
        FirstMemoryNode = New MemoryNodeClass(Nothing, 0, Max_MemorySize)
        MemoryManagerPanel.Image = CreateMemoryBitmap()
        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        CreateFreeMemoryBitmap()
        FreeMemorySize = Max_MemorySize
        NextPID = 0
        GC.Collect()
    End Sub

    Private Sub SegmentPageButton_Click(sender As Object, e As EventArgs) Handles SegmentPageButton.Click
        DispathComboBox.Visible = Not DispathComboBox.Visible
        SegmentPageNumeric.Visible = Not SegmentPageNumeric.Visible
        Select Case DispathComboBox.Visible
            Case True
                SegmentPageLabel.Text = "请选择空白内存选择算法："
                SegmentPageButton.Text = "     禁止分页"
            Case False
                SegmentPageLabel.Text = "设置分页大小："
                SegmentPageButton.Text = "     允许分页"
        End Select
    End Sub

    Private Sub AddProcessButton_Click(sender As Object, e As EventArgs) Handles AddProcessButton.Click
        If FreeMemorySize < ProcessMemorySizeNumeric.Value Then
            MsgBox("内存不足！")
            Exit Sub
        End If
        CreateFreeMemoryBitmap()
        Dim NextFreeMemoryNode As MemoryNodeClass = NextFreeMemoryNodes(DispathComboBox.SelectedIndex)
        Dim InsProcess As ProcessClass = New ProcessClass(NextPID, "进程-" & NextPID, ProcessMemorySizeNumeric.Value, Color.FromArgb(UnityRandom.Next(256), UnityRandom.Next(256), UnityRandom.Next(256)))

        If NextFreeMemoryNode.Size - InsProcess.Size < ProcessMemorySizeNumeric.Minimum Then
            '空闲内存块载入进程后剩余小于最小碎片大小，不生成新的内存块
            NextFreeMemoryNode.Process = InsProcess
            FreeMemorySize -= NextFreeMemoryNode.Size
        Else
            '空闲内存块载入进程后剩余大于最小碎片大小，生成新的内存块
            Dim InsMemoryNode As MemoryNodeClass = New MemoryNodeClass(InsProcess, NextFreeMemoryNode.StartPoint, InsProcess.Size)
            FreeMemorySize -= InsProcess.Size
            InsMemoryNode.NextNode = NextFreeMemoryNode
            If IsNothing(NextFreeMemoryNode.LastNode) Then
                FirstMemoryNode = InsMemoryNode
            Else
                InsMemoryNode.LastNode = NextFreeMemoryNode.LastNode
                NextFreeMemoryNode.LastNode.NextNode = InsMemoryNode
            End If
            NextFreeMemoryNode.LastNode = InsMemoryNode
            NextFreeMemoryNode.Size -= InsMemoryNode.Size
            NextFreeMemoryNode.StartPoint += InsMemoryNode.Size
        End If

        ProcessList.Add(InsProcess)
        ProcessListComboBox.Items.Add(InsProcess.Name)

        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmap()

        NextPID += 1
        GC.Collect()
    End Sub

    Private Sub DisposeProcessButton_Click(sender As Object, e As EventArgs) Handles DisposeProcessButton.Click
        If ProcessListComboBox.SelectedIndex = -1 Then Exit Sub
        FreeMemorySize += ProcessList(ProcessListComboBox.SelectedIndex).Size
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Do While True
            '寻找进程关联的内存并释放内存
            If Not IsNothing(InsMemoryNode.Process) AndAlso InsMemoryNode.Process.Name = ProcessListComboBox.Text Then
                InsMemoryNode.Process = Nothing
                '合并空闲分区
                If Not IsNothing(InsMemoryNode.LastNode) AndAlso IsNothing(InsMemoryNode.LastNode.Process) Then
                    InsMemoryNode.LastNode.Size += InsMemoryNode.Size
                    InsMemoryNode.LastNode.NextNode = InsMemoryNode.NextNode
                    InsMemoryNode.NextNode.LastNode = InsMemoryNode.LastNode
                    InsMemoryNode = InsMemoryNode.LastNode
                End If
                If Not IsNothing(InsMemoryNode.NextNode) AndAlso IsNothing(InsMemoryNode.NextNode.Process) Then
                    InsMemoryNode.Size += InsMemoryNode.NextNode.Size
                    InsMemoryNode.NextNode = InsMemoryNode.NextNode.NextNode
                    If Not IsNothing(InsMemoryNode.NextNode) Then
                        InsMemoryNode.NextNode.LastNode = InsMemoryNode
                    End If
                End If
                Exit Do
            End If
            If IsNothing(InsMemoryNode.NextNode) Then Exit Do
            InsMemoryNode = InsMemoryNode.NextNode
        Loop

        ProcessList.RemoveAt(ProcessListComboBox.SelectedIndex)
        ProcessListComboBox.Items.RemoveAt(ProcessListComboBox.SelectedIndex)

        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmap()
        CreateFreeMemoryBitmap()

        GC.Collect()
    End Sub

    Private Sub RelocateButton_Click(sender As Object, e As EventArgs) Handles RelocateButton.Click
        If IsNothing(FirstMemoryNode.NextNode) Then Exit Sub
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Do While True
            If IsNothing(InsMemoryNode.Process) Then
                If Not IsNothing(InsMemoryNode.LastNode) Then
                    '消除两个节点中间的空节点
                    InsMemoryNode.LastNode.NextNode = InsMemoryNode.NextNode
                    InsMemoryNode.NextNode.LastNode = InsMemoryNode.LastNode
                    InsMemoryNode.NextNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                    InsMemoryNode = InsMemoryNode.NextNode
                Else
                    '首节点为空
                    InsMemoryNode.NextNode.StartPoint = 0
                    InsMemoryNode = InsMemoryNode.NextNode
                    FirstMemoryNode = InsMemoryNode
                    InsMemoryNode.LastNode = Nothing
                End If
            Else
                '重定位非空节点的地址
                If InsMemoryNode.Size <> InsMemoryNode.Process.Size Then
                    FreeMemorySize += InsMemoryNode.Size - InsMemoryNode.Process.Size
                    InsMemoryNode.Size = InsMemoryNode.Process.Size
                End If
                If Not IsNothing(InsMemoryNode.LastNode) Then
                    InsMemoryNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                End If
                InsMemoryNode = InsMemoryNode.NextNode
            End If

            If IsNothing(InsMemoryNode.NextNode) Then
                '处理最后一个节点
                If IsNothing(InsMemoryNode.Process) Then
                    If Not IsNothing(InsMemoryNode.LastNode) Then InsMemoryNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                    InsMemoryNode.Size = FreeMemorySize
                Else
                    If InsMemoryNode.Size <> InsMemoryNode.Process.Size Then
                        FreeMemorySize += InsMemoryNode.Size - InsMemoryNode.Process.Size
                        InsMemoryNode.Size = InsMemoryNode.Process.Size
                        Dim LastFreeMemoryNode As MemoryNodeClass = New MemoryNodeClass(Nothing, InsMemoryNode.StartPoint + InsMemoryNode.Size, FreeMemorySize)
                        InsMemoryNode.NextNode = LastFreeMemoryNode
                        LastFreeMemoryNode.LastNode = InsMemoryNode
                    End If
                    If Not IsNothing(InsMemoryNode.LastNode) Then
                        InsMemoryNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                    End If
                End If

                Exit Do
            End If
        Loop

        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmap()
        CreateFreeMemoryBitmap()

        GC.Collect()
    End Sub

#End Region

#Region "功能函数"

    ''' <summary>
    ''' 创建内存可视化图像
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateMemoryBitmap() As Bitmap
        Dim UnityBitmap As Bitmap = New Bitmap(MemoryManagerPanel.Width, MemoryManagerPanel.Height)
        Dim UnityBrush As SolidBrush, UnityPen As Pen
        Dim UnityPoint As Point = Point.Empty
        Dim UnitySize As Size = Size.Empty
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmap)
            Do While True
                UnityPoint = New Point(MemoryRectangle.Left, CInt(MemoryRectangle.Top + InsMemoryNode.StartPoint * MemoryCellHeight))
                UnitySize = New Size(MemoryRectangle.Width, CInt(MemoryCellHeight * InsMemoryNode.Size))
                If IsNothing(InsMemoryNode.Process) Then
                    UnityBrush = New SolidBrush(Color.FromArgb(150, Color.Gray))
                    UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, UnitySize.Width, UnitySize.Height)
                    UnityBrush.Color = Color.FromArgb(200, Color.White)
                    UnityGraphics.DrawString(String.Format(" *空闲内存 Addr:{0},Size:{1}", InsMemoryNode.StartPoint, InsMemoryNode.Size), Me.Font, UnityBrush, UnityPoint)
                Else
                    UnityBrush = New SolidBrush(Color.FromArgb(150, InsMemoryNode.Process.Color))
                    UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, UnitySize.Width, UnitySize.Height)
                    'UnityPen = New Pen(Color.FromArgb(200, InsMemoryNode.Process.Color), 1)
                    'UnityGraphics.DrawRectangle(UnityPen, UnityPoint.X + 1, UnityPoint.Y + 1, UnitySize.Width - 2, UnitySize.Height - 2)
                    UnityBrush.Color = Color.Gold
                    UnityGraphics.DrawString(String.Format("{0} Addr:{1},Size:{2}{3}", InsMemoryNode.Process.Name, InsMemoryNode.StartPoint, InsMemoryNode.Size, IIf(InsMemoryNode.Size = InsMemoryNode.Process.Size, "", ",进程大小:" & InsMemoryNode.Process.Size)), Me.Font, UnityBrush, UnityPoint)
                End If

                If IsNothing(InsMemoryNode.NextNode) Then Exit Do

                InsMemoryNode = InsMemoryNode.NextNode
            Loop

            UnityPen = New Pen(Color.FromArgb(100, Color.White), 1)
            UnityGraphics.DrawRectangle(UnityPen, MemoryRectangle)
        End Using
        Return UnityBitmap
    End Function

    ''' <summary>
    ''' 创建空白区域链表图像
    ''' </summary>
    Private Sub CreateFreeMemoryBitmap()
        ReDim NextFreeMemoryNodes(3)
        Dim UnityBitmapSortByAddress As Bitmap = New Bitmap(FreeMemorySortByAddressPanel.Width, FreeMemorySortByAddressPanel.Height)
        'Dim UnityBitmapSortBySize As Bitmap = New Bitmap(FreeMemorySortBySizePanel.Width, FreeMemorySortBySizePanel.Height)
        Dim UnityBrush As SolidBrush, UnityPen As Pen = New Pen(Color.FromArgb(150, Color.White), 1)
        Dim UnityPoint As Point = Point.Empty
        Dim UnitySize As Size = Size.Empty
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Dim LastX As Integer = FreeMemoryRectangle.Left
        FreeMemoryCellWidth = FreeMemoryRectangle.Width / FreeMemorySize

        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmapSortByAddress)
            Do While True
                If IsNothing(InsMemoryNode.Process) Then
                    UnityPoint = New Point(LastX, FreeMemoryRectangle.Top)
                    UnitySize = New Size(InsMemoryNode.Size * FreeMemoryCellWidth, FreeMemoryRectangle.Height)
                    LastX += UnitySize.Width
                    UnityBrush = New SolidBrush(Color.FromArgb(120, Color.Gray))
                    UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, UnitySize.Width, UnitySize.Height)
                    UnityBrush.Color = Color.FromArgb(200, Color.White)
                    UnityGraphics.DrawLine(UnityPen, UnityPoint.X, FreeMemoryRectangle.Top, UnityPoint.X, FreeMemoryRectangle.Bottom)
                    UnityGraphics.DrawString(String.Format("空闲区-Addr:- {0}-Size:- {1}".Replace("-", vbCrLf), InsMemoryNode.StartPoint, InsMemoryNode.Size), Me.Font, UnityBrush, UnityPoint)
                    NextFreeMemoryNodes(0) = InsMemoryNode
                End If

                If IsNothing(InsMemoryNode.NextNode) Then Exit Do
                InsMemoryNode = InsMemoryNode.NextNode
            Loop

            UnityPen = New Pen(Color.FromArgb(100, Color.White), 1)
            UnityGraphics.DrawRectangle(UnityPen, FreeMemoryRectangle)
        End Using

        FreeMemorySortByAddressPanel.Image = UnityBitmapSortByAddress
    End Sub

#End Region

End Class
