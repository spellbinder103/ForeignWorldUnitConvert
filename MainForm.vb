Public Class MainForm
    Private Sub ReadRTC(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            DateTimePicker1.Enabled = False
            Timer1.Start()
        Else
            DateTimePicker1.Enabled = True
            Timer1.Stop()
        End If
    End Sub

    Private Sub RealTimeUpdate(sender As Object, e As EventArgs) Handles Timer1.Tick
        DateTimePicker1.Value = Now()
    End Sub

    Private Sub TimeCompare(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Dim HourName As String() = {"子时", "丑时", "寅时", "卯时", "辰时", "巳时", "午时", "未时", "申时", "酉时", "戌时", "亥时"}
        Dim MinuteName As String() = {"", "一刻", "二刻", "三刻", "四刻", "五刻", "六刻", "七刻"}
        Dim NightHourName As String() = {"一更", "二更", "三更", "四更", "五更"}
        Dim NightMinuteName As String() = {"", "一点", "二点", "三点", "四点"}
        Dim CurrentTime As TimeSpan = DateTimePicker1.Value.TimeOfDay
        Static ForeignHour, ForeignNightHour As String
        Static ForeignMinute, ForeignNightMinute As String

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
    End Sub

    Private Sub ValidityNumber(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, TextBox9.KeyPress
        Dim CurrentTextBox As TextBox = CType(sender, TextBox)
        Dim CurrentStringLength As Integer = CurrentTextBox.Text.Length()
        Dim NumberOfDot As Integer = InStr(CurrentTextBox.Text, ".")

        Select Case True
            Case Char.IsNumber(e.KeyChar)
                e.Handled = False
            Case Char.IsControl(e.KeyChar)
                e.Handled = False
            Case e.KeyChar = "."
                If CurrentStringLength > 0 AndAlso NumberOfDot = 0 Then
                    e.Handled = False
                Else
                    e.Handled = True
                End If
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub TextBox1Active(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox1.Text)
            TextBox2.Text = CStr(Input * 15)
            TextBox3.Text = CStr(Input * 150)
            TextBox4.Text = CStr(Input * 1500)
            TextBox5.Text = CStr(Input * 15000)
            TextBox6.Text = CStr(Input * 150000)
            TextBox7.Text = CStr(Input * 15 * 0.0231)
            TextBox8.Text = CStr(Input * 15 * 23.1)
            TextBox9.Text = CStr(Input * 15 * 23100)
        End If
    End Sub

    Private Sub TextBox2Active(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox2.Text)
            TextBox1.Text = CStr(Input / 15)
            TextBox3.Text = CStr(Input * 10)
            TextBox4.Text = CStr(Input * 100)
            TextBox5.Text = CStr(Input * 1000)
            TextBox6.Text = CStr(Input * 10000)
            TextBox7.Text = CStr(Input * 0.0231)
            TextBox8.Text = CStr(Input * 23.1)
            TextBox9.Text = CStr(Input * 23100)
        End If
    End Sub

    Private Sub TextBox3Active(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox3.Text)
            TextBox1.Text = CStr(Input / 150)
            TextBox2.Text = CStr(Input / 10)
            TextBox4.Text = CStr(Input * 10)
            TextBox5.Text = CStr(Input * 100)
            TextBox6.Text = CStr(Input * 1000)
            TextBox7.Text = CStr(Input / 10 * 0.0231)
            TextBox8.Text = CStr(Input / 10 * 23.1)
            TextBox9.Text = CStr(Input / 10 * 23100)
        End If
    End Sub

    Private Sub TextBox4Active(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox4.Text)
            TextBox1.Text = CStr(Input / 1500)
            TextBox2.Text = CStr(Input / 100)
            TextBox3.Text = CStr(Input / 10)
            TextBox5.Text = CStr(Input * 10)
            TextBox6.Text = CStr(Input * 100)
            TextBox7.Text = CStr(Input / 100 * 0.0231)
            TextBox8.Text = CStr(Input / 100 * 23.1)
            TextBox9.Text = CStr(Input / 100 * 23100)
        End If
    End Sub

    Private Sub TextBox5Active(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox5.Text)
            TextBox1.Text = CStr(Input / 15000)
            TextBox2.Text = CStr(Input / 1000)
            TextBox3.Text = CStr(Input / 100)
            TextBox4.Text = CStr(Input / 10)
            TextBox6.Text = CStr(Input * 10)
            TextBox7.Text = CStr(Input / 1000 * 0.0231)
            TextBox8.Text = CStr(Input / 1000 * 23.1)
            TextBox9.Text = CStr(Input / 1000 * 23100)
        End If
    End Sub

    Private Sub TextBox6Active(sender As Object, e As KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox6.Text)
            TextBox1.Text = CStr(Input / 150000)
            TextBox2.Text = CStr(Input / 10000)
            TextBox3.Text = CStr(Input / 1000)
            TextBox4.Text = CStr(Input / 100)
            TextBox5.Text = CStr(Input / 10)
            TextBox7.Text = CStr(Input / 10000 * 0.0231)
            TextBox8.Text = CStr(Input / 10000 * 23.1)
            TextBox9.Text = CStr(Input / 10000 * 23100)
        End If
    End Sub

    Private Sub TextBox7Active(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox7.Text)
            TextBox1.Text = CStr(Input / 15 / 0.0231)
            TextBox2.Text = CStr(Input / 0.0231)
            TextBox3.Text = CStr(Input / 0.00231)
            TextBox4.Text = CStr(Input / 0.000231)
            TextBox5.Text = CStr(Input / 0.0000231)
            TextBox6.Text = CStr(Input / 0.00000231)
            TextBox8.Text = CStr(Input * 1000)
            TextBox9.Text = CStr(Input * 1000000)
        End If
    End Sub

    Private Sub TextBox8Active(sender As Object, e As KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox8.Text)
            TextBox1.Text = CStr(Input / 15 / 23.1)
            TextBox2.Text = CStr(Input / 23.1)
            TextBox3.Text = CStr(Input / 2.31)
            TextBox4.Text = CStr(Input / 0.231)
            TextBox5.Text = CStr(Input / 0.0231)
            TextBox6.Text = CStr(Input / 0.00231)
            TextBox7.Text = CStr(Input / 1000)
            TextBox9.Text = CStr(Input * 1000)
        End If
    End Sub

    Private Sub TextBox9Active(sender As Object, e As KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Input As Double = CDbl(TextBox9.Text)
            TextBox1.Text = CStr(Input / 15 / 23100)
            TextBox2.Text = CStr(Input / 23100)
            TextBox3.Text = CStr(Input / 2310)
            TextBox4.Text = CStr(Input / 231)
            TextBox5.Text = CStr(Input / 23.1)
            TextBox6.Text = CStr(Input / 2.31)
            TextBox7.Text = CStr(Input / 1000000)
            TextBox8.Text = CStr(Input / 1000)
        End If
    End Sub
End Class
