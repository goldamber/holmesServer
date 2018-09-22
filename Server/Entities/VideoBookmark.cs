using System;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Position - the time, when a user stopped watching.
    /// UserID - the id of user.
    /// Users - the user.
    /// VideoID - the id of a video.
    /// Videos - the video.
    /// </summary>
    public class VideoBookmark : EntityTable
    {
        [DataMember]
        public TimeSpan Position { get; set; }

        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public virtual User Users { get; set; }

        [DataMember]
        public int VideoID { get; set; }
        [DataMember]
        public virtual Video Videos { get; set; }
    }
}