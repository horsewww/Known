﻿namespace Known.Razor;

public class KInput : Field
{
    // button         定义可点击的按钮（大多与 JavaScript 使用来启动脚本）
    // checkbox       定义复选框。
    // color          定义拾色器。
    // date           定义日期字段（带有 calendar 控件）
    // datetime       定义日期字段（带有 calendar 和 time 控件）
    // datetime-local 定义日期字段（带有 calendar 和 time 控件）
    // month          定义日期字段的月（带有 calendar 控件）
    // week           定义日期字段的周（带有 calendar 控件）
    // time           定义日期字段的时、分、秒（带有 time 控件）
    // email          定义用于 e-mail 地址的文本字段
    // file           定义输入字段和 "浏览..." 按钮，供文件上传
    // hidden         定义隐藏输入字段
    // image          定义图像作为提交按钮
    // number         定义带有 spinner 控件的数字字段 max,min,step
    // password       定义密码字段。字段中的字符会被遮蔽。
    // radio          定义单选按钮。
    // range          定义带有 slider 控件的数字字段。max,min,step
    // reset          定义重置按钮。重置按钮会将所有表单字段重置为初始值。
    // search         定义用于搜索的文本字段。
    // submit         定义提交按钮。提交按钮向服务器发送数据。
    // tel            定义用于电话号码的文本字段。
    // text           默认。定义单行输入字段，用户可在其中输入文本。默认是 20 个字符。
    // url            定义用于 URL 的文本字段。
    private string type => Type.ToString().ToLower();

    [Parameter] public InputType Type { get; set; }
    [Parameter] public string Placeholder { get; set; }
    [Parameter] public string Icon { get; set; }
    [Parameter] public string OnEnter { get; set; }

    protected override void BuildText(RenderTreeBuilder builder)
    {
        if (Type == InputType.Color)
        {
            BuildInput(builder);
            return;
        }
        base.BuildText(builder);
    }

    protected override void BuildInput(RenderTreeBuilder builder)
    {
        var enabled = Enabled;
        if (Type == InputType.Color && ReadOnly)
            enabled = false;

        BuildIcon(builder, Icon);
        builder.Input(attr =>
        {
            //var value = BindConverter.FormatValue(Value);
            //var hasChanged = !EqualityComparer<string>.Default.Equals(value, Value);
            attr.Type(type).Id(Id).Name(Id).Disabled(!enabled).Readonly(ReadOnly)
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