﻿using Known.Blazor;
using Known.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Known.Designers;

class FormProperty : BaseProperty<FormFieldInfo>
{
    private List<CodeInfo> controlTypes;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        controlTypes = Cache.GetCodes(nameof(FieldType)).Select(c => new CodeInfo(c.Name, c.Name)).ToList();
    }

    protected override void BuildForm(RenderTreeBuilder builder)
    {
        if (Model == null)
            return;

        builder.Div("caption", () => builder.Div("title", $"{Language["Designer.FieldProperty"]} - {Model.Id}"));
        BuildPropertyItem(builder, Language["Name"], b => b.Span(Model.Name));
        BuildPropertyItem(builder, Language["Row"], b => UI.BuildNumber(b, new InputModel<int>
        {
            Disabled = IsReadOnly,
            Value = Model.Row,
            ValueChanged = this.Callback<int>(value => { Model.Row = value; OnChanged?.Invoke(Model); })
        }));
        BuildPropertyItem(builder, Language["Column"], b => UI.BuildNumber(b, new InputModel<int>
        {
            Disabled = IsReadOnly,
            Value = Model.Column,
            ValueChanged = this.Callback<int>(value => { Model.Column = value; OnChanged?.Invoke(Model); })
        }));
        BuildPropertyItem(builder, Language["Type"], b => UI.BuildSelect(b, new InputModel<string>
        {
            Disabled = IsReadOnly,
            Codes = controlTypes,
            Value = Model.Type.ToString(),
            ValueChanged = this.Callback<string>(value =>
            {
                if (Model != null)
                    Model.Type = Utils.ConvertTo<FieldType>(value);
                OnChanged?.Invoke(Model);
            })
        }));
        BuildPropertyItem(builder, Language["Required"], b => UI.BuildSwitch(b, new InputModel<bool>
        {
            Disabled = IsReadOnly,
            Value = Model.Required,
            ValueChanged = this.Callback<bool>(value => { Model.Required = value; OnChanged?.Invoke(Model); })
        }));
        BuildPropertyItem(builder, Language["ReadOnly"], b => UI.BuildSwitch(b, new InputModel<bool>
        {
            Disabled = IsReadOnly,
            Value = Model.ReadOnly,
            ValueChanged = this.Callback<bool>(value => { Model.ReadOnly = value; OnChanged?.Invoke(Model); })
        }));
        BuildPropertyItem(builder, Language["Placeholder"], b => UI.BuildText(b, new InputModel<string>
        {
            Disabled = IsReadOnly,
            Value = Model.Placeholder,
            ValueChanged = this.Callback<string>(value => { Model.Placeholder = value; OnChanged?.Invoke(Model); })
        }));
        if (Model.Type == FieldType.Select || Model.Type == FieldType.RadioList || Model.Type == FieldType.CheckList)
        {
            BuildPropertyItem(builder, Language["Category"], b => UI.BuildText(b, new InputModel<string>
            {
                Disabled = IsReadOnly,
                Value = Model.Category,
                ValueChanged = this.Callback<string>(value => { Model.Category = value; OnChanged?.Invoke(Model); })
            }));
        }
        if (Model.Type == FieldType.File)
        {
            BuildPropertyItem(builder, Language["MultiFile"], b => UI.BuildSwitch(b, new InputModel<bool>
            {
                Disabled = IsReadOnly,
                Value = Model.MultiFile,
                ValueChanged = this.Callback<bool>(value => { Model.MultiFile = value; OnChanged?.Invoke(Model); })
            }));
        }
    }
}