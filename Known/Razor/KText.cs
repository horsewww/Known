﻿using Known.Extensions;

namespace Known.Razor;

public class KText : Field
{
    [Parameter] public string Icon { get; set; }
    [Parameter] public string Placeholder { get; set; }
    [Parameter] public string OnEnter { get; set; }

    protected override void BuildInput(RenderTreeBuilder builder)
    {
        BuildIcon(builder, Icon);
        builder.Input(attr =>
        {
            //var value = BindConverter.FormatValue(Value);
            //var hasChanged = !EqualityComparer<string>.Default.Equals(value, Value);
            attr.Type("text").Id(Id).Name(Id).Disabled(!Enabled)
                .Value(Value).Required(Required)
                .Placeholder(Placeholder)
                .Add("autocomplete", "off")
                .OnChange(CreateBinder())
                .OnEnter(OnEnter);
            //builder.SetUpdatesAttributeName("value");
        });
    }

    private static void BuildIcon(RenderTreeBuilder builder, string icon)
    {
        if (!string.IsNullOrWhiteSpace(icon))
            builder.Icon(icon);
    }
}