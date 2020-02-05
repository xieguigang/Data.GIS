﻿Imports System.Drawing
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
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
        Dim scaleW As Func(Of Double, Double)
        Dim scaleH As Func(Of Double, Double)

        Private Sub New()
        End Sub

        Private Sub doInitial(canvas As SizeF)
            Dim x As DoubleRange = {0, 0}
            Dim y As DoubleRange = {0, 0}
            Dim width As DoubleRange = {0.0, canvas.Width}
            Dim height As DoubleRange = {0.0, canvas.Height}

            For Each area As Feature In features
                For Each a In area.geometry.coordinates
                    For Each b In a
                        For Each c As Double() In b
                            If x.Min > c(0) Then
                                x.Min = c(0)
                            ElseIf x.Max < c(0) Then
                                x.Max = c(0)
                            End If
                            If y.Min > c(1) Then
                                y.Min = c(1)
                            ElseIf y.Max < c(1) Then
                                y.Max = c(1)
                            End If
                        Next
                    Next
                Next
            Next

            scaleW = Function(xi) x.ScaleMapping(xi, width)
            scaleH = Function(yi) y.ScaleMapping(yi, height)
        End Sub

        Public Sub Plot(ByRef g As IGraphics, layout As GraphicsRegion)
            Dim svg As GraphicsSVG = DirectCast(g, GraphicsSVG)
            Dim id$
            Dim name$
            Dim guid As i32 = 1000000
            Dim geo As Double()()()()
            Dim polygon As PointF()

            Call doInitial(canvas:=layout.PlotRegion.Size)

            For Each area As Feature In features
                id = area.properties.TryGetValue("id", [default]:=++guid)
                name = area.properties.TryGetValue("name", [default]:=id)
                geo = area.geometry.coordinates

                For Each a In geo
                    For Each b In a
                        polygon = b _
                            .Select(Function(t)
                                        Return New PointF(scaleW(t(Scan0)), scaleH(t(1)))
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