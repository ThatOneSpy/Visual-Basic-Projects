Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    Private snake As List(Of Point)
    Private direction As Point
    Private apple As Point
    Private timer As Timer
    Private gridSize As Integer = 20
    Private cellSize As Integer = 20
    Private btnPlayAgain As Button
    Private btnReady As Button

    Public Sub New()
        InitializeComponent()
        snake = New List(Of Point) From {New Point(10, 10)}
        direction = New Point(0, 1)
        GenerateApple()
        timer = New Timer() With {.Interval = 200}
        AddHandler timer.Tick, AddressOf Timer_Tick
        ' timer.Start()  ' Removed to start with Ready button
        Me.KeyPreview = True
        AddHandler Me.KeyDown, AddressOf Form1_KeyDown
        Me.DoubleBuffered = True
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Snake Game"
        Me.Size = New Size(gridSize * cellSize + 20, gridSize * cellSize + 60)
        Me.BackColor = Color.Black

        Dim offsetX As Integer = (Me.ClientSize.Width - gridSize * cellSize) \ 2
        Dim offsetY As Integer = (Me.ClientSize.Height - gridSize * cellSize) \ 2

        btnPlayAgain = New Button()
        btnPlayAgain.Text = "Play Again"
        btnPlayAgain.Location = New Point(offsetX + (gridSize * cellSize - 100) \ 2, offsetY + (gridSize * cellSize - 30) \ 2)
        btnPlayAgain.Size = New Size(100, 30)
        btnPlayAgain.Visible = False
        btnPlayAgain.ForeColor = Color.White
        AddHandler btnPlayAgain.Click, AddressOf PlayAgainButtonClick
        Me.Controls.Add(btnPlayAgain)

        btnReady = New Button()
        btnReady.Text = "Ready"
        btnReady.Location = New Point(offsetX + (gridSize * cellSize - 100) \ 2, offsetY + (gridSize * cellSize - 30) \ 2)
        btnReady.Size = New Size(100, 30)
        btnReady.Visible = True
        btnReady.ForeColor = Color.White
        AddHandler btnReady.Click, AddressOf ReadyButtonClick
        Me.Controls.Add(btnReady)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim offsetX As Integer = (Me.ClientSize.Width - gridSize * cellSize) \ 2
        Dim offsetY As Integer = (Me.ClientSize.Height - gridSize * cellSize) \ 2
        If snake.Count > 0 Then
            ' Draw head yellow
            e.Graphics.FillRectangle(Brushes.Yellow, offsetX + snake(0).X * cellSize, offsetY + snake(0).Y * cellSize, cellSize, cellSize)
            ' Draw body green
            For i As Integer = 1 To snake.Count - 1
                e.Graphics.FillRectangle(Brushes.Green, offsetX + snake(i).X * cellSize, offsetY + snake(i).Y * cellSize, cellSize, cellSize)
            Next
        End If
        e.Graphics.FillRectangle(Brushes.Red, offsetX + apple.X * cellSize, offsetY + apple.Y * cellSize, cellSize, cellSize)
        ' Draw border
        e.Graphics.DrawRectangle(Pens.White, offsetX, offsetY, gridSize * cellSize, gridSize * cellSize)
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        Dim head As Point = snake(0)
        head = New Point(head.X + direction.X, head.Y + direction.Y)
        If head = apple Then
            snake.Insert(0, head)
            GenerateApple()
        Else
            snake.Insert(0, head)
            snake.RemoveAt(snake.Count - 1)
        End If
        ' Check collision
        If head.X < 0 Or head.X >= gridSize Or head.Y < 0 Or head.Y >= gridSize Or snake.Skip(1).Contains(head) Then
            timer.Stop()
            btnPlayAgain.Visible = True
        End If
        Me.Invalidate()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Up
                If direction <> New Point(0, 1) Then direction = New Point(0, -1)
            Case Keys.Down
                If direction <> New Point(0, -1) Then direction = New Point(0, 1)
            Case Keys.Left
                If direction <> New Point(1, 0) Then direction = New Point(-1, 0)
            Case Keys.Right
                If direction <> New Point(-1, 0) Then direction = New Point(1, 0)
        End Select
    End Sub

    Private Sub GenerateApple()
        Dim rand As New Random()
        Do
            apple = New Point(rand.Next(gridSize), rand.Next(gridSize))
        Loop While snake.Contains(apple)
    End Sub

    Private Sub ReadyButtonClick(sender As Object, e As EventArgs)
        timer.Start()
        btnReady.Visible = False
    End Sub

    Private Sub PlayAgainButtonClick(sender As Object, e As EventArgs)
        snake = New List(Of Point) From {New Point(10, 10)}
        direction = New Point(0, 1)
        GenerateApple()
        timer.Start()
        btnPlayAgain.Visible = False
        Me.Invalidate()
    End Sub
End Class