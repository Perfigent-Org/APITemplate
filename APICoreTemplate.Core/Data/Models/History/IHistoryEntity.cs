using System;

namespace APICoreTemplate.Core.Data.Models.History
{
    public interface IHistoryEntity
    {
        DateTime SysStartTime { get; set; }
        DateTime SysEndTime { get; set; }
    }
}
