using UserManager.Core.Enums;

namespace UserManager.BL.Models
{
    public class GetUserInfoResponse
    {
        public bool Success { get; set; }
        public ErrorStringIdEnum ErrorId { get; set; }
        public string ErrorMsg { get; set; }
        public User User { get; set; }

    }
}