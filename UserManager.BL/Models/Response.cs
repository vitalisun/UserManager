using System.Xml.Serialization;
using UserManager.Core.Enums;

namespace UserManager.BL.Models
{
    public class Response
    {
        [XmlAttribute]
        public bool Success { get; set; }
        [XmlAttribute]
        public ErrorIntIdEnum ErrorId { get; set; }
        public string ErrorMsg { get; set; }
        public User User { get; set; }
    }
}