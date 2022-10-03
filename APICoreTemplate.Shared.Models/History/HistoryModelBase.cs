using System;
using System.ComponentModel.DataAnnotations;

namespace APICoreTemplate.Shared.Models.History
{
    public class HistoryModelBase
    {
        [Display(Name = "Start Time")]
        public DateTime SysStartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime SysEndTime { get; set; }
    }
}
