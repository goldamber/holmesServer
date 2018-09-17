using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// MarkCount - the quantity of marking stars for this video (1 - 5).
    /// VideoID - videos id.
    /// VideoName - the video.
    /// UserID - users id.
    /// UserName - the user.
    /// </summary>
    public class VideoStar : EntityTable
    {
        [DataMember, Required]
        public int MarkCount { get; set; }

        [DataMember]
        public int VideoID { get; set; }
        [DataMember]
        public virtual Video VideoName { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public virtual User UserName { get; set; }
    }
}