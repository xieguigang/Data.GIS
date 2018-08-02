Imports System.Drawing
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.ManView
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.GIS
Imports Microsoft.VisualBasic.Imaging.SVG.XML
Imports Microsoft.VisualBasic.Scripting.MetaData

<Package("GIS_rendering.Utilities.CLI",
                  Description:="Data visualization for the information about and appraisal of an epidemic or human population genetics data associated with GIS data by using VisualBasic language hybrids with SVG/CSS.",
                  Category:=APICategories.CLI_MAN,
                  Publisher:="xie.guigang@gcmodeller.org")>
<ExceptionHelp(Documentation:="https://github.com/xieguigang/Data.GIS/tree/master/Coloring_an_SVG_map_to_a_numeric_variable",
               EMailLink:="xie.guigang@gcmodeller.org",
               Debugging:="http://blog.xieguigang.me/")>
Public Module CLI

    <ExportAPI("/Rendering",
               Usage:="/Rendering /in <data.csv> [/main <title> /legend.title <legend title> /map.levels <512> /map <map.svg> /iso_3166 <iso_3166.csv> /map.Name <default> /out <out.svg>]")>
    <Argument("/in", False,
              AcceptTypes:={GetType(Data)},
              Description:="A data file template example can be found in the ./Templates/ folder.")>
    <Argument("/map.levels", True,
              AcceptTypes:={GetType(Integer)},
              Description:="Any positive integer value, this will adjust the color depth for the value mappings.")>
    <Argument("/map", True,
              Description:="User custom map svg, please note that the id attribute of the ``g`` or ``path`` object in the svg stands for the country for region should be the iso-3166-1 alpha2 code.")>
    <Argument("/map.Name", True,
              AcceptTypes:={GetType(String)},
              Description:="The color map pattern profile name, the VisualBasic language build in color patterns name can be found at github: https://github.com/xieguigang/sciBASIC/tree/master/gr
                   And this value is set as ``default`` if this parameter is not specified, in this situation, the parameter value of /map.levels is only allowd 256 or 512.")>
    Public Function Rendering(args As CommandLine) As Integer
        Dim [in] As String = args("/in")
        Dim map As String = args("/map")
        Dim iso_3166 As String = args("/iso_3166")
        Dim levels As Integer = args.GetValue("/map.levels", 512)
        Dim mapName As String = args.GetValue("/map.Name", "default")
        Dim main As String = args("/main")
        Dim out As String = args.GetValue(
            "/out",
            [in].TrimSuffix & $".rendering;levels={levels},map.Name={mapName.NormalizePathString}.svg")
        Dim data As IEnumerable(Of Data) = [in].LoadCsv(Of Data)
        Dim legend As Bitmap = Nothing
        Dim legendTitle As String = args.GetValue("/legend.title", "Legend title")

        If iso_3166.FileExists Then
            Call iso_3166.LoadCsv(Of ISO_3166).SetISO_3166
        End If

        Dim svg As SVGXml = data.Rendering(
            levels,
            mapTemplate:=map.ReadAllText(throwEx:=False, suppress:=True),
            mapName:=mapName,
            legend:=legend,
            title:=legendTitle)

        If Not String.IsNullOrEmpty(main) Then
            svg.title = main
        End If

        Return svg.SaveAsXml(out).CLICode
    End Function
End Module
