using Server.Database;
using Server.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;

namespace Server.Service
{
    public partial class EngService : IEngService
    {
        EngContext _context;

        public EngService()
        {
            _context = new EngContext();
            _context.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["engEntDB"].ConnectionString;
        }
                
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
                case ServerData.Word:
                    Word word = _context.Dictionary.Where(w => w.Id == id).FirstOrDefault();
                    if (word == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            word.Name = changes;
                            break;
                        case PropertyData.Imgpath:
                            word.ImgPath = changes;
                            break;
                        case PropertyData.PastForm:
                            word.Form.PastForm = changes;
                            break;
                        case PropertyData.PastThForm:
                            word.Form.PastThForm = changes;
                            break;
                        case PropertyData.PluralForm:
                            word.Form.PluralForm = changes;
                            break;
                        case PropertyData.British:
                            word.Transcriptions.British = changes;
                            break;
                        case PropertyData.American:
                            word.Transcriptions.American = changes;
                            break;
                        case PropertyData.Australian:
                            word.Transcriptions.Australian = changes;
                            break;
                        case PropertyData.Canadian:
                            word.Transcriptions.Canadian = changes;
                            break;
                    }
                    break;
                case ServerData.Grammar:
                    Grammar grammar = _context.Grammars.Where(w => w.Id == id).FirstOrDefault();
                    if (grammar == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            grammar.Title = changes;
                            break;
                        case PropertyData.Description:
                            grammar.Description = changes;
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
                            if (File.Exists($@"Books\{book.Path}") && book.IsAbsolute == false)
                                File.Delete($@"Books\{book.Path}");
                            book.Path = changes;
                            break;
                        case PropertyData.Imgpath:
                            book.ImgPath = changes;
                            break;
                        case PropertyData.Year:
                            if (changes == null)
                                book.Year = null;
                            else
                                book.Year = Convert.ToInt32(changes);
                            break;
                        case PropertyData.IsAbsolute:
                            book.IsAbsolute = changes != null;
                            break;
                    }
                    break;
                case ServerData.Video:
                    Video video = _context.Videos.Where(u => u.Id == id).FirstOrDefault();
                    if (video == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            video.Name = changes;
                            break;
                        case PropertyData.Description:
                            video.Description = changes;
                            break;
                        case PropertyData.Path:
                            if (File.Exists($@"Books\{video.Path}") && video.IsAbsolute == false)
                                File.Delete($@"Books\{video.Path}");
                            video.Path = changes;
                            break;
                        case PropertyData.SubPath:
                            video.SubPath = changes;
                            break;
                        case PropertyData.Imgpath:
                            video.ImgPath = changes;
                            break;
                        case PropertyData.Year:
                            if (changes == null)
                                video.Year = null;
                            else
                                video.Year = Convert.ToInt32(changes);
                            break;
                        case PropertyData.IsAbsolute:
                            video.IsAbsolute = changes != null;
                            break;
                    }
                    break;
                case ServerData.Author:
                    Author author = _context.Authors.Where(u => u.Id == id).FirstOrDefault();
                    if (author == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            author.Name = changes;
                            break;
                        case PropertyData.Surname:
                            author.Surname = changes;
                            break;
                    }
                    break;

                case ServerData.BookCategory:
                    BookCategory bc = _context.BookCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (bc == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            bc.Name = changes;
                            break;
                    }
                    break;
                case ServerData.VideoCategory:
                    VideoCategory vc = _context.VideoCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (vc == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            vc.Name = changes;
                            break;
                    }
                    break;
                case ServerData.WordCategory:
                    WordCategory wc = _context.WordCategories.Where(u => u.Id == id).FirstOrDefault();
                    if (wc == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            wc.Name = changes;
                            break;
                        case PropertyData.Abbreviation:
                            wc.Abbreviation = changes;
                            break;
                    }
                    break;
                case ServerData.Group:
                    WordsGroup wg = _context.Groups.Where(u => u.Id == id).FirstOrDefault();
                    if (wg == null)
                        return;
                    switch (property)
                    {
                        case PropertyData.Name:
                            wg.Name = changes;
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
                case ServerData.Word:
                    if (_context.Dictionary.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Dictionary.Remove(_context.Dictionary.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.Grammar:
                    if (_context.Grammars.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Grammars.Remove(_context.Grammars.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.Author:
                    if (_context.Authors.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Authors.Remove(_context.Authors.Where(b => b.Id == id).FirstOrDefault());
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
                case ServerData.WordCategory:
                    if (_context.WordCategories.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.WordCategories.Remove(_context.WordCategories.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.Group:
                    if (_context.Groups.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Groups.Remove(_context.Groups.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.GrammarExample:
                    if (_context.GrammarExamples.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.GrammarExamples.Remove(_context.GrammarExamples.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.GrammarException:
                    if (_context.Exceptions.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Exceptions.Remove(_context.Exceptions.Where(b => b.Id == id).FirstOrDefault());
                    break;
                case ServerData.Rule:
                    if (_context.Rules.Where(b => b.Id == id).FirstOrDefault() != null)
                        _context.Rules.Remove(_context.Rules.Where(b => b.Id == id).FirstOrDefault());
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
                case ServerData.WordCategory:
                    _context.Dictionary.Where(b => b.Id == id).FirstOrDefault()?.Categories.Clear();
                    break;
                case ServerData.Translation:
                    _context.Dictionary.Where(b => b.Id == id).FirstOrDefault()?.Translations.Clear();
                    break;
                case ServerData.Definition:
                    _context.Dictionary.Where(b => b.Id == id).FirstOrDefault()?.Descriptions.Clear();
                    break;
                case ServerData.Group:
                    _context.Dictionary.Where(b => b.Id == id).FirstOrDefault()?.Groups.Clear();
                    break;
                case ServerData.Example:
                    _context.Examples.RemoveRange(_context.Examples.Where(b => b.WordID == id).ToList());
                    break;
                case ServerData.GrammarExample:
                    _context.Grammars.Where(b => b.Id == id).FirstOrDefault()?.Examples.Clear();
                    break;
                case ServerData.GrammarException:
                    _context.Grammars.Where(b => b.Id == id).FirstOrDefault()?.Exceptions.Clear();
                    break;
                case ServerData.Rule:
                    _context.Grammars.Where(b => b.Id == id).FirstOrDefault()?.Rules.Clear();
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
                    _context.Videos.Where(u => u.Id == item).FirstOrDefault()?.Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                    break;

                case ServerData.Book:
                    _context.Books.Where(u => u.Id == item).FirstOrDefault()?.Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                    break;

                case ServerData.User:
                    bool removeAll = true;
                    _context.Users.Where(u => u.Id == item).FirstOrDefault()?.Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                    _context.SaveChanges();
                    foreach (User user in _context.Users.ToList())
                    {
                        if (user.Words.Select(w => w.Id).ToList().Contains(word))
                        {
                            removeAll = false;
                            break;
                        }
                    }
                    if (removeAll)
                    {
                        foreach (Book book in _context.Books.ToList())
                        {
                            if (book.Words.Select(w => w.Id).ToList().Contains(word))
                                book.Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                        }
                        foreach (Video video in _context.Videos.ToList())
                        {
                            if (video.Words.Select(w => w.Id).ToList().Contains(word))
                                video.Words.Remove(_context.Dictionary.Where(u => u.Id == word).FirstOrDefault());
                        }
                    }
                    break;
            }
            _context.SaveChanges();
        }
        public void ClearResources()
        {
            List<Translation> lst = _context.Translations.ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Words?.Count == 0)
                    _context.Translations.Remove(_context.Translations.ElementAt(i));
            }
            List<Definition> def = _context.Definitions.ToList();
            for (int i = 0; i < def.Count; i++)
            {
                if (def[i].Words?.Count == 0)
                    _context.Definitions.Remove(_context.Definitions.ElementAt(i));
            }
            _context.SaveChanges();
        }
        #endregion
    }
}