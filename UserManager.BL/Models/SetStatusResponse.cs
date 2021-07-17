using UserManager.Core.Enums;

namespace UserManager.BL.Models
{
    public class SetStatusResponse
    {
        public int Id { get; set; }
        public UserStatusEnum Status { get; set; }
        public string Name { get; set; }
    }
}