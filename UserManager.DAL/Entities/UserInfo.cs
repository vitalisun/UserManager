using UserManager.Core.Enums;

namespace UserManager.DAL.Entities
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UserStatusEnum Status { get; set; }

    }
}
