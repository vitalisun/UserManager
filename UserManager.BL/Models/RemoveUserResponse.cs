using UserManager.Core.Enums;
using UserManager.DAL.Entities;

namespace UserManager.BL.Models
{
    public class RemoveUserResponse
    {
        public ErrorIntIdEnum ErrorId { get; set; }
        public string Msg { get; set; }
        public bool Success { get; set; }
        public UserInfo User { get; set; }
    }
}