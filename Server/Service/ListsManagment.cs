using Server.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Server.Service
{
    public partial class EngService : IEngService
    {
        #region Get lists.
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
                        case PropertyData.Mark:
                            List<int> lst = new List<int>();
                            foreach (int item in _context.VideoStars.Where(v => v.MarkCount.ToString().Equals(filter)).Select(v => v.VideoID))
                            {
                                if (!lst.Contains(item))
                                    lst.Add(item);
                            }
                            return lst;
                        case PropertyData.Category:
                        case PropertyData.Categories:
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
                        case PropertyData.Mark:
                            List<int> lst = new List<int>();
                            foreach (int item in _context.BookStars.Where(v => v.MarkCount.ToString().Equals(filter)).Select(v => v.BookID))
                            {
                                if (!lst.Contains(item))
                                    lst.Add(item);
                            }
                            return lst;

                        case PropertyData.Category:
                        case PropertyData.Categories:
                            if (_context.BookCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Books.Where(v => v.Categories.Contains(_context.BookCategories.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault())).Select(f => f.Id).ToList();
                        case PropertyData.Author:
                        case PropertyData.Authors:
                            if (_context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter) || (c.Name.ToLower() + " " + c.Surname.ToLower()).Equals(filter) || (c.Surname.ToLower() + " " + c.Name.ToLower()).Equals(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Books.Where(v => v.Authors.Contains(_context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter) || (c.Name.ToLower() + " " + c.Surname.ToLower()).Equals(filter) || (c.Surname.ToLower() + " " + c.Name.ToLower()).Equals(filter)).FirstOrDefault())).Select(f => f.Id).ToList();
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
                        case PropertyData.Categories:
                            if (_context.WordCategories.Where(c => c.Name.ToLower().Contains(filter) || c.Abbreviation.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            return _context.Dictionary.Where(v => v.Categories.Contains(_context.WordCategories.Where(c => c.Name.ToLower().Contains(filter) || c.Abbreviation.ToLower().Contains(filter)).FirstOrDefault())).Select(f => f.Id).ToList();

                        case PropertyData.Synonyms:
                            if (_context.Translations.Where(c => c.Name.ToLower() == filter).FirstOrDefault() == null && _context.Definitions.Where(c => c.Name.ToLower() == filter).FirstOrDefault() == null)
                                return null;
                            return _context.Translations.Where(c => c.Name.ToLower() == filter).FirstOrDefault() != null ? _context.Dictionary.Where(v => v.Translations.Contains(_context.Translations.Where(c => c.Name.ToLower() == filter).FirstOrDefault())).Select(f => f.Id).ToList() : _context.Dictionary.Where(v => v.Descriptions.Contains(_context.Definitions.Where(c => c.Name.ToLower() == filter).FirstOrDefault())).Select(f => f.Id).ToList();

                        case PropertyData.Translation:
                        case PropertyData.Translations:
                            if (_context.Translations.Where(c => c.Name.ToLower().Contains(filter)) == null)
                                return null;
                            return _context.Dictionary.Where(w => w.Translations.Contains(_context.Translations.Where(c => c.Name.ToLower().Contains(filter)).FirstOrDefault())).Select(w => w.Id).ToList();

                        case PropertyData.Definition:
                        case PropertyData.Definitions:
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
                            return desc ? _context.Videos.OrderByDescending(v => v.Year).Select(v => v.Id).ToList() : _context.Videos.OrderBy(v => v.Year).Select(v => v.Id).ToList();
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
                            return desc ? _context.Books.OrderByDescending(v => v.Year).Select(v => v.Id).ToList() : _context.Books.OrderBy(v => v.Year).Select(v => v.Id).ToList();
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
                            roles = desc ? roles.OrderBy(r => r).ToList() : roles.OrderByDescending(r => r).ToList();
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
                        case ServerData.Group:
                            return word.Groups.Select(c => c.Id);
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