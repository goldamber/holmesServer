using Server.Entities;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Server.Service
{
    //Types of data, that can be used for the general actions (insert, remove, edit, view).
    public enum ServerData { Video, Book, User, Role, VideoCategory, BookCategory, Word, WordForm, WordCategory, Translation, Definition, Author, Game, Example, Bookmark, VideoBookmark, Group, Transcription, GrammarExample, Grammar, GrammarException, Rule }
    //Describes the properties, that have to be sent to the client.
    public enum PropertyData { Name, Surname, Login, Abbreviation, Role, RolesName, Description, Path, IsAbsolute, SubPath, Imgpath, Mark, Created, Date, Position, ScoreCount, Password, Level, Year, PastForm, PastThForm, PluralForm, Category, Categories, Author, Authors, Synonyms, Homophones, Translation, Translations, Definition, Definitions, Group, Groups, Book, Books, Word, Words, Video, Videos, Example, Examples, Transcription, British, American, Australian, Canadian }
    //Types of files to be uploaded or downloaded.
    public enum FilesType { Videos, Avatars, BooksImages, WordsImages, VideosImages, Books, Subtitles }

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
                case ServerData.Grammar:
                    Grammar grammar = _context.Grammars.Where(u => u.Id == id).FirstOrDefault();
                    if (grammar == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return grammar.Title;
                        case PropertyData.Description:
                            return grammar.Description;
                    }
                    break;
                case ServerData.Rule:
                    Entities.Rule rule = _context.Rules.Where(u => u.Id == id).FirstOrDefault();
                    switch (property)
                    {
                        case PropertyData.Name:
                            return rule?.Name;
                    }
                    break;
                case ServerData.GrammarExample:
                   GrammarExample ge = _context.GrammarExamples.Where(u => u.Id == id).FirstOrDefault();
                    switch (property)
                    {
                        case PropertyData.Name:
                            return ge?.Name;
                    }
                    break;
                case ServerData.GrammarException:
                    GrammarException gex = _context.Exceptions.Where(u => u.Id == id).FirstOrDefault();
                    switch (property)
                    {
                        case PropertyData.Name:
                            return gex?.Name;
                    }
                    break;
                case ServerData.Role:
                    Role role = _context.Roles.Where(u => u.Id == id).FirstOrDefault();
                    switch (property)
                    {
                        case PropertyData.Name:
                            return role?.Name;
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
                        case PropertyData.PluralForm:
                            return _context.WordForms.Where(f => f.Id == word.FormID).FirstOrDefault()?.PluralForm;
                        case PropertyData.PastForm:
                            return _context.WordForms.Where(f => f.Id == word.FormID).FirstOrDefault()?.PastForm;
                        case PropertyData.PastThForm:
                            return _context.WordForms.Where(f => f.Id == word.FormID).FirstOrDefault()?.PastThForm;
                        case PropertyData.Transcription:
                            return word.TranscriptionID == null? null: word.TranscriptionID.ToString();
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
                case ServerData.Group:
                    WordsGroup group = _context.Groups.Where(u => u.Id == id).FirstOrDefault();
                    if (group == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Name:
                            return group.Name;
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
                        case PropertyData.Abbreviation:
                            return wordCategory.Abbreviation;
                    }
                    break;
                case ServerData.Transcription:
                    Transcription transcription = _context.Transcriptions.Where(u => u.Id == id).FirstOrDefault();
                    if (transcription == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.British:
                            return transcription.British;
                        case PropertyData.Canadian:
                            return transcription.Canadian;
                        case PropertyData.Australian:
                            return transcription.Australian;
                        case PropertyData.American:
                            return transcription.American;
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
                case ServerData.VideoBookmark:
                    VideoBookmark videoBookmark = _context.VideoBookmarks.Where(u => u.Id == id).FirstOrDefault();
                    if (videoBookmark == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Position:
                            return videoBookmark.Position.ToString();
                    }
                    break;
                case ServerData.Bookmark:
                    Bookmark bookmark = _context.Bookmarks.Where(u => u.Id == id).FirstOrDefault();
                    if (bookmark == null)
                        return null;
                    switch (property)
                    {
                        case PropertyData.Position:
                            return bookmark.Position.ToString();
                    }
                    break;
            }

            return null;
        }
        public int? GetWord(string search)
        {
            search = search.ToLower();
            Word word = _context.Dictionary.Where(w => w.Name.ToLower().Equals(search) || w.Form.PluralForm.ToLower().Equals(search)
                || w.Form.PastForm.ToLower().Equals(search) || w.Form.PastThForm.ToLower().Equals(search)
                || (w.Name.ToLower() + "s").Equals(search) || (w.Name.ToLower() + "es").Equals(search)
                || (w.Name.ToLower().EndsWith("ies") && (w.Name.ToLower().Substring(0, w.Name.Length - 3) + "y").Equals(search))
                || (w.Name.ToLower().EndsWith("ing") && (w.Name.ToLower().Substring(0, w.Name.Length - 3).Equals(search)))).FirstOrDefault();
            if (word == null)
            {
                if (search == "has" || search == "having")
                    word = _context.Dictionary.Where(w => w.Name == "have").FirstOrDefault();
                if (search == "won't")
                    word = _context.Dictionary.Where(w => w.Name == "will").FirstOrDefault();
            }
            return word?.Id;
        }
        public int? GetItemsId(string name, ServerData type)
        {
            switch (type)
            {
                case ServerData.User:
                    return _context.Users.Where(u => u.Username == name).FirstOrDefault()?.Id;
                case ServerData.Word:
                    return _context.Dictionary.Where(u => u.Name == name).FirstOrDefault()?.Id;
                case ServerData.Translation:
                    return _context.Translations.Where(u => u.Name == name).FirstOrDefault()?.Id;
                case ServerData.Definition:
                    return _context.Definitions.Where(u => u.Name == name).FirstOrDefault()?.Id;
                case ServerData.Example:
                    return _context.Examples.Where(u => u.Name == name).FirstOrDefault()?.Id;
                case ServerData.Group:
                    return _context.Groups.Where(u => u.Name == name).FirstOrDefault()?.Id;
                case ServerData.GrammarExample:
                    return _context.GrammarExamples.Where(u => u.Name == name).FirstOrDefault()?.Id;
                case ServerData.GrammarException:
                    return _context.Exceptions.Where(u => u.Name == name).FirstOrDefault()?.Id;
                case ServerData.Rule:
                    return _context.Rules.Where(u => u.Name == name).FirstOrDefault()?.Id;
                default:
                    return null;
            }
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
        public int? GetLastMark(int item, int user, ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    return _context.VideoBookmarks.Where(vb => vb.UserID == user && vb.VideoID == item).FirstOrDefault()?.Id;
                case ServerData.Book:
                    return _context.Bookmarks.Where(bm => bm.UserID == user && bm.BookID == item).FirstOrDefault()?.Id;
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
                case ServerData.WordCategory:
                    return _context.WordCategories.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Transcription:
                    return _context.Transcriptions.Where(u => u.British == name || u.American == name || u.Canadian == name || u.Australian == name).FirstOrDefault() != null;
                case ServerData.Translation:
                    return _context.Translations.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Definition:
                    return _context.Definitions.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Example:
                    return _context.Examples.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Group:
                    return _context.Groups.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Word:
                    return _context.Dictionary.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.Grammar:
                    return _context.Grammars.Where(u => u.Title == name).FirstOrDefault() != null;
                case ServerData.Rule:
                    return _context.Rules.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.GrammarExample:
                    return _context.GrammarExamples.Where(u => u.Name == name).FirstOrDefault() != null;
                case ServerData.GrammarException:
                    return _context.Exceptions.Where(u => u.Name == name).FirstOrDefault() != null;
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
        #region Upload/Download.
        public byte[] Download(string name, FilesType type)
        {
            if (File.Exists($@"{type.ToString()}\{name}"))
                return File.ReadAllBytes($@"{type.ToString()}\{name}");
            return null;
        }
        public bool Upload(byte[] file, string name, FilesType type)
        {
            try
            {
                string fileName = "";
                if (!Directory.Exists(type.ToString()))
                    Directory.CreateDirectory(type.ToString());
                fileName = $@"{type.ToString()}\{name}";

                using (FileStream fs = File.Create(fileName))
                {
                    fs.Write(file, 0, file.Length);
                    fs.Dispose();
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
            if (File.Exists($@"{type.ToString()}\{name}"))
                File.Delete($@"{type.ToString()}\{name}");
        }
        #endregion
    }
}