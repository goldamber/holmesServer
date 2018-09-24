using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Service
{
    public partial class EngService : IEngService
    {
        public IEnumerable<int> GetItems(ServerData data)
        {
            switch (data)
            {
                case ServerData.Video:
                    return _context.Videos.Select(f => f.Id).ToList();
                case ServerData.Book:
                    return _context.Books.Select(f => f.Id).ToList();
                case ServerData.User:
                    return _context.Users.Select(f => f.Id).ToList();
                case ServerData.Role:
                    return _context.Roles.Select(f => f.Id).ToList();
                case ServerData.VideoCategory:
                    return _context.VideoCategories.Select(f => f.Id).ToList();
                case ServerData.BookCategory:
                    return _context.BookCategories.Select(f => f.Id).ToList();
                case ServerData.Word:
                    return _context.Dictionary.Select(f => f.Id).ToList();
                case ServerData.WordCategory:
                    return _context.WordCategories.Select(f => f.Id).ToList();
                case ServerData.Group:
                    return _context.Groups.Select(f => f.Id).ToList();
                case ServerData.Author:
                    return _context.Authors.Select(f => f.Id).ToList();
                case ServerData.Game:
                    return _context.Games.Select(f => f.Id).ToList();
                case ServerData.Grammar:
                    return _context.Grammars.Select(f => f.Id).ToList();
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
                        case PropertyData.Categories:
                            if (_context.VideoCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> vc = new List<int>();
                            foreach (VideoCategory item in _context.VideoCategories.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Video val in _context.Videos.ToList())
                                {
                                    if (val.Categories.Contains(item) && !vc.Contains(val.Id))
                                        vc.Add(val.Id);
                                }
                            }
                            return vc.Count == 0 ? null : vc;
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
                        case PropertyData.Categories:
                            if (_context.BookCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> bc = new List<int>();
                            foreach (BookCategory item in _context.BookCategories.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Book val in _context.Books.ToList())
                                {
                                    if (val.Categories.Contains(item) && !bc.Contains(val.Id))
                                        bc.Add(val.Id);
                                }
                            }
                            return bc.Count == 0 ? null : bc;
                        case PropertyData.Author:
                        case PropertyData.Authors:
                            if (_context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter) || (c.Name.ToLower() + " " + c.Surname.ToLower()).Equals(filter) || (c.Surname.ToLower() + " " + c.Name.ToLower()).Equals(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> book = new List<int>();
                            foreach (Author item in _context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter) || (c.Name.ToLower() + " " + c.Surname.ToLower()).Equals(filter) || (c.Surname.ToLower() + " " + c.Name.ToLower()).Equals(filter)).ToList())
                            {
                                foreach (Book val in _context.Books.ToList())
                                {
                                    if (val.Authors.Contains(item) && !book.Contains(val.Id))
                                        book.Add(val.Id);
                                }
                            }
                            return book.Count == 0 ? null : book;
                    }
                    break;
                case ServerData.User:
                    switch (fil)
                    {
                        case PropertyData.Name:
                        case PropertyData.Login:
                            return _context.Users.Where(v => v.Username.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Level:
                            return _context.Users.Where(v => v.Level.ToString().Equals(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Role:
                        case PropertyData.RolesName:
                            if (_context.Roles.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Users.Where(v => v.RoleID == _context.Roles.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault().Id).Select(f => f.Id).ToList();
                    }
                    break;
                case ServerData.Word:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Dictionary.Where(v => v.Name.ToLower().Contains(filter) || (v.Form != null && v.Form.PluralForm.Equals(filter)) || (v.Form != null && v.Form.PastForm.Equals(filter)) || (v.Form != null && v.Form.PastThForm.Equals(filter))).Select(f => f.Id).ToList();
                        case PropertyData.Category:
                        case PropertyData.Categories:
                            if (_context.WordCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> wc = new List<int>();
                            foreach (WordCategory item in _context.WordCategories.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Word val in item.Words)
                                {
                                    if (!wc.Contains(val.Id))
                                        wc.Add(val.Id);
                                }
                            }
                            return wc.Count == 0 ? null : wc;
                        case PropertyData.Group:
                        case PropertyData.Groups:
                            if (_context.Groups.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> wg = new List<int>();
                            foreach (WordsGroup item in _context.Groups.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Word val in item.Words)
                                {
                                    if (!wg.Contains(val.Id))
                                        wg.Add(val.Id);
                                }
                            }
                            return wg.Count == 0 ? null : wg;
                        case PropertyData.Homophones:
                            Word word = _context.Dictionary.Where(v => v.Name.ToLower().Contains(filter) || (v.Form != null && v.Form.PluralForm.Equals(filter)) || (v.Form != null && v.Form.PastForm.Equals(filter)) || (v.Form != null && v.Form.PastThForm.Equals(filter))).FirstOrDefault();
                            if (word == null || word.TranscriptionID == null)
                                return null;
                            return _context.Dictionary.Where(v => v.Transcriptions.British.ToLower() == word.Transcriptions.British.ToLower()).Select(f => f.Id).ToList();
                        case PropertyData.Synonyms:
                            Word wordS = _context.Dictionary.Where(v => v.Name.ToLower().Contains(filter) || (v.Form != null && v.Form.PluralForm.Equals(filter)) || (v.Form != null && v.Form.PastForm.Equals(filter)) || (v.Form != null && v.Form.PastThForm.Equals(filter))).FirstOrDefault();
                            if (wordS == null || (wordS.Translations == null && wordS.Descriptions == null))
                                return null;

                            List<int> words = new List<int>();
                            if (wordS.Translations != null)
                            {
                                foreach (Translation item in _context.Translations.ToList())
                                {
                                    if (wordS.Translations.Select(t => t.Name).ToList().Contains(item.Name))
                                    {
                                        foreach (Word val in item.Words.ToList())
                                        {
                                            if (!words.Contains(val.Id))
                                                words.Add(val.Id);
                                        }
                                    }
                                }
                            }
                            if (wordS.Descriptions != null)
                            {
                                foreach (Definition item in _context.Definitions.ToList())
                                {
                                    if (wordS.Descriptions.Select(t => t.Name).ToList().Contains(item.Name))
                                    {
                                        foreach (Word val in item.Words.ToList())
                                        {
                                            if (!words.Contains(val.Id))
                                                words.Add(val.Id);
                                        }
                                    }
                                }
                            }
                            words.Remove(wordS.Id);
                            return words.Count == 0 ? null : words;

                        case PropertyData.Translation:
                        case PropertyData.Translations:
                            if (_context.Translations.Where(c => c.Name.ToLower().Contains(filter)) == null)
                                return null;
                            List<int> trWords = new List<int>();
                            foreach (Translation item in _context.Translations.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Word val in item.Words)
                                {
                                    if (!trWords.Contains(val.Id))
                                        trWords.Add(val.Id);
                                }
                            }
                            return trWords.Count == 0 ? null : trWords;
                        case PropertyData.Definition:
                        case PropertyData.Description:
                        case PropertyData.Definitions:
                            if (_context.Definitions.Where(c => c.Name.ToLower().Contains(filter)) == null)
                                return null;
                            List<int> defWords = new List<int>();
                            foreach (Definition item in _context.Definitions.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Word val in _context.Dictionary.ToList())
                                {
                                    if (val.Descriptions.Contains(item) && !defWords.Contains(val.Id))
                                        defWords.Add(val.Id);
                                }
                            }
                            return defWords.Count == 0 ? null : defWords;
                        case PropertyData.Example:
                        case PropertyData.Examples:
                            if (_context.Examples.Where(c => c.Name == filter) == null)
                                return null;
                            List<int> expWords = new List<int>();
                            foreach (Example item in _context.Examples.Where(c => c.Name == filter).ToList())
                            {
                                if (!expWords.Contains(Convert.ToInt32(item.WordID)))
                                    expWords.Add(Convert.ToInt32(item.WordID));
                            }
                            return expWords.Count == 0 ? null : expWords;
                    }
                    break;
                case ServerData.VideoCategory:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.VideoCategories.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Video:
                        case PropertyData.Videos:
                            if (_context.Videos.Where(v => v.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> videoCat = new List<int>();
                            foreach (Video item in _context.Videos.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (VideoCategory val in _context.VideoCategories.ToList())
                                {
                                    if (val.Videos.Contains(item) && !videoCat.Contains(val.Id))
                                        videoCat.Add(val.Id);
                                }
                            }
                            return videoCat.Count == 0 ? null : videoCat;
                    }
                    break;
                case ServerData.BookCategory:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.BookCategories.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Book:
                        case PropertyData.Books:
                            if (_context.Books.Where(v => v.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> bookCat = new List<int>();
                            foreach (Book item in _context.Books.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (BookCategory val in _context.BookCategories.ToList())
                                {
                                    if (val.Books.Contains(item) && !bookCat.Contains(val.Id))
                                        bookCat.Add(val.Id);
                                }
                            }
                            return bookCat.Count == 0 ? null : bookCat;
                    }
                    break;
                case ServerData.WordCategory:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.WordCategories.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Abbreviation:
                            return _context.WordCategories.Where(v => v.Abbreviation.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Word:
                        case PropertyData.Words:
                            if (_context.Dictionary.Where(v => v.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> wordCat = new List<int>();
                            foreach (Word item in _context.Dictionary.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (WordCategory val in item.Categories.ToList())
                                {
                                    if (!wordCat.Contains(val.Id))
                                        wordCat.Add(val.Id);
                                }
                            }
                            return wordCat.Count == 0 ? null : wordCat;
                    }
                    break;
                case ServerData.Group:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Groups.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Word:
                        case PropertyData.Words:
                            if (_context.Dictionary.Where(v => v.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> wordCat = new List<int>();
                            foreach (Word item in _context.Dictionary.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (WordsGroup val in item.Groups.ToList())
                                {
                                    if (!wordCat.Contains(val.Id))
                                        wordCat.Add(val.Id);
                                }
                            }
                            return wordCat.Count == 0 ? null : wordCat;
                    }
                    break;
                case ServerData.Author:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Authors.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Surname:
                            return _context.Authors.Where(v => v.Surname.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                        case PropertyData.Book:
                        case PropertyData.Books:
                            List<int> result = new List<int>();
                            foreach (Book item in _context.Books.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Author val in item.Authors)
                                {
                                    if (!result.Contains(val.Id))
                                        result.Add(val.Id);
                                }
                            }
                            return result.Count == 0 ? null : result;
                    }
                    break;
                case ServerData.Game:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Games.Where(v => v.Name.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                    }
                    break;
                case ServerData.Grammar:
                    switch (fil)
                    {
                        case PropertyData.Name:
                            return _context.Grammars.Where(v => v.Title.ToLower().Contains(filter)).Select(f => f.Id).ToList();
                    }
                    break;
            }
            return null;
        }
    }
}