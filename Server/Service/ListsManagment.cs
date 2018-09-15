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
                            List<int> vc = new List<int>();
                            foreach (VideoCategory item in _context.VideoCategories.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Video val in _context.Videos)
                                {
                                    if (val.Categories.Contains(item) && !vc.Contains(val.Id))
                                        vc.Add(val.Id);
                                }
                            }
                            return vc.Count == 0? null: vc;
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
                            List<int> bc = new List<int>();
                            foreach (BookCategory item in _context.BookCategories.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Book val in _context.Books)
                                {
                                    if (val.Categories.Contains(item) && !bc.Contains(val.Id))
                                        bc.Add(val.Id);
                                }
                            }
                            return bc.Count == 0? null : bc;

                        case PropertyData.Author:
                        case PropertyData.Authors:
                            if (_context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter) || (c.Name.ToLower() + " " + c.Surname.ToLower()).Equals(filter) || (c.Surname.ToLower() + " " + c.Name.ToLower()).Equals(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> book = new List<int>();
                            foreach (Author item in _context.Authors.Where(c => c.Name.ToLower().Contains(filter) || c.Surname.ToLower().Contains(filter) || (c.Name.ToLower() + " " + c.Surname.ToLower()).Equals(filter) || (c.Surname.ToLower() + " " + c.Name.ToLower()).Equals(filter)).ToList())
                            {
                                foreach (Book val in _context.Books)
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
                                foreach (VideoCategory val in _context.VideoCategories)
                                {
                                    if (val.Videos.Contains(item) && !videoCat.Contains(val.Id))
                                        videoCat.Add(val.Id);
                                }
                            }
                            return videoCat.Count == 0? null: videoCat;
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
                                foreach (BookCategory val in _context.BookCategories)
                                {
                                    if (val.Books.Contains(item) && !bookCat.Contains(val.Id))
                                        bookCat.Add(val.Id);
                                }
                            }
                            return bookCat.Count == 0? null: bookCat;
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
                            if (_context.Books.Where(v => v.Name.ToLower().Contains(filter)).FirstOrDefault() == null)
                                return null;
                            List<int> auth = new List<int>();
                            foreach (Book item in _context.Books.Where(v => v.Name.ToLower().Contains(filter)).ToList())
                            {
                                foreach (Author val in _context.Authors)
                                {
                                    if (val.Books.Contains(item) && !auth.Contains(val.Id))
                                        auth.Add(val.Id);
                                }
                            }
                            return auth.Count == 0? null: auth;
                    }
                    break;
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
                        case ServerData.Book:
                            return vc.Videos.Select(c => c.Id);
                    }
                    break;
            }

            return null;
        }
        #endregion
    }
}