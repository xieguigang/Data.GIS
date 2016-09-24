# GIS_render [version 1.0.0.0]
> Data visualization for the information about and appraisal of an epidemic or human population genetics data associated with GIS data by using VisualBasic language hybrids with SVG/CSS.

<!--more-->

**GIS_render CLI tool**
_Data visualization for the information about and appraisal of an epidemic or human population genetics data associated with GIS data by using VisualBasic language hybrids with SVG/CSS._
Copyright © http://xieguigang.me 2016

**Module AssemblyName**: file:///G:/Data.GIS/CLI/bin/Release/GIS_render.exe
**Root namespace**: ``GIS_render.CLI``

------------------------------------------------------------
If you are having trouble debugging this Error, first read the best practices tutorial for helpful tips that address many common problems:
> https://github.com/xieguigang/Data.GIS/tree/master/Coloring_an_SVG_map_to_a_numeric_variable


The debugging facility Is helpful To figure out what's happening under the hood:
> http://blog.xieguigang.me/


If you're still stumped, you can try get help from author directly from E-mail:
> xie.guigang@gcmodeller.org



All of the command that available in this program has been list below:

|Function API|Info|
|------------|----|
|[/Rendering](#/Rendering)||


## CLI API list
--------------------------
<h3 id="/Rendering"> 1. /Rendering</h3>


**Prototype**: ``GIS_render.CLI::Int32 Rendering(Microsoft.VisualBasic.CommandLine.CommandLine)``

###### Usage
```bash
GIS_render /Rendering /in <data.csv> [/main <title> /legend.title <legend title> /map.levels <512> /map <map.svg> /map.Name <default> /out <out.svg>]
```
###### Example
```bash
GIS_render
```



#### Parameters information:
##### /in
A data file template example can be found in the ./Templates/ folder.

###### Example
```bash

```
##### [/map.levels]
Any positive integer value, this will adjust the color depth for the value mappings.

###### Example
```bash

```
##### [/map]
User custom map svg, please note that the id attribute of the ``g`` or ``path`` object in the svg stands for the country for region should be the iso-3166-1 alpha2 code.

###### Example
```bash

```
##### [/map.Name]
The color map pattern profile name, the VisualBasic language build in color patterns name can be found at github: https://github.com/xieguigang/VisualBasic_AppFramework/tree/master/gr
And this value is set as ``default`` if this parameter is not specified, in this situation, the parameter value of /map.levels is only allowd 256 or 512.

###### Example
```bash

```
##### Accepted Types
###### /in
**Decalre**:  _Microsoft.VisualBasic.Data.GIS.Data_
Example: 
```json
{
    "color": "System.String",
    "state": "System.String",
    "value": 0
}
```

###### /map.levels
**Decalre**:  _System.Int32_
Example: 
```json
0
```

###### /map
###### /map.Name
**Decalre**:  _System.String_
Example: 
```json
"System.String"
```

