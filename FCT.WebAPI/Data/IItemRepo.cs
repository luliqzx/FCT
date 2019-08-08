using FCT.WebAPI.Models;

namespace FCT.WebAPI.Data {
    public interface IItemRepo {
        // Store an item to the repository.
        // Parameter itemType is used to differentiate JSON or XML.
        // 1 = itemContent is a JSON string.
        // 2 = itemContent is an XML string.
        // void Register (string itemName, string itemContent, int itemType);
        void RegisterItem(Item item);

        // // Retrieve an item from the repository.
        // string Retrieve (string itemName);
        Item RetrieveItem(string itemName);

        // // Retrieve the type of the item (JSON or XML).
        // int GetType (string itemName);

        // Remove an item from the repository.
        void Deregister (string itemName);
    }
}