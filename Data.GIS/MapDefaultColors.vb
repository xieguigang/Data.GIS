﻿Imports System.Drawing
Imports Microsoft.VisualBasic.Linq

Public Module MapDefaultColors

    ''' <summary>
    ''' 512
    ''' </summary>
    Public ReadOnly HexDefaults512 As String() = {
        "#CCEBC5", "#CCEBC5", "#CBEBC5", "#CBEBC4", "#CBEBC4", "#CAEAC4", "#CAEAC4", "#CAEAC3", "#C9EAC3", "#C9EAC3", "#C9EAC3", "#C8EAC3", "#C8EAC2", "#C8E9C2", "#C8E9C2", "#C7E9C2", "#C7E9C1", "#C7E9C1", "#C6E9C1", "#C6E9C1", "#C6E9C1", "#C5E9C0", "#C5E8C0",
        "#C5E8C0", "#C4E8C0", "#C4E8C0", "#C4E8BF", "#C3E8BF", "#C3E8BF", "#C3E8BF", "#C2E7BE", "#C2E7BE", "#C2E7BE", "#C1E7BE", "#C1E7BE", "#C1E7BD", "#C0E7BD", "#C0E7BD", "#C0E6BD", "#BFE6BD", "#BFE6BD", "#BFE6BC", "#BEE6BC", "#BEE6BC", "#BEE6BC", "#BDE6BC",
        "#BDE5BB", "#BDE5BB", "#BCE5BB", "#BCE5BB", "#BCE5BB", "#BBE5BA", "#BBE5BA", "#BBE4BA", "#BAE4BA", "#BAE4BA", "#BAE4BA", "#B9E4B9", "#B9E4B9", "#B8E4B9", "#B8E4B9", "#B8E3B9", "#B7E3B9", "#B7E3B9", "#B7E3B8", "#B6E3B8", "#B6E3B8", "#B6E3B8", "#B5E2B8",
        "#B5E2B8", "#B5E2B8", "#B4E2B7", "#B4E2B7", "#B3E2B7", "#B3E2B7", "#B3E1B7", "#B2E1B7", "#B2E1B7", "#B2E1B7", "#B1E1B7", "#B1E1B6", "#B0E0B6", "#B0E0B6", "#B0E0B6", "#AFE0B6", "#AFE0B6", "#AEE0B6", "#AEE0B6", "#AEDFB6", "#ADDFB6", "#ADDFB6", "#ADDFB6",
        "#ACDFB5", "#ACDFB5", "#ABDEB5", "#ABDEB5", "#ABDEB5", "#AADEB5", "#AADEB5", "#A9DEB5", "#A9DDB5", "#A8DDB5", "#A8DDB5", "#A8DDB5", "#A7DDB5", "#A7DDB5", "#A6DCB5", "#A6DCB5", "#A6DCB5", "#A5DCB5", "#A5DCB5", "#A4DBB5", "#A4DBB5", "#A3DBB5", "#A3DBB5",
        "#A3DBB5", "#A2DAB5", "#A2DAB5", "#A1DAB5", "#A1DAB5", "#A0DAB5", "#A0DAB5", "#9FD9B5", "#9FD9B5", "#9FD9B5", "#9ED9B5", "#9ED9B5", "#9DD8B6", "#9DD8B6", "#9CD8B6", "#9CD8B6", "#9BD8B6", "#9BD7B6", "#9BD7B6", "#9AD7B6", "#9AD7B6", "#99D7B6", "#99D7B6",
        "#98D6B6", "#98D6B7", "#97D6B7", "#97D6B7", "#96D6B7", "#96D5B7", "#96D5B7", "#95D5B7", "#95D5B7", "#94D5B8", "#94D4B8", "#93D4B8", "#93D4B8", "#92D4B8", "#92D4B8", "#91D4B8", "#91D3B9", "#90D3B9", "#90D3B9", "#90D3B9", "#8FD3B9", "#8FD2B9", "#8ED2BA",
        "#8ED2BA", "#8DD2BA", "#8DD2BA", "#8CD2BA", "#8CD1BA", "#8BD1BB", "#8BD1BB", "#8BD1BB", "#8AD1BB", "#8AD1BB", "#89D0BC", "#89D0BC", "#88D0BC", "#88D0BC", "#87D0BD", "#87D0BD", "#87CFBD", "#86CFBD", "#86CFBD", "#85CFBE", "#85CFBE", "#84CFBE", "#84CFBE",
        "#83CEBF", "#83CEBF", "#83CEBF", "#82CEBF", "#82CEC0", "#81CEC0", "#81CEC0", "#80CDC0", "#80CDC1", "#80CDC1", "#7FCDC1", "#7FCDC1", "#7ECDC2", "#7ECDC2", "#7ECDC2", "#7DCDC2", "#7DCCC3", "#7CCCC3", "#7CCCC3", "#7CCCC4", "#7BCCC4", "#7BCCC4", "#7ACCC4",
        "#7ACCC5", "#7ACCC5", "#79CCC5", "#79CCC6", "#78CBC6", "#78CBC6", "#78CBC7", "#77CBC7", "#77CBC7", "#77CBC7", "#76CBC8", "#76CBC8", "#75CBC8", "#75CBC9", "#75CBC9", "#74CBC9", "#74CBCA", "#74CACA", "#73CACA", "#73CACB", "#72CACB", "#72CACB", "#72CACB",
        "#71CACC", "#71CACC", "#71CACC", "#70CACD", "#70CACD", "#70CACD", "#6FCACE", "#6FC9CE", "#6EC9CE", "#6EC9CE", "#6EC9CF", "#6DC9CF", "#6DC9CF", "#6DC9D0", "#6CC9D0", "#6CC9D0", "#6BC8D0", "#6BC8D1", "#6BC8D1", "#6AC8D1", "#6AC8D1", "#69C8D2", "#69C8D2",
        "#69C7D2", "#68C7D2", "#68C7D2", "#67C7D3", "#67C7D3", "#67C7D3", "#66C6D3", "#66C6D3", "#65C6D4", "#65C6D4", "#65C6D4", "#64C5D4", "#64C5D4", "#63C5D4", "#63C5D5", "#62C4D5", "#62C4D5", "#62C4D5", "#61C4D5", "#61C3D5", "#60C3D5", "#60C3D5", "#5FC2D5",
        "#5FC2D6", "#5EC2D6", "#5EC1D6", "#5DC1D6", "#5DC1D6", "#5CC0D6", "#5CC0D6", "#5BC0D6", "#5BBFD6", "#5ABFD6", "#5ABED6", "#59BED6", "#59BDD6", "#58BDD6", "#58BDD6", "#57BCD6", "#57BCD5", "#56BBD5", "#56BBD5", "#55BAD5", "#54BAD5", "#54B9D5", "#53B8D5",
        "#53B8D5", "#52B7D4", "#51B7D4", "#51B6D4", "#50B5D4", "#50B5D4", "#4FB4D3", "#4EB3D3", "#4EB3D3", "#4DB2D3", "#4CB1D2", "#4CB1D2", "#4BB0D2", "#4AAFD1", "#4AAED1", "#49AED1", "#48ADD0", "#48ACD0", "#47ABD0", "#46AACF", "#46A9CF", "#45A9CE", "#44A8CE",
        "#43A7CE", "#43A6CD", "#42A5CD", "#41A4CC", "#40A3CC", "#40A2CB", "#3FA2CB", "#3EA1CA", "#3DA0CA", "#3D9FC9", "#3C9EC9", "#3B9DC8", "#3A9CC8", "#3A9BC7", "#399AC7", "#3899C6", "#3798C6", "#3697C5", "#3696C5", "#3595C4", "#3494C3", "#3393C3", "#3392C2",
        "#3291C2", "#3190C1", "#308FC0", "#2F8EC0", "#2F8DBF", "#2E8CBF", "#2D8BBE", "#2C8ABD", "#2C89BD", "#2B88BC", "#2A87BC", "#2986BB", "#2885BA", "#2884BA", "#2783B9", "#2682B8", "#2581B8", "#2580B7", "#247FB7", "#237EB6", "#227DB5", "#227CB5", "#217BB4",
        "#207AB4", "#1F79B3", "#1F78B2", "#1E77B2", "#1D76B1", "#1D75B0", "#1C74B0", "#1B73AF", "#1A72AF", "#1A71AE", "#1970AD", "#186FAD", "#186EAC", "#176DAC", "#166CAB", "#166BAB", "#156AAA", "#156AA9", "#1469A9", "#1368A8", "#1367A8", "#1266A7", "#1165A7",
        "#1164A6", "#1064A6", "#1063A5", "#0F62A5", "#0F61A4", "#0E61A4", "#0E60A3", "#0D5FA3", "#0C5EA2", "#0C5EA2", "#0B5DA1", "#0B5CA1", "#0B5CA0", "#0A5BA0", "#0A5A9F", "#095A9F", "#09599F", "#08589E", "#08589E", "#08579E", "#07579D", "#07569D", "#06569D",
        "#06559C", "#06559C", "#05549C", "#05549B", "#05539B", "#04539B", "#04539B", "#04529A", "#04529A", "#03529A", "#03519A", "#035199", "#035199", "#035099", "#025099", "#025099", "#024F98", "#024F98", "#024F98", "#014F98", "#014F98", "#014E98", "#014E98",
        "#014E97", "#014E97", "#014E97", "#014E97", "#014D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97", "#004D97",
        "#004D97", "#004D97", "#004D97", "#004D97", "#014D97", "#014E97", "#014E97", "#014E97", "#014E97", "#014E98", "#014E98", "#014E98", "#014F98", "#014F98", "#024F98", "#024F98", "#024F98", "#024F98", "#025099", "#025099", "#025099", "#025099", "#035099",
        "#035199", "#035199", "#035199", "#03519A", "#03529A", "#04529A", "#04529A", "#04529A", "#04539A", "#04539B", "#04539B", "#05539B", "#05549B", "#05549B", "#05549B", "#05549C", "#06559C", "#06559C", "#06559C", "#06559C", "#06569D", "#07569D", "#07569D",
        "#07579D", "#07579D", "#07579D", "#08579E", "#08589E", "#08589E"
    }

    Public ReadOnly HexDefault256 As String() = {
        "#CCEBC5", "#CBEBC5", "#CBEBC4", "#CAEAC4", "#C9EAC3", "#C9EAC3", "#C8EAC2", "#C7E9C2", "#C7E9C1", "#C6E9C1", "#C6E9C1", "#C5E8C0", "#C4E8C0", "#C4E8BF", "#C3E8BF", "#C2E7BE", "#C2E7BE", "#C1E7BE", "#C0E7BD", "#C0E6BD", "#BFE6BC", "#BEE6BC", "#BEE6BC",
        "#BDE5BB", "#BCE5BB", "#BCE5BB", "#BBE5BA", "#BAE4BA", "#B9E4BA", "#B9E4B9", "#B8E3B9", "#B7E3B9", "#B7E3B8", "#B6E3B8", "#B5E2B8", "#B4E2B7", "#B4E2B7", "#B3E1B7", "#B2E1B7", "#B1E1B7", "#B1E1B6", "#B0E0B6", "#AFE0B6", "#AEE0B6", "#AEDFB6", "#ADDFB6",
        "#ACDFB5", "#ABDEB5", "#AADEB5", "#AADEB5", "#A9DDB5", "#A8DDB5", "#A7DDB5", "#A6DCB5", "#A5DCB5", "#A5DCB5", "#A4DBB5", "#A3DBB5", "#A2DAB5", "#A1DAB5", "#A0DAB5", "#9FD9B5", "#9ED9B6", "#9ED9B6", "#9DD8B6", "#9CD8B6", "#9BD8B6", "#9AD7B6", "#99D7B7",
        "#98D6B7", "#97D6B7", "#96D6B7", "#95D5B7", "#94D5B8", "#94D5B8", "#93D4B8", "#92D4B9", "#91D3B9", "#90D3B9", "#8FD3BA", "#8ED2BA", "#8DD2BA", "#8CD2BB", "#8BD1BB", "#8AD1BB", "#8AD1BC", "#89D0BC", "#88D0BD", "#87D0BD", "#86CFBE", "#85CFBE", "#84CFBE",
        "#83CEBF", "#82CEBF", "#82CEC0", "#81CEC0", "#80CDC1", "#7FCDC1", "#7ECDC2", "#7DCDC2", "#7DCCC3", "#7CCCC3", "#7BCCC4", "#7ACCC5", "#79CCC5", "#79CBC6", "#78CBC6", "#77CBC7", "#76CBC7", "#76CBC8", "#75CBC9", "#74CAC9", "#73CACA", "#73CACA", "#72CACB",
        "#71CACB", "#70CACC", "#70C9CC", "#6FC9CD", "#6EC9CE", "#6DC9CE", "#6DC8CF", "#6CC8CF", "#6BC8D0", "#6AC8D0", "#6AC7D0", "#69C7D1", "#68C7D1", "#67C6D2", "#66C6D2", "#66C6D2", "#65C5D3", "#64C5D3", "#63C4D3", "#62C4D4", "#61C3D4", "#60C3D4", "#60C2D4",
        "#5FC1D5", "#5EC1D5", "#5DC0D5", "#5CBFD5", "#5BBED5", "#5ABED5", "#59BDD5", "#57BCD5", "#56BBD5", "#55BAD5", "#54B9D4", "#53B8D4", "#52B7D4", "#51B6D4", "#4FB4D3", "#4EB3D3", "#4DB2D3", "#4BB0D2", "#4AAFD2", "#49ADD1", "#47ACD1", "#46AAD0", "#44A9CF",
        "#43A7CF", "#41A5CE", "#40A4CD", "#3EA2CC", "#3DA0CB", "#3B9ECB", "#3A9CCA", "#389BC9", "#3799C8", "#3597C7", "#3495C6", "#3293C5", "#3091C4", "#2F8FC3", "#2D8DC2", "#2C8BC1", "#2A89BF", "#2987BE", "#2785BD", "#2683BC", "#2481BB", "#237FBA", "#217DB8",
        "#207BB7", "#1E79B6", "#1D77B5", "#1B75B3", "#1A73B2", "#1971B1", "#176FB0", "#166DAE", "#156CAD", "#136AAC", "#1268AA", "#1166A9", "#1064A8", "#0F63A7", "#0E61A5", "#0D5FA4", "#0C5EA3", "#0B5CA2", "#0A5BA0", "#09599F", "#08589E", "#07579D", "#06559C",
        "#06549A", "#055399", "#055298", "#045197", "#044F96", "#034E95", "#034D94", "#024D93", "#024C91", "#024B90", "#014A8F", "#01498E", "#01488D", "#01488C", "#01478B", "#01468A", "#004689", "#004588", "#004587", "#004486", "#004385", "#014384", "#014383",
        "#014282", "#014281", "#014180", "#01417F", "#01417E", "#02407D", "#02407C", "#02407B", "#02407A", "#033F7A", "#033F79", "#033F78", "#043F77", "#043F76", "#043E75", "#053E74", "#053E73", "#063E72", "#063E71", "#063E70", "#073E6F", "#073D6F", "#083D6E",
        "#083D6D", "#093D6C", "#093D6B"
    }

    Public ReadOnly HexDefault10 As String() = {"#CCEBC5", "#B9E4BA", "#A3DBB5", "#8AD0BC", "#72CACB", "#5BBFD6", "#3595C4", "#0D5FA3", "#004D97", "#08589E"}

    Public ReadOnly Property DefaultColors512 As Color() = HexDefaults512.Select(AddressOf ColorTranslator.FromHtml).ToArray
    Public ReadOnly Property DefaultColors256 As Color() = HexDefault256.Select(AddressOf ColorTranslator.FromHtml).ToArray
    Public ReadOnly Property DefaultColors10 As Color() = HexDefault10.Select(AddressOf ColorTranslator.FromHtml).ToArray

End Module
