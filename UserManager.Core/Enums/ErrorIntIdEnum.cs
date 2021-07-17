using System.Xml.Serialization;

namespace UserManager.Core.Enums
{
    public enum ErrorIntIdEnum
    {
        [XmlEnum("0")] NoError = 0,
        [XmlEnum("1")] EntityAlreadyExists = 1,
        [XmlEnum("2")] EntityNotFound = 2
    }


}
