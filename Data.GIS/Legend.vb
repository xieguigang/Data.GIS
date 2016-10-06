Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D

Public Module Legend

    <Extension>
    Public Function DrawLegend(designer As ColorDesigner, title As String, Optional bg As String = "transparent") As Bitmap
        Return GraphicsPlots(
            New Size(800, 1000), New Size(50, 50), bg,
            Sub(ByRef g, grect)
                Dim graphicsRegion As Rectangle = grect.GraphicsRegion
                Dim size As Size = grect.Size
                Dim margin As Size = grect.Margin
                Dim grayHeight As Integer = size.Height * 0.05
                Dim y As Single
                Dim font As New Font(FontFace.MicrosoftYaHei, 42)
                Dim fSize As SizeF
                Dim pt As Point
                Dim rectWidth As Integer = 150
                Dim legendsHeight As Integer = size.Height - (margin.Height * 3) - grayHeight * 3
                Dim d As Single = legendsHeight / designer.Depth
                Dim left As Integer = margin.Width + 30 + rectWidth

                Call g.DrawString(title, font, Brushes.Black, New Point(margin.Width, 0))

                y = margin.Height * 2
                font = New Font(FontFace.MicrosoftYaHei, 32)

                Call g.DrawString(Math.Round(designer.raw.Max, 1), font, Brushes.Black, New Point(left, y))

                For i As Integer = designer.Depth - 1 To 0 Step -1
                    Call g.FillRectangle(
                        New SolidBrush(designer.Colors(i)),
                        New RectangleF(New PointF(margin.Width, y),
                                      New SizeF(rectWidth, d)))
                    y += d
                Next

                fSize = g.MeasureString(designer.raw.Min, font)
                Call g.DrawString(
                    Math.Round(designer.raw.Min, 1),
                    font,
                    Brushes.Black,
                    New Point(left, If(designer.Depth > 100, d, 0) + y - fSize.Height))

                y = size.Height - margin.Height - grayHeight
                fSize = g.MeasureString("Unknown", font)
                pt = New Point(
                    left,
                    y - (grayHeight - fSize.Height) / 2)
                Call g.FillRectangle(
                    Brushes.LightGray,
                    New Rectangle(New Point(margin.Width, y),
                                  New Size(rectWidth, grayHeight)))
                Call g.DrawString("Unknown", font, Brushes.Black, pt)
            End Sub)
    End Function
End Module
