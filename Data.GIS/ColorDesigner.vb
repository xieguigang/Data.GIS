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

    Dim clSequence As Color()
    Dim mappings As Dictionary(Of Double, Integer)
    Dim translate As Func(Of Double, Double) = AddressOf Math.Log

    Sub New(data As IEnumerable(Of Data), mapName As String, mapLevels As Integer)
        Dim array As Data() = data.ToArray
        Dim values As Double() = array _
            .Select(Function(x) translate(x.value)) _
            .Distinct.ToArray

        If Not String.IsNullOrEmpty(mapName) AndAlso
            Not mapName.TextEquals("default") Then
            Dim maps As New ColorMap(mapLevels)
            clSequence = ColorSequence(maps.GetMaps(mapName), maps)
        Else
            If mapLevels = 512 Then
                clSequence = MapDefaultColors.DefaultColors512
            Else
                clSequence = MapDefaultColors.DefaultColors256
            End If
        End If

        Me.mappings = values.GenerateMapping(mapLevels / 2) _
            .SeqIterator _
            .ToDictionary(Function(x) values(x.i),
                          Function(x) x.obj)
        Me.data = array

        For Each x In Me.data
            x.value = translate(x.value)
        Next
    End Sub

    Public Function GetColor(x As Double) As Color
        Return clSequence(mappings(x) - 1)
    End Function
End Class
