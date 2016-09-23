Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.SoftwareToolkits

Public Module ColorRender

    Sub New()
        Dim res As New Resources(GetType(ColorRender))
        Dim ISO_3166_1 As String = res.GetString(NameOf(ISO_3166_1))
    End Sub

    <Extension>
    Public Function Rendering(data As IEnumerable(Of Data)) As SVGXml

    End Function
End Module
