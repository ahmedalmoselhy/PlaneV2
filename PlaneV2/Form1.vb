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
    <VBFixedArray(59)> Dim korsi() As Integer
    <VBFixedArray(59)> Dim passenger() As String
    <VBFixedArray(59)> Dim phone() As String
    <VBFixedArray(59)> Dim address() As String
End Structure

Public Class Form1

End Class
