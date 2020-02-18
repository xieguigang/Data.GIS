Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.GIS.GeoMap.geojson
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.Imaging.SVG.XML
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.Text

<HideModuleName>
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

    <Extension>
    Public Function AddAdministrativeInformation(map As SVGData) As SVGData
        Dim administrative = My.Resources.province_id _
            .LineTokens _
            .Select(Function(line) line.Split(ASCII.TAB)) _
            .ToDictionary(Function(t) t(1),
                          Function(t)
                              Return New NamedValue(Of String)(t(0), t(2), t(1))
                          End Function)
        Dim id$
        Dim names As NamedValue(Of String)

        For Each layer As g In map.SVG.AsEnumerable
            If layer.id = "1000032" Then
                ' 香港
                id = "810000"
            ElseIf layer.id = "1000033" Then
                ' 澳门
                id = "820000"
            Else
                id = layer.id.Split("_"c).First & "0000"
            End If

            names = administrative(id)
            layer.class = names.Name

            For Each el In layer.polygon
                el.class = names.Name
            Next
        Next

        Return map
    End Function
End Module
