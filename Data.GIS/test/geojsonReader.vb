Imports Microsoft.VisualBasic.Data.GIS.GeoMap
Imports Microsoft.VisualBasic.Data.GIS.GeoMap.geojson
Imports Microsoft.VisualBasic.MIME.application
Imports Microsoft.VisualBasic.MIME.application.json

Module geojsonReader

    Sub Main()
        Call renderSVG("E:\Data.GIS\geojson-map-china\geometryCouties\140300.json")

        Pause()
    End Sub

    Sub loadTest()
        Dim geo = json.ParseJsonFile("E:\Data.GIS\geojson-map-china\geometryCouties\140300.json").CreateObject(GetType(FeatureCollection))

        Pause()
    End Sub

    Sub renderSVG(file$)
        Call GeoMapRender.Render("E:\Data.GIS\geojson-map-china\geometryCouties\140300.json").Save("./test.svg")
    End Sub

End Module
