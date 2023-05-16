﻿/* -------------------------------------------------------------------------------
 * Project: Known管理系统（Template）
 * 
 * Change Logs:
 * Date           Author       Notes
 * 2022-10-18     KnownChen    初始化
 * ------------------------------------------------------------------------------- */

namespace Template;

public class AppConfig
{
    public static void Initialize()
    {
        Config.AppId = "Template";
        Config.AppName = "Known管理系统";
        Config.SysVersion = "1.0.0";
        Config.AppAssembly = typeof(AppConfig).Assembly;

        PagingCriteria.DefaultPageSize = 20;
        DicCategory.AddCategories<AppDictionary>();
        Cache.AttachCodes(typeof(AppConfig).Assembly);
    }
}