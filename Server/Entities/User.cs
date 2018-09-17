using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Avatar - an image of this client.
    /// Username - login of this client.
    /// Password - password of this client.
    /// Level - level of this client. Depends on the quantity of played games and the score of each game.
    /// RoleID - users role id.
    /// Roles - users role.
    /// Words - a list of words connected with this user.
    /// </summary>
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
        public virtual Role Roles { get; set; }

        [DataMember]
        public virtual List<Word> Words { get; set; }

        public User()
        {
            Words = new List<Word>();
        }
    }
}