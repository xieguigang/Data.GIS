Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Mathematical
Imports Microsoft.VisualBasic.SoftwareToolkits

Public Class ColorDesigner

    Public Colors As Color()
    Public data As Data()
    Public raw As Double()

    Dim mappings As Dictionary(Of Double, Integer)
    Dim translate As Func(Of Double, Double) = AddressOf Math.Log

    Public ReadOnly Property Depth As Integer
        Get
            Return Colors.Length
        End Get
    End Property

    Sub New(data As IEnumerable(Of Data), mapName As String, mapLevels As Integer)
        Dim array As Data() = data.ToArray
        Dim values As Double() = array _
            .Select(Function(x) translate(x.value)) _
            .Distinct.ToArray

        If Not String.IsNullOrEmpty(mapName) AndAlso
            Not mapName.TextEquals("default") Then
            Dim maps As New ColorMap(mapLevels)
            Colors = ColorSequence(maps.GetMaps(mapName), maps)
        Else
            If mapLevels = 512 Then
                Colors = MapDefaultColors.DefaultColors512
            Else
                Colors = MapDefaultColors.DefaultColors256
            End If
        End If

        Me.mappings = values.GenerateMapping(mapLevels / 2) _
            .SeqIterator _
            .ToDictionary(Function(x) values(x.i),
                          Function(x) x.obj)
        Me.data = array
        Me.raw = array.ToArray(Function(x) x.value)

        For Each x In Me.data
            x.value = translate(x.value)
        Next
    End Sub

    Public Function GetColor(x As Double) As Color
        Return Colors(mappings(x) - 1)
    End Function
End Class
