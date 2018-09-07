using Server.Database;
using Server.Entities;
using System;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Server.Service
{
    /// <summary>
    /// Types of data, that can be used for the general actions (insert, remove, edit, view).
    /// </summary>
    public enum ServerData { Video, Book, User, Role, VideoCategory, BookCategory, Word, WordForm, WordCategory, Translation, Definition, Author, Game, Example, Bookmark, Group }
    /// <summary>
    /// Describes the properties, that have to be sent to the client.
    /// </summary>
    public enum PropertyData { Name, Login, Role, RolesName, Description, Path, SubPath, Imgpath, Mark, Created, Date, Position, ScoreCount, Password, Level, Year, PastForm, PastThForm, PluralForm, Category, Categories, Author, Authors, Synonyms, Translation, Translations, Definition, Definitions, Group, Groups }

    public partial class EngService : IEngService
    {
        EngContext _context;

        public EngService()
        {
            _context = new EngContext();
            _context.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["engEntDB"].ConnectionString;
        }

        #region Add.
        public int? AddAuthor(string name, string surname)
        {
            _context.Authors.Add(new Author { Name = name, Surname = surname });
            _context.SaveChanges();

            return _context.Authors.Where(a => a.Name == name && a.Surname == surname).FirstOrDefault()?.Id;
        }
        public int? AddBook(string name, string desc, string path, string img, bool absolute, int? mark, int? year, DateTime created)
        {
            _context.Books.Add(new Book { Name = name, Description = (desc == "" ? null : desc), Path = path, ImgPath = img, IsAbsolute = absolute, Created = created, Mark = mark, Year = year });
            _context.SaveChanges();

            return _context.Books.Where(b => b.Name == name).FirstOrDefault()?.Id;
        }
        public int? AddUser(string login, string pswd, string avatar, string role, int level = 0)
        {
            User val = new User { Username = login, Password = pswd, Avatar = avatar, Level = level };
            Role tmp = _context.Roles.Where(c => c.Name == role).FirstOrDefault();
            val.Roles = tmp;
            _context.Users.Add(val);
            _context.SaveChanges();

            return _context.Users.Where(b => b.Username == login).FirstOrDefault()?.Id;
        }
        public int? AddVideo(string name, string desc, string path, string sub, string img, bool absolute, int? mark, int? year, DateTime created)
        {
            _context.Videos.Add(new Video { Name = name, Description = (desc == "" ? null : desc), Path = path, SubPath = sub, ImgPath = img, IsAbsolute = absolute, Created = created, Mark = mark, Year = year });
            _context.SaveChanges();

            return _context.Videos.Where(b => b.Name == name).FirstOrDefault()?.Id;
        }
        public int? AddCategory(string name, ServerData data)
        {
            switch (data)
            {
                case ServerData.VideoCategory:
                    _context.VideoCategories.Add(new VideoCategory { Name = name });
                    return _context.VideoCategories.Where(b => b.Name == name).FirstOrDefault()?.Id;

                case ServerData.BookCategory:
                    _context.BookCategories.Add(new BookCategory { Name = name });
                    return _context.BookCategories.Where(b => b.Name == name).FirstOrDefault()?.Id;
            }

            _context.SaveChanges();
            return null;
        }

        public void AddBookAuthor(int bookId, int author)
        {
            Book book = _context.Books.Where(u => u.Id == bookId).FirstOrDefault();
            Author auth = _context.Authors.Where(u => u.Id == author).FirstOrDefault();
            if (book == null || auth == null)
                return;

            book.Authors.Add(auth);
            _context.SaveChanges();
        }        
        public void AddItemCategory(int item, int cat, ServerData data)
        {
            switch (data)
            {
                case ServerData.VideoCategory:
                    if (_context.Videos.Where(u => u.Id == item).FirstOrDefault() == null || _context.VideoCategories.Where(u => u.Id == cat).FirstOrDefault() == null)
                        return;
                    _context.Videos.Where(u => u.Id == item).FirstOrDefault().Categories.Add(_context.VideoCategories.Where(u => u.Id == cat).FirstOrDefault());
                    break;

                case ServerData.BookCategory:
                    Book book = (_context.Books.Where(u => u.Id == item).FirstOrDefault());
                    BookCategory bookCategory = _context.BookCategories.Where(u => u.Id == cat).FirstOrDefault();
                    if (book == null || bookCategory == null)
                        return;
                    book.Categories.Add(bookCategory);
                    break;
            }
            _context.SaveChanges();
        }
        #endregion
        #region Edit.
        public void EditData(int id, string changes, ServerData data, PropertyData property)
        {
            switch (data)
            {
                case ServerData.User:
                    User user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
                    if (user == null)
                        return;

                    switch (property)
                    {
                        case PropertyData.Name:
                            user.Username = changes;
                            break;
                        case PropertyData.Role:
                            user.Roles = _context.Roles.Where(r => r.Name == changes).FirstOrDefault();
                            break;
                        case PropertyData.Imgpath:
                            user.Avatar = changes;
                            break;
                        case PropertyData.Password:
                            user.Password = changes;
                            break;
                        case PropertyData.Level:
                            user.Level = Convert.ToInt32(changes);
                            break;
                    }
                    _context.SaveChanges();
                    break;
            }
        }
        #endregion
        #region Check.
        public bool? CheckAbsolute(int id, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    Video tmp = _context.Videos.Where(u => u.Id == id).FirstOrDefault();
                    if (tmp == null)
                        return null;

                    return tmp.IsAbsolute;

                case ServerData.Book:
                    Book book = _context.Books.Where(u => u.Id == id).FirstOrDefault();
                    if (book == null)
                        return null;

                    return book.IsAbsolute;
            }

            return null;
        }
        public bool CheckExistence(string name, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    return _context.Videos.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Book:
                    return _context.Books.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.User:
                    return _context.Users.Where(u => u.Username == name).FirstOrDefault() != null;
                case ServerData.VideoCategory:
                    return _context.VideoCategories.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.BookCategory:
                    return _context.BookCategories.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Word:
                    return _context.Dictionary.Where(u => u.Name == name).FirstOrDefault() != null;
            }

            return false;
        }
        public bool CheckAuthor(string name, string surname)
        {
            return _context.Authors.Where(u => u.Name == name && u.Surname == surname).FirstOrDefault() != null;
        }
        public bool CheckUserPswd(string login, string pswd)
        {
            User tmp = _context.Users.Where(u => u.Username == login).FirstOrDefault();
            if (tmp == null)
                return false;

            return tmp.Password == pswd;
        }
        #endregion
        #region Remove.
        public void RemoveItem(int id, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    if (_context.Videos.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Videos.Remove(_context.Videos.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.Book:
                    if (_context.Books.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Books.Remove(_context.Books.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.User:
                    if (_context.Users.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Users.Remove(_context.Users.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.VideoCategory:
                    if (_context.VideoCategories.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.VideoCategories.Remove(_context.VideoCategories.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.BookCategory:
                    if (_context.BookCategories.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.BookCategories.Remove(_context.BookCategories.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.Word:
                    if (_context.Dictionary.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Dictionary.Remove(_context.Dictionary.Where(b => b.Id == id).FirstOrDefault());
                    break;
            }
            
            _context.SaveChanges();
        }
        public void RemoveItemWord(int item, int word, ServerData data)
        {
            if (_context.Dictionary.Where(u => u.Id == word).FirstOrDefault() == null)
                return;

            switch (data)
            {
                case ServerData.Video:
                    if (_context.Videos.Where(u => u.Id == item).FirstOrDefault() == null)
                        return;
                    _context.Videos.Where(u => u.Id == item).FirstOrDefault().Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                    break;

                case ServerData.Book:
                    if (_context.Books.Where(u => u.Id == item).FirstOrDefault() == null)
                        return;
                    _context.Books.Where(u => u.Id == item).FirstOrDefault().Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                    break;

                case ServerData.User:
                    if (_context.Users.Where(u => u.Id == item).FirstOrDefault() == null)
                        return;
                    _context.Users.Where(u => u.Id == item).FirstOrDefault().Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                    break;
            }
            _context.SaveChanges();
        }
        #endregion
    }
}