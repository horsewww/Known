﻿namespace Known.Razor.Pages.Forms;

class ColumnGrid : EditGrid<ColumnInfo>
{
    private List<Type> modelTypes;
    private CodeInfo[] models;
    private List<ColumnInfo> orgData;

    [Parameter] public bool IsModule { get; set; }
    [Parameter] public string PageId { get; set; }
    [Parameter] public Action OnSetting { get; set; }

    protected override void OnInitialized()
    {
        Columns = GetColumns(IsModule);

        if (IsModule)
        {
            modelTypes = KRConfig.GetModelTypes();
            models = modelTypes.Select(t => new CodeInfo(t.FullName, t.Name)).ToArray();
            ActionHead = b =>
            {
                b.Link(Language.Add, Callback(OnAdd));
                b.Link("插入", Callback(OnInsert));
            };
        }
        else
        {
            Style = "form-grid";
            ActionHead = null;
            Actions = new List<ButtonInfo> { GridAction.MoveUp, GridAction.MoveDown };

            orgData = Data;
            Data = Setting.GetUserColumns(PageId, orgData);
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (IsModule)
        {
            base.BuildRenderTree(builder);
            
            if (ReadOnly)
                return;

            builder.Div("tool", attr =>
            {
                builder.Span("实体模型：");
                builder.Field<Select>("Model").IsInput(true)
                       .Set(f => f.Items, models)
                       .Set(f => f.ValueChanged, OnModelChanged)
                       .Build();
            });
        }
        else
        {
            builder.Form(base.BuildRenderTree, BuildAction);
        }
    }

    private void BuildAction(RenderTreeBuilder builder)
    {
        if (OnSetting != null)
            builder.Button(FormButton.Reset, Callback(OnReset));
        builder.Button(FormButton.OK, Callback(OnOK));
        builder.Button(FormButton.Cancel, Callback(OnCancel));
    }

    private static List<Column<ColumnInfo>> GetColumns(bool isModule)
    {
        var builder = new ColumnBuilder<ColumnInfo>();
        if (isModule)
            builder.Field(r => r.Id).Name("ID").Edit().Width(100);
        builder.Field(r => r.Name).Name("名称").Edit();
        if (isModule)
            builder.Field(r => r.Type).Name("类型").Edit(new SelectOption(typeof(ColumnType))).Width(100);
        builder.Field(r => r.Align).Name("对齐").Edit(new SelectOption(typeof(AlignType))).Width(100);
        builder.Field(r => r.Width).Name("宽度").Edit<Number>().Center(100);
        builder.Field(r => r.IsVisible).Name("显示").Edit();
        builder.Field(r => r.IsQuery).Name("查询").Edit();
        builder.Field(r => r.IsAdvQuery).Name("高级查询").Edit();
        if (isModule)
            builder.Field(r => r.IsFixed).Name("固定").Edit();
        builder.Field(r => r.IsSort).Name("排序").Edit();
        if (isModule)
            builder.Field(r => r.IsSum).Name("合计").Edit();
        return builder.ToColumns();
    }

    private void OnModelChanged(string value)
    {
        var type = modelTypes.FirstOrDefault(t => t.FullName == value);
        var attrs = TypeHelper.GetColumnAttributes(type);
        Data = attrs.Where(a => a.IsGrid).Select(a =>
        {
            var info = new ColumnInfo(a.Description, a.Property.Name);
            info.SetColumnType(a.Property.PropertyType);
            return info;
        }).ToList();
        StateChanged();
    }

    private async void OnReset()
    {
        var info = new SettingFormInfo { Type = UserSetting.KeyColumn, Name = PageId };
        await Platform.User.DeleteSettingAsync(info);
        Setting.UserSetting.Columns.Remove(PageId);
        Data = Setting.GetUserColumns(PageId, orgData);
        StateChanged();
        OnSetting?.Invoke();
    }

    private async void OnOK()
    {
        if (OnSetting != null)
        {
            var info = new SettingFormInfo
            {
                Type = UserSetting.KeyColumn,
                Name = PageId,
                Data = Utils.ToJson(Data)
            };
            await Platform.User.SaveSettingAsync(info);
            Setting.UserSetting.Columns[PageId] = Data;
            OnSetting.Invoke();
        }
        OnCancel();
    }

    private void OnCancel() => UI.CloseDialog();
}