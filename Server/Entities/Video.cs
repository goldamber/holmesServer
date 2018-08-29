using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Video : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? Mark { get; set; } = null;
        [DataMember]
        public int? Year { get; set; } = null;
        [DataMember]
        public DateTime Created { get; set; } = DateTime.Today;
        [DataMember]
        public DateTime? Seen { get; set; } = null;
        [DataMember, Required]
        public string Path { get; set; }
        [DataMember, Required]
        public string SubPath { get; set; }
        [DataMember]
        public string ImgPath { get; set; } = "WolfV.png";
        [DataMember]
        public bool IsAbsolulute { get; set; } = false;

        [DataMember]
        public virtual List<VideoCategory> Categories { get; set; }
        [DataMember]
        public virtual List<Word> Words { get; set; }
        
        public Video()
        {
            Categories = new List<VideoCategory>();
            Words = new List<Word>();
        }
    }
}