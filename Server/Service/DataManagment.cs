using Server.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Server.Service
{
    public partial class EngService : IEngService
    {
        #region Check.
        public bool? CheckAbsolute(int id, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    Video tmp = _context.Videos.Where(u => u.Id == id).FirstOrDefault();
                    if (tmp == null)
                        return null;

                    return tmp.IsAbsolulute;

                case ServerData.Book:
                    Book book = _context.Books.Where(u => u.Id == id).FirstOrDefault();
                    if (book == null)
                        return null;

                    return book.IsAbsolulute;
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
        #region Get data.
        public string GetItemProperty(int id, ServerData data, PropertyData property)
        {
            switch (data)
            {
                case ServerData.Video:
                    Video video = _context.Videos.Where(u => u.Id == id).FirstOrDefault();
                    if (video == null)
                        return null;

                    switch (property)
                    {
                        case PropertyData.Name:
                            return video.Name;
                        case PropertyData.Description:
                            return video.Description;
                        case PropertyData.Path:
                            return video.Path;
                        case PropertyData.Imgpath:
                            return video.ImgPath;
                        case PropertyData.SubPath:
                            return video.SubPath;
                        case PropertyData.Mark:
                            return video.Mark.ToString();
                        case PropertyData.Year:
                            return video.Year.ToString();
                        case PropertyData.Created:
                            return video.Created.ToLongDateString();
                    }
                    break;

                case ServerData.Book:
                    Book book = _context.Books.Where(u => u.Id == id).FirstOrDefault();
                    if (book == null)
                        return null;

                    switch (property)
                    {
                        case PropertyData.Name:
                            return book.Name;
                        case PropertyData.Description:
                            return book.Description;
                        case PropertyData.Path:
                            return book.Path;
                        case PropertyData.Imgpath:
                            return book.ImgPath;
                        case PropertyData.Mark:
                            return book.Mark.ToString();
                        case PropertyData.Year:
                            return book.Year.ToString();
                        case PropertyData.Created:
                            return book.Created.ToLongDateString();
                    }
                    break;
                case ServerData.User:
                    User user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
                    if (user == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return user.Username;
                        case PropertyData.Imgpath:
                            return user.Avatar;
                        case PropertyData.Password:
                            return user.Password;
                        case PropertyData.Level:
                            return user.Level.ToString();
                        case PropertyData.Role:
                            return user.RoleID.ToString();
                    }
                    break;
                case ServerData.VideoCategory:
                    VideoCategory videoCategory = _context.VideoCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (videoCategory == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return videoCategory.Name;
                    }
                    break;
                case ServerData.BookCategory:
                    BookCategory bookCategory = _context.BookCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (bookCategory == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return bookCategory.Name;
                    }
                    break;
                case ServerData.Word:
                    Word word = _context.Dictionary.Where(u => u.Id == id).FirstOrDefault();
                    if (word == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return word.Name;
                        case PropertyData.Imgpath:
                            return word.ImgPath;
                    }
                    break;
                case ServerData.WordForm:
                    WordForm wordForm = _context.WordForms.Where(u => u.Id == id).FirstOrDefault();
                    if (wordForm == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.PastForm:
                            return wordForm.PastForm;
                        case PropertyData.PastThForm:
                            return wordForm.PastThForm;
                        case PropertyData.PluralForm:
                            return wordForm.PluralForm;
                    }
                    break;
                case ServerData.WordCategory:
                    WordCategory wordCategory = _context.WordCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (wordCategory == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return wordCategory.Name;
                    }
                    break;
                case ServerData.Translation:
                    Translation translation = _context.Translations.Where(u => u.Id == id).FirstOrDefault();
                    if (translation == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return translation.Name;
                    }
                    break;
                case ServerData.Definition:
                    Definition definition = _context.Definitions.Where(u => u.Id == id).FirstOrDefault();
                    if (definition == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return definition.Name;
                    }
                    break;
                case ServerData.Author:
                    Author author = _context.Authors.Where(u => u.Id == id).FirstOrDefault();
                    if (author == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return author.Name + " " + author.Surname;
                    }
                    break;
                case ServerData.Game:
                    Game game = _context.Games.Where(u => u.Id == id).FirstOrDefault();
                    if (game == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return game.Name;
                        case PropertyData.Description:
                            return game.Description;
                    }
                    break;
                case ServerData.Example:
                    Example example = _context.Examples.Where(u => u.Id == id).FirstOrDefault();
                    if (example == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return example.Name;
                    }
                    break;
            }

            return null;
        }
        public int? GetUserId(string login)
        {
            return _context.Users.Where(u => u.Username == login).FirstOrDefault()?.Id;
        }

        public IEnumerable<int> GetItems(ServerData data)
        {
            List<string> lst = new List<string>();
            switch (data)
            {
                case ServerData.Video:
                    return _context.Videos.Select(f => f.Id).ToList();
                case ServerData.Book:
                    return _context.Books.Select(f => f.Id).ToList();
                case ServerData.User:
                    return _context.Users.Select(f => f.Id).ToList();
                case ServerData.VideoCategory:
                    return _context.VideoCategories.Select(f => f.Id).ToList();
                case ServerData.BookCategory:
                    return _context.BookCategories.Select(f => f.Id).ToList();
                case ServerData.Word:
                    return _context.Dictionary.Select(f => f.Id).ToList();
                case ServerData.WordCategory:
                    return _context.WordCategories.Select(f => f.Id).ToList();
                case ServerData.Author:
                    return _context.Authors.Select(f => f.Id).ToList();
                case ServerData.Game:
                    return _context.Games.Select(f => f.Id).ToList();
            }

            return null;
        }
        public IEnumerable<int> GetFItems(string filter, ServerData data, PropertyData fil)
        {
            filter = filter.ToLower();

            switch (data)
            {
                case ServerData.Video:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Videos.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Description:
                            return _context.Videos.Where(v => v.Description.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Year:
                            return _context.Videos.Where(v => v.Year.ToString().Equals(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Category:
                            if (_context.VideoCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Videos.Where(v => v.Categories.Contains(_context.VideoCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault())).Select(f => f.Id).ToList();
                    }
                    break;
                case ServerData.Book:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Books.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Description:
                            return _context.Books.Where(v => v.Description.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Year:
                            return _context.Books.Where(v => v.Year.ToString().Equals(filter)).Select(f => f.Id).ToList();

                        case PropertyData.Category:
                            if (_context.BookCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Books.Where(v => v.Categories.Contains(_context.BookCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault())).Select(f => f.Id).ToList();
                        case PropertyData.Author:
                            if (_context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Books.Where(v => v.Authors.Contains(_context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter)).FirstOrDefault())).Select(f => f.Id).ToList();
                    }
                    break;
                case ServerData.User:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Users.Where(v => v.Username.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Role:
                            if (_context.Roles.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Users.Where(v => v.RoleID == _context.Roles.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault().Id).Select(f => f.Id).ToList();
                    }
                    break;
                case ServerData.VideoCategory:
                    return _context.VideoCategories.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                case ServerData.BookCategory:
                    return _context.BookCategories.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                case ServerData.Word:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Dictionary.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Category:
                            if (_context.WordCategories.Where(c => c.Name.ToLower().Contains(filter) || c.Abbreviation.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Dictionary.Where(v => v.Categories.Contains(_context.WordCategories.Where(c => c.Name.ToLower().Contains(filter) || c.Abbreviation.ToLower().Contains(filter)).FirstOrDefault())).Select(f => f.Id).ToList();

                        case PropertyData.Synonyms:
                            if (_context.Translations.Where(c => c.Name.ToLower() == filter).FirstOrDefault() == null && _context.Definitions.Where(c => c.Name.ToLower() == filter).FirstOrDefault() == null)
                                return null;
                            return _context.Translations.Where(c => c.Name.ToLower() == filter).FirstOrDefault() != null ? _context.Dictionary.Where(v => v.Translations.Contains(_context.Translations.Where(c => c.Name.ToLower() == filter).FirstOrDefault())).Select(f => f.Id).ToList() : _context.Dictionary.Where(v => v.Descriptions.Contains(_context.Definitions.Where(c => c.Name.ToLower() == filter).FirstOrDefault())).Select(f => f.Id).ToList();

                        case PropertyData.Translation:
                            if (_context.Translations.Where(c => c.Name.ToLower().Contains(filter)) == null)
                                return null;
                            return _context.Dictionary.Where(w => w.Translations.Contains(_context.Translations.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault())).Select(w => w.Id).ToList();

                        case PropertyData.Definition:
                            if (_context.Definitions.Where(c => c.Name.ToLower().Contains(filter)) == null)
                                return null;
                            return _context.Dictionary.Where(w => w.Descriptions.Contains(_context.Definitions.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault())).Select(w => w.Id).ToList();
                    }
                    break;
                case ServerData.Author:
                    return _context.Authors.Where(v => v.Name.ToLower().Contains(filter) || v.Surname.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                case ServerData.Game:
                    return _context.Games.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
            }

            return null;
        }

        public IEnumerable<int> GetUserItemWords(int user, int item, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    if (_context.Users.Where(u => u.Id == user).FirstOrDefault() == null || _context.Videos.Where(u => u.Id == item).FirstOrDefault() == null)
                        return null;
                    return _context.Dictionary.Where(w => w.Users.Contains(_context.Users.Where(u => u.Id == user).FirstOrDefault()) && w.Videos.Contains(_context.Videos.Where(u => u.Id == item).FirstOrDefault())).Select(w => w.Id).ToList();

                case ServerData.Book:
                    if (_context.Users.Where(u => u.Id == user).FirstOrDefault() == null || _context.Books.Where(u => u.Id == item).FirstOrDefault() == null)
                        return null;
                    return _context.Dictionary.Where(w => w.Users.Contains(_context.Users.Where(u => u.Id == user).FirstOrDefault()) && w.Books.Contains(_context.Books.Where(u => u.Id == item).FirstOrDefault())).Select(w => w.Id).ToList();

                default:
                    return null;
            }
        }
        public IEnumerable<int> GetItemData(int id, ServerData data, ServerData res)
        {
            switch (data)
            {
                case ServerData.Video:
                    Video video = _context.Videos.Where(u => u.Id == id).FirstOrDefault();
                    if (video == null)
                        return null;

                    switch (res)
                    {
                        case ServerData.VideoCategory:
                            return video.Categories.Select(c => c.Id);
                        case ServerData.Word:
                            return video.Words.Select(c => c.Id);
                    }
                    break;

                case ServerData.Book:
                    Book book = _context.Books.Where(u => u.Id == id).FirstOrDefault();
                    if (book == null)
                        return null;

                    switch (res)
                    {
                        case ServerData.Author:
                            return book.Authors.Select(c => c.Id);
                        case ServerData.BookCategory:
                            return book.Categories.Select(c => c.Id);
                        case ServerData.Word:
                            return book.Words.Select(c => c.Id);
                    }
                    break;

                case ServerData.Word:
                    Word word = _context.Dictionary.Where(u => u.Id == id).FirstOrDefault();
                    if (word == null)
                        return null;

                    switch (res)
                    {
                        case ServerData.WordCategory:
                            IEnumerable<int> lst = _context.WordCategories.Where(w => w.Words.Contains(_context.Dictionary.Where(v => v.Id == word.Id).FirstOrDefault())).Select(c => c.Id);
                            return lst;
                        case ServerData.Translation:
                            return word.Translations.Select(c => c.Id);
                        case ServerData.Definition:
                            return word.Descriptions.Select(c => c.Id);
                        case ServerData.Example:
                            return _context.Examples.Where(e => e.WordID == word.Id).Select(e => e.Id);
                    }
                    break;
            }

            return null;
        }        
        #endregion
    }
}