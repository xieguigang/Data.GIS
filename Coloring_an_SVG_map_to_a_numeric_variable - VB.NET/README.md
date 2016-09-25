# Rendering GIS data on world map

The new VB.NET language that applied in data science.

> For the furthering developement of this program, install the Microsoft VisualBasic CLI App runtime from nuget at first:
> ```
> PM> Install-Package VB_AppFramework
> ```
> And then add reference to these dll modules:
>
> + Microsoft.VisualBasic.Architecture.Framework_v3.0_22.0.76.201__8da45dcd8060cc9a.dll (VB.NET runtime core)
> + Microsoft.VisualBasic.Data.Csv.dll
> + Microsoft.VisualBasic.MIME.Markup.dll
> + Microsoft.VisualBasic.Imaging.dll

### 1. The blank world map SVG

![](./BlankMap-World6_svg.png)
> source from wiki: [File:**BlankMap-World6.svg**](https://en.wikipedia.org/wiki/Wikipedia:Blank_maps#/media/File:BlankMap-World6.svg)

Here is the blank map data that i download from wiki and makes some tweaks: removes the small region circle to solve the GIMP rendering problem and removes the oceans drawing Polygon path data, so that the final map it looks like:

![](./BlankMap-World6_modified.png)

This modified blank map svg data can be found at my github repository: https://github.com/xieguigang/Data.GIS/blob/master/data/BlankMap-World6.svg

### 2. How to rendering the colors on SVG Polygon?

### 3. Level Mappings

By measure the difference of value ``x`` with the minimum value of the vector elements, and then calculate the proportion by Divided the difference with the extreme distance of the max and min value in the input vector, that we can do a linear scale mapping of the input data:

```vbnet
For Each x As Double In array
    Dim lv As Integer = CInt(Level * (x - MinValue) / d)
    chunkBuf(++i) = lv + offset
Next
```

Here is the linear mapping function that defined in VisualBasic:
Microsoft.VisualBasic.Mathematical::[ScaleMaps.GenerateMapping(System.Collections.Generic.IEnumerable(Of Double), Integer, Integer) As Integer()](https://github.com/xieguigang/VisualBasic_AppFramework/blob/master/Microsoft.VisualBasic.Architecture.Framework/Extensions/Math/ScaleMaps.vb)

```vbnet
''' <summary>
''' Linear mappings the vector elements in to another scale within specifc range from
''' parameter <paramref name="Level"></paramref>.
''' (如果每一个数值之间都是相同的大小，则返回原始数据，因为最大值与最小值的差为0，无法进行映射的创建
''' （会出现除0的错误）)
''' </summary>
''' <param name="data">Your input numeric vector.</param>
''' <param name="Level">The scaler range.</param>
''' <returns></returns>
''' <remarks>为了要保持顺序，不能够使用并行拓展</remarks>
''' <param name="offset">
''' The default scaler range output is [1, <paramref name="Level"></paramref>],
''' but you can modify this parameter value for moving the range to
''' [<paramref name="offset"></paramref>, <paramref name="Level"></paramref> +
''' <paramref name="offset"></paramref>].
''' (默认是 [1, <paramref name="Level"></paramref>]，当offset的值为0的时候，则为
''' [0, <paramref name="Level"></paramref>-1]，当然这个参数也可以使其他的值)
''' </param>
<ExportAPI("Ranks.Mapping")>
<Extension> Public Function GenerateMapping(data As IEnumerable(Of Double), Optional Level As Integer = 10, Optional offset As Integer = 1) As Integer()
    Dim array As Double() = data.ToArray
    Dim MinValue As Double = array.Min
    Dim MaxValue As Double = array.Max
    Dim d As Double = MaxValue - MinValue

    If d = 0R Then ' 所有的值都是一样的，则都是同等级的
        Return 1.CopyVector(array.Length)
    End If

    Dim chunkBuf As Integer() = New Integer(array.Length - 1) {}
    Dim i As int = 0

    For Each x As Double In array
        Dim lv As Integer = CInt(Level * (x - MinValue) / d)
        chunkBuf(++i) = lv + offset
    Next

    Return chunkBuf
End Function
```