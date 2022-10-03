using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICoreTemplate.Core.Data.Models.History
{
    [Table("Users", Schema = "History")]
    public class UserHistory : User, IHistoryEntity
    {
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
    }
}
