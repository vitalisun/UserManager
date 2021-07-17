using System.Xml.Serialization;
using UserManager.Core.Enums;

namespace UserManager.BL.Models
{
    public class User
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}
