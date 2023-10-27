using Known;
using Known.Cells;
using Known.Demo;
using Known.Web;
using Known.Web.Pages;

var builder = WebApplication.CreateBuilder(args);

builder.InitApp();                //��ʼ������

builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

builder.Services.AddKnown();      //���Known���
builder.Services.AddKnownCells(); //���Known.Cells����Excel
builder.Services.AddApp();        //���APPȫ������
builder.Services.AddDemo();       //���APP��Demoģ��

var app = builder.Build();
app.Services.UseApp();            //ʹ��APP

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();