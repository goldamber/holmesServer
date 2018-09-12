using Server.Entities;
using System.Data;
using System.Linq;

namespace Server.Service
{
    public partial class EngService : IEngService
    {
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
                        case PropertyData.Year:
                            return video.Year == null? null: video.Year.ToString();
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
                        case PropertyData.Year:
                            return book.Year == null ? null : book.Year.ToString();
                        case PropertyData.Created:
                            return book.Created.ToLongDateString();
                    }
                    break;

                case ServerData.Role:
                    Role role = _context.Roles.Where(u => u.Id == id).FirstOrDefault();
                    if (role == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return role.Name;
                    }
                    break;

                case ServerData.User:
                    User user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
                    if (user == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                        case PropertyData.Login:
                            return user.Username;
                        case PropertyData.Imgpath:
                            return user.Avatar;
                        case PropertyData.Password:
                            return user.Password;
                        case PropertyData.Level:
                            return user.Level.ToString();
                        case PropertyData.Role:
                            return user.RoleID.ToString();
                        case PropertyData.RolesName:
                            return _context.Roles.Where(r => r.Id == user.RoleID).FirstOrDefault()?.Name;
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
                            return author.Name;
                        case PropertyData.Surname:
                            return author.Surname;
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

        public int GetLastId(ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    return _context.Videos.Select(v => v.Id).Max() + 1;
                case ServerData.Book:
                    return _context.Books.Select(v => v.Id).Max() + 1;
                case ServerData.User:
                    return _context.Users.Select(v => v.Id).Max() + 1;
                case ServerData.Word:
                    return _context.Dictionary.Select(v => v.Id).Max() + 1;
                default:
                    return -1;
            }
        }
        public int? GetUserId(string login)
        {
            return _context.Users.Where(u => u.Username == login).FirstOrDefault()?.Id;
        }
        public int? GetMark(int itemId, int userId, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    return _context.VideoStars.Where(v => v.VideoID == itemId && v.UserID == userId).FirstOrDefault()?.MarkCount;
                case ServerData.Book:
                    return _context.BookStars.Where(b => b.BookID == itemId && b.UserID == userId).FirstOrDefault()?.MarkCount;
                default:
                    return null;
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
    }
}