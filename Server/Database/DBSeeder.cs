using Server.Entities;
using System.Data.Entity;
using System.Linq;

namespace Server.Database
{
    class DBSeeder : CreateDatabaseIfNotExists<EngContext>
    {
        protected override void Seed(EngContext context)
        {
            #region Roles.
            context.Roles.Add(new Role { Name = "admin" });
            context.Roles.Add(new Role { Name = "user" });
            #endregion
            context.SaveChanges();
            #region Users.
            User val = new User { Username = "John", Password = "221BCA" };
            val.Roles = context.Roles.Where(c => c.Name == "admin").FirstOrDefault();
            context.Users.Add(val);
            #endregion

            #region Categories.
            #region Word.
            context.WordCategories.Add(new WordCategory { Name = "Noun", Abbreviation = "n." });
            context.WordCategories.Add(new WordCategory { Name = "Countable noun", Abbreviation = "cn." });
            context.WordCategories.Add(new WordCategory { Name = "Uncountable noun", Abbreviation = "un." });
            context.WordCategories.Add(new WordCategory { Name = "Compound noun", Abbreviation = "cn." });
            context.WordCategories.Add(new WordCategory { Name = "Verb", Abbreviation = "v." });
            context.WordCategories.Add(new WordCategory { Name = "Helping verb", Abbreviation = "hv." });
            context.WordCategories.Add(new WordCategory { Name = "Linking verb", Abbreviation = "lv." });
            context.WordCategories.Add(new WordCategory { Name = "Compound verb", Abbreviation = "cv." });
            context.WordCategories.Add(new WordCategory { Name = "Phrasal verb", Abbreviation = "phrv." });
            context.WordCategories.Add(new WordCategory { Name = "Adjective", Abbreviation = "adj." });
            context.WordCategories.Add(new WordCategory { Name = "Compound adjective", Abbreviation = "cadj." });
            context.WordCategories.Add(new WordCategory { Name = "Article adjective", Abbreviation = "a." });
            context.WordCategories.Add(new WordCategory { Name = "Adverb", Abbreviation = "adv." });
            context.WordCategories.Add(new WordCategory { Name = "Compound adverb", Abbreviation = "cadv." });
            context.WordCategories.Add(new WordCategory { Name = "Pronoun", Abbreviation = "pro." });
            context.WordCategories.Add(new WordCategory { Name = "Compound pronoun", Abbreviation = "cp." });
            context.WordCategories.Add(new WordCategory { Name = "Preposition", Abbreviation = "p." });
            context.WordCategories.Add(new WordCategory { Name = "Conjunction", Abbreviation = "c." });
            context.WordCategories.Add(new WordCategory { Name = "Interjunction", Abbreviation = "i." });
            #endregion
            #region Video.
            context.VideoCategories.Add(new VideoCategory { Name = "Movie" });
            context.VideoCategories.Add(new VideoCategory { Name = "Song" });
            context.VideoCategories.Add(new VideoCategory { Name = "Trailer" });
            context.VideoCategories.Add(new VideoCategory { Name = "Marvel" });
            context.VideoCategories.Add(new VideoCategory { Name = "Comic" });
            #endregion
            #region Book.
            context.BookCategories.Add(new BookCategory { Name = "Poem" });
            context.BookCategories.Add(new BookCategory { Name = "Novel" });
            #endregion
            context.SaveChanges();
            #endregion

            #region Videos.
            Video tmp = new Video { Name = "Avengers: trailer", Path = "Avengers.mp4", SubPath = "Avengers" };
            tmp.Categories.Add(context.VideoCategories.Where(c => c.Name == "Trailer").FirstOrDefault());
            tmp.Categories.Add(context.VideoCategories.Where(c => c.Name == "Marvel").FirstOrDefault());
            tmp.Categories.Add(context.VideoCategories.Where(c => c.Name == "Comic").FirstOrDefault());
            context.Videos.Add(tmp);

            tmp = new Video { Name = "Hobbit", Path = @"D:\My folder\Projects\English\I See Fire.mp4", SubPath = "I See Fire", IsAbsolulute = true, Description = "Ed Sheeran. I see Fire." };
            tmp.Categories.Add(context.VideoCategories.Where(c => c.Name == "Song").FirstOrDefault());
            context.Videos.Add(tmp);

            tmp = new Video { Name = "Thor: Ragnarok", Path = @"http://89.150.0.90/scrt/102/2018022621353732_high_eng.mp4?md5=Qn8vxm2qssj0LYlC2JtYgA&expires=1527679499", SubPath = "Thor", IsAbsolulute = true };
            tmp.Categories.Add(context.VideoCategories.Where(c => c.Name == "Movie").FirstOrDefault());
            tmp.Categories.Add(context.VideoCategories.Where(c => c.Name == "Marvel").FirstOrDefault());
            tmp.Categories.Add(context.VideoCategories.Where(c => c.Name == "Comic").FirstOrDefault());
            context.Videos.Add(tmp);
            #endregion
            context.SaveChanges();

            #region Words.
            #region Translations.            
            context.Translations.Add(new Translation { Name = "кролик" });
            context.Translations.Add(new Translation { Name = "заяц" });
            #endregion
            #region Definitions. 
            context.Definitions.Add(new Definition { Name = "animal" });
            #endregion
            context.SaveChanges();
            #region Words.
            Word word = new Word { Name = "rabbit" };
            word.Categories.Add(context.WordCategories.Where(t => t.Name == "Noun").FirstOrDefault());
            word.Categories.Add(context.WordCategories.Where(t => t.Name == "Countable noun").FirstOrDefault());
            word.Translations.Add(context.Translations.Where(a => a.Name == "кролик").FirstOrDefault());
            word.Translations.Add(context.Translations.Where(a => a.Name == "заяц").FirstOrDefault());
            word.Descriptions.Add(context.Definitions.Where(a => a.Name == "animal").FirstOrDefault());
            word.Users.Add(context.Users.Where(a => a.Username == "John").FirstOrDefault());
            #endregion
            context.SaveChanges();
            #region Examples.
            context.Examples.Add(new Example { Name = "In general, rabbits have long ears.", Words = word });
            #endregion
            #endregion

            #region Authors.
            context.Authors.Add(new Author { Name = "George Gordon", Surname = "Byron" });
            context.Authors.Add(new Author { Name = "Richard", Surname = "Adams" });
            #endregion
            context.SaveChanges();
            #region Books.
            Book _book = new Book { Name = "Watership Down", Path = @"D:\My folder\Projects\English\watershipdown.pdf", IsAbsolulute = true };            
            _book.Words.Add(word);
            _book.Authors.Add(context.Authors.Where(a => a.Surname == "Adams").FirstOrDefault());
            _book.Categories.Add(context.BookCategories.Where(c => c.Name == "Novel").FirstOrDefault());
            context.Books.Add(_book);

            _book = new Book { Name = "I would I were a careless child", Path = "Child.txt" };
            _book.Authors.Add(context.Authors.Where(a => a.Surname == "Byron").FirstOrDefault());
            _book.Categories.Add(context.BookCategories.Where(c => c.Name == "Poem").FirstOrDefault());
            context.Books.Add(_book);
            #endregion
            context.SaveChanges();

            base.Seed(context);
        }
    }
}