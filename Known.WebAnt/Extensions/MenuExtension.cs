﻿using AntDesign.ProLayout;

namespace Known.WebAnt.Extensions;

static class MenuExtension
{
    internal static List<MenuDataItem> ToAntMenus(this List<MenuInfo> menus)
    {
        Config.UserMenus = menus;

        var items = new List<MenuDataItem>();
        if (menus == null || menus.Count == 0)
            return items;

        var tops = menus.Where(m => m.ParentId == "0").OrderBy(m => m.Sort).ToList();
        foreach (var menu in tops)
        {
            var item = CreateAntMenuItem(menu);
            items.Add(item);
            AddChildren(menus, item);
        }
        return items;
    }

    private static void AddChildren(List<MenuInfo> menus, MenuDataItem item)
    {
        var items = menus.Where(m => m.ParentId == item.Key).OrderBy(m => m.Sort).ToList();
        if (items == null || items.Count == 0)
            return;

        foreach (var menu in items)
        {
            var sub = CreateAntMenuItem(menu);
            sub.ParentKeys = [item.Key];
            //item.Children.Add(sub);
            AddChildren(menus, sub);
        }
    }

    public static MenuDataItem CreateAntMenuItem(MenuInfo menu)
    {
        return new MenuDataItem
        {
            Key = menu.Id,
            Icon = menu.Icon,
            Name = menu.Name,
            Path = menu.Target
        };
    }
}