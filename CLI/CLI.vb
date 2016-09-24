Imports System.Drawing
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.GIS
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.SVG

Module CLI

    <ExportAPI("/Rendering",
               Usage:="/Rendering /in <data.csv> [/map.levels <512> /map <map.svg> /map.Name <default> /iso_3166 <iso_3166.csv> /out <out.svg>]")>
    Public Function Rendering(args As CommandLine) As Integer
        Dim [in] As String = args("/in")
        Dim map As String = args("/map")
        Dim iso_3166 As String = args("/iso_3166")
        Dim levels As Integer = args.GetValue("/map.levels", 512)
        Dim mapName As String = args.GetValue("/map.Name", "default")
        Dim out As String = args.GetValue(
            "/out",
            [in].TrimSuffix & $".rendering;levels={levels},map.Name={mapName}.svg")
        Dim data As IEnumerable(Of Data) = [in].LoadCsv(Of Data)
        Dim legend As Bitmap = Nothing
        Dim svg As SVGXml = data.Rendering(
            levels,
            mapTemplate:=map.ReadAllText(throwEx:=False, suppress:=True),
            mapName:=mapName,
            legend:=legend)

        Call legend.SaveAs(out.TrimSuffix & "-legend.png")

        Return svg.SaveAsXml(out).CLICode
    End Function
End Module
