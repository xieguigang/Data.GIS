Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Data.GIS.GeoMap.geojson
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application
Imports Microsoft.VisualBasic.MIME.application.json

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

    Public Function GeoChina() As FeatureCollection
        Dim resJson$ = My.Resources _
            .__china_geo _
            .DoCall(AddressOf Encoding.UTF8.GetString)

        Return json _
            .ParseJson(JsonStr:=resJson) _
            .CreateObject(GetType(FeatureCollection))
    End Function
End Module
