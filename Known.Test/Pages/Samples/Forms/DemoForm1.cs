﻿using Known.Test.Pages.Samples.DataList;

namespace Known.Test.Pages.Samples.Forms;

class DemoForm1 : BaseForm
{
    private readonly string Codes = "孙膑,后羿,妲己";

    protected override void BuildFields(RenderTreeBuilder builder)
    {
        builder.Table(table =>
        {
            table.ColGroup(11, 22, 11, 22, 11, 23);
            table.Tr(attr =>
            {
                table.Field<Text>("文本", "Text", true).Build();
                table.Field<Number>("数值", "Number").Build();
                table.Field<Select>("下拉", "Select", true).Set(f => f.Codes, Codes).Build();
            });
            table.Tr(attr =>
            {
                table.Field<Date>("日期", "Date").Build();
                table.Field<Date>("月份", "Month").Set(f => f.DateType, DateType.Month).Build();
                table.Field<Date>("日期时间", "DateTime").Set(f => f.DateType, DateType.DateTime).Build();
            });
            table.Tr(attr =>
            {
                table.Field<RadioList>("单选", "RadioList").Set(f => f.Codes, Codes).Build();
                table.Th("", "选项");
                table.Td("inline", attr =>
                {
                    table.Field<Razor.Components.Fields.CheckBox>("CheckBox1").IsInput(true).Set(f => f.Text, "启用").Build();
                    table.Field<Razor.Components.Fields.CheckBox>("CheckBox").IsInput(true)
                         .Set(f => f.Switch, true).Set(f => f.Text, "启用").Build();
                });
                table.Field<CheckList>("多选", "CheckList", true).Set(f => f.Codes, Codes).Build();
            });
            table.Tr(attr =>
            {
                table.Field<Picker>("选择（单选）", "Picker1").Set(f => f.Pick, new CommonTable()).Build();
                table.Field<Picker>("选择（多选）", "Picker2").Build();
                table.Field<Upload>("附件", "Upload").Build();
            });
            table.Tr(attr => table.Field<TextArea>("文本域", "TextArea").ColSpan(5).Build());
        });
    }

    public override void Load()
    {
        SetData(new
        {
            Text = "test",
            Number = 20,
            Select = "孙膑",
            Date = new DateTime(2020, 01, 01),
            Month = $"{DateTime.Now:yyyy-MM}",
            DateTime = DateTime.Now,
            RadioList = "后羿",
            CheckBox = true,
            CheckList = "孙膑,妲己",
            Picker1 = "test1",
            Picker2 = "test1,test2",
            TextArea = "Test Note"
        });
    }
}