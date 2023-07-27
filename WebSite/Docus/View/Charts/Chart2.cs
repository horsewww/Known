﻿namespace WebSite.Docus.View.Charts;

class Chart2 : BaseComponent
{
    private Chart chart;
    private readonly ChartDataInfo[] datas;

    public Chart2()
    {
        var data1 = new ChartDataInfo { Name = "类型一", Series = new Dictionary<string, object>() };
        var data2 = new ChartDataInfo { Name = "类型二", Series = new Dictionary<string, object>() };
        for (int i = 2010; i < DateTime.Now.Year; i++)
        {
            var rand1 = new Random();
            data1.Series[$"{i}"] = rand1.Next(100, 1000);
            var rand2 = new Random();
            data2.Series[$"{i}"] = rand2.Next(200, 1000);
        }
        datas = new ChartDataInfo[] { data1, data2 };
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Component<Chart>().Id("chart2").Build(value => chart = value);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            chart.YAxis = new { title = new { text = "数量" } };
            chart.ShowBar("柱状图统计", datas);
        }
    }
}