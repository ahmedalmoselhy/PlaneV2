Imports System.IO
Imports System.IO.IOException
Imports System.Windows.Forms.Control

Public Structure hagz
    Dim trip_no As Integer
    Dim trip_date As String
    Dim trip_time As String
    Dim arrive_time As String
    Dim city1 As String
    Dim city2 As String
    ' Define the capacity of arrays
    <VBFixedArray(71)> Dim korsi() As Integer
    <VBFixedArray(71)> Dim passenger() As String
    <VBFixedArray(71)> Dim phone() As String
    <VBFixedArray(71)> Dim address() As String
End Structure

Public Class Form1
    Inherits System.Windows.Forms.Form

    Dim buttons(71) As Button
    Dim z, i, s, x, j As Integer



    Dim position As Integer

    Public prec As hagz
    Dim m, n, l As String

    Private Sub Button73_Click(sender As Object, e As EventArgs) Handles Button73.Click
        position = Loc(1)

        ' تستخدم هذه الدالة لتغيير للموضع الذي تريد القراءة أو الكتابة عليه
        Seek(1, Val(TextBox1.Text))

        prec.trip_no = Val(TextBox1.Text)
        prec.trip_date = TextBox2.Text
        prec.trip_time = TextBox4.Text
        prec.arrive_time = TextBox5.Text
        prec.city1 = TextBox3.Text
        prec.city2 = TextBox6.Text

        For i = 0 To 71
            prec.korsi(i) = 0
            prec.passenger(i) = "0"
            prec.phone(i) = "0"
            prec.address(i) = "0"
        Next
        s = Val(TextBox1.Text)
        FilePut(1, prec, s)
        clear()
    End Sub

    Private Sub Button74_Click(sender As Object, e As EventArgs) Handles Button74.Click
        view()
        x = MsgBox("هل تريد حجز هذا الكرسي؟", MsgBoxStyle.OkCancel)
        If x = 1 Then
            buttons(j).BackColor = Color.Orange
            z = 1
            FileGet(1, prec, Val(TextBox1.Text))
            prec.korsi(j) = z
            prec.passenger(j) = TextBox7.Text
            prec.phone(j) = TextBox8.Text
            prec.address(j) = TextBox9.Text

            s = Val(TextBox1.Text)

            FilePut(1, prec, s)
        ElseIf x = 2 Then
            buttons(j).BackColor = Color.Lime

        End If
        clear2()
    End Sub

    Private Sub Button76_Click(sender As Object, e As EventArgs) Handles Button76.Click
        view()
        If z = 1 Then
            x = MsgBox("هل تريد إلغاء حجز هذا الكرسي؟", MsgBoxStyle.OkCancel)
            If x = 1 Then
                buttons(j).BackColor = Color.Lime
                z = 0
                prec.korsi(j) = 0
                prec.passenger(j) = "0"
                prec.phone(j) = "0"
                prec.address(j) = "0"

            ElseIf x = 2 Then
                buttons(j).BackColor = Color.Orange
                Exit Sub
            End If
        ElseIf z = 2 Then
            MsgBox("لا يمكن إلغاء حجز هذا الكرسي")

        End If
        clear2()
    End Sub

    Private Sub Button75_Click(sender As Object, e As EventArgs) Handles Button75.Click
        view()
        If z = 0 Then
            MsgBox("لم يسبق الحجز")
        ElseIf z = 1 Then
            x = MsgBox("هل تريد تأكيد حجز هذا الكرسي؟", MsgBoxStyle.OkCancel)
            If x = 1 Then
                buttons(j).BackColor = Color.Red
                z = 2
                FileGet(1, prec, Val(TextBox1.Text))

                prec.korsi(j) = z
                s = Val(TextBox1.Text)
                FilePut(1, prec, s)

            ElseIf x = 2 Then
                buttons(j).BackColor = Color.Orange
                Exit Sub

            End If
        End If
        clear2()

    End Sub

    Private Sub Button77_Click(sender As Object, e As EventArgs) Handles Button77.Click
        ' Close The File
        FileClose(1)

        ' Exit Program
        End
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FileOpen(1, "records.txt", OpenMode.Random,,, Len(prec))

        prec.korsi = New Integer(71) {}
        prec.passenger = New String(71) {}
        prec.phone = New String(71) {}
        prec.address = New String(71) {}

        Label1.Text = Now()

        For i = 1 To 72
            buttons(i - 1) = Me.Controls("button" & i)
            AddHandler buttons(i - 1).Click, AddressOf korsyy_no
        Next
    End Sub

    Public Sub korsyy_no(ByVal sender As System.Object, ByVal e As System.EventArgs)

        FileGet(1, prec, Val(TextBox1.Text))
        For i = 0 To buttons.Length - 1
            If buttons(i) Is sender Then
                j = i
                Exit For
            End If
        Next

        z = prec.korsi(j)
        If z = 0 Then
        Else
            TextBox7.Text = prec.passenger(j)
            TextBox8.Text = prec.phone(j)
            TextBox9.Text = prec.address(j)
        End If

        If j < 8 Then
            Label15.Text = "First Class"
        Else
            Label15.Text = "Second Class"
        End If
    End Sub

    Public Sub clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
    End Sub

    Public Sub clear2()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            View()
        Catch ex As Exception

        End Try
    End Sub

    Sub view()
        FileGet(1, prec, Val(TextBox1.Text))

        If prec.trip_no <> Val(TextBox1.Text) Then
            MsgBox("No trip Exists under this number")
            FileClose(1)
            Exit Sub
        End If

        TextBox2.Text = prec.trip_date
        TextBox3.Text = prec.city1
        TextBox4.Text = prec.trip_time
        TextBox5.Text = prec.arrive_time
        TextBox6.Text = prec.city2

        For i = 0 To 71
            If prec.korsi(i) = 1 Then
                buttons(i).BackColor = Color.Orange
            ElseIf prec.korsi(i) = 2 Then
                buttons(i).BackColor = Color.Red
            Else
                buttons(i).BackColor = Color.Lime
            End If
        Next
    End Sub
End Class
