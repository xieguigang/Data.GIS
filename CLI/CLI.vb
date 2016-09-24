Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.GIS
Imports Microsoft.VisualBasic.Imaging.SVG

Module CLI

    Sub New()
        Dim template As String = App.HOME & "/Templates/test.csv"

        If Not template.FileExists Then
            Call {
                New Data With {.state = "CN", .value = 255},
                New Data With {.state = "US", .value = 1}
            }.SaveTo(template)
        End If
    End Sub

    <ExportAPI("/Rendering",
               Usage:="/Rendering /in <data.csv> [/map.levels <512> /map <map.svg> /iso_3166 <iso_3166.csv> /out <out.svg>]")>
    Public Function Rendering(args As CommandLine) As Integer
        Dim [in] As String = args("/in")
        Dim map As String = args("/map")
        Dim iso_3166 As String = args("/iso_3166")
        Dim levels As Integer = args.GetValue("/map.levels", 512)
        Dim out As String = args.GetValue(
            "/out",
            [in].TrimSuffix & $".rendering;levels={levels}.svg")
        Dim data As IEnumerable(Of Data) = [in].LoadCsv(Of Data)
        Dim svg As SVGXml = data.Rendering(levels, mapTemplate:=map.ReadAllText(throwEx:=False))
        Return svg.SaveAsXml(out).CLICode
    End Function
End Module
