﻿namespace KnownAntDesign.Renders;

class KaInputRender : BaseRender<KInput>
{
    private string type => Component.Type.ToString().ToLower();

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var enabled = Component.Enabled;
        if (Component.Type == InputType.Color && Component.ReadOnly)
            enabled = false;

        builder.Component<Input<string>>(Component.Id)
               .Set(c => c.Type, type)
               .Set(c => c.Prefix, BuildIcon)
               .Set(c => c.Placeholder, Component.Placeholder)
               .Set(c => c.Value, Component.Value)
               .Set(c => c.ValueChanged, Component.Callback<string>(OnValueChanged))
               .Build();

        //BuildIcon(builder, Component.Icon);
        //builder.Input(attr =>
        //{
        //    //var value = BindConverter.FormatValue(Value);
        //    //var hasChanged = !EqualityComparer<string>.Default.Equals(value, Value);
        //    attr.Type(type).Id(Component.Id).Name(Component.Id).Disabled(!enabled).Readonly(Component.ReadOnly)
        //        .Value(Component.Value).Required(Component.Required)
        //        .Placeholder(Component.Placeholder)
        //        .Add("autocomplete", "off")
        //        .OnChange(Component.CreateBinder())
        //        .OnEnter(Component.OnEnter);
        //    //builder.SetUpdatesAttributeName("value");
        //});
    }

    private void BuildIcon(RenderTreeBuilder builder)
    {
        if (!string.IsNullOrWhiteSpace(Component.Icon))
            builder.Icon(Component.Icon);
    }

    private void OnValueChanged(string value)
    {
        Component.SetValue(value);
    }
}