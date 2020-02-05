Imports System.Drawing
Imports Microsoft.VisualBasic.Data.GIS.GeoMap.geojson
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.MIME.application
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.Scripting.Runtime

Namespace GeoMap

    ''' <summary>
    ''' Convert geojson data to svg map model
    ''' </summary>
    Public Class GeoMapRender

        Dim features As Feature()

        Private Sub New()
        End Sub

        Public Sub Plot(ByRef g As IGraphics, layout As GraphicsRegion)
            Dim svg As GraphicsSVG = DirectCast(g, GraphicsSVG)
            Dim id$
            Dim name$
            Dim guid As i32 = 1000000
            Dim geo As Double()()()()
            Dim polygon As PointF()

            For Each area As Feature In features
                id = area.properties.TryGetValue("id", [default]:=++guid)
                name = area.properties.TryGetValue("name", [default]:=id)
                geo = area.geometry.coordinates

                For Each a In geo
                    For Each b In a
                        polygon = b _
                            .Select(Function(t)
                                        Return New PointF(t(Scan0), t(1))
                                    End Function) _
                            .ToArray

                        svg.FillPolygon(Brushes.Black, polygon)
                    Next
                Next
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
    End Class
End Namespace