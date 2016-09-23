Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.SoftwareToolkits
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Language

Public Module ColorRender

    ReadOnly __iso_3166 As ISO_3166()
    ReadOnly __worldMap As SVGXml
    ReadOnly statDict As Dictionary(Of String, String)

    Sub New()
        Dim res As New Resources(GetType(ColorRender))
        Dim ISO_3166_1 As String = res.GetString(NameOf(ISO_3166_1))
        Dim BlankMap_World6 As String = res.GetString(NameOf(BlankMap_World6))

        __iso_3166 = ImportsData(Of ISO_3166)(ISO_3166_1,)
        __worldMap = BlankMap_World6 _
            .LoadFromXml(Of SVGXml)(False)

        statDict = (From x As ISO_3166
                    In __iso_3166
                    Select {
                        x.alpha2,
                        x.alpha3,
                        x.code}.Select(Function(code) New With {
                            .code = code,
                            .alpha2 = x.alpha2
                        })).MatrixAsIterator.ToDictionary(
                            Function(x) x.code,
                            Function(x) x.alpha2)
    End Sub

    <Extension>
    Public Function Rendering(data As IEnumerable(Of Data)) As SVGXml
        Dim empty As SVGXml = __worldMap

        For Each state As Data In data
            Dim c As g = empty.__country(state.state)
        Next

        Return empty
    End Function

    ''' <summary>
    ''' thanks to the XML/HTML style of the SVG (and Nathan’s explanation) I can create CSS classes per country 
    ''' (the polygons of each country uses the alpha-2 country code as a class id)
    ''' </summary>
    ''' <param name="map"></param>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <Extension>
    Private Function __country(map As SVGXml, code As String) As g
        Dim c As g = map.gs.__country(alpha2:=statDict(code))

        If c Is Nothing Then
            Throw New NullReferenceException($"Unable found object named '{code}'!")
        Else
            Return c
        End If
    End Function

    <Extension>
    Private Function __country(subs As g(), alpha2 As String) As g
        Dim state As New Value(Of g)

        For Each c As g In subs
            If String.Equals(alpha2, c.[class], StringComparison.OrdinalIgnoreCase) Then
                Return c
            End If

            If Not (state = c.gs.__country(alpha2)) Is Nothing Then
                Return state
            End If
        Next

        Return Nothing
    End Function
End Module
