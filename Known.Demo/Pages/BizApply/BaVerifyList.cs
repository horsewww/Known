﻿using Known.Blazor;
using Known.Demo.Entities;
using Known.Demo.Services;
using Known.Extensions;
using Known.WorkFlows;
using Microsoft.AspNetCore.Components.Rendering;

namespace Known.Demo.Pages.BizApply;

//业务审核列表
class BaVerifyList : BaseTablePage<TbApply>
{
    private ApplyService Service => new() { CurrentUser = CurrentUser };

    protected override async Task OnInitPageAsync()
    {
        await base.OnInitPageAsync();
		Table.OnQuery = QueryApplysAsync;
		Table.Form.Width = 800;
		Table.Column(c => c.BizNo).DefaultDescend();
		Table.Column(c => c.BizStatus).Template(BuildBizStatus);
    }

	//审核操作
    [Action] public void Verify(TbApply row) => this.VerifyFlow(row);

	private Task<PagingResult<TbApply>> QueryApplysAsync(PagingCriteria criteria)
	{
		criteria.Parameters[nameof(PageType)] = PageType.Verify;
		return Service.QueryApplysAsync(criteria);
	}

	private void BuildBizStatus(RenderTreeBuilder builder, TbApply row) => UI.BizStatus(builder, row.BizStatus);
}