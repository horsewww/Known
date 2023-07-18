﻿namespace Known;

public sealed class Config
{
    private Config() { }

    static Config()
    {
        AppId = "KIMS";
        AppName = "Known信息管理系统";
        Version = "1.0";
        var version = typeof(Config).Assembly.GetName().Version;
        FrameVersion = $"Known V{version.Major}.{version.Minor}.{version.Build}";
    }

    public static string DateFormat => "yyyy-MM-dd";
    public static string DateTimeFormat => "yyyy-MM-dd HH:mm:ss";
    public static string AppVersion => $"PM-{AppId} {Version}";
    public static string AppId { get; set; }
    public static string AppName { get; set; }
    public static string Version { get; private set; }
    public static string SoftVersion { get; private set; }
    public static string FrameVersion { get; private set; }
    public static Assembly AppAssembly { get; private set; }
    public static bool IsPlatform { get; set; }
    public static bool IsWebApi { get; set; } = true;

    public static void SetAppAssembly(Assembly assembly)
    {
        AppAssembly = assembly;
        var version = assembly.GetName().Version;
        SoftVersion = version.ToString();
        Version = $"{version.Major}.{version.Minor}";
    }

    public static string GetSysVersion(Assembly assembly)
    {
        var version = assembly.GetName().Version;
        var date = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
        return $"{Version}.{date:yyMMdd}";
    }
}