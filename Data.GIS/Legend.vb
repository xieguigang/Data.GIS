Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D

Public Module Legend

    <Extension>
    Public Function DrawLegend(designer As ColorDesigner, title As String, Optional bg As String = "white") As Bitmap
        Return GraphicsPlots(
            New Size(400, 1200), New Size(50, 50), bg,
            Sub(g, grect)
                Dim graphicsRegion As Rectangle = grect.GraphicsRegion
                Dim size As Size = grect.Size
                Dim margin As Size = grect.Margin
                Dim grayHeight As Integer = size.Height * 0.05
                Dim y As Integer
                Dim font As New Font(FontFace.MicrosoftYaHei, 20)
                Dim fSize As SizeF
                Dim pt As Point
                Dim rectWidth As Integer = graphicsRegion.Width * 2 / 3
                Dim legendsHeight As Integer = size.Height - (margin.Height * 2) - grayHeight * 3
                Dim d As Integer = legendsHeight / designer.Depth
                Dim left As Integer = margin.Width + 10 + rectWidth

                y = margin.Height

                Call g.DrawString(designer.raw.Max, font, Brushes.Black, New Point(left, y))

                For i As Integer = designer.Depth - 1 To 0 Step -1
                    Call g.FillRectangle(
                        New SolidBrush(designer.Colors(i)),
                        New Rectangle(New Point(margin.Width, y),
                                      New Size(rectWidth, d)))
                    y += d
                Next

                fSize = g.MeasureString(designer.raw.Min, font)
                Call g.DrawString(designer.raw.Min, font, Brushes.Black, New Point(left, d + y - fSize.Height))

                y = size.Height - margin.Height - grayHeight
                fSize = g.MeasureString("Unknown", font)
                pt = New Point(
                    left,
                    y - (fSize.Height - grayHeight) / 2)
                Call g.FillRectangle(
                    Brushes.LightGray,
                    New Rectangle(New Point(margin.Width, y),
                                  New Size(rectWidth, grayHeight)))
                Call g.DrawString("Unknown", font, Brushes.Black, pt)
            End Sub)
    End Function
End Module
