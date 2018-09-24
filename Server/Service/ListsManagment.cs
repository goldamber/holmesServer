using Server.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Server.Service
{
    public partial class EngService : IEngService
    {
        #region Get lists.
        public IEnumerable<int> GetSortedItems(ServerData data, PropertyData property, bool desc)
        {
            switch (data)
            {
                case ServerData.Video:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.Videos.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.Videos.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                        case PropertyData.Created:
                        case PropertyData.Date:
                            return desc ? _context.Videos.OrderByDescending(v => v.Created).Select(v => v.Id).ToList() : _context.Videos.OrderBy(v => v.Created).Select(v => v.Id).ToList();
                        case PropertyData.Year:
                            List<Video> tmp = new List<Video>(_context.Videos.Where(b => b.Year != null));
                            tmp = desc ? tmp.OrderByDescending(b => b.Year).ToList() : tmp.OrderBy(b => b.Year).ToList();
                            List<int> lst = new List<int>(tmp.Select(b => b.Id));
                            lst.AddRange(_context.Videos.Where(b => b.Year == null).Select(b => b.Id));
                            return lst;
                    }
                    break;
                case ServerData.Book:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.Books.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.Books.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                        case PropertyData.Created:
                        case PropertyData.Date:
                            return desc ? _context.Books.OrderByDescending(v => v.Created).Select(v => v.Id).ToList() : _context.Books.OrderBy(v => v.Created).Select(v => v.Id).ToList();
                        case PropertyData.Year:
                            List<Book> tmp = new List<Book>(_context.Books.Where(b => b.Year != null));
                            tmp = desc ? tmp.OrderByDescending(b => b.Year).ToList() : tmp.OrderBy(b => b.Year).ToList();
                            List<int> lst = new List<int>(tmp.Select(b => b.Id));
                            lst.AddRange(_context.Books.Where(b => b.Year == null).Select(b => b.Id));
                            return lst;
                    }
                    break;
                case ServerData.Author:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.Authors.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.Authors.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                        case PropertyData.Surname:
                            return desc ? _context.Authors.OrderByDescending(v => v.Surname).Select(v => v.Id).ToList() : _context.Authors.OrderBy(v => v.Surname).Select(v => v.Id).ToList();
                    }
                    break;
                case ServerData.BookCategory:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.BookCategories.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.BookCategories.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                    }
                    break;
                case ServerData.VideoCategory:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.VideoCategories.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.VideoCategories.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                    }
                    break;
                case ServerData.WordCategory:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.WordCategories.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.WordCategories.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                        case PropertyData.Abbreviation:
                            return desc ? _context.WordCategories.OrderByDescending(v => v.Abbreviation).Select(v => v.Id).ToList() : _context.WordCategories.OrderBy(v => v.Abbreviation).Select(v => v.Id).ToList();
                    }
                    break;
                case ServerData.Group:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.Groups.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.Groups.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                    }
                    break;
                case ServerData.User:
                    switch (property)
                    {
                        case PropertyData.Name:
                        case PropertyData.Login:
                            return desc ? _context.Users.OrderByDescending(v => v.Username).Select(v => v.Id).ToList() : _context.Users.OrderBy(v => v.Username).Select(v => v.Id).ToList();
                        case PropertyData.Level:
                            return desc ? _context.Users.OrderByDescending(v => v.Level).Select(v => v.Id).ToList() : _context.Users.OrderBy(v => v.Level).Select(v => v.Id).ToList();
                        case PropertyData.Role:
                            List<Role> roles = new List<Role>(_context.Roles);
                            roles = desc ? roles.OrderBy(r => r.Name).ToList() : roles.OrderByDescending(r => r.Name).ToList();
                            List<int> users = new List<int>();
                            foreach (Role item in roles)
                            {
                                foreach (User val in _context.Users)
                                {
                                    if (val.RoleID == item.Id && !users.Contains(val.Id))
                                        users.Add(val.Id);
                                }
                            }
                            return users;
                    }
                    break;
                case ServerData.Word:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.Dictionary.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.Dictionary.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                    }
                    break;
                case ServerData.Game:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.Games.OrderByDescending(v => v.Name).Select(v => v.Id).ToList() : _context.Games.OrderBy(v => v.Name).Select(v => v.Id).ToList();
                    }
                    break;
                case ServerData.Grammar:
                    switch (property)
                    {
                        case PropertyData.Name:
                            return desc ? _context.Grammars.OrderByDescending(v => v.Title).Select(v => v.Id).ToList() : _context.Grammars.OrderBy(v => v.Title).Select(v => v.Id).ToList();
                    }
                    break;
            }
            return null;
        }
        public Dictionary<int, int> GetHighScores(int game)
        {
            if (_context.Games.Where(g => g.Id == game).FirstOrDefault() == null)
                return null;
            Dictionary<int, int> lst = new Dictionary<int, int>();
            foreach (Score item in _context.Scores.Where(s => s.GameID == game).OrderByDescending(s => s.ScoreCount).ToList())
            {
                lst.Add(item.UserID, item.ScoreCount);
            }
            return lst;
        }
        public IEnumerable<int> GetToungeTwisters()
        {
            WordsGroup cat = _context.Groups.Where(c => c.Name == "Toungue twisters").FirstOrDefault();
            if (cat == null)
                return null;
            return cat.Words.Count == 0 ? null : cat.Words.Select(w => w.Id).ToList();
        }
        public IEnumerable<int> GetUserItemWords(int user, int item, ServerData data)
        {
            List<int> res = new List<int>();
            switch (data)
            {
                case ServerData.Video:
                    if (_context.Users.Where(u => u.Id == user).FirstOrDefault() == null || _context.Videos.Where(u => u.Id == item).FirstOrDefault() == null)
                        return null;
                    res = _context.Dictionary.Where(w => w.Users.Contains(_context.Users.Where(u => u.Id == user).FirstOrDefault()) && w.Videos.Contains(_context.Videos.Where(u => u.Id == item).FirstOrDefault())).Select(w => w.Id).ToList();
                    break;
                case ServerData.Book:
                    if (_context.Users.Where(u => u.Id == user).FirstOrDefault() == null || _context.Books.Where(u => u.Id == item).FirstOrDefault() == null)
                        return null;
                    res = _context.Dictionary.Where(w => w.Users.Contains(_context.Users.Where(u => u.Id == user).FirstOrDefault()) && w.Books.Contains(_context.Books.Where(u => u.Id == item).FirstOrDefault())).Select(w => w.Id).ToList();
                    break;
            }
            return res.Count == 0 ? null : res;
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
                            return video.Words.Count == 0 ? null : video.Words.Select(w => w.Id);
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
                            return book.Words.Count == 0? null: book.Words.Select(w => w.Id);
                    }
                    break;
                case ServerData.Grammar:
                    Grammar grammar = _context.Grammars.Where(u => u.Id == id).FirstOrDefault();
                    if (grammar == null)
                        return null;
                    switch (res)
                    {
                        case ServerData.Rule:
                            return grammar.Rules.Count == 0? null : grammar.Rules.Select(c => c.Id);
                        case ServerData.GrammarExample:
                            return grammar.Examples.Count == 0? null : grammar.Examples.Select(c => c.Id);
                        case ServerData.GrammarException:
                            return grammar.Exceptions.Count == 0 ? null : grammar.Exceptions.Select(w => w.Id);
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
                        case ServerData.Group:
                            return word.Groups.Select(c => c.Id);
                        case ServerData.Example:
                            return _context.Examples.Where(e => e.WordID == word.Id).Select(e => e.Id);
                    }
                    break;
                case ServerData.Author:
                    Author author = _context.Authors.Where(u => u.Id == id).FirstOrDefault();
                    if (author == null)
                        return null;
                    switch (res)
                    {
                        case ServerData.Book:
                            return author.Books.Select(c => c.Id);
                    }
                    break;
                case ServerData.BookCategory:
                    BookCategory bc = _context.BookCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (bc == null)
                        return null;
                    switch (res)
                    {
                        case ServerData.Book:
                            return bc.Books.Select(c => c.Id);
                    }
                    break;
                case ServerData.VideoCategory:
                    VideoCategory vc = _context.VideoCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (vc == null)
                        return null;
                    switch (res)
                    {
                        case ServerData.Video:
                            return vc.Videos.Select(c => c.Id);
                    }
                    break;
                case ServerData.WordCategory:
                    WordCategory wc = _context.WordCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (wc == null)
                        return null;
                    switch (res)
                    {
                        case ServerData.Word:
                            return wc.Words.Select(c => c.Id);
                    }
                    break;
                case ServerData.Group:
                    WordsGroup wg = _context.Groups.Where(u => u.Id == id).FirstOrDefault();
                    if (wg == null)
                        return null;
                    switch (res)
                    {
                        case ServerData.Word:
                            return wg.Words.Select(c => c.Id);
                    }
                    break;
            }
            return null;
        }
        public IEnumerable<int> GetWordsWithImages(int cat, ServerData type)
        {
            switch (type)
            {
                case ServerData.Group:
                    WordsGroup group = _context.Groups.Where(g => g.Id == cat).FirstOrDefault();
                    if (group == null)
                        return null;
                    return group.Words.Where(w => w.ImgPath != null).Select(w => w.Id).ToList();
                case ServerData.WordCategory:
                    WordCategory category = _context.WordCategories.Where(g => g.Id == cat).FirstOrDefault();
                    if (category == null)
                        return null;
                    return category.Words.Where(w => w.ImgPath != null).Select(w => w.Id).ToList();
                default:
                    return null;
            }
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