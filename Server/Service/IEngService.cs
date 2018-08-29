using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Service
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IEngService
    {
        #region Add.
        [OperationContract]
        int? AddUser(string login, string pswd, string avatar, string role, int level = 0);
        [OperationContract]
        int? AddVideo(string name, string desc, string path, string sub, string img, bool absolute, int? mark, int? year, DateTime created, DateTime? updated);
        [OperationContract]
        int? AddBook(string name, string desc, string path, string img, bool absolute, int? mark, int? year, DateTime created, DateTime? updated);
        [OperationContract]
        int? AddAuthor(string name, string surname);         //Create an author.
        [OperationContract]
        int? AddCategory(string name, ServerData data);         //Create a category.

        [OperationContract]
        void AddItemCategory(int item, int cat, ServerData data);         //Add a category to the books or videos.        
        [OperationContract]
        void AddBookAuthor(int bookId, int author);         //Add an author to a book.        
        #endregion

        #region Get data.
        [OperationContract]
        string GetItemProperty(int id, ServerData data, PropertyData property);

        [OperationContract]
        IEnumerable<int> GetItems(ServerData data);         //Get a list of all items.
        [OperationContract]
        IEnumerable<int> GetFItems(string filter, ServerData data, FilerData fil);        //Filter result.

        [OperationContract]
        IEnumerable<int> GetUserItemWords(int user, int item, ServerData data);         //Get words related to specific user and item (video or book).
        [OperationContract]
        IEnumerable<int> GetItemData(int id, ServerData data, ServerData res);     //Get words or categories.

        [OperationContract]
        int? GetUserId(string login);
        #endregion
        #region Check data.
        [OperationContract]
        bool CheckExistence(string name, ServerData data);     //Check if an item exists (true - exists).
        [OperationContract]
        bool CheckAuthor(string name, string surname);         //Check if an author exists (true - exists).
        [OperationContract]
        bool CheckUserPswd(string login, string pswd);         //Check if passwords match (true - passwords match).

        [OperationContract]
        bool? CheckAbsolute(int id, ServerData data);
        #endregion

        #region Remove.
        [OperationContract]
        void RemoveItem(int id, ServerData data);       //Remove items.
        [OperationContract]
        void RemoveItemWord(int item, int word, ServerData data);       //Remove words from users, cideos or books.
        #endregion
    }
}