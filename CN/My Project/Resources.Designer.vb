﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Microsoft.VisualBasic.Data.GIS.CN.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property __china_geo() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("__china_geo", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to # 2016年11月,中华人民共和国县以上行政区划代码		
        '''行政区划代码  单位名称						
        '''110000	北京市						
        '''110101	    东城区						
        '''110102	    西城区						
        '''110105	    朝阳区						
        '''110106	    丰台区						
        '''110107	    石景山区						
        '''110108	    海淀区						
        '''110109	    门头沟区						
        '''110111	    房山区						
        '''110112	    通州区						
        '''110113	    顺义区						
        '''110114	    昌平区						
        '''110115	    大兴区						
        '''110116	    怀柔区						
        '''110117	    平谷区						
        '''110118	    密云区						
        '''110119	    延庆区						
        '''120000	天津市						
        '''120101	    和平区						
        '''120102	    河东区						
        '''120103	    河西区						
        '''12010 [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property cn() As String
            Get
                Return ResourceManager.GetString("cn", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Henan	410000	河南省
        '''Beijing	110000	北京市
        '''Neimenggu	150000	内蒙古自治区
        '''Yunnan	530000	云南省
        '''Jiangxi	360000	江西省
        '''Hunan	430000	湖南省
        '''Guangdong	440000	广东省
        '''Anhui	340000	安徽省
        '''Zhejiang	330000	浙江省
        '''Guizhou	520000	贵州省
        '''Jiangsu	320000	江苏省
        '''Sichuan	510000	四川省
        '''Xinjiang	650000	新疆维吾尔自治区
        '''Shandong	370000	山东省
        '''Fujian	350000	福建省
        '''Guangxi	450000	广西壮族自治区
        '''Heilongjiang	230000	黑龙江省
        '''Hebei	130000	河北省
        '''Shanghai	310000	上海市
        '''Hubei	420000	湖北省
        '''Chongqing	500000	重庆市
        '''Shanxi	140000	山西省
        '''Gansu	620000	甘肃省
        '''Tianjin	120000	天津市
        '''Shaanxi	610000	陕西省        ''' [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property province_id() As String
            Get
                Return ResourceManager.GetString("province_id", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
