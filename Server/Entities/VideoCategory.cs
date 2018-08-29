using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class VideoCategory : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Video> Videos { get; set; }

        public VideoCategory()
        {
            Videos = new List<Video>();
        }
    }
}