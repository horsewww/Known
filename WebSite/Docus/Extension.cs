﻿using WebSite.Data;

namespace WebSite.Docus;

static class Extension
{
    internal static void BuildMarkdown(this RenderTreeBuilder builder, string markdown)
    {
        var html = Markdown.ToHtml(markdown);
        builder.Markup(html);
    }

    internal static void BuildDemo<T>(this RenderTreeBuilder builder, string title, string style = "") where T : BaseComponent
    {
        var type = typeof(T);
        var code = ComponentService.GetCode(type);
        builder.H3(title);
        builder.Div($"demo {style}", attr =>
        {
            builder.Div("view", attr => builder.Component<T>().Build());
            builder.Div("code", attr =>
            {
                builder.Element("pre", attr => builder.Element("code", attr => builder.Text(code)));
            });
        });
    }

    internal static void BuildDemo(this RenderTreeBuilder builder, string title, string code)
    {
        builder.H3(title);
        builder.Div("demo block", attr =>
        {
            builder.Div("code", attr =>
            {
                builder.Element("pre", attr => builder.Element("code", attr => builder.Text(code)));
            });
        });
    }
}