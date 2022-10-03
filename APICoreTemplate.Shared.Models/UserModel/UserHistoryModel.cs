using APICoreTemplate.Shared.Models.History;
using System;
using System.ComponentModel.DataAnnotations;

namespace APICoreTemplate.Shared.Models.UserModel
{
    public class UserHistoryModel : HistoryModelBase
    {

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Roles { get; set; }

        [Display(Name = "Last Login")]
        public DateTime LastLoginDateTime { get; set; }
    }
}
