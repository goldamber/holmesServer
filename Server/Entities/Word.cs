using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Word : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }
        [DataMember]
        public string ImgPath { get; set; }

        [DataMember]
        public int? FormID { get; set; }
        [DataMember]
        public WordForm Form { get; set; }

        [DataMember]
        public List<WordCategory> Categories { get; set; }
        [DataMember]
        public virtual List<Translation> Translations { get; set; }
        [DataMember]
        public virtual List<Definition> Descriptions { get; set; }        
        [DataMember]
        public virtual List<Video> Videos { get; set; }
        [DataMember]
        public virtual List<Book> Books { get; set; }
        [DataMember]
        public virtual List<User> Users { get; set; }

        public Word()
        {
            Categories = new List<WordCategory>();
            Descriptions = new List<Definition>();
            Translations = new List<Translation>();
            Videos = new List<Video>();
            Books = new List<Book>();
            Users = new List<User>();
        }
    }
}