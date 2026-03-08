Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class Form1
    Inherits Form

    Private txtDisplay As TextBox
    Private btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 As Button
    Private btnAdd, btnSubtract, btnMultiply, btnDivide, btnEquals, btnClear As Button

    Private currentValue As Double = 0
    Private operation As String = ""
    Private isOperationPerformed As Boolean = False
    Private expression As String = ""

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Simple Calculator"
        Me.Size = New Size(300, 400)

        txtDisplay = New TextBox()
        txtDisplay.Location = New Point(10, 10)
        txtDisplay.Size = New Size(260, 30)
        txtDisplay.TextAlign = HorizontalAlignment.Right
        txtDisplay.ReadOnly = True
        Me.Controls.Add(txtDisplay)

        ' Number buttons
        btn7 = New Button()
        btn7.Text = "7"
        btn7.Location = New Point(10, 50)
        btn7.Size = New Size(50, 50)
        AddHandler btn7.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn7)

        btn8 = New Button()
        btn8.Text = "8"
        btn8.Location = New Point(70, 50)
        btn8.Size = New Size(50, 50)
        AddHandler btn8.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn8)

        btn9 = New Button()
        btn9.Text = "9"
        btn9.Location = New Point(130, 50)
        btn9.Size = New Size(50, 50)
        AddHandler btn9.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn9)

        btnDivide = New Button()
        btnDivide.Text = "/"
        btnDivide.Location = New Point(190, 50)
        btnDivide.Size = New Size(50, 50)
        AddHandler btnDivide.Click, AddressOf OperationButtonClick
        Me.Controls.Add(btnDivide)

        btn4 = New Button()
        btn4.Text = "4"
        btn4.Location = New Point(10, 110)
        btn4.Size = New Size(50, 50)
        AddHandler btn4.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn4)

        btn5 = New Button()
        btn5.Text = "5"
        btn5.Location = New Point(70, 110)
        btn5.Size = New Size(50, 50)
        AddHandler btn5.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn5)

        btn6 = New Button()
        btn6.Text = "6"
        btn6.Location = New Point(130, 110)
        btn6.Size = New Size(50, 50)
        AddHandler btn6.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn6)

        btnMultiply = New Button()
        btnMultiply.Text = "*"
        btnMultiply.Location = New Point(190, 110)
        btnMultiply.Size = New Size(50, 50)
        AddHandler btnMultiply.Click, AddressOf OperationButtonClick
        Me.Controls.Add(btnMultiply)

        btn1 = New Button()
        btn1.Text = "1"
        btn1.Location = New Point(10, 170)
        btn1.Size = New Size(50, 50)
        AddHandler btn1.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn1)

        btn2 = New Button()
        btn2.Text = "2"
        btn2.Location = New Point(70, 170)
        btn2.Size = New Size(50, 50)
        AddHandler btn2.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn2)

        btn3 = New Button()
        btn3.Text = "3"
        btn3.Location = New Point(130, 170)
        btn3.Size = New Size(50, 50)
        AddHandler btn3.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn3)

        btnSubtract = New Button()
        btnSubtract.Text = "-"
        btnSubtract.Location = New Point(190, 170)
        btnSubtract.Size = New Size(50, 50)
        AddHandler btnSubtract.Click, AddressOf OperationButtonClick
        Me.Controls.Add(btnSubtract)

        btn0 = New Button()
        btn0.Text = "0"
        btn0.Location = New Point(10, 230)
        btn0.Size = New Size(110, 50)
        AddHandler btn0.Click, AddressOf NumberButtonClick
        Me.Controls.Add(btn0)

        btnClear = New Button()
        btnClear.Text = "C"
        btnClear.Location = New Point(130, 230)
        btnClear.Size = New Size(50, 50)
        AddHandler btnClear.Click, AddressOf ClearButtonClick
        Me.Controls.Add(btnClear)

        btnAdd = New Button()
        btnAdd.Text = "+"
        btnAdd.Location = New Point(190, 230)
        btnAdd.Size = New Size(50, 50)
        AddHandler btnAdd.Click, AddressOf OperationButtonClick
        Me.Controls.Add(btnAdd)

        btnEquals = New Button()
        btnEquals.Text = "="
        btnEquals.Location = New Point(10, 290)
        btnEquals.Size = New Size(230, 50)
        AddHandler btnEquals.Click, AddressOf EqualsButtonClick
        Me.Controls.Add(btnEquals)
    End Sub

    Private Sub NumberButtonClick(sender As Object, e As EventArgs)
        Dim button As Button = DirectCast(sender, Button)
        If isOperationPerformed Then
            expression &= button.Text
            isOperationPerformed = False
        Else
            expression &= button.Text
        End If
        txtDisplay.Text = expression
    End Sub

    Private Sub OperationButtonClick(sender As Object, e As EventArgs)
        Dim button As Button = DirectCast(sender, Button)
        operation = button.Text
        currentValue = Val(expression)
        expression &= operation
        txtDisplay.Text = expression
        isOperationPerformed = True
    End Sub

    Private Sub EqualsButtonClick(sender As Object, e As EventArgs)
        Dim result As Double
        Select Case operation
            Case "+"
                result = currentValue + Val(expression.Split(operation.ToCharArray())(1))
            Case "-"
                result = currentValue - Val(expression.Split(operation.ToCharArray())(1))
            Case "*"
                result = currentValue * Val(expression.Split(operation.ToCharArray())(1))
            Case "/"
                Dim secondValue As Double = Val(expression.Split(operation.ToCharArray())(1))
                If secondValue <> 0 Then
                    result = currentValue / secondValue
                Else
                    txtDisplay.Text = "Error"
                    Return
                End If
        End Select
        expression = result.ToString()
        txtDisplay.Text = expression
        isOperationPerformed = True
    End Sub

    Private Sub ClearButtonClick(sender As Object, e As EventArgs)
        txtDisplay.Text = ""
        expression = ""
        currentValue = 0
        operation = ""
    End Sub
End Class