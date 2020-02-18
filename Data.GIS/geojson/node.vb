Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.Javascript

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
        Public Property size As String
        Public Property cp As Double()

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

        Public Property id As String
        Public Property geometry As PolygonVariant
        Public Property properties As FeatureProperties

    End Class

    Public Class FeatureProperties

        Public Property id As String
        Public Property name As String
        Public Property cp As Double()
        Public Property childNum As String

        Public Property longitude As Double
        Public Property latitude As Double

        Public Function TryGetValue(propName$, default$) As String
            Select Case propName
                Case NameOf(id) : Return If(id.StringEmpty, [default], id)
                Case NameOf(name) : Return If(name.StringEmpty, [default], name)
                Case Else
                    Return [default]
            End Select
        End Function

    End Class

    Public Class PolygonVariant : Inherits [Variant]

        Sub New()
            Call MyBase.New(GetType(Polygon), GetType(MultiPolygon))
        End Sub

        Protected Overrides Function which(json As JsonObject) As Type
            If json.ContainsKey("type") AndAlso json!type.GetType Is GetType(JsonValue) Then
                Dim type$ = json!type.As(Of JsonValue).AsString

                Select Case type
                    Case "Polygon" : Return GetType(Polygon)
                    Case "MultiPolygon" : Return GetType(MultiPolygon)
                    Case Else
                        Throw New NotImplementedException(type)
                End Select
            Else
                Throw New InvalidExpressionException
            End If
        End Function
    End Class

    Public Class Polygon : Inherits node

        Public Property coordinates As Double()()()

    End Class

    Public Class MultiPolygon : Inherits node

        Public Property coordinates As Double()()()()

    End Class
End Namespace