Imports Microsoft.VisualBasic.Linq

Namespace GeoMap.geojson

    ''' <summary>
    ''' A json node
    ''' </summary>
    Public MustInherit Class node

        Public Property type As String

    End Class

    Public Class FeatureCollection : Inherits node
        Implements Enumeration(Of Feature)

        Public Property features As Feature()

        Public Iterator Function GenericEnumerator() As IEnumerator(Of Feature) Implements Enumeration(Of Feature).GenericEnumerator
            For Each feature As Feature In features
                Yield feature
            Next
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator Implements Enumeration(Of Feature).GetEnumerator
            Yield GenericEnumerator()
        End Function
    End Class

    Public Class Feature : Inherits node

        Public Property properties As Dictionary(Of String, String)
        Public Property geometry As MultiPolygon

    End Class

    Public Class MultiPolygon : Inherits node

        Public Property coordinates As Double()()()()

    End Class
End Namespace