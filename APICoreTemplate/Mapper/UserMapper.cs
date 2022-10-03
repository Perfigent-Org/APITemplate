using APICoreTemplate.Core.Data.Models;
using APICoreTemplate.Core.Data.Models.History;
using APICoreTemplate.Shared.Models.UserModel;

namespace APICoreTemplate.Mapper
{
    public static class UserMapper
    {
        public static UsersDetailsModel MapDetailsModel(User item)
        {
            if (item != null)
            {
                return new UsersDetailsModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Roles = item.Roles,
                    LastLoginDateTime = item.LastLoginDateTime
                };
            }

            return null;
        }

        public static UserHistoryModel MapHistoryModel(UserHistory item)
        {
            if (item != null)
            {
                return new UserHistoryModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Roles = item.Roles,
                    LastLoginDateTime = item.LastLoginDateTime,
                    SysStartTime = item.SysStartTime,
                    SysEndTime = item.SysEndTime
                };
            }

            return null;
        }
    }
}
