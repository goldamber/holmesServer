using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - the content of this word.
    /// ImgPath - the content of this word.
    /// FormID - forms id.
    /// Form - the exceptional situations for this word (plural or past forms)content of this word.
    /// Categories - a list of categories for this word.
    /// Translations - a list of translations for this word.
    /// Descriptions - a list of definitions for this word.
    /// Videos - a list of videos, to which this word is connected.
    /// Books - a list of books, to which this word is connected.
    /// Users - a list of users, to whom this word is connected.
    /// </summary>
    public class Word : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }
        [DataMember]
        public string ImgPath { get; set; } = null;

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