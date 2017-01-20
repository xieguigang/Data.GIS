Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Language

Public Class Region

    Public Property Code As String
    Public Property Province As String
    Public Property CityName As String
    Public Property District As String

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function

    Public Shared Function LoadData() As Region()
        Dim text$ = My.Resources.cn
        Dim lines$() = text _
            .lTokens _
            .Where(Function(s) Not s.IsBlank AndAlso Not s.First = "#"c) _
            .Skip(1) _
            .ToArray

        Dim out As New List(Of Region)
        Dim prov$ = "", city$ = ""
        Dim code_header$() = {"", ""}

        For Each line As String In lines
            Dim t As String() = line _
                .StringSplit("\s+") _
                .Where(Function(s) Not s.IsBlank) _
                .ToArray
            Dim code$ = t(Scan0)
            Dim h = code.Split(2)
            Dim pcode = h(0) & h(1)
            Dim ccode = h(2) & h(3)
            Dim region$ = t(1)

            If pcode <> code_header(0) Then
                ' 变换了省份
                prov = region
                city = region
                code_header = {pcode, ccode}

                Continue For
            End If

            If ccode <> code_header(1) Then
                ' 变换了市名称
                city = region
                code_header(1) = ccode

                Continue For
            End If

            ' 县和地区
            out += New Region With {
                .CityName = city,
                .Code = code,
                .District = region,
                .Province = prov
            }
        Next

        Return out
    End Function
End Class
