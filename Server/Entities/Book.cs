using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Book : EntityTable
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
        [DataMember]
        public string ImgPath { get; set; } = "WolfB.png";
        [DataMember]
        public bool IsAbsolulute { get; set; } = false;

        [DataMember]
        public virtual List<BookCategory> Categories { get; set; }
        [DataMember]
        public virtual List<Word> Words { get; set; }
        [DataMember]
        public virtual List<Author> Authors { get; set; }
        
        public Book()
        {
            Categories = new List<BookCategory>();
            Words = new List<Word>();
            Authors = new List<Author>();
        }
    }
}