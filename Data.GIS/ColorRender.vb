Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.SoftwareToolkits
Imports Microsoft.VisualBasic.Data.csv

Public Module ColorRender

    ReadOnly __iso_3166 As ISO_3166()
    ReadOnly __worldMap As SVGXml

    Sub New()
        Dim res As New Resources(GetType(ColorRender))
        Dim ISO_3166_1 As String = res.GetString(NameOf(ISO_3166_1))
        Dim BlankMap_World6 As String = res.GetString(NameOf(BlankMap_World6))

        __iso_3166 = ImportsData(Of ISO_3166)(ISO_3166_1,)
        __worldMap = BlankMap_World6 _
            .LoadFromXml(Of SVGXml)(False)
    End Sub

    <Extension>
    Public Function Rendering(data As IEnumerable(Of Data)) As SVGXml
        Dim empty As SVGXml = __worldMap
    End Function
End Module
