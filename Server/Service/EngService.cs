using Server.Database;
using Server.Entities;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;

namespace Server.Service
{
    //Types of data, that can be used for the general actions (insert, remove, edit, view).
    public enum ServerData { Video, Book, User, Role, VideoCategory, BookCategory, Word, WordForm, WordCategory, Translation, Definition, Author, Game, Example, Bookmark, Group }
    //Describes the properties, that have to be sent to the client.
    public enum PropertyData { Name, Login, Role, RolesName, Description, Path, IsAbsolute, SubPath, Imgpath, Mark, Created, Date, Position, ScoreCount, Password, Level, Year, PastForm, PastThForm, PluralForm, Category, Categories, Author, Authors, Synonyms, Translation, Translations, Definition, Definitions, Group, Groups }
    //Types of files to be uploaded or downloaded.
    public enum FilesType { Video, Avatar, BookImage, WordImage, VideoImage, Book }

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
        public int? AddBook(string name, string desc, string path, string img, bool absolute, int? mark, int? user, int? year, DateTime created)
        {
            _context.Books.Add(new Book { Name = name, Description = (desc == "" ? null : desc), Path = path, ImgPath = img, IsAbsolute = absolute, Created = created, Year = year });
            _context.SaveChanges();
            if (mark != null)
                _context.BookStars.Add(new BookStar { MarkCount = Convert.ToInt32(mark), BookID = _context.Books.Where(v => v.Name == name).FirstOrDefault().Id, UserID = Convert.ToInt32(user) });
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
        public int? AddVideo(string name, string desc, string path, string sub, string img, bool absolute, int? mark, int? user, int? year, DateTime created)
        {
            _context.Videos.Add(new Video { Name = name, Description = (desc == "" ? null : desc), Path = path, SubPath = sub, ImgPath = img, IsAbsolute = absolute, Created = created, Year = year });
            _context.SaveChanges();
            if (mark != null)
                _context.VideoStars.Add(new VideoStar { MarkCount = Convert.ToInt32(mark), VideoID = _context.Videos.Where(v => v.Name == name).FirstOrDefault().Id, UserID = Convert.ToInt32(user) });
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
                    break;

                case ServerData.Book:
                    Book book = _context.Books.Where(u => u.Id == id).FirstOrDefault();
                    if (book == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            book.Name = changes;
                            break;
                        case PropertyData.Description:
                            book.Description = changes;
                            break;
                        case PropertyData.Path:
                            book.Path = changes;
                            break;
                        case PropertyData.Imgpath:
                            book.ImgPath = changes;
                            break;
                        case PropertyData.Year:
                            book.Year = Convert.ToInt32(changes);
                            break;
                        case PropertyData.IsAbsolute:
                            book.IsAbsolute = changes != null;
                            break;
                    }
                    break;
            }
            _context.SaveChanges();
        }
        public void EditMark(int id, int usersId, int changes, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    VideoStar video = _context.VideoStars.Where(v => v.VideoID == id && v.UserID == usersId).FirstOrDefault();
                    if (video == null)
                        _context.VideoStars.Add(new VideoStar { MarkCount = changes, UserID = usersId, VideoID = id });
                    else
                        video.MarkCount = changes;
                    break;
                case ServerData.Book:
                    BookStar book = _context.BookStars.Where(b => b.BookID == id && b.UserID == usersId).FirstOrDefault();
                    if (book == null)
                        _context.BookStars.Add(new BookStar { MarkCount = changes, UserID = usersId, BookID = id });
                    else
                        book.MarkCount = changes;
                    break;
            }
            _context.SaveChanges();
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
        public void RemoveFullItemData(int id, ServerData data)
        {
            switch (data)
            {
                case ServerData.VideoCategory:
                    _context.Videos.Where(v => v.Id == id).FirstOrDefault()?.Categories.Clear();
                    break;
                case ServerData.BookCategory:
                    _context.Books.Where(b => b.Id == id).FirstOrDefault()?.Categories.Clear();
                    break;
                case ServerData.Author:
                    _context.Books.Where(b => b.Id == id).FirstOrDefault()?.Authors.Clear();
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

        #region Upload/Download.
        public byte[] Download(string name, FilesType type)
        {
            switch (type)
            {
                case FilesType.Video:
                    if (File.Exists($@"Videos\{name}"))
                        return File.ReadAllBytes($@"Videos\{name}");
                    return null;
                case FilesType.BookImage:
                    if (File.Exists($@"BookImages\{name}"))
                        return File.ReadAllBytes($@"BookImages\{name}");
                    return null;
                case FilesType.WordImage:
                    if (File.Exists($@"WordImages\{name}"))
                        return File.ReadAllBytes($@"WordImages\{name}");
                    return null;
                case FilesType.VideoImage:
                    if (File.Exists($@"VideoImages\{name}"))
                        return File.ReadAllBytes($@"VideoImages\{name}");
                    return null;
                case FilesType.Avatar:
                    if (File.Exists($@"Avatars\{name}"))
                        return File.ReadAllBytes($@"Avatars\{name}");
                    return null;
                case FilesType.Book:
                    if (File.Exists($@"Books\{name}"))
                        return File.ReadAllBytes($@"Books\{name}");
                    return null;
                default:
                    return null;
            }
        }
        public bool Upload(byte[] file, string name, FilesType type)
        {
            try
            {
                string fileName = "";
                switch (type)
                {
                    case FilesType.Video:
                        if (!Directory.Exists("Videos"))
                            Directory.CreateDirectory("Videos");
                        fileName = $@"Videos\{name}";
                        break;
                    case FilesType.BookImage:
                        if (!Directory.Exists("BookImages"))
                            Directory.CreateDirectory("BookImages");
                        fileName = $@"BookImages\{name}";
                        break;
                    case FilesType.VideoImage:
                        if (!Directory.Exists("VideoImages"))
                            Directory.CreateDirectory("VideoImages");
                        fileName = $@"VideoImages\{name}";
                        break;
                    case FilesType.Avatar:
                        if (!Directory.Exists("Avatars"))
                            Directory.CreateDirectory("Avatars");
                        fileName = $@"Avatars\{name}";
                        break;
                    case FilesType.WordImage:
                        if (!Directory.Exists("WordImages"))
                            Directory.CreateDirectory("WordImages");
                        fileName = $@"WordImages\{name}";
                        break;
                    case FilesType.Book:
                        if (!Directory.Exists("Books"))
                            Directory.CreateDirectory("Books");
                        fileName = $@"Books\{name}";
                        break;
                }

                using (FileStream fs = File.Create(fileName))
                {
                    fs.Write(file, 0, file.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void Delete(string name, FilesType type)
        {
            switch (type)
            {
                case FilesType.Video:
                    if (File.Exists($@"Videos\{name}"))
                        File.Delete($@"Videos\{name}");
                    break;
                case FilesType.BookImage:
                    if (File.Exists($@"BookImages\{name}"))
                        File.Delete($@"BookImages\{name}");
                    break;
                case FilesType.WordImage:
                    if (File.Exists($@"WordImages\{name}"))
                        File.Delete($@"WordImages\{name}");
                    break;
                case FilesType.VideoImage:
                    if (File.Exists($@"VideoImages\{name}"))
                        File.Delete($@"VideoImages\{name}");
                    break;
                case FilesType.Avatar:
                    if (File.Exists($@"Avatars\{name}"))
                        File.Delete($@"Avatars\{name}");
                    break;
                case FilesType.Book:
                    if (File.Exists($@"Books\{name}"))
                        File.Delete($@"Books\{name}");
                    break;
            }
        }
        #endregion
    }
}