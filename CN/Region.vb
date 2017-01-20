Imports Microsoft.VisualBasic.Serialization.JSON

Public Class Region

    Public Property Code As String
    Public Property Province As String
    Public Property CityName As String
    Public Property District As String

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function
End Class
