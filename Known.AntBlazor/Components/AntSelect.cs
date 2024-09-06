﻿namespace Known.AntBlazor.Components;

/// <summary>
/// 扩展Ant选择框组件类。
/// </summary>
public class AntSelect : Select<string, string>
{
    [CascadingParameter] private IAntForm AntForm { get; set; }
    [CascadingParameter] private DataItem Item { get; set; }

    /// <summary>
    /// 初始化组件。
    /// </summary>
    protected override void OnInitialized()
    {
        if (AntForm != null)
            Disabled = AntForm.IsView;
        if (Item != null)
        {
            Item.Type = typeof(string);
            Placeholder = Item.Language.GetString("PleaseSelect");
        }
        EnableSearch = true;
        base.OnInitialized();
    }
}

/// <summary>
/// 扩展Ant代码表选择框组件类。
/// </summary>
public class AntSelectCode : Select<string, CodeInfo>
{
    [CascadingParameter] private IAntForm AntForm { get; set; }
    [CascadingParameter] private DataItem Item { get; set; }

    /// <summary>
    /// 取得或设置选择框组件关联的数据字典类别名或可数项目（用逗号分割，如：项目1,项目2）。
    /// </summary>
    [Parameter] public string Category { get; set; }

    /// <summary>
    /// 初始化组件。
    /// </summary>
    protected override void OnInitialized()
    {
        if (AntForm != null)
            Disabled = AntForm.IsView;
        var emptyText = "";
        if (Item != null)
        {
            Item.Type = typeof(string);
            emptyText = Item.Language.GetString("PleaseSelect");
            Placeholder = emptyText;
        }
        if (!string.IsNullOrWhiteSpace(Category))
            DataSource = Cache.GetCodes(Category).ToCodes(emptyText);
        ValueName = nameof(CodeInfo.Code);
        LabelName = nameof(CodeInfo.Name);
        EnableSearch = true;
        AllowClear = true;
        base.OnInitialized();
    }
}