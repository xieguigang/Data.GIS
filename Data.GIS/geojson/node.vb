Namespace GeoMap.geojson

    ''' <summary>
    ''' A json node
    ''' </summary>
    Public MustInherit Class node

        Public Property type As String

    End Class

    Public Class FeatureCollection : Inherits node

        Public Property features As Feature()

    End Class

    Public Class Feature : Inherits node

        Public Property properties As Dictionary(Of String, String)
        Public Property geometry As MultiPolygon

    End Class

    Public Class MultiPolygon : Inherits node

        Public Property coordinates As Double()()()()

    End Class
End Namespace