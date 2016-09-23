Imports Microsoft.VisualBasic.Serialization.JSON

Public Class ISO_3166

    Public Property name As String
    Public Property alpha2 As String
    Public Property alpha3 As String
    Public Property code As String

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function
End Class
