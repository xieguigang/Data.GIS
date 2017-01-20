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

    Public Function MatchRegion(text$) As Boolean
        If InStr(text, Province) > 0 Then
            Return True
        ElseIf InStr(text, CityName) > 0 Then
            Return True
        ElseIf InStr(text, District) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function LoadData(Optional stripSuffix As Boolean = False) As Region()
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
            Dim t As String() =
                If(stripSuffix, __stripSuffix(line), line) _
                .StringSplit("\s+") _
                .Where(Function(s) Not s.IsBlank) _
                .ToArray
            Dim code$ = t(Scan0)
            Dim h = code.Split(2)
            Dim pcode As New String(h(0))
            Dim ccode As New String(h(1))
            Dim region$ = t(1)
            Dim createRegionData As Boolean = False

            If pcode <> code_header(0) Then
                ' 变换了省份
                prov = region
                city = region
                code_header = {pcode, ccode}
                createRegionData = True

                Continue For
            End If

            If ccode <> code_header(1) Then
                code_header(1) = ccode

                If createRegionData = True Then
                    ' 变换了市名称
                    city = region
                    createRegionData = False
                Else
                    out += New Region With {
                        .CityName = prov,
                        .Province = prov,
                        .District = region,
                        .Code = code
                    }

                    city = region
                End If

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

    Private Shared Function __stripSuffix(s$) As String
        s = s.StripBlank
        s = s.Replace("自治区", "")
        s = s.Replace("自治县", "")
        s = s.Replace("特别行政区", "")
        s = s.TrimEnd("市", "县", "区", "省")
        Return Trim(s)
    End Function
End Class
