Imports Microsoft.VisualBasic.Data.GIS.geojson
Imports Microsoft.VisualBasic.MIME.application
Imports Microsoft.VisualBasic.MIME.application.json

Module geojsonReader

    Sub Main()
        Dim geo = json.ParseJsonFile("E:\Data.GIS\geojson-map-china\geometryCouties\140300.json").CreateObject(GetType(FeatureCollection))

        Pause()
    End Sub

End Module
