using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - a name of users role.
    /// </summary>
    public class Role : EntityTable
    {
        [DataMember]
        public string Name { get; set; }
    }
}