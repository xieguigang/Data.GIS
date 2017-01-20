Imports System.Runtime.CompilerServices

Public Module Extensions

    <Extension>
    Public Function MatchRegion(regions As IEnumerable(Of Region), text$) As Region
        For Each x As Region In regions
            If x.MatchRegion(text) Then
                Return x
            End If
        Next

        Return Nothing
    End Function
End Module
