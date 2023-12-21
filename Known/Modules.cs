﻿using System.ComponentModel;

namespace Known;

public class EntityInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsFlow { get; set; }
    public List<FieldInfo> Fields { get; set; } = [];
}

public enum FieldType { Text, TextArea, Date, Number, Switch, CheckBox, CheckList, RadioList, Select, Password, File }

public class FieldInfo
{
    [DisplayName("ID")]
    public string Id { get; set; }
    [DisplayName("名称")]
    public string Name { get; set; }
    [DisplayName("类型")]
    public FieldType Type { get; set; }
    [DisplayName("长度")]
    public string Length { get; set; }
    [DisplayName("必填")]
    public bool Required { get; set; }
}

public class PageInfo
{
    public string Type { get; set; }
    public List<string> Tools { get; set; }
    public List<string> Actions { get; set; }
    public List<PageColumnInfo> Columns { get; set; } = [];
}

public class PageColumnInfo
{
    [DisplayName("ID")]
    public string Id { get; set; }
    [DisplayName("名称")]
    public string Name { get; set; }
    [DisplayName("查看链接")]
    public bool IsViewLink { get; set; }
    [DisplayName("查询")]
    public bool IsQuery { get; set; }
    [DisplayName("查询全部")]
    public bool IsQueryAll { get; set; }
    [DisplayName("排序")]
    public bool IsSort { get; set; }
    [DisplayName("默认排序")]
    public string DefaultSort { get; set; }
}

public class FormInfo
{
    public List<FormFieldInfo> Fields { get; set; } = [];
}

public class FormFieldInfo : FieldInfo
{
    [DisplayName("行")]
    public int Row { get; set; } = 1;
    [DisplayName("列")]
    public int Column { get; set; } = 1;
    [DisplayName("代码类别")]
    public string Category { get; set; }
    [DisplayName("占位符")]
    public string Placeholder { get; set; }
    [DisplayName("只读")]
    public bool ReadOnly { get; set; }
    [DisplayName("多文件")]
    public bool MultiFile { get; set; }
}