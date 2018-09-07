using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - the title of this book.
    /// Description - the description of this book.
    /// Mark - the quantity of marking stars for this book (NG - 5).
    /// Year - the year of release.
    /// Created - the time, when this book was added to the databse for the first time.
    /// Path - the location of a 'Book' file.
    /// ImgPath - the location of the poster.
    /// IsAbsolute - if the path is absolute or not. If not - the file is in 'Books/...'.
    /// Categories - the list of books categories.
    /// Words - the list of words, related to this book.
    /// Authors - the list of authors, who wrote this book.
    /// </summary>
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
        [DataMember, Required]
        public string Path { get; set; }
        [DataMember]
        public string ImgPath { get; set; } = "WolfB.png";
        [DataMember]
        public bool IsAbsolute { get; set; } = false;

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