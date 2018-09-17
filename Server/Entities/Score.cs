using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// ScoreCount - the number of points gained by the user.
    /// GameID - games id.
    /// GameName - the game.
    /// UserID - users id.
    /// UserName - the user.
    /// </summary>
    public class Score : EntityTable
    {
        [DataMember, Required]
        public int ScoreCount { get; set; }

        [DataMember]
        public int GameID { get; set; }
        [DataMember]
        public virtual Game GameName { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public virtual User UserName { get; set; }
    }
}