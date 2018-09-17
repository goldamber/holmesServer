using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Service
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IEngService
    {
        #region Add.
        /// <summary>
        /// Add a new user to a database.
        /// </summary>
        /// <param name="login">Login (unique).</param>
        /// <param name="pswd">Password (minlength = 6).</param>
        /// <param name="avatar">Avatar (default = "Wolf.png").</param>
        /// <param name="role">Users Role.</param>
        /// <param name="level">Level (depends on the quantity of played games and the score of each game)</param>
        /// <returns>Returns the Id of added 'User', if the operation succeeded.</returns>
        [OperationContract]
        int? AddUser(string login, string pswd, string avatar, string role, int level = 0);
        /// <summary>
        /// Add a video item to a database.
        /// </summary>
        /// <param name="name">Name (unique).</param>
        /// <param name="desc">Description (NULL).</param>
        /// <param name="path">The location of a video file.</param>
        /// <param name="sub">The location of subs (NOT NULL).</param>
        /// <param name="img">Poster (default = 'WolfV.png').</param>
        /// <param name="absolute">Is the path absolute? If not it is located at 'Videos/...'</param>
        /// <param name="mark">The quantity of marking stars (NG - 5).</param>
        /// <param name="year">The year of release (NULL).</param>
        /// <param name="created">The date of creation (in DB).</param>
        /// <returns>Returns the Id of added 'Video' item, if the operation succeeded.</returns>
        [OperationContract]
        int? AddVideo(string name, string desc, string path, string sub, string img, bool absolute, int? mark, int? user, int? year, DateTime created);
        /// <summary>
        /// Inserts a new 'Book' item.
        /// </summary>
        /// <param name="name">The title of a book.</param>
        /// <param name="desc">Description (NULL).</param>
        /// <param name="path">The location of a book.</param>
        /// <param name="img">Poster (default = 'WolfB.png').</param>
        /// <param name="absolute">Is the path absolute? If not it is located at 'Books/...'</param>
        /// <param name="mark">The quantity of marking stars (NG - 5).</param>
        /// <param name="year">The year of release (NULL).</param>
        /// <param name="created">The date of creation (in DB).</param>
        /// <returns>Returns the Id of an added 'Book' item, if the operation succeeded.</returns>
        [OperationContract]
        int? AddBook(string name, string desc, string path, string img, bool absolute, int? mark, int? user, int? year, DateTime created);
        /// <summary>
        /// Inserts a new author into the database.
        /// </summary>
        /// <param name="name">The name of an author.</param>
        /// <param name="surname">The last name of an author.</param>
        /// <returns>Returns the Id of an added 'Author', if the operation succeeded.</returns>
        [OperationContract]
        int? AddAuthor(string name, string surname);
        /// <summary>
        /// Inserts a new 'Category' item.
        /// </summary>
        /// <param name="name">The name of a category.</param>
        /// <param name="data">Describes the type of an item ('VideoCategory' OR 'BideoCategory').</param>
        /// <returns>Returns the Id of an added 'Author', if the operation succeeded.</returns>
        [OperationContract]
        int? AddCategory(string name, ServerData data);
        /// <summary>
        /// Inserts a new 'WordCategory' item.
        /// </summary>
        /// <param name="name">The name of a category.</param>
        /// <param name="abbr">Abbreviation.</param>
        /// <returns>Returns the Id of an added 'Author', if the operation succeeded.</returns>
        [OperationContract]
        int? AddWordsCategory(string name, string abbr);
        /// <summary>
        /// Inserts a new bookmark.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="item">Books id.</param>
        /// <param name="user">Users id.</param>
        [OperationContract]
        void AddBookmark(int pos, int item, int user);

        /// <summary>
        /// Adds a category to the books or videos.
        /// </summary>
        /// <param name="item">The id of a video or a book.</param>
        /// <param name="cat">The id of a category.</param>
        /// <param name="data">>Describes the type of a category ('VideoCategory' OR 'BideoCategory').</param>
        [OperationContract]
        void AddItemCategory(int item, int cat, ServerData data);
        /// <summary>
        /// Adds an author to a book.
        /// </summary>
        /// <param name="bookId">The id of a book.</param>
        /// <param name="author">The id of an author.</param>
        [OperationContract]
        void AddBookAuthor(int bookId, int author);
        #endregion
        #region Edit.
        /// <summary>
        /// Changes data.
        /// </summary>
        /// <param name="id">Id of an item.</param>
        /// <param name="changes">New data.</param>
        /// <param name="data">Type of the item.</param>
        /// <param name="property">Type of the property.</param>
        [OperationContract]
        void EditData(int id, string changes, ServerData data, PropertyData property);
        /// <summary>
        /// Changes rating.
        /// </summary>
        /// <param name="id">Items id.</param>
        /// <param name="usersId">Users id.</param>
        /// <param name="changes">New rating.</param>
        /// <param name="data">Items type.</param>
        [OperationContract]
        void EditMark(int id, int usersId, int changes, ServerData data);
        #endregion

        #region Files.
        /// <summary>
        /// Uploads a new file.
        /// </summary>
        /// <param name="file">Array of bytes to be ulodaed.</param>
        /// <param name="name">The name of new file.</param>
        /// <param name="type">Type of file to be uploaded.</param>
        /// <returns></returns>
        [OperationContract]
        bool Upload(byte[] file, string name, FilesType type);
        /// <summary>
        /// Downloads a file from server.
        /// </summary>
        /// <param name="name">Files name.</param>
        /// <param name="type">Files type.</param>
        /// <returns></returns>
        [OperationContract]
        byte[] Download(string name, FilesType type);
        /// <summary>
        /// Deltes file from server.
        /// </summary>
        /// <param name="name">Files name.</param>
        /// <param name="type">Files type.</param>
        [OperationContract]
        void Delete(string name, FilesType type);
        #endregion

        #region Get data.
        /// <summary>
        /// Returns the property of an item.
        /// </summary>
        /// <param name="id">The id of an item.</param>
        /// <param name="data">The type of an item.</param>
        /// <param name="property">The type of a property to return.</param>
        /// <returns>The properties data coverted ro string.</returns>
        [OperationContract]
        string GetItemProperty(int id, ServerData data, PropertyData property);
        /// <summary>
        /// Returns the items mark.
        /// </summary>
        /// <param name="itemId">Items id.</param>
        /// <param name="userId">Users id.</param>
        /// <param name="data">Items type.</param>
        /// <returns>The quantity of marking stars.</returns>
        [OperationContract]
        int? GetMark(int itemId, int userId, ServerData data);
        /// <summary>
        /// Returns the last tabels id.
        /// </summary>
        /// <param name="data">Items type.</param>
        /// <returns>Future id of an item to be addded.</returns>
        [OperationContract]
        int GetLastId(ServerData data);

        /// <summary>
        /// Gets a list of all items.
        /// </summary>
        /// <param name="data">The items' type.</param>
        /// <returns>List of items' ids.</returns>
        [OperationContract]
        IEnumerable<int> GetItems(ServerData data);
        /// <summary>
        /// Returns a list of fitered items.
        /// </summary>
        /// <param name="filter">A string to filter.</param>
        /// <param name="data">The type of an items.</param>
        /// <param name="fil">The type of a property to be fitered.</param>
        /// <returns>List of items' ids.</returns>
        [OperationContract]
        IEnumerable<int> GetFItems(string filter, ServerData data, PropertyData fil);
        /// <summary>
        /// Gets a list of all items sorted by specific propperty.
        /// </summary>
        /// <param name="data">The items' type.</param>
        /// <param name="property">The selectors type.</param>
        /// <returns>List of items' ids.</returns>
        [OperationContract]
        IEnumerable<int> GetSortedItems(ServerData data, PropertyData property, bool desc);

        /// <summary>
        /// Returns a list of the words related to a specific user and item (video or book).
        /// </summary>
        /// <param name="user">The id of a user.</param>
        /// <param name="item">The id of an item.</param>
        /// <param name="data">The type of an item.</param>
        /// <returns>List of words' ids.</returns>
        [OperationContract]
        IEnumerable<int> GetUserItemWords(int user, int item, ServerData data);
        /// <summary>
        /// Returns the words or categories of a specific item.
        /// </summary>
        /// <param name="id">The id of an item.</param>
        /// <param name="data">The type of an item.</param>
        /// <param name="res">The type of data to be returned (Word, Category).</param>
        /// <returns>List of items' ids.</returns>
        [OperationContract]
        IEnumerable<int> GetItemData(int id, ServerData data, ServerData res);

        /// <summary>
        /// Returns the id of the user.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <returns>If given user exists, then returns users id. Unless - NULL.</returns>
        [OperationContract]
        int? GetUserId(string login);
        /// <summary>
        /// Returns bookmark if exists.
        /// </summary>
        /// <param name="item">Items id.</param>
        /// <param name="user">Users id.</param>
        /// <param name="data">Items type.</param>
        /// <returns>Bookmarks position.</returns>
        [OperationContract]
        int? GetLastMark(int item, int user, ServerData data);
        #endregion
        #region Check data.
        /// <summary>
        /// Checks if an item exists (true - exists).
        /// </summary>
        /// <param name="name">The name of an item.</param>
        /// <param name="data">The type of an item.</param>
        /// <returns>
        /// TRUE - an item exists
        /// FALSE - an item does not exist
        /// </returns>
        [OperationContract]
        bool CheckExistence(string name, ServerData data);
        /// <summary>
        /// Checks if an author exists.
        /// </summary>
        /// <param name="name">The name of an author.</param>
        /// <param name="surname">The last name of an author.</param>
        /// <returns>
        /// TRUE - an author exists
        /// FALSE - an author does not exist
        /// </returns>
        [OperationContract]
        bool CheckAuthor(string name, string surname);
        /// <summary>
        /// Checks if the given password match to the given user.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <param name="pswd">Password.</param>
        /// <returns>
        /// TRUE - the password matches
        /// FALSE - the password does not match
        /// </returns>
        [OperationContract]
        bool CheckUserPswd(string login, string pswd);

        /// <summary>
        /// Checks if an items path is absolute or not.
        /// </summary>
        /// <param name="id">The id of an item.</param>
        /// <param name="data">The type of an item.</param>
        /// <returns>
        /// NULL - an item does not exist
        /// TRUE - absolute
        /// FALSE - not absolute
        /// </returns>
        [OperationContract]
        bool? CheckAbsolute(int id, ServerData data);
        #endregion

        #region Remove.
        /// <summary>
        /// Removes any items, depending on the type.
        /// </summary>
        /// <param name="id">The id of an item.</param>
        /// <param name="data">The type of an item (Video, Book, ...).</param>
        [OperationContract]
        void RemoveItem(int id, ServerData data);
        /// <summary>
        /// Removes all data related to specific item.
        /// </summary>
        /// <param name="id">Items id.</param>
        /// <param name="data">Items type.</param>
        [OperationContract]
        void RemoveFullItemData(int id, ServerData data);
        /// <summary>
        /// Removes words from users, videos or books.
        /// </summary>
        /// <param name="item">The id of an item.</param>
        /// <param name="word">The id of a word.</param>
        /// <param name="data">The type of an item.</param>
        [OperationContract]
        void RemoveItemWord(int item, int word, ServerData data);
        #endregion
    }
}