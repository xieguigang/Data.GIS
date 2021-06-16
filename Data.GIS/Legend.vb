Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.MIME.Html.CSS
Imports stdNum = System.Math

Public Module Legend

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Function DrawLegend(designer As ColorDesigner, title$, Optional bg$ = "transparent") As GraphicsData
        Return g.GraphicsPlots(
            New Size(800, 1000), New Padding(25, 25, 25, 25), bg,
            Sub(ByRef g, region)
                Call designer.__plot(g, region, title$)
            End Sub)
    End Function

    <Extension>
    Private Sub __plot(designer As ColorDesigner, g As IGraphics, grect As GraphicsRegion, title$)
        Dim graphicsRegion As Rectangle = grect.PlotRegion
        Dim size As Size = grect.Size
        Dim margin As Padding = grect.Padding
        Dim grayHeight As Integer = size.Height * 0.05
        Dim y As Single
        Dim font As New Font(FontFace.MicrosoftYaHei, 42)
        Dim fSize As SizeF
        Dim pt As Point
        Dim rectWidth As Integer = 150
        Dim legendsHeight As Integer = size.Height - (margin.Vertical * 3) - grayHeight * 3
        Dim d As Single = legendsHeight / designer.Depth
        Dim left As Integer = margin.Horizontal + 30 + rectWidth

        Call g.DrawString(title, font, Brushes.Black, New Point(margin.Horizontal, 0))

        y = margin.Vertical * 2
        font = New Font(FontFace.MicrosoftYaHei, 32)

        Call g.DrawString(stdNum.Round(designer.raw.Max, 1), font, Brushes.Black, New Point(left, y))

        For i As Integer = designer.Depth - 1 To 0 Step -1
            Call g.FillRectangle(
                New SolidBrush(designer.Colors(i)),
                New RectangleF(New PointF(margin.Horizontal, y),
                              New SizeF(rectWidth, d)))
            y += d
        Next

        fSize = g.MeasureString(designer.raw.Min, font)
        Call g.DrawString(
            stdNum.Round(designer.raw.Min, 1),
            font,
            Brushes.Black,
            New Point(left, If(designer.Depth > 100, d, 0) + y - fSize.Height))

        y = size.Height - margin.Vertical - grayHeight
        fSize = g.MeasureString("Unknown", font)
        pt = New Point(
            left,
            y - (grayHeight - fSize.Height) / 2)
        Call g.FillRectangle(
            Brushes.LightGray,
            New Rectangle(New Point(margin.Horizontal, y),
                          New Size(rectWidth, grayHeight)))
        Call g.DrawString("Unknown", font, Brushes.Black, pt)
    End Sub
End Module
