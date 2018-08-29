using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class User : EntityTable
    {
        [DataMember]
        public string Avatar { get; set; } = "Wolf.png";
        [DataMember, Required]
        public string Username { get; set; }
        [DataMember, Required, MinLength(6)]
        public string Password { get; set; }
        [DataMember]
        public int Level { get; set; } = 0;

        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public Role Roles { get; set; }

        [DataMember]
        public virtual List<Word> Words { get; set; }

        public User()
        {
            Words = new List<Word>();
        }
    }
}