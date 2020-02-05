Imports Microsoft.VisualBasic.Data.GIS.GeoMap.geojson
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.MIME.application
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.Scripting.Runtime

Namespace GeoMap

    ''' <summary>
    ''' Convert geojson data to svg map model
    ''' </summary>
    Public Class GeoMapRender

        Private Sub New()
        End Sub

        Public Sub Plot(ByRef g As IGraphics, layout As GraphicsRegion)

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
                plotAPI:=AddressOf New GeoMapRender().Plot,
                driver:=Drivers.SVG
            )
        End Function
    End Class
End Namespace