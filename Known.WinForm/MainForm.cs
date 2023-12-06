using System.ComponentModel;
using Known.Shared;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace Known.WinForm;

public partial class MainForm : Form
{
    private readonly BlazorWebView blazorWebView;

    public MainForm()
    {
        CheckForIllegalCrossThreadCalls = false;
        InitializeComponent();

        AppSetting.Load();
        blazorWebView = new BlazorWebView();
        blazorWebView.Dock = DockStyle.Fill;
        blazorWebView.BlazorWebViewInitialized = new EventHandler<BlazorWebViewInitializedEventArgs>(WebViewInitialized);
        Controls.Add(blazorWebView);
        AddBlazorWebView();

        WindowState = FormWindowState.Maximized;
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        Text = Config.App.Name;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        var result = Dialog.Confirm("ȷ���˳�ϵͳ��");
        if (result == DialogResult.Cancel)
        {
            e.Cancel = true;
        }
        else
        {
            AppSetting.ZoomFactor = blazorWebView.WebView.ZoomFactor;
            AppSetting.Save();
            Environment.Exit(0);
        }
    }

    private void WebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.ZoomFactor = AppSetting.ZoomFactor;
    }

    private void AddBlazorWebView()
    {
        var services = new ServiceCollection();
        services.AddWindowsFormsBlazorWebView();
#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
        services.AddDemoApp(info =>
        {
            //���û���
            info.Type = AppType.WinForm;
            info.WebRoot = Application.StartupPath;
            info.ContentRoot = Application.StartupPath;
#if DEBUG
            info.IsDevelopment = true;
#endif
            info.Connections[0].ConnectionString = "Data Source=..\\Sample.db";
        });
        blazorWebView.HostPage = "wwwroot\\index.html";
        blazorWebView.Services = services.BuildServiceProvider();
        blazorWebView.RootComponents.Add<App>("#app");
    }
}