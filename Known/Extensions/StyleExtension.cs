﻿using Known.Razor;

namespace Known.Extensions;

public static class StyleExtension
{
    public static void AddRandomColor(this AttributeBuilder attr, string name)
    {
        if (Setting.Info == null || !Setting.Info.RandomColor)
            return;

        var color = GetRandomColor();
        attr.Style($"{name}:{color}");
    }

    internal static string GetRandomColor()
    {
        if (Setting.Info == null || !Setting.Info.RandomColor)
            return string.Empty;

        var rndColor = Utils.GetRandomColor();
        return Utils.ToHtml(rndColor);
    }
}