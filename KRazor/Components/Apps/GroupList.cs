﻿/* -------------------------------------------------------------------------------
 * Copyright (c) Suzhou Puman Technology Co., Ltd. All rights reserved.
 * 
 * WebSite: https://www.pumantech.com
 * Contact: knownchen@163.com
 * 
 * Change Logs:
 * Date           Author       Notes
 * 2022-04-01     KnownChen
 * ------------------------------------------------------------------------------- */

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Known.Razor;

public class GroupListItem
{
    public string Icon { get; set; }
    public string Text { get; set; }
    public string Route { get; set; }
    public Action OnClick { get; set; }
}

public class GroupList : AppComponent
{
    [Parameter] public string Style { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public GroupListItem[] Items { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Div($"glist {Style}", attr =>
        {
            if (Items != null && Items.Length > 0)
            {
                foreach (var item in Items)
                {
                    BuildItem(builder, item);
                }
            }
            else if (ChildContent != null)
            {
                builder.Fragment(ChildContent);
            }
        });
    }

    private void BuildItem(RenderTreeBuilder builder, GroupListItem item)
    {
        builder.Div("glist-item", attr =>
        {
            if (!string.IsNullOrWhiteSpace(item.Route))
                attr.OnClick(Callback(e => Navigation.NavigateTo(item.Route)));
            else if (item.OnClick != null)
                attr.OnClick(Callback(e => item.OnClick()));

            if (!string.IsNullOrWhiteSpace(item.Icon))
                builder.Icon(item.Icon);

            builder.Span("text", item.Text);

            if (!string.IsNullOrWhiteSpace(item.Route) || item.OnClick != null)
                builder.Icon("right fa fa-chevron-right");
        });
    }
}
