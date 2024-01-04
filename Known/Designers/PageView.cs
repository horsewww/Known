﻿using Known.Blazor;
using Known.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Known.Designers;

class PageView : BaseView<PageInfo>
{
    private readonly TableModel<PageColumnInfo> list = new();
    private readonly TabModel tab = new();
    private TableModel<Dictionary<string, object>> table;
    private string codePage;
    private string codeService;
    private string codeRepository;
    private List<CodeInfo> actions;

    [Parameter] public EntityInfo Entity { get; set; }

    protected override void OnInitialized()
    {
        actions = Config.Actions.Select(a =>
        {
            var name = Language[$"Button.{a.Id}"];
            return new CodeInfo(a.Id, name);
        }).ToList();
        base.OnInitialized();
        SetModel();
        Tab.Items.Add(new ItemModel(Language["Designer.View"]) { Content = BuildView });
        Tab.Items.Add(new ItemModel(Language["Designer.Fields"]) { Content = BuildList });
        Tab.Items.Add(new ItemModel(Language["Designer.PageCode"]) { Content = BuildPage });
        Tab.Items.Add(new ItemModel(Language["Designer.ServiceCode"]) { Content = BuildService });
        Tab.Items.Add(new ItemModel(Language["Designer.RepositoryCode"]) { Content = BuildRepository });

        list.FixedHeight = "380px";
        list.OnQuery = c =>
        {
            var result = new PagingResult<PageColumnInfo>(Model?.Columns);
            return Task.FromResult(result);
        };

        tab.Items.Add(new ItemModel(Language["Designer.Property"]) { Content = BuildProperty });
        tab.Items.Add(new ItemModel(Language["Designer.Toolbar"]) { Content = BuildToolbar });
        tab.Items.Add(new ItemModel(Language["Designer.Action"]) { Content = BuildAction });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            codePage = await JS.HighlightAsync(codePage, "csharp");
            codeService = await JS.HighlightAsync(codeService, "csharp");
            codeRepository = await JS.HighlightAsync(codeRepository, "csharp");
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    internal override void SetModel(PageInfo model)
    {
        base.SetModel(model);
        SetModel();
        StateChanged();
    }

    private void BuildView(RenderTreeBuilder builder)
    {
        builder.Div("view", () =>
        {
            builder.Div("kui-top", () =>
            {
                UI.BuildQuery(builder, table);
                UI.BuildToolbar(builder, table?.Toolbar);
            });
            builder.Div("kui-table", () => UI.BuildTable(builder, table));
        });
        builder.Div("setting", () => UI.BuildTabs(builder, tab));
    }

    private void BuildList(RenderTreeBuilder builder) => BuildList(builder, list);
    private void BuildPage(RenderTreeBuilder builder) => BuildCode(builder, codePage);
    private void BuildService(RenderTreeBuilder builder) => BuildCode(builder, codeService);
    private void BuildRepository(RenderTreeBuilder builder) => BuildCode(builder, codeRepository);

    private void BuildProperty(RenderTreeBuilder builder)
    {
        builder.Div("setting-row", () =>
        {
            BuildPropertyItem(builder, Language["Designer.ShowPager"], b => UI.BuildSwitch(b, new InputModel<bool>
            {
                Disabled = ReadOnly,
                Value = Model.ShowPager,
                ValueChanged = this.Callback<bool>(value => { Model.ShowPager = value; OnPropertyChanged(); })
            }));
            BuildPropertyItem(builder, Language["Designer.FixedWidth"], b => UI.BuildText(b, new InputModel<string>
            {
                Disabled = ReadOnly,
                Value = Model.FixedWidth,
                ValueChanged = this.Callback<string>(value => { Model.FixedWidth = value; OnPropertyChanged(); })
            }));
            BuildPropertyItem(builder, Language["Designer.FixedHeight"], b => UI.BuildText(b, new InputModel<string>
            {
                Disabled = ReadOnly,
                Value = Model.FixedHeight,
                ValueChanged = this.Callback<string>(value => { Model.FixedHeight = value; OnPropertyChanged(); })
            }));
        });
    }

    private void BuildToolbar(RenderTreeBuilder builder)
    {
        UI.BuildCheckList(builder, new InputModel<string[]>
        {
            Disabled = ReadOnly,
            Codes = actions,
            Value = Model.Tools,
            ValueChanged = this.Callback<string[]>(value => { Model.Tools = value; OnPropertyChanged(); })
        });
    }

    private void BuildAction(RenderTreeBuilder builder)
    {
        UI.BuildCheckList(builder, new InputModel<string[]>
        {
            Disabled = ReadOnly,
            Codes = actions,
            Value = Model.Actions,
            ValueChanged = this.Callback<string[]>(value => { Model.Actions = value; OnPropertyChanged(); })
        });
    }

    private void SetModel()
    {
        table = new DemoPageModel(UI, Model, Entity);
        codePage = Generator.GetPage(Model, Entity);
        codeService = Generator.GetService(Model, Entity);
        codeRepository = Generator.GetRepository(Model, Entity);
    }

    private void OnPropertyChanged()
    {
        SetModel(Model);
        OnChanged?.Invoke(Model);
    }
}