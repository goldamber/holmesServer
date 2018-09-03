using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - the title of this movie.
    /// Description - the description of this video.
    /// Mark - the quantity of marking stars for this video (NG - 5).
    /// Year - the year of release.
    /// Created - the time, when this video was added to the databse for the first time.
    /// Path - the location of a 'Video' file. If NULL - the file is in 'Videos/...'.
    /// SubPath - the location of subs file for this video.
    /// ImgPath - the location of the poster.
    /// Categories - the list of videos categories.
    /// Words - the list of words, related to this video.
    /// Words - the list of words, related to this video.
    /// </summary>
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
        public string Path { get; set; } = null;
        [DataMember, Required]
        public string SubPath { get; set; }
        [DataMember]
        public string ImgPath { get; set; } = "WolfV.png";

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