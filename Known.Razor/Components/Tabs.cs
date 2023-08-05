﻿namespace Known.Razor.Components;

public class Tabs : BaseComponent
{
    private List<MenuItem> TabItems { get; set; }
    private string PositionCss => Position.ToString().ToLower();

    [Parameter] public string Style { get; set; }
    [Parameter] public bool Justified { get; set; }
    [Parameter] public PositionType Position { get; set; } = PositionType.Top;
    [Parameter] public string Codes { get; set; }
    [Parameter] public MenuItem CurItem { get; set; }
    [Parameter] public List<MenuItem> Items { get; set; }
    [Parameter] public Action<MenuItem> OnChanged { get; set; }
    [Parameter] public Action<RenderTreeBuilder, MenuItem> Body { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Items != null && Items.Count > 0)
            TabItems = Items;
        else
            TabItems = CodeInfo.GetCodes(Codes).Select(c => new MenuItem(c.Code, c.Name)).ToList();

        CurItem ??= TabItems[0];
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var items = TabItems;
        if (items == null || items.Count == 0)
            return;

        if (Body == null)
        {
            BuildTabHead(builder, items);
        }
        else
        {
            var css = CssBuilder.Default("tabs").AddClass(PositionCss).AddClass(Style).Build();
            builder.Div(css, attr =>
            {
                if (Position == PositionType.Top || Position == PositionType.Left)
                {
                    BuildTabHead(builder, items);
                    builder.Div("tab-body", attr => Body.Invoke(builder, CurItem));
                }
                else
                {
                    builder.Div("tab-body", attr => Body.Invoke(builder, CurItem));
                    BuildTabHead(builder, items);
                }
            });
        }
    }

    private void BuildTabHead(RenderTreeBuilder builder, List<MenuItem> items)
    {
        builder.Ul("tab", attr =>
        {
            foreach (var item in items)
            {
                builder.Li(Active(item), attr =>
                {
                    attr.OnClick(Callback(e => OnItemClick(item)));
                    if (Justified && Position == PositionType.Top)
                    {
                        var width = Math.Round(100M / items.Count, 2);
                        attr.Style($"width:{width}%");
                    }
                    builder.IconName(item.Icon, item.Name);
                });
            }
        });
    }

    private void OnItemClick(MenuItem item)
    {
        CurItem = item;
        OnChanged?.Invoke(item);
    }

    private string Active(MenuItem item) => CurItem == item ? "active" : "";
}