Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.SVG.XML
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Language.Default
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Html

Public Module ColorRender

    ''' <summary>
    ''' <see cref="SVGXml"/>
    ''' </summary>
    ReadOnly BlankMap_World6 As [Default](Of String)

    Public ReadOnly Property statDict As Dictionary(Of String, String)

    Const ManualResourceWarning As String = "Sattlite assembly would not working on Linux/Mac platform, please manual setup the resource."

    Sub New()
        If App.IsMicrosoftPlatform Then
            Dim res As New ResourcesSatellite(GetType(ColorRender))
            Dim ISO_3166_1 As String = res.GetString(NameOf(ISO_3166_1))

            SetISO_3166(ImportsData(Of ISO_3166)(ISO_3166_1,))
            BlankMap_World6 = res.GetString(NameOf(BlankMap_World6))
        Else
            Call ManualResourceWarning.Warning
        End If
    End Sub

    <Extension> Public Sub SetISO_3166(__iso_3166 As IEnumerable(Of ISO_3166))
        _statDict = (From x As ISO_3166
                     In __iso_3166
                     Select {
                         x.name.ToLower,
                         x.alpha2,
                         x.alpha3,
                         x.code
                     }.Select(Function(code) New With {
                         .code = code,
                         .alpha2 = x.alpha2
                         })).IteratesALL _
                            .ToDictionary(
                            Function(x) x.code,
                            Function(x) x.alpha2)
    End Sub

    Const Russian As String = "Russian Federation"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="mapLevels"></param>
    ''' <param name="mapTemplate">Using this parameter for custom map template.(自定义的地图模板)</param>
    ''' <returns></returns>
    <Extension>
    Public Function Rendering(data As IEnumerable(Of Data),
                              Optional mapLevels As Integer = 256,
                              Optional mapTemplate As String = Nothing,
                              Optional mapName As String = Nothing,
                              Optional ByRef legend As Bitmap = Nothing,
                              Optional title As String = "Legend title") As SVGXml

        Dim renderedMap As SVGXml = SVGXml.TryLoad(mapTemplate Or BlankMap_World6)
        Dim designer As New ColorDesigner(data, mapName, mapLevels)

        Call renderedMap.Reset(Color.LightGray)

        For Each state As Data In designer.data
            Dim c As SVG.XML.node = renderedMap.__country(state.state)

            If c Is Nothing Then
                Continue For
            End If

            Dim mapsColor As Color = designer.GetColor(state.value)
            Dim color As Color = If(
                String.IsNullOrEmpty(state.color),
                mapsColor,
                state.color.ToColor(
                onFailure:=mapsColor))
            Call c.FillColor(color.RGBExpression)
        Next

        ' Call renderedMap.CSSRender(designer)
        ' 2016-9-26, bugs fixed when removes the transform for russian country, not sure why this happened???
        DirectCast(renderedMap.__country(Russian), g).transform = Nothing

        legend = designer.DrawLegend(title).AsGDIImage
        ' 将所生成legend图片镶嵌进入SVG矢量图之中
        renderedMap.images = {
            New SVG.XML.Image(legend) With {
                .height = legend.Height * 0.5,
                .width = legend.Width * 0.5,
                .x = .width / 2,
                .y = renderedMap.height - .height
            }
        }

        Return renderedMap
    End Function

    <Extension>
    Public Sub CSSRender(ByRef map As SVGXml, designer As ColorDesigner)
        Dim stateLevels = (From stat As Data
                           In designer.data
                           Where String.IsNullOrEmpty(stat.color)
                           Select stat.state,
                              lv = designer.mappings(stat.value)
                           Group By lv Into Group).ToDictionary(
                                Function(x) x.lv,
                                Function(x)
                                    Return x.Group.Select(Function(c) c.state).ToArray
                                End Function)
        Dim css As New StringBuilder(map.style)

        For Each level In stateLevels
            Dim value As Color = designer.GetColor(level.Key)
            Dim color As String =
                ColorTranslator.ToHtml(value)
            Dim attrs As New Dictionary(Of String, String) From {
                {
                    "fill", LCase(color)
                }
            }
            Dim fill As String = XmlMeta.CSS.Generator(
                level.Value _
                    .Select(AddressOf alpha2) _
                    .Select(AddressOf LCase),
                attrs)

            Call css.AppendLine(fill)
        Next

        map.style = css.ToString
    End Sub

    <Extension>
    Public Sub Reset(ByRef svg As SVGXml, baseColor As Color)
        Dim color As String = baseColor.RGBExpression

        For Each g As g In svg.Layers.SafeQuery
            Call g.FillColor(color)
        Next
        For Each path As path In svg.path.SafeQuery
            Call path.FillColor(color)
        Next
    End Sub

    <Extension> Public Sub FillColor(ByRef g As SVG.XML.node, color As String)
        g.style = $"fill: {color};"  ' path/g

        If TypeOf g Is g Then
            Dim x As g = DirectCast(g, g)

            For Each [sub] As g In x.Layers.SafeQuery
                Call [sub].FillColor(color)
            Next

            For Each path As path In x.path.SafeQuery
                path.style = g.style
            Next
        End If
    End Sub

    Public Function alpha2(term As String) As String
        Return If(statDict.ContainsKey(term),
            statDict(term),
            statDict.TryGetValue(term.ToLower))
    End Function

    ''' <summary>
    ''' thanks to the XML/HTML style of the SVG (and Nathan’s explanation) I can create CSS classes per country 
    ''' (the polygons of each country uses the alpha-2 country code as a class id)
    ''' </summary>
    ''' <param name="map"></param>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <Extension>
    Private Function __country(map As SVGXml, code As String) As SVG.XML.node
        Dim alpha2 As String = ColorRender.alpha2(term:=code)
        Dim c As SVG.XML.node = map.Layers.__country(alpha2)

        If c Is Nothing Then
            c = map.path.__country(alpha2)

            If c Is Nothing Then
                Call $"Unable found Object named '{code}'!".PrintException
            End If
        End If

        Return c
    End Function

    <Extension>
    Private Function __country(subs As path(), alpha2 As String) As path
        For Each path As path In subs.SafeQuery
            If path.id.TextEquals(alpha2) Then
                Return path
            End If
        Next

        Return Nothing
    End Function

    <Extension>
    Private Function __country(subs As g(), alpha2 As String) As SVG.XML.node
        Dim state As New Value(Of SVG.XML.node)

        For Each c As g In subs
            If alpha2.TextEquals(c.id) Then
                Return c
            Else
                If c.Layers.IsNullOrEmpty Then
                    Continue For
                End If
            End If

            If Not (state = c.Layers.__country(alpha2)) Is Nothing Then
                Return state
            End If

            If Not (state = c.path.__country(alpha2)) Is Nothing Then
                Return state  ' fix error for GF island
            End If
        Next

        Return Nothing
    End Function
End Module
