Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.GIS
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.SVG

Module Program

    Sub New()
        Dim template As String = App.HOME & "/Templates/Example.csv"

        If Not template.FileExists Then
            Call {
                New Data With {.state = "CN", .value = 255},
                New Data With {.state = "Pakistan", .value = 120},
                New Data With {.state = "US", .value = 1},
                New Data With {.state = "GB", .value = 100},
                New Data With {.state = "JP", .value = 69, .color = "red"},
                New Data With {.state = "RUS", .value = 300, .color = "lime"}
            }.SaveTo(template)
        End If
    End Sub

    Public Function Main() As Integer
        Return GetType(CLI).RunCLI(App.CommandLine)
    End Function
End Module
