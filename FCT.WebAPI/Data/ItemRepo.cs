using System.Linq;
using FCT.WebAPI.Models;

namespace FCT.WebAPI.Data {
    public class ItemRepo : IItemRepo {
        private readonly DataContext _context;
        public ItemRepo (DataContext context) {
            _context = context;
        }

        public void Deregister (string itemName) {
            Item item = RetrieveItem(itemName);
            if (item != null) {
                _context.Items.Remove (item);
            }
            _context.SaveChanges ();
        }

        // public int GetType (string itemName) {
        //     int itemType = 0;
        //     Item item = RetrieveItem(itemName);
        //     if (item != null) {
        //         itemType = item.ItemType;
        //     }
        //     return itemType;
        // }

        // public void Register (string itemName, string itemContent, int itemType) {
        //     Item item = new Item ();
        //     item.ItemContent = itemContent;
        //     item.ItemName = itemName;
        //     item.ItemType = itemType;
        //     _context.Items.Add (item);
        //     _context.SaveChanges ();
        // }

        public void RegisterItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        // public string Retrieve (string itemName) {
        //     string ItemContent = string.Empty;
        //     Item item = RetrieveItem(itemName);
        //     if (item != null) {
        //         ItemContent = item.ItemContent;
        //     }
        //     return ItemContent;
        // }

        public Item RetrieveItem(string itemName)
        {
            Item item = _context.Items.FirstOrDefault(x=>x.ItemName == itemName);
            return item;
        }
    }
}