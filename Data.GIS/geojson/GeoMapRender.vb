Imports System.Drawing
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Data.GIS.GeoMap.geojson
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.Scripting.Runtime
Imports SVGLayer = Microsoft.VisualBasic.Imaging.SVG.XML.g

Namespace GeoMap

    ''' <summary>
    ''' Convert geojson data to svg map model
    ''' </summary>
    Public Class GeoMapRender

        Dim features As Feature()
        Dim scaleW As Func(Of Double, Double)
        Dim scaleH As Func(Of Double, Double)
        Dim x As DoubleRange = {0, 0}
        Dim y As DoubleRange = {0, 0}

        Private Sub New()
        End Sub

        Private Sub doInitial(canvas As SizeF)
            ' Dim width As DoubleRange = {0.0, canvas.Width}
            ' Dim height As DoubleRange = {0.0, canvas.Height}
            Dim geo As Double()()()()
            Dim xl As New List(Of Double)
            Dim yl As New List(Of Double)

            For Each area As Feature In features
                Dim geometry As PolygonVariant = area.geometry

                Select Case geometry.jsonValue.GetType
                    Case GetType(MultiPolygon) : geo = DirectCast(geometry.jsonValue, MultiPolygon).coordinates
                    Case GetType(Polygon) : geo = {DirectCast(geometry.jsonValue, Polygon).coordinates}
                    Case Else
                        Throw New NotImplementedException
                End Select

                For Each a In geo
                    For Each b In a
                        For Each c As Double() In b
                            xl += c(0)
                            yl += c(1)
                        Next
                    Next
                Next
            Next

            Dim setWidth = canvas.Width
            Dim setHeight = canvas.Height
            Dim imagewidth = xl.Max - xl.Min
            Dim imageheight = yl.Max - yl.Min
            Dim xmin = xl.Min
            Dim ymin = yl.Min

            x = xl.ToArray
            y = yl.ToArray

            If setWidth / imagewidth < setHeight / imageheight Then
                Dim scale = (setWidth / imagewidth)

                scaleW = Function(xi) (xi - xmin) * scale
                scaleH = Function(yi) setHeight - (yi - ymin) * scale
            Else
                Dim scale = (setHeight / imageheight)

                scaleW = Function(xi) (xi - xmin) * scale
                scaleH = Function(yi) setHeight - (yi - ymin) * scale
            End If

            ' scaleW = Function(xi) x.ScaleMapping(xi, width)
            ' scaleH = Function(yi) height.Max - y.ScaleMapping(yi, height)
        End Sub

        Public Sub Plot(ByRef g As IGraphics, layout As GraphicsRegion)
            Dim svg As GraphicsSVG = DirectCast(g, GraphicsSVG)
            Dim id$
            Dim name$
            Dim guid As i32 = 1000000
            Dim polygon As PointF()
            Dim layers As New List(Of SVGLayer)
            Dim geometry As PolygonVariant
            Dim geo As Double()()()()

            Call doInitial(canvas:=layout.PlotRegion.Size)

            For Each area As Feature In features
                id = area.properties.TryGetValue("id", [default]:=++guid)
                name = area.properties.TryGetValue("name", [default]:=id)
                layers *= 0

                geometry = area.geometry

                Select Case geometry.jsonValue.GetType
                    Case GetType(MultiPolygon) : geo = DirectCast(geometry.jsonValue, MultiPolygon).coordinates
                    Case GetType(Polygon) : geo = {DirectCast(geometry.jsonValue, Polygon).coordinates}
                    Case Else
                        Throw New NotImplementedException
                End Select

                For Each a In geo
                    For Each b In a
                        polygon = b _
                            .Select(Function(t)
                                        Return New PointF(scaleW(t(Scan0)), scaleH(t(1)))
                                    End Function) _
                            .ToArray

                        svg.FillPolygon(Brushes.Transparent, polygon)
                        layers += svg.GetLastLayer
                    Next
                Next

                ' add id and name title
                If layers = 1 Then
                    ' only one polygon
                    With layers(Scan0)
                        .id = id
                        .title = name
                    End With
                Else
                    For Each layer In layers.SeqIterator
                        layer.value.id = $"{id}_{layer.i + 1}"
                        layer.value.title = name
                    Next
                End If
            Next
        End Sub

        Public Shared Function Render(geo As String) As SVGData
            Dim geoJson As FeatureCollection = json _
                .ParseJson(JsonStr:=geo.SolveStream) _
                .CreateObject(GetType(FeatureCollection))
            Dim svg As SVGData = GeoMapRender.Render(geoJson)

            Return svg
        End Function

        Public Shared Function Render(geo As FeatureCollection, Optional size$ = "5000,3000") As SVGData
            Return g.GraphicsPlots(
                size:=size.SizeParser,
                padding:=g.ZeroPadding,
                bg:="white",
                plotAPI:=AddressOf New GeoMapRender() With {.features = geo.features}.Plot,
                driver:=Drivers.SVG
            )
        End Function

        Public Shared Function RenderFolder(geometry$, Optional size$ = "5000,3000") As SVGData
            Dim features = geometry.EnumerateFiles("*.json") _
                .Select(Function(path) As FeatureCollection
                            Return json.ParseJson(path.ReadAllText).CreateObject(GetType(FeatureCollection))
                        End Function) _
                .Select(Function(collect) collect.AsEnumerable) _
                .IteratesALL _
                .ToArray

            Return Render(New FeatureCollection With {.features = features}, size)
        End Function
    End Class
End Namespace