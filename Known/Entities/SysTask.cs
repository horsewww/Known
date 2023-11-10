﻿namespace Known.Entities;

/// <summary>
/// 系统任务实体类。
/// </summary>
public class SysTask : EntityBase
{
    /// <summary>
    /// 取得或设置业务ID。
    /// </summary>
    [Column("业务ID", "", true, "1", "50")]
    public string BizId { get; set; }

    /// <summary>
    /// 取得或设置类型。
    /// </summary>
    [Column("类型", "", true, "1", "50", IsGrid = true, IsQuery = true)]
    public string Type { get; set; }

    /// <summary>
    /// 取得或设置名称。
    /// </summary>
    [Column("名称", "", true, "1", "50", IsGrid = true, IsQuery = true)]
    public string Name { get; set; }

    /// <summary>
    /// 取得或设置执行目标。
    /// </summary>
    [Column("执行目标", "", true, "1", "200", IsGrid = true)]
    public string Target { get; set; }

    /// <summary>
    /// 取得或设置执行状态。
    /// </summary>
    [Column("执行状态", "", true, "1", "50", IsGrid = true, CodeType = nameof(TaskStatus))]
    public string Status { get; set; }

    /// <summary>
    /// 取得或设置开始时间。
    /// </summary>
    [Column("开始时间", "", false, IsGrid = true)]
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 取得或设置结束时间。
    /// </summary>
    [Column("结束时间", "", false, IsGrid = true)]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 取得或设置备注。
    /// </summary>
    [Column("备注", "", false, IsGrid = true)]
    public string Note { get; set; }
}