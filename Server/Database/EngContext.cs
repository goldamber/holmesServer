using Server.Entities;
using System.Data.Entity;

namespace Server.Database
{
    public class EngContext : DbContext
    {
        public DbSet<Word> Dictionary { get; set; }
        public DbSet<WordForm> WordForms { get; set; }
        public DbSet<Transcription> Transcriptions { get; set; }
        public DbSet<WordCategory> WordCategories { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Example> Examples { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoCategory> VideoCategories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<VideoBookmark> VideoBookmarks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<WordsGroup> Groups { get; set; }
    }
}