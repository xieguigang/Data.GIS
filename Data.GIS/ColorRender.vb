Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.SoftwareToolkits
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Linq

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

    End Function
End Module
