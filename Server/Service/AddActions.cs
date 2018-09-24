using System;
using Server.Entities;
using System.Data;
using System.Linq;

namespace Server.Service
{
    public partial class EngService : IEngService
    {
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
        public int? AddWord(string name, string img, int user)
        {
            User tmp = _context.Users.Where(u => u.Id == user).FirstOrDefault();
            if (tmp == null)
                return null;
            Word word = new Word { Name = name, ImgPath = img };
            word.Users.Add(tmp);
            _context.Dictionary.Add(word);
            _context.SaveChanges();
            return _context.Dictionary.Where(a => a.Name == name && a.ImgPath == img).FirstOrDefault()?.Id;
        }
        public int? AddGrammar(string name, string desc)
        {
            _context.Grammars.Add(new Grammar { Title = name, Description = desc });
            _context.SaveChanges();
            return _context.Grammars.Where(a => a.Title == name).FirstOrDefault()?.Id;
        }
        public int? AddData(string name, ServerData data)
        {
            switch (data)
            {
                case ServerData.VideoCategory:
                    _context.VideoCategories.Add(new VideoCategory { Name = name });
                    _context.SaveChanges();
                    return _context.VideoCategories.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.BookCategory:
                    _context.BookCategories.Add(new BookCategory { Name = name });
                    _context.SaveChanges();
                    return _context.BookCategories.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.Group:
                    _context.Groups.Add(new WordsGroup { Name = name });
                    _context.SaveChanges();
                    return _context.Groups.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.Translation:
                    _context.Translations.Add(new Translation { Name = name });
                    _context.SaveChanges();
                    return _context.Translations.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.Definition:
                    _context.Definitions.Add(new Definition { Name = name });
                    _context.SaveChanges();
                    return _context.Definitions.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.Example:
                    _context.Examples.Add(new Example { Name = name });
                    _context.SaveChanges();
                    return _context.Examples.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.GrammarExample:
                    _context.GrammarExamples.Add(new GrammarExample { Name = name });
                    _context.SaveChanges();
                    return _context.GrammarExamples.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.GrammarException:
                    _context.Exceptions.Add(new GrammarException { Name = name });
                    _context.SaveChanges();
                    return _context.Exceptions.Where(b => b.Name == name).FirstOrDefault()?.Id;
                case ServerData.Rule:
                    _context.Rules.Add(new Entities.Rule { Name = name });
                    _context.SaveChanges();
                    return _context.Rules.Where(b => b.Name == name).FirstOrDefault()?.Id;
            }
            return null;
        }
        public int? AddWordsForm(int word, string past, string plural, string pastTh)
        {
            Word item = _context.Dictionary.Where(w => w.Id == word).FirstOrDefault();
            if (item == null)
                return null;
            _context.WordForms.Add(new WordForm { PastForm = past, PastThForm = pastTh, PluralForm = plural });
            _context.SaveChanges();
            item.Form = _context.WordForms.Where(wf => wf.PastForm == past && wf.PastThForm == pastTh && wf.PluralForm == plural).FirstOrDefault();
            _context.SaveChanges();
            return item.FormID;
        }
        public int? AddWordsTranscription(int word, string british, string canadian, string american, string australian)
        {
            Word item = _context.Dictionary.Where(w => w.Id == word).FirstOrDefault();
            if (item == null)
                return null;
            _context.Transcriptions.Add(new Transcription { British = british, Canadian = canadian, American = american, Australian = australian });
            _context.SaveChanges();
            item.Transcriptions = _context.Transcriptions.Where(wf => wf.British == british && wf.Canadian == canadian && wf.American == american && wf.Australian == australian).FirstOrDefault();
            _context.SaveChanges();
            return item.TranscriptionID;
        }
        public int? AddWordsCategory(string name, string abbr)
        {
            _context.WordCategories.Add(new WordCategory { Name = name, Abbreviation = abbr });
            _context.SaveChanges();
            return _context.WordCategories.Where(b => b.Name == name).FirstOrDefault()?.Id;
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
        public void AddScore(int count, int user, int game)
        {
            if (_context.Scores.Where(s => s.UserID == user && s.GameID == game).FirstOrDefault() == null)
                _context.Scores.Add(new Score { ScoreCount = count, GameID = game, UserID = user });
            else
                _context.Scores.Where(s => s.UserID == user && s.GameID == game).FirstOrDefault().ScoreCount += count;
            _context.SaveChanges();
        }
        public void AddItemData(int item, int cat, ServerData data)
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
                case ServerData.WordCategory:
                    Word word = (_context.Dictionary.Where(u => u.Id == item).FirstOrDefault());
                    WordCategory wCategory = _context.WordCategories.Where(u => u.Id == cat).FirstOrDefault();
                    if (word == null || wCategory == null)
                        return;
                    word.Categories.Add(wCategory);
                    break;
                case ServerData.Translation:
                    Word wordT = (_context.Dictionary.Where(u => u.Id == item).FirstOrDefault());
                    Translation tr = _context.Translations.Where(u => u.Id == cat).FirstOrDefault();
                    if (wordT == null || tr == null)
                        return;
                    wordT.Translations.Add(tr);
                    break;
                case ServerData.Definition:
                    Word wordD = (_context.Dictionary.Where(u => u.Id == item).FirstOrDefault());
                    Definition def = _context.Definitions.Where(u => u.Id == cat).FirstOrDefault();
                    if (wordD == null || def == null)
                        return;
                    wordD.Descriptions.Add(def);
                    break;
                case ServerData.Group:
                    Word wordG = (_context.Dictionary.Where(u => u.Id == item).FirstOrDefault());
                    WordsGroup gr = _context.Groups.Where(u => u.Id == cat).FirstOrDefault();
                    if (wordG == null || gr == null)
                        return;
                    wordG.Groups.Add(gr);
                    break;
                case ServerData.Example:
                    Word wordE = (_context.Dictionary.Where(u => u.Id == item).FirstOrDefault());
                    Example ex = _context.Examples.Where(u => u.Id == cat).FirstOrDefault();
                    if (wordE == null || ex == null)
                        return;
                    ex.WordID = wordE.Id;
                    break;
                case ServerData.GrammarExample:
                    Grammar grammar = (_context.Grammars.Where(u => u.Id == item).FirstOrDefault());
                    GrammarExample example = _context.GrammarExamples.Where(u => u.Id == cat).FirstOrDefault();
                    if (grammar == null || example == null)
                        return;
                    grammar.Examples.Add(example);
                    break;
                case ServerData.GrammarException:
                    Grammar grammarE = (_context.Grammars.Where(u => u.Id == item).FirstOrDefault());
                    GrammarException exception = _context.Exceptions.Where(u => u.Id == cat).FirstOrDefault();
                    if (grammarE == null || exception == null)
                        return;
                    grammarE.Exceptions.Add(exception);
                    break;
                case ServerData.Rule:
                    Grammar grammarR = (_context.Grammars.Where(u => u.Id == item).FirstOrDefault());
                    Entities.Rule rule = _context.Rules.Where(u => u.Id == cat).FirstOrDefault();
                    if (grammarR == null || rule == null)
                        return;
                    grammarR.Rules.Add(rule);
                    break;
            }
            _context.SaveChanges();
        }
        public void AddBookmark(int pos, int item, int user)
        {
            if (_context.Users.Where(u => u.Id == user).FirstOrDefault() == null || _context.Books.Where(b => b.Id == item).FirstOrDefault() == null)
                return;
            if (_context.Bookmarks.Where(bm => bm.BookID == item && bm.UserID == user).FirstOrDefault() != null)
                _context.Bookmarks.Where(bm => bm.BookID == item && bm.UserID == user).FirstOrDefault().Position = pos;
            else
                _context.Bookmarks.Add(new Bookmark { Position = pos, BookID = item, UserID = user });
            _context.SaveChanges();
        }
        public void AddVideoBookmark(TimeSpan pos, int item, int user)
        {
            if (_context.Users.Where(u => u.Id == user).FirstOrDefault() == null || _context.Videos.Where(b => b.Id == item).FirstOrDefault() == null)
                return;
            if (_context.VideoBookmarks.Where(bm => bm.VideoID == item && bm.UserID == user).FirstOrDefault() != null)
                _context.VideoBookmarks.Where(bm => bm.VideoID == item && bm.UserID == user).FirstOrDefault().Position = pos;
            else
                _context.VideoBookmarks.Add(new VideoBookmark { Position = pos, VideoID = item, UserID = user });
            _context.SaveChanges();
        }
        public void AddItemsWord(int word, int item, ServerData type)
        {
            Word tmp = _context.Dictionary.Where(w => w.Id == word).FirstOrDefault();
            if (tmp == null)
                return;
            switch (type)
            {
                case ServerData.Video:
                    Video video = _context.Videos.Where(w => w.Id == item).FirstOrDefault();
                    if (video != null && !video.Words.Contains(tmp))
                        video.Words.Add(tmp);
                    break;
                case ServerData.Book:
                    Book book = _context.Books.Where(w => w.Id == item).FirstOrDefault();
                    if (book != null && !book.Words.Contains(tmp))
                        book.Words.Add(tmp);
                    break;
                case ServerData.User:
                    User user = _context.Users.Where(w => w.Id == item).FirstOrDefault();
                    if (user != null && !user.Words.Contains(tmp))
                        user.Words.Add(tmp);
                    break;
            }
            _context.SaveChanges();
        }
        #endregion
    }
}