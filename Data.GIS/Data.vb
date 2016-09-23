Imports Microsoft.VisualBasic.Serialization.JSON

Public Class Data

    ''' <summary>
    ''' ISO 3166-1 alpha2/alpha3/code
    ''' </summary>
    ''' <returns></returns>
    Public Property state As String
    Public Property value As Double

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function
End Class
