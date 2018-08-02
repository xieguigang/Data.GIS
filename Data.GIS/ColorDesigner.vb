Imports System.Drawing
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Colors
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Math
Imports Microsoft.VisualBasic.Serialization.JSON

Public Class ColorDesigner

    Public Colors As Color()

    ''' <summary>
    ''' 筛除掉了一些无法查找到对象的数据点
    ''' </summary>
    Public data As Data()
    Public raw As Double()

    Public mappings As Dictionary(Of Double, Integer)

    Public ReadOnly Property Depth As Integer
        Get
            Return Colors.Length
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="mapName"></param>
    ''' <param name="mapLevels">对于默认的颜色谱，只允许10/256/512这三个值</param>
    Sub New(data As IEnumerable(Of Data), mapName As String, mapLevels As Integer)
        Dim array As Data() = data.ToArray
        Dim values As Double() = array _
            .Select(Function(x) x.value) _
            .Distinct.ToArray

        If Not String.IsNullOrEmpty(mapName) AndAlso
            Not mapName.TextEquals("default") Then
            Colors = Designer.GetColors(mapName, mapLevels)
        Else
            If mapLevels = 512 Then
                Colors = MapDefaultColors.DefaultColors512
            ElseIf mapLevels = 10 Then
                Colors = MapDefaultColors.DefaultColors10
            Else
                Colors = MapDefaultColors.DefaultColors256
            End If
        End If

        Me.mappings = values.GenerateMapping(mapLevels / 2) _
            .SeqIterator _
            .ToDictionary(Function(x) values(x.i),
                          Function(x) x.value)
        Me.data = array
        Me.raw = array.Select(Function(x) x.value).ToArray

        Dim trim As New List(Of Data)

        For Each x As Data In Me.data
            Dim alpha2 As String = ColorRender.alpha2(x.state)

            If alpha2 Is Nothing Then
                Call $"Unable found Object named '{x.GetJson}'!".PrintException
                Continue For
            End If

            trim += x
        Next

        Me.data = trim
    End Sub

    Public Function GetColor(x As Double) As Color
        Return GetColor(mappings(x), raw:=x)
    End Function

    Public Function GetColor(x As Integer, Optional raw As Double = -100) As Color
        Try
            Return Colors(x - 1)
        Catch ex As Exception
            ex = New Exception($"Depth:={Depth}, x:={raw}, level:={x - 1}", ex)
            Call App.LogException(ex)
            Return Colors(If(x = 0, 0, Depth - 1))
        End Try
    End Function
End Class
