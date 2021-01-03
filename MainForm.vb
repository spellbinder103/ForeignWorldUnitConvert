Public Class MainForm
    ' when checkbox checked, start the timer
    ' 选中复选框后，启动计时器
    Private Sub ReadRTC(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            DateTimePicker1.Enabled = False
            Timer1.Start()
        Else
            DateTimePicker1.Enabled = True
            Timer1.Stop()
        End If
    End Sub

    ' when timer is begin to tick, get current date time
    ' 当计时器开始计时时，获取当前日期时间
    Private Sub RealTimeUpdate(sender As Object, e As EventArgs) Handles Timer1.Tick
        DateTimePicker1.Value = Now()
    End Sub

    ' when date time picker value begin to change, calculate china format date time according current date time
    ' 当日期时间选择器的值开始更改时，会根据当前日期时间计算中国格式的日期时间
    Private Sub TimeCompare(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ' create string array of chinese word
        ' 创建中文单词的字符串数组
        Dim HourName As String() = {"子时", "丑时", "寅时", "卯时", "辰时", "巳时", "午时", "未时", "申时", "酉时", "戌时", "亥时"}
        Dim MinuteName As String() = {"", "一刻", "二刻", "三刻", "四刻", "五刻", "六刻", "七刻"}
        Dim NightHourName As String() = {"一更", "二更", "三更", "四更", "五更"}
        Dim NightMinuteName As String() = {"", "一点", "二点", "三点", "四点"}
        ' obtain current time without date
        ' 取得不含日期的当前时间
        Dim CurrentTime As TimeSpan = DateTimePicker1.Value.TimeOfDay
        ' create reusable variable
        ' 创建可复用的变量
        Static ForeignHour, ForeignNightHour As String
        Static ForeignMinute, ForeignNightMinute As String
        ' begin to calculate
        ' 开始运算
        Dim CurrentForeignHour As Integer = DateTimePicker1.Value.Hour \ 2
        Dim HourParityCheck As Integer = DateTimePicker1.Value.Hour Mod 2
        Dim HourCheckSum As Integer = CurrentForeignHour + HourParityCheck
        If HourCheckSum > 11 Then
            HourCheckSum = 0
        End If

        ForeignHour = HourName(HourCheckSum)

        Dim CurrentForeignMinute As Integer = DateTimePicker1.Value.Minute \ 15
        Dim MinuteParityCheck As Integer = DateTimePicker1.Value.Hour Mod 2
        If MinuteParityCheck <> 0 Then
            ForeignMinute = MinuteName(CurrentForeignMinute)
        Else
            ForeignMinute = MinuteName(CurrentForeignMinute + 4)
        End If

        TimeLable.Text = "异世界的时辰是" + ForeignHour + ForeignMinute

        Select Case CurrentTime
            Case New TimeSpan(19, 0, 0) To New TimeSpan(20, 59, 59)
                ForeignNightHour = NightHourName(0)
            Case New TimeSpan(21, 0, 0) To New TimeSpan(22, 59, 59)
                ForeignNightHour = NightHourName(1)
            Case New TimeSpan(23, 0, 0) To New TimeSpan(23, 59, 59)
                ForeignNightHour = NightHourName(2)
            Case New TimeSpan(0, 0, 0) To New TimeSpan(0, 59, 59)
                ForeignNightHour = NightHourName(2)
            Case New TimeSpan(1, 0, 0) To New TimeSpan(2, 59, 59)
                ForeignNightHour = NightHourName(3)
            Case New TimeSpan(3, 0, 0) To New TimeSpan(4, 59, 59)
                ForeignNightHour = NightHourName(4)
            Case Else
                ForeignNightHour = "尚未开始"
        End Select

        Dim CurrentHour As Integer = DateTimePicker1.Value.Hour
        If CurrentHour >= 19 Or CurrentHour <= 5 Then
            If HourParityCheck > 0 Then
                Dim NightMinute As Integer = DateTimePicker1.Value.Minute \ 24
                ForeignNightMinute = NightMinuteName(NightMinute)
            Else
                Select Case DateTimePicker1.Value.Minute
                    Case < 12
                        ForeignNightMinute = NightMinuteName(2)
                    Case < 36
                        ForeignNightMinute = NightMinuteName(3)
                    Case >= 36
                        ForeignNightMinute = NightMinuteName(4)
                End Select
            End If
        Else
            ForeignNightMinute = ""
        End If

        NightTimeLable.Text = ForeignNightHour + ForeignNightMinute
        ' calculation end and display resule
        ' 运算结束并显示结果
    End Sub

    ' check only allow numeric button key-in for every textbox
    ' 检查每个文本框仅允许数字按钮键入
    Private Sub NumericOnly(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, TextBox9.KeyPress
        ' pairing the incoming objects as textbox
        ' 配对传入的物件为文本框
        Dim CurrentTextBox As TextBox = CType(sender, TextBox)
        ' get string length from textbox
        ' 从文本框中获取字符串长度
        Dim CurrentStringLength As Integer = CurrentTextBox.Text.Length()
        ' Get number of points from textbox
        ' 从文本框中获取点的数量
        Dim NumberOfPoint As Integer = InStr(CurrentTextBox.Text, ".")

        Select Case True
            Case Char.IsNumber(e.KeyChar)
                ' if return from key press event argument is numeric key, it will not be handled
                ' 如果从按键事件参数传回来的是数字键，则不处理
                e.Handled = False
            Case Char.IsControl(e.KeyChar)
                ' if return from key press event argument is control key, it will not be handled
                ' 如果从按键事件参数传回来的是控制键，则不处理
                e.Handled = False
            Case e.KeyChar = "."
                ' if return from key press event argument is points
                ' 如果从按键事件参数传回来的是点
                If CurrentStringLength > 0 AndAlso NumberOfPoint = 0 Then
                    ' if string length is larger than 0 and number of points is 0, it will not be handled
                    ' 如果字符串长度大于0且点数为0，则不处理
                    e.Handled = False
                Else
                    ' otherwise handled
                    ' 否则就处理掉
                    e.Handled = True
                End If
            Case Else
                ' handled otherwise
                ' 处理掉其他的
                e.Handled = True
        End Select
    End Sub

    ' calculation subroutine
    ' 计算子程序
    Private Sub EnterAndCalculate(sender As Object, e As KeyEventArgs) Handles TextBox9.KeyDown, TextBox8.KeyDown, TextBox7.KeyDown, TextBox6.KeyDown, TextBox5.KeyDown, TextBox4.KeyDown, TextBox3.KeyDown, TextBox2.KeyDown, TextBox1.KeyDown
        ' pairing the incoming objects as textbox
        ' 配对传入的物件为文本框
        Dim CurrentTextBox As TextBox = CType(sender, TextBox)

        ' calculate the formula according which textbox where enter key pressed
        ' 根据按下回车键的文本框计算公式
        If e.KeyCode = Keys.Enter Then
            Dim num As Double = CDbl(CurrentTextBox.Text)

            Select Case CurrentTextBox.Name
                Case "TextBox1"
                    TextBox2.Text = CStr(num * 15)
                    TextBox3.Text = CStr(num * 150)
                    TextBox4.Text = CStr(num * 1500)
                    TextBox5.Text = CStr(num * 15000)
                    TextBox6.Text = CStr(num * 150000)
                    TextBox7.Text = CStr(num * 15 * 0.0231)
                    TextBox8.Text = CStr(num * 15 * 23.1)
                    TextBox9.Text = CStr(num * 15 * 23100)
                Case "TextBox2"
                    TextBox1.Text = CStr(num / 15)
                    TextBox3.Text = CStr(num * 10)
                    TextBox4.Text = CStr(num * 100)
                    TextBox5.Text = CStr(num * 1000)
                    TextBox6.Text = CStr(num * 10000)
                    TextBox7.Text = CStr(num * 0.0231)
                    TextBox8.Text = CStr(num * 23.1)
                    TextBox9.Text = CStr(num * 23100)
                Case "TextBox3"
                    TextBox1.Text = CStr(num / 150)
                    TextBox2.Text = CStr(num / 10)
                    TextBox4.Text = CStr(num * 10)
                    TextBox5.Text = CStr(num * 100)
                    TextBox6.Text = CStr(num * 1000)
                    TextBox7.Text = CStr(num / 10 * 0.0231)
                    TextBox8.Text = CStr(num / 10 * 23.1)
                    TextBox9.Text = CStr(num / 10 * 23100)
                Case "TextBox4"
                    TextBox1.Text = CStr(num / 1500)
                    TextBox2.Text = CStr(num / 100)
                    TextBox3.Text = CStr(num / 10)
                    TextBox5.Text = CStr(num * 10)
                    TextBox6.Text = CStr(num * 100)
                    TextBox7.Text = CStr(num / 100 * 0.0231)
                    TextBox8.Text = CStr(num / 100 * 23.1)
                    TextBox9.Text = CStr(num / 100 * 23100)
                Case "TextBox5"
                    TextBox1.Text = CStr(num / 15000)
                    TextBox2.Text = CStr(num / 1000)
                    TextBox3.Text = CStr(num / 100)
                    TextBox4.Text = CStr(num / 10)
                    TextBox6.Text = CStr(num * 10)
                    TextBox7.Text = CStr(num / 1000 * 0.0231)
                    TextBox8.Text = CStr(num / 1000 * 23.1)
                    TextBox9.Text = CStr(num / 1000 * 23100)
                Case "TextBox6"
                    TextBox1.Text = CStr(num / 150000)
                    TextBox2.Text = CStr(num / 10000)
                    TextBox3.Text = CStr(num / 1000)
                    TextBox4.Text = CStr(num / 100)
                    TextBox5.Text = CStr(num / 10)
                    TextBox7.Text = CStr(num / 10000 * 0.0231)
                    TextBox8.Text = CStr(num / 10000 * 23.1)
                    TextBox9.Text = CStr(num / 10000 * 23100)
                Case "TextBox7"
                    TextBox1.Text = CStr(num / 15 / 0.0231)
                    TextBox2.Text = CStr(num / 0.0231)
                    TextBox3.Text = CStr(num / 0.00231)
                    TextBox4.Text = CStr(num / 0.000231)
                    TextBox5.Text = CStr(num / 0.0000231)
                    TextBox6.Text = CStr(num / 0.00000231)
                    TextBox8.Text = CStr(num * 1000)
                    TextBox9.Text = CStr(num * 1000000)
                Case "TextBox8"
                    TextBox1.Text = CStr(num / 15 / 23.1)
                    TextBox2.Text = CStr(num / 23.1)
                    TextBox3.Text = CStr(num / 2.31)
                    TextBox4.Text = CStr(num / 0.231)
                    TextBox5.Text = CStr(num / 0.0231)
                    TextBox6.Text = CStr(num / 0.00231)
                    TextBox7.Text = CStr(num / 1000)
                    TextBox9.Text = CStr(num * 1000)
                Case "TextBox9"
                    TextBox1.Text = CStr(num / 15 / 23100)
                    TextBox2.Text = CStr(num / 23100)
                    TextBox3.Text = CStr(num / 2310)
                    TextBox4.Text = CStr(num / 231)
                    TextBox5.Text = CStr(num / 23.1)
                    TextBox6.Text = CStr(num / 2.31)
                    TextBox7.Text = CStr(num / 1000000)
                    TextBox8.Text = CStr(num / 1000)
                Case Else
                    ' do nothing
                    ' 什么都不做
            End Select
        End If
    End Sub
End Class
