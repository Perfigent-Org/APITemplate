using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICoreTemplate.Core.Data.Models
{
    [Table("Users")]
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Roles { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
}
